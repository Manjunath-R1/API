using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Notification;

namespace ThoughtFocus.Domain.Params
{
    [Serializable]
    public class EmailTemplateParam
    {
        #region Properties

        public long NotificationID { get; set; }

        public long ActivityID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }

        public long CreatedByUserID { get; set; }

        public System.DateTime LastModifiedDateTime { get; set; }

        public long LastModifiedByUserID { get; set; }

        public bool IsActive { get; set; }

        public string NotificationType { get; set; }

        public string EmailFormat { get; set; }

        public string MessageSubject { get; set; }

        public string TemplateName { get; set; }

        public string NotificationTypeDescription { get; set; }

        public string Header { get; set; }

        public string Salutation { get; set; }

        public string MessageBody { get; set; }

        public string Footer { get; set; }

        public string Signature { get; set; }

        public string RecipientEmailAddress { get; set; }

        public List<EmailTemplatePlaceholderEntity> RecipientPlaceholders { get; set; }

        public List<EmailTemplatePlaceholderTypeEntity> PlaceholdersTypes { get; set; }

        public List<NotificationPlaceholdersEntity> NotificationPlaceholders { get; set; }

        public long ActivityNotificationTypeID { get; set; }

        public int RecipientType { get; set; }

        #endregion Properties
    }
}
