using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("EmailNotificationLogAddressee", Schema = "Notification")]
    public partial class EmailNotificationLogAddressee
    {
        [Key]
        public long EmailNotificationLogAddresseeID { get; set; }
         [ForeignKey("EmailNotificationLog")]
        public long EmailNotificationLogID { get; set; }
        public string  EmailAddresses { get; set; }
        public bool IsCc { get; set; }

        [ForeignKey("NotificationStatus")]
        public long? NotificationStatusID { get; set; }

        public bool IsActive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        
        public virtual NotificationStatus NotificationStatus { get; set; }

         public virtual EmailNotificationLog EmailNotificationLog { get; set; }

    }
}
