using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("ProgramInvitees", Schema = "Admin")]
    public partial class ProgramInvitee: AuditBase
    {
        [Key]
        public long ID { get; set; }
        [ForeignKey("ProgramInvitation")]
        public long ProgramInvitationID { get; set; }
        [ForeignKey("Contact")]
        public long ContactID { get; set; }
        
        public virtual ProgramInvitation ProgramInvitation{ get; set; }
        public virtual ThoughtFocus.DataAccess.Models.Contact.Contact Contact{ get; set; }
        
    }
}
