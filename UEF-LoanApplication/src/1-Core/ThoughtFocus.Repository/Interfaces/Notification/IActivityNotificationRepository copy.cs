using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.Repository.Interfaces.Notification
{
    public interface INotificationModeRepository : IEFApplicationBaseRepository<NotificationModesType>
    {
        void SaveOrUpdateNotificationType(NotificationModesType NotificationMode, long? userID);
        NotificationModesType GetNotificationModeByUser(long userID);
    }
}
