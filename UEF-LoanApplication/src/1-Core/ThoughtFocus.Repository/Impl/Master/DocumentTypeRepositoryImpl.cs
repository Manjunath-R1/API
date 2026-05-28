using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Repository;
using Microsoft.EntityFrameworkCore;

namespace ThoughtFocus.Repository.Impl.Master
{
    public class DocumentTypeRepositoryImpl : AbstractEFApplicationBaseRepository<DocumentType>, IDocumentTypeRepository
    {
        private ApplicationDBContext context;
        private readonly ILogger<DocumentTypeRepositoryImpl> _logger;
        #region Constructors

        public DocumentTypeRepositoryImpl(ApplicationDBContext context, ILogger<DocumentTypeRepositoryImpl> logger) : base(context)
        {
            this.context = context;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        public List<DocumentType> GetDocumentTypeByCategoryID(int documentCategoryID)
        {
            try
            {
                return context.DocumentTypes.Where(a => a.DocumentCategoryID == documentCategoryID && a.IsActive == true).OrderBy(a => a.DisplayOrder).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByCategoryID(int documentCategoryID) >>", ex);
                throw new RepositoryException("");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByCategoryID(int documentCategoryID) >>", ex);
                throw new RepositoryException("");
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByCategoryID(int documentCategoryID) >>", ex);
                throw new RepositoryException("");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByCategoryID(int documentCategoryID) >>", ex);
                throw new RepositoryException("");
            }
           
            
        }

        public DocumentType GetDocumentTypeByID(int documentTypeID)
        {
            try
            {
                return context.DocumentTypes.Where(a => a.DocumentTypeID == documentTypeID && a.IsActive == true).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByID(int documentTypeID) >>", ex);
                throw new RepositoryException("");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByID(int documentTypeID) >>", ex);
                throw new RepositoryException("");
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByID(int documentTypeID) >>", ex);
                throw new RepositoryException("");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at  GetDocumentTypeByID(int documentTypeID) >>", ex);
                throw new RepositoryException("");
            }
           
            
        }
         public void SaveOrUpdateDocumentTypes(DocumentType documentInfo, long? userID)
        {
            try
            {
                if (documentInfo.DocumentTypeID > 0)
                {
                    context.Entry(documentInfo).State = EntityState.Modified;
                }
                else
                    context.DocumentTypes.Add(documentInfo);

                this.context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in DocumentTypeRepositoryImpl-> SaveOrUpdateDocumentTypes", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in DocumentTypeRepositoryImpl-> SaveOrUpdateDocumentTypes", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in DocumentTypeRepositoryImpl-> SaveOrUpdateDocumentTypes", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in DocumentTypeRepositoryImpl-> SaveOrUpdateDocumentTypes", ex);
            }
        }

        public DocumentType GetDocumentName(string name)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.Name == name && x.IsActive==true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);

            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);
            }
        }

        #endregion


    }
}
