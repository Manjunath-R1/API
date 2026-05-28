 
using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Linq; 
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ThoughtFocus.Repository.Impl.Notification
{
    public class ActivityNotificationRepository  : AbstractEFApplicationBaseRepository<ActivityNotification>, IActivityNotificationRepository
    {
         #region Fields

        private ApplicationDBContext _context;
        private readonly ILogger<ActivityNotificationRepository > _logger;  
        
        #endregion Fields

        #region Constructors

        public ActivityNotificationRepository (ApplicationDBContext context, ILogger<ActivityNotificationRepository > logger)
            : base(context)
        {
            this._context = context;
            this._logger = logger;
        }

        #endregion Constructors

        #region Methods

        public List<ActivityNotification> GetRActivityPlaceholdersByNotificationID(long notificationID)
        {
            List<ActivityNotification> ActivityNotification = new List<ActivityNotification>();
            try
            {
                ActivityNotification = GetAll().Where(x => x.NotificationID == notificationID && x.IsActive == true) == null ? ActivityNotification : GetAll().Where(x => x.NotificationID == notificationID && x.IsActive == true).ToList();
                if (ActivityNotification != null && ActivityNotification.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no activity assigned for NotificationID-{0}", notificationID));
                }
                else if (ActivityNotification == null)
                {
                    _logger.LogDebug(String.Format("There is no activity assigned for NotificationID-{0}", notificationID));
                }

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholdersByNotificationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholdersByNotificationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholdersByNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholdersByNotificationID", ex);
            }
            return ActivityNotification;
        }

        public List<ActivityNotification> GetActivityNotificationsByActivityID(long activityID)
        {
            List<ActivityNotification> ActivityNotification = new List<ActivityNotification>();
            try
            {
                ActivityNotification = GetAll().Where(x => x.ActivityID == activityID && x.IsActive == true) == null ? ActivityNotification : GetAll().Where(x => x.ActivityID == activityID && x.IsActive == true).ToList();
                if (ActivityNotification != null && ActivityNotification.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no notification assigned for activityID-{0}", activityID));
                }
                else if (ActivityNotification == null)
                {
                    _logger.LogDebug(String.Format("There is no notification assigned for activityID-{0}", activityID));
                }

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetActivityNotificationsByActivityID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetActivityNotificationsByActivityID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetActivityNotificationsByActivityID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetActivityNotificationsByActivityID", ex);
            }
            return ActivityNotification;
        }

        public ActivityNotification GetRActivityPlaceholders(long notificationID, long ActivityID)
        {
            ActivityNotification ActivityNotification = new ActivityNotification();
            try
            {
                ActivityNotification = FirstOrDefault(x => x.NotificationID == notificationID && x.ActivityID == ActivityID) == null ? ActivityNotification : FirstOrDefault(x => x.NotificationID == notificationID && x.ActivityID == ActivityID);

                if (ActivityNotification == null)
                {
                    _logger.LogDebug(String.Format("There is no activity assigned for NotificationID-{0}", notificationID));
                }

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholders", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholders", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholders", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ActivityNotificationRepository-> GetRActivityPlaceholders", ex);
            }
            return ActivityNotification;
        }

        #endregion Methods
    }
}
