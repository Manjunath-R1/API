using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.Repository.Interfaces.SMS;

namespace ThoughtFocus.Repository.Impl.SMS
{

    public class SMSNotificationTemplateRepository : AbstractEFApplicationBaseRepository<SMSNotificationTemplate>, ISMSNotificationTemplateRepository
    {
        #region Fields

        private ApplicationDBContext _context;
        private readonly ILogger<SMSNotificationTemplateRepository> _logger;

        #endregion Fields

        #region Constructors

        public SMSNotificationTemplateRepository(ApplicationDBContext context, ILogger<SMSNotificationTemplateRepository> logger)
            : base(context)
        {
            this._context = context;
            this._logger = logger;
        }

        #endregion Constructors

        #region Methods

        public SMSNotificationTemplate GetSMSNotificationTemplates(long applicationStatusID)
        {
           var smsNotificationTemplate = GetAll().Where(x => x.ApplicationStatusID == applicationStatusID && x.IsActive == true).FirstOrDefault();
            try
            {

                if (smsNotificationTemplate == null)
                {
                    _logger.LogDebug(String.Format("There is no SMS Template assigned for applicationStatusID{0}", applicationStatusID));
                }


            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception at SMSNotificationTemplateRepository-> GetSMSNotificationTemplates", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception at SMSNotificationTemplateRepository-> GetSMSNotificationTemplates", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception at SMSNotificationTemplateRepository-> GetRActivityPlaceholdersByNotificationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at SMSNotificationTemplateRepository-> GetRActivityPlaceholdersByNotificationID", ex);
            }
            return smsNotificationTemplate;
        }
        public void SaveSMSNotificationLog(SMSNotificationLog sMSNotificationLog)
        {
            try
            {
                if (sMSNotificationLog.ID > 0)//need to change the id 
                {
                    this._context.Entry(sMSNotificationLog).State = EntityState.Modified;
                }
                else
                {
                   this._context.SMSNotificationLog.Add(sMSNotificationLog);
                }
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from SMSNotificationTemplateRepository table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from SMSNotificationTemplateRepository table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from SMSNotificationTemplateRepository table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }

        }


        #endregion Methods
    }
}
