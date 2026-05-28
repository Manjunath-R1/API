using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Response
{
    public class ConsolidatedFundReportDataResponse:BaseResponse
    {
        public List<BusinessEntities> BusinessEntities { get; set; }
        public List<ProgramResponse> Programs { get; set; }
        public List<ConsolidatedFundDetail> ConsolidatedFundDetails { get; set; }

    }
    public class ConsolidatedFundDetail
    {
        public string BusinessName { get; set; }
        public string ProgramName { get; set; }
        public string FundedAmount { get; set; }
        public string FundsAllocated { get; set; }
        public string FundedDisbursed { get; set; }

    }
}
