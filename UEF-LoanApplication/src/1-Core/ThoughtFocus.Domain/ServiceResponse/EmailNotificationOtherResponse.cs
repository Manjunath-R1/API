using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.ServiceResponse
{
    public class EmailNotificationOtherResponse : BaseResponse
    {
        public List<EmailTemplateEntity> notificationOtherEntity { get; set; }
        public long OtherNotificationCount { get; set; }
    }
}
