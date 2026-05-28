using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("SMSNotificationLog", Schema = "Notification")]
    public class SMSNotificationLog
    {
        [Key]
        public long ID { get; set; }
        public string To { get; set; }
        public string FROM { get; set; }
        [ForeignKey("Users")]
        public long UserID { get; set; }
        [ForeignKey("SMSNotificationTemplate")]
        public long TemplateID { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public virtual SMSNotificationTemplate SMSNotificationTemplate { get; set; }
        public virtual ThoughtFocus.DataAccess.Models.User.User Users { get; set; }

    }
}
