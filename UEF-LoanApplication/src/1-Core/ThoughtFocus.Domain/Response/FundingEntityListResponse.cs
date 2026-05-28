using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class FundingEntityListResponse:BaseResponse 
    {
        public PageResultEntity<FundingEntityListingViewEntity> FundingEntityPageResultEntity { get; set; }
    }
}
