using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("ProgramInvitation", Schema = "Admin")]
    public partial class ProgramInvitation: AuditBase
    {
        public ProgramInvitation()
        {
            ProgramInvitee = new List<ProgramInvitee>();
        }

        [Key]
        public long ProgramInvitationID { get; set; }
        [ForeignKey("BusinessEntity")]
        public long BusinessID { get; set; }
        [ForeignKey("FundingSource")]
        public long ProgramID { get; set; }
        
        [ForeignKey("ProgramStatus")]        
        public long ProgramStatusID { get; set; }
        public System.DateTime InvitationSentDateTime { get; set; }
        
        public virtual BusinessEntity BusinessEntity{ get; set; }
        public virtual ThoughtFocus.DataAccess.Models.FundingSource.FundingSource FundingSource{ get; set; }
        public virtual ICollection<ProgramInvitee> ProgramInvitee{ get; set; }
        public virtual ProgramStatus ProgramStatus{ get; set; }
        
    }
}



