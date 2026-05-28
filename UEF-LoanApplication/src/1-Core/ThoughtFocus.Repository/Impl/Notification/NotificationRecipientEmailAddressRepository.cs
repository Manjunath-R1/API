 
using System; 
using System.Data.SqlClient;
using System.Linq; 
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models.Notification;
using Microsoft.EntityFrameworkCore;

namespace ThoughtFocus.Repository.Impl.Notification
{
    public class NotificationRecipientEmailAddressRepository : AbstractEFApplicationBaseRepository<NotificationRecipientEmailAddress>, INotificationRecipientEmailAddressRepository
    {
        #region Fields

        private ApplicationDBContext _context;
        
        #endregion Fields

        #region Constructors

        public NotificationRecipientEmailAddressRepository(ApplicationDBContext context)
            : base(context)
        {
            this._context = context;
        }

        #endregion Constructors

        #region Methods

        public NotificationRecipientEmailAddress GetRecipientEmailAddressByActivityNotificationID(long ActivityNotificationID)
        {

            try
            {
                NotificationRecipientEmailAddress notificationRecipientEmailAddress = _context.NotificationRecipientEmailAddress.Where(a => a.ActivityNotification.ActivityNotificationID == ActivityNotificationID).FirstOrDefault();

                return notificationRecipientEmailAddress;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in GetRecipientByActivityNotificationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in GetRecipientByActivityNotificationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetRecipientByActivityNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetRecipientByActivityNotificationID", ex);
            }
        }

        public NotificationRecipientEmailAddress GetRecipientByActivityNotificationID(long ActivityNotificationID)
        {

            try
            {
                
                NotificationRecipientEmailAddress notificationRecipientEmailAddress = _context.NotificationRecipientEmailAddress.Where(a => a.ActivityNotification.ActivityNotificationID == ActivityNotificationID).FirstOrDefault();

                return notificationRecipientEmailAddress;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in GetRecipientByActivityNotificationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in GetRecipientByActivityNotificationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetRecipientByActivityNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetRecipientByActivityNotificationID", ex);
            }
        }

        
       

        #endregion Methods
    }
}
