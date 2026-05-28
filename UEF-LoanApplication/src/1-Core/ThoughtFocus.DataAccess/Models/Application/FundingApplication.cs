using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.DataAccess.Models.Application
{ 
    [Table("FundingApplication", Schema = "Application")]
    public partial class FundingApplication
    {
         
        [Key]
        public long ID { get; set; }

        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }

        [ForeignKey("FundingSource")]
        public long ProgramID { get; set; }
        public decimal RequestedFundAmount { get; set; }
        public string Purpose { get; set; }
        public bool? IsPaymentSchedule { get; set; }
        public virtual LoanApplication LoanApplication{ get; set; }
        public virtual ThoughtFocus.DataAccess.Models.FundingSource.FundingSource FundingSource { get; set; }
    }
}