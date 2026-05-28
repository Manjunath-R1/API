using System;
using System.Collections.Generic;
using System.Net.Sockets;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Business.Interfaces.User;
using ThoughtFocus.Constants;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.Service.Interfaces;

namespace ThoughtFocus.Service.Impl
{
    public class AccountServiceImpl : IAccountService
    {

        #region Fields

        private IUserAccount _userAccount;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountServiceImpl> Logger;
        public ILogger<AccountServiceImpl> Object { get; }

        #endregion Fields

        public AccountServiceImpl(IUserAccount userAccount, IUserRepository userRepository,
         IMapper _mapper, ILogger<AccountServiceImpl> logger)
        {
            this._userAccount = userAccount;
            this._userRepository = userRepository;
            this._mapper = _mapper;
            this.Logger = logger;
        }

        public CommonResponse saveUserConsent(UserSessionEntity userSessionEntity)
        {
            CommonResponse commonResponse = null;
            List<string> validationMessages = new List<string>();
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter User Session is null.");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            try
            {
                long userID = userSessionEntity.UserID;
                User user = this._userRepository.GetByUserID(userID);

                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                string IPAddress = "";
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IPAddress = ip.ToString();
                    }
                }

                UserConsent userConsent = new UserConsent();
                userConsent.UserID = user.UserID;
                userConsent.IPAddress = IPAddress;
                userConsent.ConsentDateTime = System.DateTime.Now;

                user.UserConsent = userConsent;

                _userRepository.SaveUser(user);
                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AccountServiceImpl-> saveUserConsent", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonResponse;
        }
    }
}