using System.Collections.Generic;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.CustomView
{
    public class EmailTemplateLogoResponse : BaseResponse
    {
        public List<EmailTemplateHeaderFooterViewModel> emailTemplateHeaderFooterViewModel { get; set; }

        public FileEntity FileEntity { get; set; }

    }
}
