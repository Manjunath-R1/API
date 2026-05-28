using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class ApplicationListResponse : BaseResponse
    {
        public PageResultEntity<ApplicationListingViewEntity> ApplicationPageResultEntity { get; set; }
        public PageResultEntity<ApplicationListingViewEntityExportToExcel> ApplicationPageResultEntityExportToExcel { get; set; }

    }
}
