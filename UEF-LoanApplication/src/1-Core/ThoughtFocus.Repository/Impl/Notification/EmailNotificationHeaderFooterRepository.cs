 
using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ThoughtFocus.Repository.Impl.Notification
{
    public class EmailNotificationHeaderFooterRepository : AbstractEFApplicationBaseRepository<EmailTemplateHeaderFooter>, IEmailNotificationHeaderFooterRepository
    {
        private ApplicationDBContext _Context;
       
        #region Constructor

        public EmailNotificationHeaderFooterRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructor

        #region Methods

        public List<EmailTemplateHeaderFooter> GetEmailTemplateHeaderFooter()
        {
            try
            {
                return this.FindBy(a=>a.IsActive==true).ToList();

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }


        }

        

        public EmailTemplateHeaderFooter GetEmailTemplateHeaderFooterByID(long EmailTemplateHeaderFooterByID)
        {
            try
            {
                return this._Context.EmailNotificationHeaderFooter.Where(e => e.EmailTemplateHeaderFooterID == EmailTemplateHeaderFooterByID).FirstOrDefault();
              
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }


        }

        public void MakeEmailTemplateHeaderFooterInactive(EmailTemplateHeaderFooter emailNotificationHeaderFooter)
        {
            try
            {
                if (emailNotificationHeaderFooter.EmailTemplateHeaderFooterID > 0)
                {
                    emailNotificationHeaderFooter.IsActive = false;
                    this._Context.Entry(emailNotificationHeaderFooter).State = EntityState.Modified;
                    _Context.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }

        }

        public void SaveEmailTemplateHeaderFooter(EmailTemplateHeaderFooter emailNotificationHeaderFooter)
        {
            try
            {
                if (emailNotificationHeaderFooter.EmailTemplateHeaderFooterID > 0)
                {
                    this._Context.Entry(emailNotificationHeaderFooter).State = EntityState.Modified;
                }
                else
                {
                    this._Context.EmailNotificationHeaderFooter.Add(emailNotificationHeaderFooter);
                }
                _Context.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }

        }

        public EmailTemplateHeaderFooter GetEmailTemplateFooter()
        {
            try
            {
                return this._Context.EmailNotificationHeaderFooter.Where(e => e.IsActive == true).OrderByDescending(e => e.EmailTemplateHeaderFooterID).FirstOrDefault();
                
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }


        }

        public bool AddorUpdateFooter(EmailTemplateHeaderFooter emailNotificationHeaderFooter)
        {
            try
            {
               if (emailNotificationHeaderFooter.EmailTemplateHeaderFooterID > 0)
                {
                    this._Context.Entry(emailNotificationHeaderFooter).State = EntityState.Modified;
                }
               else
                {
                   this._Context.EmailNotificationHeaderFooter.Add(emailNotificationHeaderFooter);
                }
               _Context.SaveChanges();
                return true;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Failed to get data from EmailNotificationHeaderFooter table from repository", ex);
            }

        }

        #endregion Methods
    }
}
