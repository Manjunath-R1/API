namespace ThoughtFocus.Repository.Impl.Notification
{
    using System;
    using System.Collections.Generic; 
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Notification;
    using ThoughtFocus.Repository.Interfaces.Notification;

    public class EmailNotificationLogRepository : AbstractEFApplicationBaseRepository<EmailNotificationLog>, IEmailNotificationLogRepository
    {
        private ApplicationDBContext _Context;

        #region Constructor

        public EmailNotificationLogRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructor

        #region Methods

        public void SaveEmailNotificationLog(EmailNotificationLog emailNotificationLog,long? userID)
        {
            try
            {
                if (emailNotificationLog.EmailNotificationLogID > 0)
                    _Context.Entry(emailNotificationLog).State = EntityState.Modified;
                else
                    _Context.EmailNotificationLogs.Add(emailNotificationLog);

                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception at EmailNotificationLogRepository-> SaveEmailNotificationLog", ex);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Exception at EmailNotificationLogRepository-> SaveEmailNotificationLog", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception at EmailNotificationLogRepository-> SaveEmailNotificationLog", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at EmailNotificationLogRepository-> SaveEmailNotificationLog", ex);
            }
        }

       

        #endregion Methods
    }
}
