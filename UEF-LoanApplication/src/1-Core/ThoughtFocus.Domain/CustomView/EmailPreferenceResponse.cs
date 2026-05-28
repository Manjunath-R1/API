using System.Collections.Generic;

namespace ThoughtFocus.Domain.CustomView
{
    public class EmailPreferenceResponse
    {
        public List<NotificationTypeViewEntity> NotificationTypes { get; set; }
        public long UserID { get; set; }
    }
}
