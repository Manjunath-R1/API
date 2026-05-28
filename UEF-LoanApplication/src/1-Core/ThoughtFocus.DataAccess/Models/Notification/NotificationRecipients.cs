using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("NotificationRecipients", Schema = "Notification")]
    public class NotificationRecipients
    {
        [Key]
        public long NotificationRecipientID { get; set; }
       
        [ForeignKey("ActivityNotification")]
        public long ActivityNotificationID { get; set; }

        [ForeignKey("Placeholders")]
        public long PlaceholderID { get; set; }
        public bool IsTo { get; set; }
        public bool IsCC { get; set; }
        public bool IsActive { get; set; }
         public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        
        public virtual ActivityNotification ActivityNotification { get; set; }
        public virtual EmailTemplatePlaceholders Placeholders { get; set; }

    }
}
