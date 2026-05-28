using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.App.ViewModels
{
    public class FundingEntityDataResponse : BaseResponse
    {
        public FundingEntityViewEntity FundingEntity { get; set; }
    }

    public class FundingEntityResponse : BaseResponse
    {
        public List<FundingEntityListingViewEntity> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }
}