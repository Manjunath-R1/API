using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Business.Interfaces.User;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.DataAccess.Models.Audit;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.Repository.Interfaces.Audit;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Common.Exceptions;
using UAParser;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Repository.Interfaces.Contact;
using System.Linq;

namespace ThoughtFocus.Service.Impl
{
    public class UserLoginServiceImpl : IUserLoginService
    {
        #region Fields
        private IUserRepository _userRepository;
        private ISessionDetailsRepository _userSessionDetailsRepository;

        private readonly AppSettings _appSettings;
        private IUserAccount _userAccount;
        private readonly IMapper _mapper;
        private readonly ILogger<UserLoginServiceImpl> Logger;
        private readonly IContactRepository _contactRepository;
        public IBusinessContactRepository _businessContactRepository { get; }


        #endregion Fields
        public UserLoginServiceImpl(IUserRepository userRepository, IUserAccount userAccount, IOptions<AppSettings> appSettings,
        IMapper _mapper, ILogger<UserLoginServiceImpl> logger, ISessionDetailsRepository userSessionDetailsRepository,
        IContactRepository contactRepository, IBusinessContactRepository businessContactRepository)
        {
            this._userAccount = userAccount;
            this._userRepository = userRepository;
            this._appSettings = appSettings.Value;
            this._mapper = _mapper;
            this.Logger = logger;
            this._userSessionDetailsRepository = userSessionDetailsRepository;
            this._contactRepository = contactRepository;
            this._businessContactRepository = businessContactRepository;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string requestInfo, string IPAddress)
        {
            var user = _userRepository.GetUserLogin(model.Username, model.Password);

            try
            {
                AuthenticateResponse AuthenticateResponse = new AuthenticateResponse();

                Contact logincontact = this._contactRepository.FirstOrDefault(a => a.Users.UserName == model.Username && a.IsActive == true);
                if (logincontact == null)
                {
                    AuthenticateResponse.UnlockBusinessContactResponce = "Incorrect User name";
                    return AuthenticateResponse;
                }

                if (logincontact.Users.IsLockedOut == true)
                {
                    AuthenticateResponse.ContactID = logincontact.ContactID;
                    AuthenticateResponse.UnlockBusinessContactResponce = "User account is locked, please contact NUL administrator";
                    return AuthenticateResponse;
                }
                if (logincontact.AccountStatusID == (long)AccountStatusEnumeration.Deactivated)
                {
                    AuthenticateResponse.UnlockBusinessContactResponce = "This account is deactivated";
                    return AuthenticateResponse;
                }
                int borrowerrolecount = logincontact.Users.UserRoles.Where(a => a.RoleID == 2).Count();
                if (logincontact.Users.PasswordHash != model.Password && borrowerrolecount > 0)
                {
                    if (logincontact.Users.FailedPasswordAttemptDateTime != null && 
                        logincontact.Users.FailedPasswordAttemptDateTime.Value !=null &&
                        logincontact.Users.FailedPasswordAttemptDateTime.Value.Date != DateTime.Now.Date)
                    {
                        logincontact.Users.FailedPasswordAttemptCount = 0;
                    }

                    if (logincontact.Users.FailedPasswordAttemptCount == null)
                    {
                        logincontact.Users.FailedPasswordAttemptCount = 1;
                    }
                    else
                    {
                        logincontact.Users.FailedPasswordAttemptCount = logincontact.Users.FailedPasswordAttemptCount + 1;
                    }


                    if (logincontact.Users.FailedPasswordAttemptCount <= 5)
                    {
                        logincontact.Users.FailedPasswordAttemptDateTime = System.DateTime.Now;
                        this._contactRepository.SaveOrUpdateContact(logincontact, logincontact.Users.UserID);
                        if (logincontact.Users.FailedPasswordAttemptCount == 5)
                        {
                            logincontact.Users.IsLockedOut = true;
                            logincontact.AccountStatusID = (long)AccountStatusEnumeration.Locked;
                            logincontact.LastModifiedByUserID = logincontact.Users.UserID;
                            logincontact.LastModifiedDateTime = System.DateTime.Now;
                            this._contactRepository.SaveOrUpdateContact(logincontact, logincontact.Users.UserID);
                            AuthenticateResponse.ContactID = logincontact.ContactID;
                            AuthenticateResponse.UnlockBusinessContactResponce = "User account is locked,please contact NUL administrator";
                            return AuthenticateResponse;
                        }
                        else
                        {
                            AuthenticateResponse.ContactID = logincontact.ContactID;
                            AuthenticateResponse.UnlockBusinessContactResponce = "Login failed! incorrect password";
                            return AuthenticateResponse;
                        }
                    }

                }
                else if (logincontact.Users.PasswordHash != model.Password)
                {
                    AuthenticateResponse.UnlockBusinessContactResponce = "Incorrect Password";
                    return AuthenticateResponse;
                }


                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                AuthenticateResponse response = new AuthenticateResponse(user, token);
                if (user.Contact != null && user.Contact.AccountStatusID == (long)AccountStatusEnumeration.Deactivated)
                {
                    response.DeactivateBusinessContactResponce = "Bussiness account deactivated.";
                    return response;
                }
                //provide business user roles 
                else if (borrowerrolecount > 0)
                {
                    var businessContacts = this._businessContactRepository.GetAll().Where(c => c.Contact.IsActive == true && c.IsActive == true && c.ContactID == user.ContactID).ToList();
                    if (businessContacts != null && businessContacts.Count() > 0)
                    {
                        response.SubRoleName = businessContacts.Select(x => x.BusinessRole.BusinessRoleName).ToList();
                        response.SubRoleID = businessContacts.Select(x => x.BusinessRoleID).ToList();
                        int ownerRoleCount = businessContacts.Where(x => x.BusinessRoleID != 4).Count();
                        if (ownerRoleCount > 0)
                            response.HasOwnerPermission = true;
                    }

                }

                //user found, save last login datetime
                _userAccount.UpdateUserOnLogin(user);
                //call audit repository to save the user details

                _userSessionDetailsRepository.SaveUserSessionDetails(getAuditUserSession(requestInfo, IPAddress, user.UserID));

                return response;
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Exception in UserLoginServiceImpl-> Authenticate " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> Authenticate ", null);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Exception in UserLoginServiceImpl-> Authenticate " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> Authenticate", null);
                throw ex;

            }

        }


        public UserSessionEntity GetUserDetailsByUserName(string UserName)
        {

            UserSessionEntity loggedUser = null;
            try
            {
                var user = _userRepository.GetByUserName(UserName);

                if (user != null)
                {
                    //mapping domain entity to DTO
                    loggedUser = this._mapper.Map<UserSessionEntity>(user);
                    Logger.LogDebug("added logged user to user session entity");

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> GetUserDetailsByUserName ", null);
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> GetUserDetailsByUserName", null);
                throw ex;
            }

            return loggedUser;
        }

        /**
          Generate JWT token based 
          Add expiry date from appsetting
         */
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            // Take key from appsetting
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecretKey);
            try
            {

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("username", user.UserName.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> generateJwtToken ", null);
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> generateJwtToken", null);
                throw ex;
            }

        }

        private AuditSessionDetails getAuditUserSession(string requestInfo, string IPAddress, long UserID)
        {

            AuditSessionDetails auditSession = new AuditSessionDetails();

            //asign browser info values
            try
            {

                auditSession.UserID = UserID;
                auditSession.BrowserType = "";
                auditSession.BrowserVersion = "";
                auditSession.IPAddress = IPAddress == null ? "" : IPAddress;
                var uaPraser = UAParser.Parser.GetDefault();
                if (requestInfo != null)
                {
                    ClientInfo userBrowserInfo = uaPraser?.Parse(requestInfo);
                    auditSession.SystemPlatform = userBrowserInfo?.OS.ToString() == null ? "" : userBrowserInfo?.OS.ToString();
                    auditSession.BrowserName = userBrowserInfo?.UserAgent?.Family == null ? "" : userBrowserInfo?.UserAgent?.Family;
                    auditSession.BrowserType = userBrowserInfo?.UserAgent?.Major + "." + userBrowserInfo?.UserAgent?.Minor;
                    auditSession.BrowserVersion = userBrowserInfo?.UserAgent?.Major + "." + userBrowserInfo?.UserAgent?.Minor;
                    auditSession.LoginDateTime = System.DateTime.Now;
                    auditSession.LogoutDateTime = null;
                }

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> getAuditUserSession ", null);
                throw ex;
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in UserLoginServiceImpl-> getAuditUserSession", null);
                throw ex;
            }
            return auditSession;
        }


    }
}
