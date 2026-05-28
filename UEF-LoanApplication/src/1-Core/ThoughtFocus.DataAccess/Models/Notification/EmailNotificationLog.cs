using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("EmailNotificationLog", Schema = "Notification")]
    public partial class EmailNotificationLog
    {
        public EmailNotificationLog()
        {
            
            this.EmailNotificationLogAddressees = new List<EmailNotificationLogAddressee>();
        }

        [Key]
        public long EmailNotificationLogID { get; set; }      
        
        public string To { get; set; }
        public string Cc { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
         [ForeignKey("Notification")]
        public long NotificationID { get; set; }
        public bool IsActive { get; set; }
         public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        
        public virtual ThoughtFocus.DataAccess.Models.Master.Notification Notification { get; set; }
 
        public virtual ICollection<EmailNotificationLogAddressee> EmailNotificationLogAddressees { get; set; }

    }
}
