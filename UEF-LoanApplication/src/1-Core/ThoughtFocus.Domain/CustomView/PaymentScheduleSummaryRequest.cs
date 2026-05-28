using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.CustomView
{
    public class PaymentScheduleSummaryRequest
    {
        public long ID { get; set; }
        public long LoanApplicationID { get; set; }
        public long ProgramID { get; set; }

        public long BusinessID { get; set; }
        public decimal FundRequestedAmount { get; set; }
        public decimal FundAllocatedAmount { get; set; }
        public decimal FundDisbursedAmount { get; set; }
        public decimal FundPendingAmount { get; set; }
    }
}
