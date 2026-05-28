using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Repository.Interfaces.Admin;

namespace ThoughtFocus.Repository.Impl.Admin
{
    //public class NotificationModeRepository : AbstractEFApplicationBaseRepository<NotificationMode>, INotificationMode    Repository
    //{
    //    private ApplicationDBContext _Context;

    //    public NotificationTypeResponse GetNotificationTypeByUser(long userID)
    //    {
    //        try
    //        {
    //            var query = GetAll().FirstOrDefault(x => x.UserID == userID && x.IsActive == true);
    //            return query;
    //        }
    //        catch (SqlException ex)
    //        {
    //            throw new RepositoryException("SqlException in NotificationTypeRepository-> GetBusinessEntity", ex);
    //        }
    //        catch (DbUpdateException ex)
    //        {
    //            throw new RepositoryException("DbUpdateException in NotificationTypeRepository-> GetBusinessEntity", ex);
    //        }
    //        catch (ObjectDisposedException ex)
    //        {
    //            throw new RepositoryException("ObjectDisposedException in NotificationTypeRepository-> GetBusinessEntity", ex);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new RepositoryException("Exception in NotificationTypeRepository-> GetContacts", ex);
    //        }
    //    }

    //    public NotificationTypeResponse SaveOrUpdateNotificationType(NotificationType notificationTypeRequest,long? userID)
    //    {
    //        try
    //        {
    //            if (notificationTypeRequest.ID > 0)
    //            {
    //                _Context.Entry(notificationTypeRequest).State = EntityState.Modified;
    //            }
    //            else
    //            {
    //                _Context.NotificationType.Add(notificationTypeRequest);
    //            }
    //            this._Context.SaveChanges(userID);
    //        }
    //        catch (SqlException ex)
    //        {
    //            throw new RepositoryException("SqlException in NotificationTypeRepository-> SaveOrUpdateBusinessEntity", ex);
    //        }
    //        catch (DbUpdateException ex)
    //        {
    //            throw new RepositoryException("DbUpdateException in NotificationTypeRepository-> SaveOrUpdateBusinessEntity", ex);
    //        }
    //        catch (ObjectDisposedException ex)
    //        {
    //            throw new RepositoryException("ObjectDisposedException in NotificationTypeRepository-> SaveOrUpdateBusinessEntity", ex);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new RepositoryException("Exception in NotificationTypeRepository-> SaveOrUpdateBusinessEntity", ex);
    //        }
    //    }
    //}
}
