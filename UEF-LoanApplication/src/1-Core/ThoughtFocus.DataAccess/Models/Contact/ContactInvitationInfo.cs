using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("ContactInvitationInfo", Schema = "Contact")]
    public partial class ContactInvitationInfo : AuditBase
    {
        public ContactInvitationInfo()
        {
            this.ContactInvitationQueues = new List<ContactInvitationQueue>();
        }

        public long ContactInvitationInfoID { get; set; }
        public long ContactID { get; set; }
        public long ContactInvitationStatusID { get; set; }
        public System.DateTime ContactInvitedDateTime { get; set; }
        public Nullable<System.DateTime> ContactActionDateTime { get; set; }
        public string InvitationDescription { get; set; }
        public string InvitationEmailAddreess { get; set; }
        public string TokenID { get; set; }
        public bool IsComplete { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ICollection<ContactInvitationQueue> ContactInvitationQueues { get; set; }
    }
}
