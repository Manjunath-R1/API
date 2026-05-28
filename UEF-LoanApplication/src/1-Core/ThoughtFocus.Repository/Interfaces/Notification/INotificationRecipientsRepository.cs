using System.Collections.Generic; 
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.Repository.Interfaces.Notification
{
    public interface INotificationRecipientsRepository : IEFApplicationBaseRepository<NotificationRecipients>
    {
        List<NotificationRecipients> GetRecipientPlaceholdersByActivityNotificationID(long ActivityNotificationID);

        List<NotificationRecipients> GetRecipientPlaceholdersByNotificationID(long NotificationID);

        List<NotificationRecipients> GetRecipientPlaceholdersByPlaceholderID(long PlaceholderID);
    }
}
