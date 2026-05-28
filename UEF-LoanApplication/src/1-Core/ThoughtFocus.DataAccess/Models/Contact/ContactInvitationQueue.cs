using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Contact;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("ContactInvitationQueue", Schema = "Contact")]
    public partial class ContactInvitationQueue
    {
        public long ContactInvitationQueueID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }

        [ForeignKey("ContactInvitationInfo")]
        public long ContactInvitationInfoID { get; set; }
        public string QueueStatus { get; set; }
        public long AttemptCount { get; set; }
        public string Message { get; set; }
        public virtual ContactInvitationInfo ContactInvitationInfo { get; set; }
    }
}
