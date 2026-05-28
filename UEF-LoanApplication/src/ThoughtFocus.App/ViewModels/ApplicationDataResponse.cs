using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.App.ViewModels
{
    // public class ApplicationDataResponse : BaseResponse
    // {
    //     public ContactViewEntity Contact { get; set; }
    // }
    public class ApplicationDataResponse : BaseResponse
    {
        public ApplicationViewEntity LoanApplication { get; set; }
    }

    public class ApplicationResponse : BaseResponse
    {
        public List<ApplicationListingViewEntity> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public long ProgramID { get; set; }
    }

    public class ApplicationSummaryResponse : BaseResponse
    {
        public ApplicationViewEntity LoanApplication { get; set; }
    }
}