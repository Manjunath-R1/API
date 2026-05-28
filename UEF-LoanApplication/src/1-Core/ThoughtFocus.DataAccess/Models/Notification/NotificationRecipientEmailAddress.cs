using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("NotificationRecipientEmailAddress", Schema = "Notification")]
    public class NotificationRecipientEmailAddress
    {
        [Key]
        public long NotificationRecipientEmailAddressID { get; set; }
        [ForeignKey("ActivityNotification")]
        public long ActivityNotificationID { get; set; }
        public string EmailAddress { get; set; }
        public virtual ActivityNotification ActivityNotification { get; set; }

    }
}
