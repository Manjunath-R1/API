
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Contact
{
    [Table("ProgramInvitationContactRole", Schema = "Contact")]
    public class ProgramInvitationContactRole
    {
        [Key]
        public long ID { get; set; }
        public long ProgramID { get; set; }
        public long RoleID { get; set; }
        public long ContactID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
    }
}
