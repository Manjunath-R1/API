
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
    public class NotificationModeRepository : AbstractEFApplicationBaseRepository<NotificationModesType>, INotificationModeRepository
    {
        private ApplicationDBContext _Context;

        #region Constructors

        public NotificationModeRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors
        
        #region Methods

        public NotificationModesType GetNotificationModeByUser(long userID)
        {
            var query =new NotificationModesType();
            try
            {
                query = _Context.NotificationModesTypes.Where(n=>n.UserID == userID).FirstOrDefault();

                if (query != null)
                {
                    // Detach the object to remove it from context’s cache.
                    _Context.Entry(query).State = EntityState.Detached;
                    // Then load it. We will get a new object with data
                    // freshly loaded from the database.
                    query = _Context.NotificationModesTypes.Find(query.ID);

                }
                

                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in NotificationTypeRepository-> GetNotificationModeByUser", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in NotificationTypeRepository-> GetNotificationModeByUser", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in NotificationTypeRepository-> GetNotificationModeByUser", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in NotificationTypeRepository-> GetNotificationModeByUser", ex);
            }
        }

        public void SaveOrUpdateNotificationType(NotificationModesType notificationTypeRequest, long? userID)
        {
            try
            {
                if (notificationTypeRequest.UserID > 0 && notificationTypeRequest.ID > 0)
                {
                    _Context.Entry(notificationTypeRequest).State = EntityState.Modified;
                }
                else
                {
                    _Context.NotificationModesTypes.Add(notificationTypeRequest);
                }
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in NotificationTypeRepository-> SaveOrUpdateNotificationType", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in NotificationTypeRepository-> SaveOrUpdateNotificationType", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in NotificationTypeRepository-> SaveOrUpdateNotificationType", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in NotificationTypeRepository-> SaveOrUpdateBusinessEntity", ex);
            }
        }

        #endregion Methods
    }
}


