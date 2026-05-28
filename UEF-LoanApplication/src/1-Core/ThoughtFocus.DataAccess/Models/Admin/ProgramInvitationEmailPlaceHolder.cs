using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("ProgramInvitationEmailPlaceHolder", Schema = "Admin")]
    public class ProgramInvitationEmailPlaceHolder
    {
        [Key]
        public long ProgramInvitationEmailID { get; set; }
        public long BusinessID { get; set; }
        public long ProgramID { get; set; }
        public long ProgramStatusID { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
    }
}
