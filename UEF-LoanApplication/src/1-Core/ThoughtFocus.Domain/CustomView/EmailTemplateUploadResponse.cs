using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.CustomView
{
    public class EmailTemplateUploadResponse : BaseResponse
    {
        public EmailTemplateHeaderFooterViewModel emailTemplateHeaderFooterViewModel { get; set; }

    }
}
