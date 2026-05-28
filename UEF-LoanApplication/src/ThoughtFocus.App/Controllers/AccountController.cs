using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.DataAccess.Models;
using Microsoft.Extensions.Options;
using ThoughtFocus.Common.Exceptions;
using System.Net.Sockets;
using System;
using System.Text;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly ILogger<ContactController> _logger;
        private IAccountService _accountServiceImpl;
        private IUserLoginService _userLoginService;
        private readonly AppSettings _appSettings;

        #endregion Fields
        #region Constructors
        public AccountController(IAccountService accountServiceImpl, IUserLoginService userLoginService, ILogger<ContactController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userLoginService = userLoginService;
            this._accountServiceImpl = accountServiceImpl;
            _appSettings = appSettings.Value;
        }
        #endregion Constructors

        // [HttpPost("userregistartion")]
        // public IActionResult UserRegistartion(UserViewEntity UserViewEntity)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest("Provided input is invalid");    // return bad response
        //     }

        //     var PasswordSalt = _appSettings.PasswordSalt;

        //     //Check if user already exist
        //     if(_accountServiceImpl.IsUserNameExist(UserViewEntity.UserName))
        //         return StatusCode(409, $"User '{UserViewEntity.UserName}' already exists.");

        //     //Encrypt and generate hashed password for security
        //     UserViewEntity.Password = StringCipher.Encrypt(UserViewEntity.Password, PasswordSalt);

        //     //Register the new user
        //     try
        //     {
        //         _accountServiceImpl.CreateNewUser(UserViewEntity, PasswordSalt);
        //         return Ok("User registered successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest("Error while registering new user");
        //     }

        // }
        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            _logger.LogDebug("Inside Login method ");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Provided input is invalid");    // return bad response
                }
                byte[] base64Pwddata = Convert.FromBase64String(model.Password);
                string password = Encoding.UTF8.GetString(base64Pwddata);

                byte[] base64UserNamedata = Convert.FromBase64String(model.Username);
                string userName = Encoding.UTF8.GetString(base64UserNamedata);

                AuthenticateRequest authenticateRequest = new AuthenticateRequest();
               
                authenticateRequest.Username = userName;

                //replace password with generated Hashed Password, it will be used to validate against database
                authenticateRequest.Password = StringCipher.Encrypt(password, _appSettings.PasswordSalt);
                _logger.LogDebug("After password encryption ");
                var requestInfo = Request.Headers["User-Agent"].ToString();
                string remoteIpAddress = "";
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                if (host != null)
                {
                    foreach (var ip in host.AddressList)
                    {
                        if (ip?.AddressFamily == AddressFamily.InterNetwork)
                        {
                            remoteIpAddress = ip.ToString();
                        }
                    }
                }

                var response = _userLoginService.Authenticate(authenticateRequest, requestInfo, remoteIpAddress);
                _logger.LogDebug("After service layer");
                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                if (!string.IsNullOrEmpty(response.DeactivateBusinessContactResponce))
                {
                    return BadRequest(new { message = "Bussiness account is deactivated." });
                }

                return Ok(response);

            }
            catch (System.Exception ex)
            {

                _logger.LogDebug("Error at Account controller-> login method " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                return BadRequest(new { message = "Exception while logging" + ex.InnerException?.ToString() });
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveConsent")]
        public CommonResponse SaveConsent()
        {
            CommonResponse commonResponse = null;
            commonResponse = this._accountServiceImpl.saveUserConsent(LoginUserInformation.getLoggedInUser(HttpContext));
            return commonResponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("CheckAuthentication")]
        public IActionResult CheckAuthentication()
        {
            return Ok("Authenticated");
        }

    }
}