using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Contact;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("ContactEmailAddress", Schema = "Contact")]
    public partial class ContactEmailAddress
    {
        [Key]
        public long ContactEmailAddressID { get; set; }
        public long ContactID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsUsedForCommunication { get; set; }
        public virtual Contact Contact { get; set; }
    }
}