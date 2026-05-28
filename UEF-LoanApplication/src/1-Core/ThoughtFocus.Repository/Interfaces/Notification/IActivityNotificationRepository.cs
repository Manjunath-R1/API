using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.Repository.Interfaces.Notification
{
    public interface IActivityNotificationRepository : IEFApplicationBaseRepository<ActivityNotification>
    {
        List<ActivityNotification> GetRActivityPlaceholdersByNotificationID(long notificationID);

        ActivityNotification GetRActivityPlaceholders(long notificationID, long ActivityID);

        List<ActivityNotification> GetActivityNotificationsByActivityID(long activityID);
    }
}
