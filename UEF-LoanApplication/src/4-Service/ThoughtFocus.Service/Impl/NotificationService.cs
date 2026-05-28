using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using System;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Domain;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Business.Interfaces.SMS;

namespace ThoughtFocus.Services.impl
{
    public class NotificationService : INotificationService
    {

        #region Fields     

        private IEmailTemplateManager _emailTemplateManager;
        private readonly ILogger<NotificationService> _logger;
        private readonly IEmailTemplateManager _iEmailTemplateManager;
        #endregion Fields

        #region Constructors
        public NotificationService(
         IEmailTemplateManager emailTemplateManager, ILogger<NotificationService> logger, IEmailTemplateManager iEmailTemplateManager)
        {

            this._emailTemplateManager = emailTemplateManager;
            this._logger = logger;
            this._iEmailTemplateManager = iEmailTemplateManager;
        }
        #endregion Constructors

        #region Methods


        public bool SendContactEmail(string subject, string callBackURL, long notificationID, long ContactID,string additionalMessage, UserSessionEntity userSessionEntity)
        {
            var response = false;
            try
            {
                EmailSenderRequest emailRequest = new EmailSenderRequest();
                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = notificationID;
                emailRequest.UserID = userSessionEntity.UserID;
                emailRequest.CallBackURL = callBackURL;
                emailRequest.ContactID = ContactID;
                emailRequest.Role = "Contact";
                emailRequest.AdditionalMessage =additionalMessage;
                _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                response = true;
            }


            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");

                response = false;
                throw ex;
            }
            return response;

        }

       public bool SendForgetPasswordEmail(string subject, string callBackURL, long notificationID, long ContactID,UserSessionEntity userSessionEntity)
        {
            var response = false;
            try
            {
                EmailSenderRequest emailRequest = new EmailSenderRequest();
                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = notificationID;
                emailRequest.UserID = userSessionEntity.UserID;
                emailRequest.CallBackURL = callBackURL;
                emailRequest.ContactID = ContactID;
               _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                response = true;
            }


            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");

                response = false;
                throw ex;
            }
            return response;

        }
        public void SendResetPasswordEmail(string userEmail, string subject, string callBackURL, long notificationID, UserSessionEntity userSessionEntity)
        {
        try
            {
            EmailSenderRequest emailRequest = new EmailSenderRequest();

            emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
            emailRequest.NotificationID = notificationID;
            emailRequest.UserID = userSessionEntity.UserID;
            emailRequest.CallBackURL = callBackURL;
            emailRequest.EmailAddress = userEmail;

            

            _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
            }


            catch (Exception ex)
            {
                 _logger.LogError(ex.InnerException.ToString());
                _logger.LogDebug("Returned unsuccessfully from SendResetPasswordEmail method");

              
                throw ex;
            }

        }

        public bool SendPostAccountActivationEmail(Contact contact, string generalEmail, long notificationID)
        {
            var response = false;

            try
            {
                EmailSenderRequest emailRequest = new EmailSenderRequest();

                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = notificationID;
                emailRequest.UserID = contact.Users.UserID;
                emailRequest.EmailAddress = generalEmail;
                emailRequest.ContactID = contact.ContactID;

                UserSessionEntity userSessionEntity = new UserSessionEntity();
                userSessionEntity.UserID = contact.Users.UserID;

                _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                response = true;
            }

            catch (Exception ex)
            {
                 _logger.LogError(ex.InnerException.ToString());
                _logger.LogDebug("Returned unsuccessfully from SendPostAccountActivationEmail method");

                response = false;
                throw ex;
            }
            return response;

        }

        public bool SendProgramInvitationEmail(string subject, long programInvitationID, long programID, string callBackURL, long ContactID, UserSessionEntity userSessionEntity, string generalEmail)
        {
            var response = false;
            try
            {
                ProgramInvitationEmailSendRequest emailRequest = new ProgramInvitationEmailSendRequest();
                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = (long)EmailTemplateNameID.PROGRAMINVITATION;
                emailRequest.UserID = userSessionEntity.UserID;
                emailRequest.ContactID = ContactID;
                emailRequest.Role = "Contact";
                emailRequest.ProgramInvitationID = programInvitationID;
                emailRequest.CallBackURL = callBackURL;
                emailRequest.EmailAddress = generalEmail;
                emailRequest.BusinessID = 0;
                emailRequest.ProgramID = programID;



                 //_emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                _emailTemplateManager.SendProgramInvitationEmail(emailRequest, userSessionEntity);
                response = true;
            }


            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendProgramInvitationEmail method");
                response = false;
                throw ex;
            }
            return response;

        }

        public bool SendResetBusinessContactEmail(string subject, string callBackURL, long notificationID, long ContactID, UserSessionEntity userSessionEntity)
        {
             var response = false;
            try
            {
                EmailSenderRequest emailRequest = new EmailSenderRequest();
                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = notificationID;
                emailRequest.UserID = userSessionEntity.UserID;
                emailRequest.CallBackURL = callBackURL;
                emailRequest.ContactID = ContactID;
                _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                response = true;
            }

            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");

                response = false;
                throw ex;
            }
            return response;
        }
        public bool SendSPAEmail(string subject, string callBackURL, long notificationID, long programInvitationID, long ContactID, long loanApplicationID, string role, UserSessionEntity userSessionEntity)
        {
            var response = false;
            try
            {
                EmailSenderRequest emailRequest = new EmailSenderRequest();
                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = notificationID;
                emailRequest.UserID = userSessionEntity.UserID;
                emailRequest.Role = role;
                emailRequest.CallBackURL = callBackURL;
                emailRequest.ContactID = ContactID;
                emailRequest.ProgramInvitationID = programInvitationID;
                emailRequest.ApplicationID = loanApplicationID;
                _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                response = true;
            }

            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");

                response = false;
                throw ex;
            }
            return response;

        }
        public bool SendPSEmail(string subject, string email, string callBackURL, long notificationID, long programInvitationID, long ContactID, long loanApplicationID, string role, UserSessionEntity userSessionEntity)
        {
            var response = false;
            try
            {
                EmailSenderRequest emailRequest = new EmailSenderRequest();
                emailRequest.WorkflowNotificationTypeID = (long)WorkflowNotificationType.Other;
                emailRequest.NotificationID = notificationID;
                emailRequest.UserID = userSessionEntity.UserID;
                emailRequest.EmailAddress = email;
                emailRequest.Role = role;
                emailRequest.CallBackURL = callBackURL;
                emailRequest.ContactID = ContactID;
                emailRequest.ProgramInvitationID = programInvitationID;
                emailRequest.ApplicationID = loanApplicationID;
                _emailTemplateManager.SendOthersEmail(emailRequest, userSessionEntity);
                response = true;
            }

            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");

                response = false;
                throw ex;
            }
            return response;

        }
        public bool SendSMS(LoanApplication loanApplications, long activityID, UserSessionEntity userSessionEntity)
        {
            bool isSmsSent = false;
            bool isSent = this._iEmailTemplateManager.SendSMS(loanApplications, activityID, userSessionEntity);
            return isSmsSent;
        }
        #endregion Methods
    }
}
