using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("Contacts", Schema = "Contact")]
    public partial class Contact : AuditBase
    {
        
        [Key]
        public long ContactID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long SalutationID { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public long AccountStatusID { get; set; }
        public virtual Salutation Salutation { get; set; }
        public virtual AccountStatus AccountStatus { get; set; }
        public virtual ThoughtFocus.DataAccess.Models.User.User Users { get; set; }    
        public virtual ICollection<ProgramInvitationContactRole> ProgramInvitationContactRoles { get; set; }
    }
}