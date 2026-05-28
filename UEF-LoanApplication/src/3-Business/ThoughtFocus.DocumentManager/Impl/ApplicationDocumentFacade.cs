using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentManager.Interfaces;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentManager.Impl
{
    public class ApplicationDocumentFacade : IApplicationDocumentFacade
    {
        #region Fields
        private readonly ILogger<ApplicationDocumentFacade> _logger;
        private IApplicationDocumentRepository _applicationDocumentRepository;
        private IDocumentDownloader _documentDownloader;

        #endregion

        #region Constructor

        public ApplicationDocumentFacade(IFileManager fileManager, 
            IApplicationDocumentRepository applicationDocumentRepository, 
            IDocumentUploader documentUploader, 
            IDocumentDownloader documentDownloader,
            ILogger<ApplicationDocumentFacade> logger)
        {
            _applicationDocumentRepository = applicationDocumentRepository;
            _documentDownloader = documentDownloader;
            _logger = logger;
        }

        #endregion

        #region Methods
        
        public ApplicationDocumentResponse GetApplicationDocumentByVersionID(string documentVersionkey, UserSessionEntity userSessionEntity)
        {
            ApplicationDocumentResponse applicationDocumentResponse = new ApplicationDocumentResponse();
            if (String.IsNullOrEmpty(documentVersionkey))
            {
                _logger.LogError("DocumentVersionKey is Empty");
                applicationDocumentResponse.IsSuccess = false;
                applicationDocumentResponse.Message = "Unable to download document. Please try after sometime.";
                return applicationDocumentResponse;
            }
            try
            {

                DocumentDownloadResponse documentDownloadResponse = this._documentDownloader.DownloadDocument(documentVersionkey, userSessionEntity.UserID);
                if (!documentDownloadResponse.IsSuccess)
                {
                    applicationDocumentResponse.IsSuccess = false;
                    applicationDocumentResponse.Message = documentDownloadResponse.Message;
                    return applicationDocumentResponse;
                }
                applicationDocumentResponse.FileEntity = new FileEntity();
                applicationDocumentResponse.FileEntity.FileName = documentDownloadResponse.FileName;
                applicationDocumentResponse.FileEntity.InputStream = documentDownloadResponse.FileStream;
                applicationDocumentResponse.IsSuccess = true;

            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("", ex);

            }
            catch (DocumentRepositoryException ex)
            {
                throw new BusinessException("", ex);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return applicationDocumentResponse;
        }

        public string GetValidationRuleName(long documentTypeID)
        {
            string ruleSetName = string.Empty;
            // if (documentTypeID != (int)DocumentTypeEnumeration.MemberReport && documentTypeID != (int)DocumentTypeEnumeration.ChairReport)
            // {
            //     ruleSetName = RuleSetEnumeration.SiteVisitDocumentEntityProperties.ToString();
            // }
            // else if (documentTypeID == (int)DocumentTypeEnumeration.MemberReport || documentTypeID == (int)DocumentTypeEnumeration.ChairReport)
            // {
            //     ruleSetName = RuleSetEnumeration.SiteVisitMemberDocumentEntityProperties.ToString();
            // }
            return ruleSetName;
        }

        public DocumentResponse GetDocumentIDByTypeAndApplication(long documentTypeID, long applicationID)
        {
            DocumentResponse baseResponse = new DocumentResponse();
            DataAccess.Models.ApplicationDocument applicationDocument = null;
            try
            {
                applicationDocument = this._applicationDocumentRepository.FirstOrDefault(a => a.IsActive == true && a.DocumentTypeID == documentTypeID && a.LoanApplicationID == applicationID);
                if (applicationDocument == null)
                {
                    baseResponse.IsSuccess = true;
                    baseResponse.ID = 0;
                }
                else
                {
                    baseResponse.IsSuccess = true;
                    baseResponse.Document = new DocumentViewModel { DocumentID = applicationDocument.DocumentGUID };
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return baseResponse;
        }

        #endregion

    }
}
