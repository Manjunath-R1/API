using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class BusinessViewEntityResponse:BaseResponse 
    {
        public BusinessViewEntity businessViewEntity { get; set; }
        public bool CanBeDeleted { get; set; }
        public bool IsPaymentSchedule { get; set; }
    }
}
