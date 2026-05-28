namespace ThoughtFocus.Repository.Interfaces.Notification
{
    using System;
    using System.Collections.Generic; 
    using ThoughtFocus.DataAccess.Models.Notification; 

    public interface IEmailNotificationLogRepository
    {
        void SaveEmailNotificationLog(EmailNotificationLog emailNotificationLog,long? userID);
    }
}
