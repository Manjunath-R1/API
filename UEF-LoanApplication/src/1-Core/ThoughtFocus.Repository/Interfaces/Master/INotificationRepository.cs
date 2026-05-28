using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Repository;

namespace ThoughtFocusRepository.Interfaces.Master
{
    public interface INotificationRepository : IEFApplicationBaseRepository<Notification>
    {
        List<Notification> GetAllNotificationTypes();

        IQueryable<Notification> GetAllEmailTemplates();

        void SaveOrUpdate(Notification notification, long? userID);

        List<Notification> GetNotificationForActivity(long ActivityID, long WorkflowNotificationType);

        List<Notification> GetNotificationForOther(long WorkflowNotificationType);

        Notification GetEmailTemplateByActivityID(long? emailTemplateID, long? activityID);

        Notification GetNotificationByNotificationType(long NotificationID);
    }
}
