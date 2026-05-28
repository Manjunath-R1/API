using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DocumentManager.Interfaces;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.StorageService;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Request;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Validations.Impl.RuleHandler;
using ThoughtFocus.Validations.InputParameterValidation.Common;
using ThoughtFocus.Validations.ValidationModels;

namespace ThoughtFocus.DocumentManager.Impl
{
    public class DocFileUploader : IDocFileUploader
    {
        #region Fields
        private readonly ILogger<DocFileUploader> _logger;
        private IFileManager _fileManager;
        private IDocumentUploader _documentUploader;
        private IDocumentInformationProvider _documentInformationProvider;
        private IStorageService _storageProvider;
        
        #endregion

        #region Constructor
        public DocFileUploader(IDocumentUploader documentUploader,
                               IFileManager fileManager, 
                               IDocumentInformationProvider documentInformationProvider,
                               IStorageService storageProvider,
                               ILogger<DocFileUploader> logger)
        {
            _documentUploader = documentUploader;
            _fileManager = fileManager;
            _documentInformationProvider = documentInformationProvider;
            _storageProvider = storageProvider;
            _logger = logger;
        }
        #endregion Constructor

        public async Task<DocumentResponse> UploadDocument(Domain.CustomView.DocumentEntity documentEntity, Domain.CustomView.FileEntity fileEntity)
        {
            DocumentResponse uploadResponse = new DocumentResponse();
            try
            {
                #region Validation
                ValidationRuleModel validationRule = new ValidationRuleModel();

                #region FileEntityValidation

                FileEntityValidation fileEntityValidation = new FileEntityValidation(validationRule);
                string[] fileRuleset = { RuleSetEnumeration.FileEntityProperties.ToString() };
                FluentValidation.Results.ValidationResult validationModelError = RuleValidater.Validate(fileEntityValidation, fileEntity, fileRuleset);

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

                ThoughtFocus.DocumentRepository.Domain.DocumentUploadRequest documentUploadRequest = new ThoughtFocus.DocumentRepository.Domain.DocumentUploadRequest();
                documentUploadRequest.DocumentID = documentEntity.DocumentID;
                documentUploadRequest.ContentLength = fileEntity.ContentLength;
                documentUploadRequest.ContentType = fileEntity.ContentType;
                documentUploadRequest.FileContent = fileEntity.FileContent;
                documentUploadRequest.FileName = fileEntity.FileName;
                documentUploadRequest.FilePath = fileEntity.FilePath;
                documentUploadRequest.InputStream = fileEntity.InputStream;
                documentUploadRequest.TargetFilePath = fileEntity.TargetFilePath;
                documentUploadRequest.ProjectID = documentEntity.ProjectID;
                documentUploadRequest.UserID = documentEntity.UserID;
                documentUploadRequest.ImpersonatedUser = documentEntity.UserID; 
                documentUploadRequest.FileSize = fileEntity.ContentLength;
                documentUploadRequest.Overwrite = fileEntity.Overwrite;

                if (documentEntity.DocumentID == Guid.Empty)
                {
                    documentUploadRequest.Number = this._fileManager.GetNextDocumentNumber();
                }

                ThoughtFocus.DocumentRepository.Domain.DocumentEntity currentVersion = _documentInformationProvider.GetDocumentByNameAndProjectID(documentUploadRequest.FileName, documentUploadRequest.ProjectID);
                if (currentVersion != null)
                    documentUploadRequest.DocumentID = currentVersion.DocumentID;

                var documentUploadResponse = await _documentUploader.UploadDocument(documentUploadRequest);

                if (documentUploadResponse.IsSuccess)
                {
                    uploadResponse.Message = documentUploadResponse.Message;
                    uploadResponse.IsSuccess = documentUploadResponse.IsSuccess;
                    uploadResponse.Document = new Domain.DocumentViewModel { DocumentID = documentEntity.DocumentID };
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

        public DocumentResponse DeleteDocument(DocumentRequest documentRequest)
        {
            var documentResponse = new DocumentResponse();
            try
            {
                #region Validation
                ValidationRuleModel validationRule = new ValidationRuleModel();
                #endregion

                var request = new DocumentRepository.Domain.DocumentRequest { Id = documentRequest.Id, ParentId = documentRequest.ParentId, UserID = documentRequest.UserID };

                var response = _documentUploader.DeleteDocumentAsync(request);
                var uploaderResponse = response;

                return new DocumentResponse
                {
                    Document = new Domain.DocumentViewModel { DocumentID = uploaderResponse.ID },
                    IsSuccess = uploaderResponse.IsSuccess,
                    Message = uploaderResponse.Message
                };
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in DeleteDocument", ex);
            }
            catch (TargetInvocationException ex)
            {
                throw new BusinessException("BusinessException in DeleteDocument", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentResponse RenameDocument(DocumentRequest documentRequest)
        {
            var documentResponse = new DocumentResponse();
            try
            {
                #region Validation
                ValidationRuleModel validationRule = new ValidationRuleModel();
                #endregion

                var request = new DocumentRepository.Domain.DocumentRequest { Name = documentRequest.NewName.Split('/').Last(), Id = documentRequest.Id, ParentId = documentRequest.ParentId, UserID = documentRequest.UserID };

                var response = _documentUploader.RenameDocument(request);
                var uploaderResponse = response;

                return new DocumentResponse
                {
                    Document = new Domain.DocumentViewModel { DocumentID = uploaderResponse.ID },
                    IsSuccess = uploaderResponse.IsSuccess,
                    Message = uploaderResponse.Message
                };
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in RenameDocument", ex);
            }
            catch (TargetInvocationException ex)
            {
                throw new BusinessException("BusinessException in RenameDocument", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentResponse> DownloadDocument(string storageKey, UserSessionEntity userSessionEntity)
        {
            var documentResponse = new DocumentResponse();
            try
            {
                #region Validation
                ValidationRuleModel validationRule = new ValidationRuleModel();
                #endregion

                var request = new DocumentRepository.Domain.DocumentRequest { PhysicalPath = storageKey};

                // var uploaderResponseTask = await _documentUploader.DownloadDocumentAsync(request);

                var uploaderResponseTask = await _storageProvider.DownloadDocumentAsync(request);

                return new DocumentResponse
                {
                    Document = new Domain.DocumentViewModel
                    {
                         DocumentID = uploaderResponseTask.DocumentID,
                        DocumentKey = uploaderResponseTask.DocumentID,
                        FileName = uploaderResponseTask.Name,
                        FilePath = uploaderResponseTask.PhysicalPath,
                        FileContent = uploaderResponseTask.Content,
                        FileStream = uploaderResponseTask.OutputStream
                    },
                    IsSuccess = uploaderResponseTask.IsSuccess,
                    Message = uploaderResponseTask.Message
                };
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in DownloadDocument", ex);
            }
            catch (TargetInvocationException ex)
            {
                throw new BusinessException("BusinessException in DownloadDocument", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DocumentResponse DownloadDocumentVersionID(Guid documentVersionId, UserSessionEntity userSessionEntity)
        {
            var documentResponse = new DocumentResponse();
            try
            {
                #region Validation
                ValidationRuleModel validationRule = new ValidationRuleModel();
                #endregion

                var request = new DocumentRepository.Domain.DocumentRequest { Id = documentVersionId, UserID = userSessionEntity.UserID };

                var uploaderResponseTask = _documentUploader.DownloadDocumentVersionAsync(request);
                var uploaderResponse = uploaderResponseTask.Result;

                return new DocumentResponse
                {
                    Document = new Domain.DocumentViewModel
                    {
                        DocumentID = uploaderResponse.DocumentID,
                        DocumentKey = uploaderResponse.DocumentID,
                        FileName = uploaderResponse.Name,
                        FilePath = uploaderResponse.PhysicalPath,
                        FileContent = uploaderResponse.Content,
                        FileStream = uploaderResponse.OutputStream
                    },
                    IsSuccess = uploaderResponse.IsSuccess,
                    Message = uploaderResponse.Message
                };
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in DownloadDocument", ex);
            }
            catch (TargetInvocationException ex)
            {
                throw new BusinessException("BusinessException in DownloadDocument", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long GetMaxFileSizeLimit()
        {

            try
            {
                return _documentUploader.GetMaxFileSizeLimit();

            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
