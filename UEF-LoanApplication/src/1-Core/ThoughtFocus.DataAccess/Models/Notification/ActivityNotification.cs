using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("ActivityNotification", Schema = "Notification")]
    public class ActivityNotification
    {
        public ActivityNotification()
        {
            this.NotificationRecipients = new List<NotificationRecipients>();
            this.NotificationRecipientEmailAddress = new List<NotificationRecipientEmailAddress>();
        }

        [Key]
        public long ActivityNotificationID { get; set; }  
         
        public Nullable<long> ActivityID { get; set; }

         [ForeignKey("Notification")]
        public Nullable<long> NotificationID { get; set; }

        [ForeignKey("WorkflowNotificationType")]
        public Nullable<long> WorkflowNotificationTypeID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }        

        public virtual WorkflowNotificationType WorkflowNotificationType { get; set; }
        public virtual ThoughtFocus.DataAccess.Models.Master.Notification Notification { get; set; }
        public virtual ICollection<NotificationRecipients> NotificationRecipients { get; set; }
        public virtual ICollection<NotificationRecipientEmailAddress> NotificationRecipientEmailAddress { get; set; }

    }
}
