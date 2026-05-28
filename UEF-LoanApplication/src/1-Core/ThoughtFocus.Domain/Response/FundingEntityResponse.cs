using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;

namespace ThoughtFocus.Domain.Response
{
    public class FundingEntityResponse:BaseResponse 
    {
        public FundingEntityViewEntity FundingEntityViewEntity { get; set; }
    }
}
