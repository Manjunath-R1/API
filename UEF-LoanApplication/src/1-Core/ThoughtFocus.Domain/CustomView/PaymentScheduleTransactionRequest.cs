using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.CustomView
{
    public class PaymentScheduleTransactionRequest
    {
        public long PaymentScheduleID { get; set; }
        public long LoanApplicationID { get; set; }
        public long ProgramID { get; set; }

        public long BusinessID { get; set; }
        public string TransactionDate { get; set; }
        public long FundingTypeID { get; set; }
        public decimal FundedAmount { get; set; }
        public string FundedAmountFormat { get; set; }
        public long TransactionStatusID { get; set; }
        public bool IsEnabled { get; set; }
        public string DisbursedDate { get; set; }
    }
    public class TransactionAccountDisbursed
    {
        public string DisbursementName { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
        public string DisbursedDate { get; set; }
        
    }
}
