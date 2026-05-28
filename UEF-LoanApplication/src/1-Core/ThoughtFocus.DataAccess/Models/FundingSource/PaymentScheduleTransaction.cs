using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("PaymentScheduleTransaction", Schema = "FundingSource")]
    public partial class PaymentScheduleTransaction : AuditBase
    {
    
        [Key]
        public long PaymentScheduleID { get; set; }
        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }

        [ForeignKey("FundingSource")]
        public long ProgramID { get; set; }

        public long BusinessID { get; set; }
        public DateTime TransactionDate { get; set; }
        public long FundingTypeID { get; set; }
        public decimal FundedAmount { get; set; }
        public long TransactionStatusID { get; set; }
        public long ContactID { get; set; }

        public DateTime? DisbursedDate { get; set; }
      

    }
}