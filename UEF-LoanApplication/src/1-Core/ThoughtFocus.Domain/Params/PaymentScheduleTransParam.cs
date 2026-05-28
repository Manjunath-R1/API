using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.Params
{
    public class PaymentScheduleTransParam
    {
        #region Properties
        public long PaymentScheduleID { get; set; }
        public long LoanApplicationID { get; set; }
        public long BusinessID { get; set; }
        public long ProgramID { get; set; }
        public DateTime TransactionDate { get; set; }
        public long FundingTypeID { get; set; }
        public decimal FundedAmount { get; set; }
        public long TransactionStatusID { get; set; }
        public long ContactID { get; set; }
      
        #endregion Properties
    }
    public class PaymentScheduleTransResponse: BaseResponse
    {
        #region Properties
        public long LoanApplicationID { get; set; }
        public long BusinessID { get; set; }
        public long ProgramID { get; set; }
        public string FundPendingAmount { get; set; }
        public string FundDisbursedAmount { get; set; }
        public string AdditionalNotesAgreement { get; set; }
        
        public CommonResponse commonResponse { get; set; }

        #endregion Properties
    }
}
