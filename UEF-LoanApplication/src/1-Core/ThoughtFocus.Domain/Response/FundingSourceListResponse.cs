using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class FundingSourceListResponse : BaseResponse
    {
        public long FundingSourceID { get; set; }
        public string ProgramName { get; set; }
        public int FundingTypeID { get; set; }
        public string FundType { get; set; }
        public string InitialFundedAmount { get; set; }
        public string AvailableLimit { get; set; }
        public string UtilizedAmount { get; set; }
        public string TotalFundedAmount { get; set; }
     
    }
}
