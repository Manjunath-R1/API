using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("LoanApplicationComments", Schema = "Application")]
    public partial class LoanApplicationComment : AuditBase
    {
        public long LoanApplicationCommentID { get; set; }

		[ForeignKey("LoanApplication")]		
        public long ApplicationID { get; set; }
        public long TransitionID { get; set; }
		public string Comments { get; set; }
        public virtual LoanApplication LoanApplication { get; set; }

    }
}
