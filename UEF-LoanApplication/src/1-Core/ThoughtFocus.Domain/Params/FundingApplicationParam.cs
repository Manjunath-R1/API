using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.Params
{
    public class FundingApplicationParam
    {
        #region Properties
        public Nullable<decimal> RequestedFundAmount { get; set; }
        public bool HasDefaultFundAmount { get; set; }
        public string Purpose { get; set; }
        public bool? IsPaymentSchedule { get; set; }
        public List<QuestionResponseForFunding> QuestionResponse { get; set; }
        public long LoanApplicationID​​​​​​​​ { get; set; }
        public long ID { get; set; }
        public List<ProgramQuestion> ProgramQuestions { get; set; }

        public bool IsbankruptcyProtection { get; set; }
        public bool IsAppliedBankLoan { get; set; }
        public bool IsAnyOWnerBankruptcy { get; set; }
        public bool IsAtleast51percentBlackOwned { get; set; }
        public bool IsApplicantAtleast25percentBlack { get; set; }
        public bool IsApplicantUScitizen { get; set; }
        public List<PaymentScheduleTransactionRequest> PaymentScheduleTransaction { get; set; }
        public PaymentScheduleSummaryRequest PaymentScheduleSummary {get;set;}

        public string PendingDisbursement { get; set; }

        public bool IsSPAExist { get; set; }
        #endregion Properties
    }
}
