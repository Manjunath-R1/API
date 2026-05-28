using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using ThoughtFocus.DocumentManager.Interfaces;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Repository.Interfaces;
using ThoughtFocus.Validations.Impl.RuleHandler;
using ThoughtFocus.Validations.InputParameterValidation.Common;
using ThoughtFocus.Validations.ValidationModels;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.DocumentRepository.StorageService;
using System.Data.SqlClient;

namespace ThoughtFocus.DocumentManager.Impl
{
    public class DocumentUploadManager : IDocumentUploadManager
    {
        #region Fields
        private readonly ILogger<DocumentUploadManager> _logger;
        private IDocumentVisitor _documentVisitor;
        private IApplicationDocumentRepository _applicationDocumentRepository;
        private IFileManager _fileManager;
        private IDocumentUploader _documentUploader;
        private IDocumentInformationProvider _documentInformationProvider;
        private IStorageService _storageProvider;
        
        #endregion

        #region Constructor
        public DocumentUploadManager(IDocumentVisitor documentVisitor, IApplicationDocumentRepository applicationDocumentRepository, 
        IDocumentUploader documentUploader, 
        IFileManager fileManager, 
        IDocumentInformationProvider documentInformationProvider,
        IStorageService storageProvider,
        ILogger<DocumentUploadManager> logger)
        {
            _documentVisitor = documentVisitor;
            _applicationDocumentRepository = applicationDocumentRepository;
            _documentUploader = documentUploader;
            _fileManager = fileManager;
            _documentInformationProvider = documentInformationProvider;
            _storageProvider = storageProvider;
            _logger = logger;
        }
        #endregion Constructor

        public async Task<DocumentResponse> UploadDocument(Domain.CustomView.DocumentEntity documentEntity)
        {
            var documentUploadResponse = new DocumentRepository.Domain.DocumentUploadResponse();
            var documentUploadRequest = new DocumentRepository.Domain.DocumentUploadRequest();
            DocumentResponse uploadResponse = new DocumentResponse();

            try
            {
                #region Validation
                ValidationRuleModel validationRule = new ValidationRuleModel();

                #region FileEntityValidation

                 FileEntityValidation fileEntityValidation = new FileEntityValidation(validationRule);
                 string[] fileRuleset = { RuleSetEnumeration.FileEntityProperties.ToString() };
                 FluentValidation.Results.ValidationResult validationModelError = RuleValidater.Validate(fileEntityValidation, documentEntity.FileEntity, fileRuleset);

                 if (!validationModelError.IsValid)
                 {
                     foreach (FluentValidation.Results.ValidationFailure rslr in validationModelError.Errors)
                     {
                         _logger.LogError(String.Format("Exception:-{0}", rslr));
                     }
                     uploadResponse.IsSuccess = false;
                     uploadResponse.Message = "Unable to upload document. Please try after sometime.";
                     return uploadResponse;
                 }
                #endregion

                #endregion

                #region FillInputParameter

                documentUploadRequest.DocumentID = Guid.NewGuid();
                documentUploadRequest.ContentLength = documentEntity.FileEntity.ContentLength;
                documentUploadRequest.ContentType = documentEntity.FileEntity.ContentType;
                documentUploadRequest.FileContent = documentEntity.FileEntity.FileContent;
                documentUploadRequest.FileName = documentEntity.FileEntity.FileName;
                documentUploadRequest.FilePath = documentEntity.FileEntity.FilePath;
                documentUploadRequest.InputStream = documentEntity.FileEntity.InputStream;
                documentUploadRequest.Key = GetStorageKey(documentUploadRequest);
             
                #endregion

                documentUploadResponse = await this._storageProvider.UploadFileAsync(documentUploadRequest);

                if (documentUploadResponse.IsSuccess)
                {
                    uploadResponse.DocumentID = documentUploadRequest.DocumentID;
                    uploadResponse.DocumentTypeID = documentEntity.DocumentTypeID;
                    uploadResponse.FileName = documentEntity.FileEntity.FileName;
                    uploadResponse.StorageKey = documentUploadRequest.Key;
                    uploadResponse.Message = documentUploadResponse.Message;
                    uploadResponse.FileSource =documentUploadResponse.FileSource+"/"+documentUploadRequest.Key;
                    uploadResponse.IsSuccess = true;
                }
                else
                {
                    uploadResponse.Message = documentUploadResponse.Message;
                    uploadResponse.IsSuccess = false;
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("", ex);
            }
            catch (TargetInvocationException ex)
            {
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return uploadResponse;
        }
        private string GetStorageKey(DocumentRepository.Domain.DocumentUploadRequest documentUploadRequest)
        {
            string storageKey = String.Empty;
            try
            {
                string cleanedFileName = documentUploadRequest.FileName.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                storageKey = String.Format("{0}_{1}", documentUploadRequest.DocumentID, cleanedFileName);
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching StorageKey {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching StorageKey {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching StorageKey {0}", ex));
                throw new BusinessException("", ex);
            }
            return storageKey;
        }
    }
}
