using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.Business.Interfaces.SMS;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Repository.Interfaces.SMS;

namespace ThoughtFocus.Business.Impl.SMS
{
    public class SMSManager : ISMSManager
    {
        #region Fields

        private readonly ILogger<SMSManager> _logger;

        private readonly SMSSettings _smsSettings;

        private readonly ISendSMS _iSendSMS;
        private readonly ISMSNotificationTemplateRepository _smsNotificationTemplateRepository;
        //private readonly ISMSManager _iSMSManager;
        private readonly AppSettings _appSettings;
        private readonly ILoanApplicationRepository _loanApplicationRepository;
        #endregion Fields

        #region Constructor

        public SMSManager(ILogger<SMSManager> logger, IOptions<SMSSettings> smsSettings,
            ISendSMS iSendSMS,
              ISMSNotificationTemplateRepository sMSNotificationTemplateRepository,
           // ISMSManager iSMSManager,
            IOptions<AppSettings> appSettings,
            ILoanApplicationRepository loanApplicationRepository
            )
        {
            this._logger = logger;
            this._smsSettings = smsSettings.Value;
            this._iSendSMS = iSendSMS;
            _smsNotificationTemplateRepository = sMSNotificationTemplateRepository;
            //_iSMSManager = iSMSManager;
            _appSettings = appSettings.Value;
            _loanApplicationRepository = loanApplicationRepository;
        }

        #endregion Constructor

        #region Methods

        //public string SendSMSNotification(string to,string message,out string from)
        //{
        public bool SendSMSNotification(SMSSenderRequest sMSSenderRequest)
        {
            bool smsSent = false;
            string errorMsg = string.Empty;
            long smsTemplateId = 0;
            SMSNotificationLog sMSNotificationLog = new SMSNotificationLog();
            var message = string.Empty;
            try
            {
                var paymentScheduleStatus = this._loanApplicationRepository.GetPaymentScheduleStatusById(sMSSenderRequest.LoanApplicationID);

                //get templates
                var smsTemplate = _smsNotificationTemplateRepository.GetSMSNotificationTemplates(sMSSenderRequest.ApplicationStatusID);
                var phoneNo = sMSSenderRequest.PhoneNumber;
                //call send sms method and send sms
                if (sMSSenderRequest.PhoneNumber != null)
                {
                    if (phoneNo.Contains("-") == true)
                    {
                        phoneNo = "+1" + phoneNo.Replace("-", "");

                    }

                    if (smsTemplate != null)
                    {
                        message = smsTemplate.Template != null ?
                        smsTemplate.Template.Replace("<#REF No>", sMSSenderRequest.LoanNumber).Replace("<Program Name>", sMSSenderRequest.ProgramName) : "";
                        if (paymentScheduleStatus != null)
                        {
                            smsTemplate.Template.Replace("<Disbursement>", paymentScheduleStatus.Status);
                        }
                            
                        string url = _appSettings.BaseUrl + "/form/0/" + sMSSenderRequest.LoanApplicationID;
                        message = message.Replace("#", "") + " " + url;
                        message = string.Format("\"{0}\"", message);
                        smsSent = _iSendSMS.SendSMSByGetAPI(phoneNo, message, _smsSettings);
                        smsTemplateId = smsTemplate.ID;


                    }

                }

            }
            catch (Exception ex)
            {
                smsSent = false;
                errorMsg = ex.Message;
            }
            sMSNotificationLog.ErrorMessage = errorMsg;
            sMSNotificationLog.IsSuccess = smsSent;

            sMSNotificationLog.To = sMSSenderRequest.PhoneNumber;
            sMSNotificationLog.FROM = _smsSettings.From;
            sMSNotificationLog.UserID = sMSSenderRequest.BusinessUserID;
            sMSNotificationLog.TemplateID = smsTemplateId;
            sMSNotificationLog.Message = message;
            sMSNotificationLog.IsActive = true;
            sMSNotificationLog.CreatedByUserID = sMSSenderRequest.UserID;
            sMSNotificationLog.CreatedDateTime = DateTime.Now;
            sMSNotificationLog.LastModifiedByUserID = sMSSenderRequest.UserID;
            sMSNotificationLog.LastModifiedDateTime = DateTime.Now;
            _smsNotificationTemplateRepository.SaveSMSNotificationLog(sMSNotificationLog);

            return smsSent;
        }
       
        #endregion Methods
    }
}
