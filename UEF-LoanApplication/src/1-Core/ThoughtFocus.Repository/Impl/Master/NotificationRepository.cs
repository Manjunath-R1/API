 
using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Repository; 
using Microsoft.EntityFrameworkCore;
using ThoughtFocusRepository.Interfaces.Master;

namespace ThoughtFocus.Repository.Impl.Master
{
    public class NotificationRepository : AbstractEFApplicationBaseRepository<ThoughtFocus.DataAccess.Models.Master.Notification>, INotificationRepository
    {
        #region Fields

        private ApplicationDBContext _context; 
        #endregion Fields

        #region Constructor

        public NotificationRepository(ApplicationDBContext context)
            : base(context)
        {
            this._context = context;
        }

        #endregion Constructor

        #region Methods
        public List<ThoughtFocus.DataAccess.Models.Master.Notification> GetAllNotificationTypes()
        {
            try
            {
                return this.GetAll().Where(a => a.IsActive == true).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from GetAllNotificationTypes table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from GetAllNotificationTypes table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from GetAllNotificationTypes table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from GetAllNotificationTypes table from repository", ex);
            }
        }

        public IQueryable<ThoughtFocus.DataAccess.Models.Master.Notification> GetAllEmailTemplates()
        {
            try
            {
                return this.GetAll();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from Notification table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from Notification table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from Notification table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from Notification table from repository", ex);
            }
        }

        public void SaveOrUpdate(ThoughtFocus.DataAccess.Models.Master.Notification notification, long? userID)
        {
            try
            {
                if (notification.NotificationID > 0)
                    _context.Entry(notification).State = EntityState.Modified;
                else
                    _context.Notifications.Add(notification);

                this._context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);

            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in SaveOrUpdate-> GetContactsByID", ex);
                
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to SaveOrUpdate at repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }


        public List<ThoughtFocus.DataAccess.Models.Master.Notification> GetNotificationForActivity(long ActivityID, long WorkflowNotificationType)
            {
            try
            {
                var activityNotification = (from AN in this._context.ActivityNotifications
                                            join N in this._context.Notifications on AN.NotificationID equals N.NotificationID
                                            where AN.ActivityID == ActivityID && N.IsActive== true && AN.WorkflowNotificationTypeID == WorkflowNotificationType
                                            select new
                                            {
                                                NotificationID = N.NotificationID,
                                                NotificationType = N.NotificationType,
                                                MessageSubject = N.MessageSubject,
                                                NotificationTypeDescription = N.NotificationTypeDescription,
                                                IsActive = AN.IsActive,
                                            }).ToList()
                                   .Select(NA => new ThoughtFocus.DataAccess.Models.Master.Notification()
                                   {
                                       NotificationID = NA.NotificationID,
                                       NotificationType = NA.NotificationType,
                                       MessageSubject = NA.MessageSubject,
                                       NotificationTypeDescription = NA.NotificationTypeDescription,
                                       IsActive = NA.IsActive,
                                   }).ToList();


                return activityNotification;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }

        }


        public List<ThoughtFocus.DataAccess.Models.Master.Notification> GetNotificationForOther(long WorkflowNotificationType)
        {
            try
            {
                var otherNotification = (from AN in this._context.ActivityNotifications
                                         join N in this._context.Notifications on AN.NotificationID equals N.NotificationID
                                         where N.IsActive == true && AN.ActivityID == 0 && AN.WorkflowNotificationTypeID == WorkflowNotificationType
                                         select new
                                         {
                                             NotificationID = N.NotificationID,
                                             NotificationType = N.NotificationType,
                                             MessageSubject = N.MessageSubject,
                                             NotificationTypeDescription = N.NotificationTypeDescription,
                                             IsActive = N.IsActive,
                                         }).ToList()
                                   .Select(NA => new ThoughtFocus.DataAccess.Models.Master.Notification()
                                   {
                                       NotificationID = NA.NotificationID,
                                       NotificationType = NA.NotificationType,
                                       MessageSubject = NA.MessageSubject,
                                       NotificationTypeDescription = NA.NotificationTypeDescription,
                                       IsActive = NA.IsActive,
                                   }).ToList();


                return otherNotification;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForOther from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForOther from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForOther from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForOther from repository", ex);
            }

        }


        public ThoughtFocus.DataAccess.Models.Master.Notification GetEmailTemplateByActivityID(long? emailTemplateID, long? activityID)
        {
            try
            {
                var NotificationByActivityID = (from AN in this._context.ActivityNotifications
                                                join N in this._context.Notifications on AN.NotificationID equals N.NotificationID
                                                where N.NotificationID == emailTemplateID && AN.ActivityID == activityID
                                                select new
                                                {
                                                    NotificationID = N.NotificationID,
                                                    NotificationType = N.NotificationType,
                                                    MessageSubject = N.MessageSubject,
                                                    NotificationTypeDescription = N.NotificationTypeDescription,
                                                    Head = N.Head,
                                                    Salutation = N.Salutation,
                                                    Body = N.Body,
                                                    Footer = N.Footer,
                                                    IsActive = AN.IsActive,
                                                    TemplateName = N.TemplateName,
                                                    RecipientType=N.RecipientType
                                                }).ToList()
                                   .Select(NA => new ThoughtFocus.DataAccess.Models.Master.Notification()
                                   {
                                       NotificationID = NA.NotificationID,
                                       NotificationType = NA.NotificationType,
                                       MessageSubject = NA.MessageSubject,
                                       NotificationTypeDescription = NA.NotificationTypeDescription,
                                       Head = NA.Head,
                                       Salutation = NA.Salutation,
                                       Body = NA.Body,
                                       Footer = NA.Footer,
                                       IsActive = NA.IsActive,
                                       TemplateName = NA.TemplateName,
                                       RecipientType = NA.RecipientType == 0 ? 1 : NA.RecipientType
                                   }).FirstOrDefault();


                return NotificationByActivityID;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationForActivity from repository", ex);
            }

        }


        public ThoughtFocus.DataAccess.Models.Master.Notification GetNotificationByNotificationType(long NotificationID)
        {
            try
            { 
                return _context.Notifications.Where(a => a.NotificationID == NotificationID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationByNotificationType from repository", ex);

            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationByNotificationType from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationByNotificationType from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from GetNotificationByNotificationType from repository", ex);
            }
        }         


        #endregion Methods

    }
}
