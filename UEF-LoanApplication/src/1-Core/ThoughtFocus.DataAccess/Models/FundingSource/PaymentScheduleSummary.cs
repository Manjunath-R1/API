using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("PaymentScheduleSummary", Schema = "FundingSource")]
    public partial class PaymentScheduleSummary : AuditBase
    {
    
        [Key]
        public long ID { get; set; }
        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }

        [ForeignKey("FundingSource")]
        public long ProgramID { get; set; }

        public long BusinessID { get; set; }
        public decimal FundRequestedAmount { get; set; }
        public decimal FundAllocatedAmount { get; set; }
        public decimal FundDisbursedAmount { get; set; }
        public decimal FundPendingAmount { get; set; }  
        public long ContactID { get; set; }
        public string AdditionalNotesAgreement { get; set; }
    }
}