using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class FundingSourceResponse : BaseResponse
    {
        public FundingSourceParam FundingSource {get; set;}
        public List<FundUtilizationParam> FundUtilization { get; set; }

    }
}
