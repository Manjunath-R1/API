//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using ThoughtFocus.Business.Interfaces.SMS;
//using ThoughtFocus.DataAccess.Models;
//using ThoughtFocus.DataAccess.Models.Notification;
//using ThoughtFocus.Domain.CustomView;
//using ThoughtFocus.Domain.User;
//using ThoughtFocus.Repository.Impl.SMS;
//using ThoughtFocus.Repository.Interfaces.Contact;
//using ThoughtFocus.Repository.Interfaces.SMS;
//using ThoughtFocus.Service.Interfaces;

//namespace ThoughtFocus.Service.Impl
//{
//    public class SMSNotificationService : ISMSNotificationService
//    {

//        private readonly IContactRepository _contactRepository;
//        private readonly ISMSNotificationTemplateRepository _smsNotificationTemplateRepository;
//       // private readonly ISMSManager _iSMSManager;
//        private readonly AppSettings _appSettings;

//        public SMSNotificationService(IContactRepository contactRepository,
//            ISMSNotificationTemplateRepository sMSNotificationTemplateRepository,
//            ISMSManager iSMSManager,
//            IOptions<AppSettings> appSettings
//            )
//        {
//            _contactRepository = contactRepository;
//            _smsNotificationTemplateRepository = sMSNotificationTemplateRepository;
//            _iSMSManager = iSMSManager;
//            _appSettings = appSettings.Value;
//        }

//        public bool SendSMS(SMSSenderRequest sMSSenderRequest)
//        {
//            bool smsSent = false;
//            string from;
//            try
//            {
//                SMSNotificationLog sMSNotificationLog = new SMSNotificationLog();

//                //get templates
//                var smsTemplate = _smsNotificationTemplateRepository.GetSMSNotificationTemplates(sMSSenderRequest.ApplicationStatusID);
//                var phoneNo = sMSSenderRequest.PhoneNumber;
//                //call send sms method and send sms
//                if (sMSSenderRequest.PhoneNumber != null)
//                {
//                    if (phoneNo.Contains("-") == true)
//                    {
//                        phoneNo = "+1" + phoneNo.Replace("-", "");

//                    }

//                    if (smsTemplate != null)
//                    {
//                        var message = smsTemplate.Template != null ?
//                        smsTemplate.Template.Replace("<#REF No>", sMSSenderRequest.LoanNumber).Replace("<Program Name>", sMSSenderRequest.ProgramName) : "";
//                        string url = _appSettings.BaseUrl + "/form/0/" + sMSSenderRequest.LoanApplicationID;
//                        message = message.Replace("#", "") + " " + url;
//                        message = string.Format("\"{0}\"", message);
//                        //var msg = _iSMSManager.SendSMSNotification(phoneNo, message, out from);

//                        //if (!string.IsNullOrEmpty(msg))
//                        //{
//                        //    sMSNotificationLog.ErrorMessage = msg;
//                        //    smsSent = false;
//                        //    sMSNotificationLog.IsSuccess = false;
//                        //}
//                        //else
//                        //{
//                        //    sMSNotificationLog.ErrorMessage = "";
//                        //    smsSent = true;
//                        //    sMSNotificationLog.IsSuccess = true;
//                        //}
//                        //sMSNotificationLog.To = sMSSenderRequest.PhoneNumber;
//                        //sMSNotificationLog.FROM = from;
//                        //sMSNotificationLog.UserID = sMSSenderRequest.BusinessUserID;
//                        //sMSNotificationLog.TemplateID = smsTemplate.ID;
//                        //sMSNotificationLog.Message = message;
//                        //sMSNotificationLog.IsActive = true;
//                        //sMSNotificationLog.CreatedByUserID = sMSSenderRequest.UserID;
//                        //sMSNotificationLog.CreatedDateTime = DateTime.Now;
//                        //sMSNotificationLog.LastModifiedByUserID = sMSSenderRequest.UserID;
//                        //sMSNotificationLog.LastModifiedDateTime = DateTime.Now;

//                        //_smsNotificationTemplateRepository.SaveSMSNotificationLog(sMSNotificationLog);


//                    }

//                }

//            }
//            catch (Exception ex)
//            {
//                smsSent = false;
//            }


//            return smsSent;
//        }
//    }
//}
