 
using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Linq; 
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using Microsoft.Extensions.Logging;
using ThoughtFocus.DataAccess.Models.Notification;
using Microsoft.EntityFrameworkCore;

namespace ThoughtFocus.Repository.Impl.Notification
{
    public class NotificationRecipientsRepository : AbstractEFApplicationBaseRepository<NotificationRecipients>, INotificationRecipientsRepository
    {
        #region Fields

        private ApplicationDBContext _context;        
        private readonly ILogger<NotificationRecipientsRepository> _logger;  
        #endregion Fields

        #region Constructors

        public NotificationRecipientsRepository(ApplicationDBContext context, ILogger<NotificationRecipientsRepository> logger)
            : base(context)
        {
            this._context = context;
            this._logger = logger;
        }

        #endregion Constructors

        #region Methods

        public List<NotificationRecipients> GetRecipientPlaceholdersByActivityNotificationID(long ActivityNotificationID)
        {
            List<NotificationRecipients> notificationRecipients = new List<NotificationRecipients>();

            try
            {
                notificationRecipients = GetAll().Where(a => a.IsActive == true && a.ActivityNotification.ActivityNotificationID == ActivityNotificationID) == null ? notificationRecipients : GetAll().Where(a => a.IsActive == true && a.ActivityNotification.ActivityNotificationID == ActivityNotificationID).ToList();

                if (notificationRecipients != null && notificationRecipients.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no Placeholders assigned for NotificationID-{0}", ActivityNotificationID));
                }
                else if (notificationRecipients == null)
                {
                    _logger.LogDebug(String.Format("There is no Placeholders assigned for NotificationID-{0}", ActivityNotificationID));
                }

                return notificationRecipients;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception at NotificationRecipientsRepository-> GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception at NotificationRecipientsRepository-> GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception at NotificationRecipientsRepository-> GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at NotificationRecipientsRepository-> GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
        }

        public List<NotificationRecipients> GetRecipientPlaceholdersByNotificationID(long NotificationID)
        {
            List<NotificationRecipients> notificationRecipients = new List<NotificationRecipients>();

            try
            {
                notificationRecipients = GetAll().Where(a => a.IsActive == true && a.ActivityNotification.NotificationID == NotificationID) == null ? notificationRecipients : GetAll().Where(a => a.IsActive == true && a.ActivityNotification.NotificationID == NotificationID).ToList();

                if (notificationRecipients != null && notificationRecipients.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no Placeholders assigned for NotificationID-{0}", NotificationID));
                }
                else if (notificationRecipients == null)
                {
                    _logger.LogDebug(String.Format("There is no Placeholders assigned for NotificationID-{0}", NotificationID));
                }

                return notificationRecipients;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
        }

        public List<NotificationRecipients> GetRecipientPlaceholdersByPlaceholderID(long PlaceholderID)
        {
            List<NotificationRecipients> notificationRecipients = new List<NotificationRecipients>();

            try
            {
                notificationRecipients = GetAll().Where(a => a.IsActive == true && a.PlaceholderID == PlaceholderID) == null ? notificationRecipients : GetAll().Where(a => a.IsActive == true && a.PlaceholderID == PlaceholderID).ToList();

                if (notificationRecipients != null && notificationRecipients.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no Placeholders assigned for PlaceholderID-{0}", PlaceholderID));
                }
                else if (notificationRecipients == null)
                {
                    _logger.LogDebug(String.Format("There is no Placeholders assigned for PlaceholderID-{0}", PlaceholderID));
                }

                return notificationRecipients;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Probelm in getting data from GetRecipientPlaceholdersByActivityNotificationID", ex);
            }
        }

        #endregion Methods
    }
}
