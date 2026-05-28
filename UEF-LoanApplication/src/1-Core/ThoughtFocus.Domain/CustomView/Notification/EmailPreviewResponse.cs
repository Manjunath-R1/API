using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.CustomView.Notification
{
    public class EmailPreviewResponse:BaseResponse
    {
        public EmailPreviewEntity EmailPreviewEntity { get; set; }
    }
}
