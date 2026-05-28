using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.ServiceResponse
{
    public class EmailNotificationReminderResponse : BaseResponse
    {
        public List<EmailTemplateEntity> ReminderNotificationEntity { get; set; }
        public long ReminderNotificationCount { get; set; }
    }
}
