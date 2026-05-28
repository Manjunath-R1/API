using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.Repository.Interfaces.Notification
{
    public interface INotificationRecipientEmailAddressRepository : IEFApplicationBaseRepository<NotificationRecipientEmailAddress>
    {
        NotificationRecipientEmailAddress GetRecipientEmailAddressByActivityNotificationID(long ActivityNotificationID);

        NotificationRecipientEmailAddress GetRecipientByActivityNotificationID(long ActivityNotificationID);

    }
}
