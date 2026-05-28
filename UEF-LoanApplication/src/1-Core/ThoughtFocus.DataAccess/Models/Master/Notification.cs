using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Notification", Schema = "Master")]
    public partial class Notification
    {
        public Notification()
        {
            this.NotificationPlaceholders = new List<NotificationPlaceholders>();
            this.ActivityNotification = new List<ActivityNotification>();
            this.NotificationPreConditions = new List<NotificationPreCondition>();
        }

        [Key]
        public long NotificationID { get; set; }     
      
        public string NotificationType { get; set; }
        public string EmailFormat { get; set; }
        public string MessageSubject { get; set; }
        public string TemplateName { get; set; }
        public string NotificationTypeDescription { get; set; }
        public string Head { get; set; }
        public string Salutation { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public int RecipientType { get; set; }

        public bool IsActive { get; set; }
       
        public virtual ICollection<NotificationPlaceholders> NotificationPlaceholders { get; set; }
        public virtual ICollection<ActivityNotification> ActivityNotification { get; set; }
        public virtual ICollection<NotificationPreCondition> NotificationPreConditions { get; set; }
  }
}