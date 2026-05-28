using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("LoanApplicationAgreementDetails", Schema = "Application")]
    public partial class LoanApplicationAgreementDetail : AuditBase
    {
        [Key]
        public long LoanApplicationAgreementDetailID { get; set; }

		[ForeignKey("LoanApplication")]		
        public long ApplicationID { get; set; }
        public long TransitionID { get; set; }
		public string Comments { get; set; }
		public string IPAddress { get; set; }
		
        public virtual LoanApplication LoanApplication { get; set; }

    }
}
