using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.DataAccess;

namespace ThoughtFocus.Repository.Impl.Application
{
    public class ApplicationDocumentRepository : AbstractEFApplicationBaseRepository<ApplicationDocument>, IApplicationDocumentRepository
    {
        private ApplicationDBContext _context;
        private readonly ILogger<ApplicationDocumentRepository> _logger;
  

        #region Constructors
        
        public ApplicationDocumentRepository(ApplicationDBContext context,ILogger<ApplicationDocumentRepository> logger): base(context)
        {
            this._context = context;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods


        public List<ApplicationDocument> GetApplicationDocuments(long applicationID)
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true && a.LoanApplicationID == applicationID).ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in ApplicationDocumentRepository-> GetApplicationDocuments",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in ApplicationDocumentRepository-> GetApplicationDocuments",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in ApplicationDocumentRepository-> GetApplicationDocuments",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ApplicationDocumentRepository-> GetApplicationDocuments",ex);
            }
        }

        public List<ApplicationDocument> GetApplicationDocumentsByApplicationIDAndDocumentCatergoryID(long applicationID, int documentCategoryID)
        {
            try
            {
                var result = this._context.ApplicationDocuments.Where(a => a.LoanApplicationID == applicationID && a.IsActive == true).ToList();
                if(result==null)
                {
                    _logger.LogError(String.Format("There is no matching document for ApplicationID {0} and CategoryID {1}", applicationID, documentCategoryID));
                    return null;
                }
                return result.Where(a => a.DocumentType.DocumentCategoryID == documentCategoryID).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        public ApplicationDocument GetApplicationDocumentByApplicationIDAndDocumentTypeID(long applicationID, int documentTypeID)
        {
            try
            {
                return this._context.ApplicationDocuments.Where(a => a.LoanApplicationID == applicationID && a.IsActive == true && a.DocumentTypeID == documentTypeID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
           

        }

        public void SaveApplicationDocument(DataAccess.Models.ApplicationDocument applicationDocument)
        {
            try
            {
                if (applicationDocument.ApplicationDocumentID == 0)
                {
                    this._context.ApplicationDocuments.Add(applicationDocument);
                }
                else
                {
                    this._context.Entry(applicationDocument).State = EntityState.Modified;
                }
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
           
        }

        public ApplicationDocument GetApplicationDocumentByApplicationIDAndDocumentTypeIDAndName(long applicationID, int documentTypeID, string name)
        {
            try
            {
                return this._context.ApplicationDocuments.Where(a => a.LoanApplicationID == applicationID && a.IsActive == true && a.DocumentTypeID == documentTypeID && a.FileName == name).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
            
        }

        public void SaveApplicationDocumentRequest(ApplicationDocumentRequestLog documentRequestLog)
        {
            try
            {

                if (documentRequestLog.RequestID > 0)
                    _context.Entry(documentRequestLog).State = EntityState.Modified;
                else
                    _context.ApplicationDocumentRequestLogs.Add(documentRequestLog);

                this._context.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogError("Error encountered at SaveApplicationDocumentRequest(ApplicationDocumentRequestLog documentRequestLog)>>", ex);
                throw new RepositoryException("");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error encountered at SaveApplicationDocumentRequest(ApplicationDocumentRequestLog documentRequestLog)>>", ex);
                throw new RepositoryException("");
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError("Error encountered at SaveApplicationDocumentRequest(ApplicationDocumentRequestLog documentRequestLog)>>", ex);
                throw new RepositoryException("");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at SaveApplicationDocumentRequest(ApplicationDocumentRequestLog documentRequestLog)>>", ex);
                throw new RepositoryException("");
            }
        }
        #endregion
    }
}