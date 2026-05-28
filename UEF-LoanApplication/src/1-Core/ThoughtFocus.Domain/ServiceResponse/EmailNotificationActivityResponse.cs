using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Notification;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.ServiceResponse
{
    public class EmailNotificationActivityResponse : BaseResponse
    {
            public long ActivityCount { get; set; }
        public NotificationActivityEntity notificationActivityEntity { get; set; }
        public EmailTemplateHeaderFooterViewModel emailTemplateHeaderFooterViewModel { get; set; }
    }
}
