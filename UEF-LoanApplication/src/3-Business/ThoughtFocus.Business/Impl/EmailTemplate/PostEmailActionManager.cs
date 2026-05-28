 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration; 
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Domain.CustomView.Notification;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Impl.EmailTemplate
{
    public class PostEmailActionManager : IPostEmailActionManager
    {
        #region Fields
 
        private IPreEmailConditionManager _preEmailConditionManager;
        private IEmailNotificationLogRepository _emailNotificationLogRepository;

        #endregion Fields

        #region Constructor

        public PostEmailActionManager(IPreEmailConditionManager preEmailConditionManage,
        IEmailNotificationLogRepository emailNotificationLogRepository)
        {
            this._preEmailConditionManager = preEmailConditionManage; 
            _emailNotificationLogRepository=emailNotificationLogRepository;
        }

        #endregion Constructor

        #region Methods

        

        //Log email notification 
        public void logEmailNotification(SendEmailParameter sendEmailParameter, MailMessage mailMessage,UserSessionEntity userSessionEntity)
        {
            try
            {
                //Notification log
                EmailNotificationLog emailNotificationLog = new EmailNotificationLog();

                emailNotificationLog.To = sendEmailParameter.To;
                emailNotificationLog.Cc = sendEmailParameter.cc;
                emailNotificationLog.MessageBody = sendEmailParameter.Header + " " + sendEmailParameter.BodySubjectHTML + " " + sendEmailParameter.salutation + " " + sendEmailParameter.body + " " + sendEmailParameter.Note + " " + sendEmailParameter.Footer;
                emailNotificationLog.CreatedDateTime = DateTime.Now;
                emailNotificationLog.LastModifiedDateTime = DateTime.Now;
                emailNotificationLog.CreatedByUserID = userSessionEntity.UserID;
                emailNotificationLog.LastModifiedByUserID = userSessionEntity.UserID;
                emailNotificationLog.IsActive = true;
                emailNotificationLog.MessageSubject = sendEmailParameter.subject;
                emailNotificationLog.NotificationID = sendEmailParameter.NotificationID;
                emailNotificationLog = this.PopulateEmailNotificationLogEntity(emailNotificationLog, mailMessage);

                
                this._emailNotificationLogRepository.SaveEmailNotificationLog(emailNotificationLog,userSessionEntity.UserID);
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Exception at PostEmailActionManager", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EmailNotificationLog PopulateEmailNotificationLogEntity(EmailNotificationLog emailNotificationLog, MailMessage mailMessage)
        {  
            return emailNotificationLog;
        }

        #endregion Methods
    }
}
