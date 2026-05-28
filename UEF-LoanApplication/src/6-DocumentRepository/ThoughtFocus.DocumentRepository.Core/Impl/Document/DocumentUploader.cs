using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.Common.Exceptions.BusinessException;
using System.Configuration;
using ThoughtFocus.DocumentRepository.StorageService;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{

    public class DocumentUploader : IDocumentUploader
    {
        private readonly ILogger<DocumentUploader> _logger;
        private IFileExtensionTypeRepository _fileExtensionTypeRepository;
        private IDocumentRepository _documentRepository;
        private IStorageService _storageProvider;
        private IDocumentInformationProvider _documentInformationProvider;
        private IVersionManager _versionManager;
        private IAuthorizer _authorizer;
        private IActionLogger _actionLogger;
        private IDocumentVersionHistoryRepository _documentVersionHistoryRepository;
        private IUserRepository _userProvider;
        private IAccessRoleRepository _accessRoleRepository;
        private RepositoryUser repositoryUser = null;
        //private IDocumentEditorManager _documentEditorManager;

        public DocumentUploader(IFileExtensionTypeRepository fileExtensionTypeRepository, 
        IDocumentRepository documentRepository, IDocumentInformationProvider documentInformationProvider, 
        IVersionManager versionManager, IAuthorizer authorizer, IActionLogger actionLogger, 
        IDocumentVersionHistoryRepository documentVersionHistoryRepository, 
        IUserRepository userProvider, IAccessRoleRepository accessRoleRepository,
        IStorageService storageProvider,
        ILogger<DocumentUploader> logger)
        {
            _fileExtensionTypeRepository = fileExtensionTypeRepository;
            _documentRepository = documentRepository;
            _storageProvider = storageProvider;
            _documentInformationProvider = documentInformationProvider;
            _versionManager = versionManager;
            _authorizer = authorizer;
            _actionLogger = actionLogger;
            _documentVersionHistoryRepository = documentVersionHistoryRepository;
            _userProvider = userProvider;
            _accessRoleRepository = accessRoleRepository;
            _logger = logger;
        }

        public const long defaultMaxFileSizeLimit = 52428800;

        public long GetMaxFileSizeLimit()
        {
            long maxFileSizeLimit = 0;
            try
            {
                maxFileSizeLimit = ConfigurationManager.AppSettings["FileSizeMaxLimit"] != null ? Convert.ToInt64(ConfigurationManager.AppSettings["FileSizeMaxLimit"]) : defaultMaxFileSizeLimit;
                return maxFileSizeLimit;
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

        public async Task<DocumentUploadResponse> UploadDocument(DocumentUploadRequest documentUploadRequest)
        {
            DocumentUploadResponse documentUploadResponse = new DocumentUploadResponse();
            DocumentRequest documentRequest = new DocumentRequest();
            DateTime currentDateTime = DateTime.Now;
            string extensionValue = Path.GetExtension(documentUploadRequest.FileName).ToLower();
            try
            {

                #region DocumentExtensionValidation
                ValidationResponse extensionValidationResponse = this._documentInformationProvider.ValidateFileExtension(documentUploadRequest.FileName);
                if (!(extensionValidationResponse.IsSuccess && extensionValidationResponse.IsValid))
                {
                    documentUploadResponse.IsSuccess = false;
                    documentUploadResponse.Message = "File Type is not supported";
                    return documentUploadResponse;

                }
                #endregion

                
                Document document = null;
                if (documentUploadRequest.DocumentID != Guid.Empty){
                    document = this._documentRepository.GetDocumentById(documentUploadRequest.DocumentID);
                }
                else
                {
                    document = new Document();
                    Project project = new Project();
                    document.Key = Guid.NewGuid();
                }

                document.StorageKey = this._documentInformationProvider.GetStorageKey(documentUploadRequest, document);
                documentRequest.ParentId = documentUploadRequest.ProjectID;
                documentUploadRequest.Key = this._documentInformationProvider.GetDocumentPhysicalPath(documentRequest, document.StorageKey);
                document.LastModifiedByUserID = documentUploadRequest.UserID;
                document.LastModifiedDateTime = currentDateTime;
                document.Path = document.StorageKey;
                document.Name = documentUploadRequest.FileName;
                document.FileExtensionTypeID = _fileExtensionTypeRepository.GetFileExtensionByValue(extensionValue).FileExtensionTypeID;
                document.FileSize = documentUploadRequest.ContentLength;
                document.LastUploadedDate = currentDateTime;
                document.Projects = null;

                documentUploadResponse = await this._storageProvider.UploadFileAsync(documentUploadRequest);

                if (documentUploadResponse.IsSuccess)
                {
                    //For new document
                    if (document.DocumentID == Guid.Empty)
                    {

                        document.DocumentID = document.Key;    //DocumentID and Key should be same
                        document.CreatedDateTime = currentDateTime;
                        document.CreatedByUserID = documentUploadRequest.UserID;
                        document.IsActive = true;
                        document.IsLocked = false;
                        document.IsInherit = true;
                        documentUploadResponse.Message = "Document uploaded successfully";
                    }
                    else //For existing document
                    {

                        documentUploadResponse.Message = "Document replaced successfully";
                        if (document.IsLocked == true && documentUploadRequest.UserID == document.LockedByUserID)
                        {
                            document.IsLocked = false;
                            document.LockedByUserID = null;
                        }
                    }
                    //Save document 
                    this._documentRepository.SaveDocument(document);

                    document.Projects = new List<Project> { new Project { ProjectID = documentUploadRequest.ProjectID } };
                   
                    //Project and document relationship
                    this._documentRepository.SaveDocumentProjectMapping(document);

                    documentUploadResponse.ParentFolderName = document.Projects.First().StorageKey;
                    documentUploadResponse.IsSuccess = true;
                    documentUploadResponse.DocumentID = document.DocumentID;
                    documentUploadResponse.StorageKey = document.StorageKey;
                    documentUploadResponse.FileSource =documentUploadResponse.FileSource+"/"+documentUploadRequest.Key;

                    
                    //Activity log added with Uploaded document
                    ActivityLog activityLog = new ActivityLog();
                    repositoryUser = _userProvider.GetUser(documentUploadRequest.UserID);
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.AddedContent.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    activityLog.KeyValue = document.DocumentID.ToString();
                    this._actionLogger.LogUserActivity(activityLog);
                }
                else
                {
                    documentUploadResponse.IsSuccess = false;
                    _logger.LogError(String.Format("Upload to storage  wasn't successful for document request {0}", documentUploadRequest));
                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentUploadResponse;
        }

        private double ConvertFileSize(long fileSize)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            long bytes = Math.Abs(fileSize);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return num;
        }

 

        public DocumentResponse RenameDocument(DocumentRequest documentRequest)
        {
            var documentResponse = new DocumentResponse();
            DateTime currentDateTime = DateTime.Now;
            DocumentUploadRequest documentUploadRequest = new DocumentUploadRequest();
            DocumentUploadResponse documentUploadResponse = new DocumentUploadResponse();
            try
            {
                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(documentRequest.UserID);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Document.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                authorizeRequest.ContentID = documentRequest.Id;
                authorizeRequest.ActionName = ActionNameEnumeration.Rename.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentResponse.IsSuccess = false;
                    documentResponse.Message = "You are not authorized to perform this action";
                    return documentResponse;
                }
                #endregion

                //If renamed from search screen, should validate for name
                Document validateDocument = this._documentRepository.GetDocumentByNameAndProjectID(documentRequest.Name, documentRequest.ParentId);
                if (validateDocument != null)
                {
                    documentResponse.Message = "Document with " + documentRequest.Name + " already exists.";
                    documentResponse.IsSuccess = false;
                    return documentResponse;
                }

                var getDocument = this._documentRepository.GetDocumentById(documentRequest.Id);

                if (getDocument != null)
                {
                    #region DocumentStateValidation
                    ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentRequest.Id, documentRequest.UserID);
                    if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                    {
                        documentResponse.IsSuccess = false;
                        documentResponse.Message = "The file is locked. Please try after sometime.";
                        return documentResponse;

                    }
                    #endregion


                    //----------------NOt Requiered -  we are not renaming file in AWS S3------------------

                    //documentUploadRequest.FileName = projectRequest.Name;
                    //documentUploadRequest.SourceKey = getDocument.Path;
                    //documentUploadRequest.ProjectID = projectRequest.ParentId;

                    //getDocument.StorageKey = this._documentInformationProvider.GetStorageKey(documentUploadRequest, getDocument);
                    // documentUploadRequest.Key = this._documentInformationProvider.GetDocumentFullPath(documentUploadRequest, getDocument.StorageKey);

                    //----------------NOt Requiered -  we are not renaming file in AWS S3------------------

                    Document document = new Document();

                    document.DocumentID = documentRequest.Id;
                    document.CreatedDateTime = getDocument.CreatedDateTime;
                    document.LastModifiedDateTime = currentDateTime;
                    document.CreatedByUserID = getDocument.CreatedByUserID;
                    document.LastModifiedByUserID = documentRequest.UserID;
                    document.IsActive = getDocument.IsActive;
                    document.Name = documentRequest.Name;
                    document.Path = getDocument.Path;
                    document.MajorVersion = getDocument.MajorVersion;
                    document.MinorVersion = getDocument.MinorVersion;
                    document.Number = getDocument.Number;
                    document.Key = getDocument.Key;
                    document.StorageKey = getDocument.StorageKey;
                    document.IsLocked = getDocument.IsLocked;
                    document.LockedByUserID = getDocument.LockedByUserID;
                    document.FileExtensionTypeID = getDocument.FileExtensionTypeID;
                    document.FileSize = getDocument.FileSize;
                    document.LastUploadedDate = getDocument.LastUploadedDate;

                    this._documentRepository.SaveDocument(document);

                    var documentVersionHistory = this._documentVersionHistoryRepository.GetDocumentVersionHistoryById(documentRequest.Id);
                    string OldName = documentVersionHistory.Name;

                    documentVersionHistory.DocumentVersionHistoryID = documentVersionHistory.DocumentVersionHistoryID;
                    documentVersionHistory.LastModifiedByUserID = documentRequest.UserID;
                    documentVersionHistory.LastModifiedDateTime = currentDateTime;
                    documentVersionHistory.Name = documentRequest.Name;
                    documentVersionHistory.Path = getDocument.Path;
                    documentVersionHistory.Key = getDocument.Key;
                    documentVersionHistory.StorageKey = getDocument.StorageKey;

                    this._documentVersionHistoryRepository.SaveDocumentVersionHistory(documentVersionHistory);

                    documentResponse.Message = "Document renamed successfully";
                    documentResponse.IsSuccess = true;

                    //Activity log added with Renamed document
                    ActivityLog activityLog = new ActivityLog();
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Renamed.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    activityLog.KeyValue = document.DocumentID.ToString();
                    activityLog.Custom1 = document.DocumentID.ToString();
                    activityLog.Custom2 = OldName;
                    activityLog.Custom3 = documentVersionHistory.Name;

                    this._actionLogger.LogUserActivity(activityLog);
                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error in RenameDocument Method", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error in RenameDocument Method", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error in RenameDocument Method", ex));
                throw new BusinessException("", ex);
            }
            return documentResponse;

        }

        public DocumentResponse DeleteDocumentAsync(DocumentRequest documentRequest)
        {
            var documentResponse = new DocumentResponse();

            DateTime currentDateTime = DateTime.Now;
            try
            {
                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(documentRequest.UserID);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Document.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                authorizeRequest.ContentID = documentRequest.Id;
                authorizeRequest.ActionName = ActionNameEnumeration.Delete.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentResponse.IsSuccess = false;
                    documentResponse.Message = "You are not authorized to perform this action";
                    return documentResponse;
                }
                #endregion
                var getDocument = this._documentRepository.GetDocumentById(documentRequest.Id);
                if (getDocument != null)
                {
                    #region DocumentStateValidation
                    ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentRequest.Id, documentRequest.UserID);
                    if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                    {
                        documentResponse.IsSuccess = false;
                        documentResponse.Message = "The file is locked. Please try after sometime.";
                        return documentResponse;

                    }
                    #endregion

                    Document document = new Document();

                    document.DocumentID = documentRequest.Id;
                    document.CreatedDateTime = getDocument.CreatedDateTime;
                    document.LastModifiedDateTime = currentDateTime;
                    document.CreatedByUserID = getDocument.CreatedByUserID;
                    document.LastModifiedByUserID = documentRequest.UserID;
                    document.IsActive = false;
                    document.Name = getDocument.Name;
                    document.Path = getDocument.Path;
                    document.MajorVersion = getDocument.MajorVersion;
                    document.MinorVersion = getDocument.MinorVersion;
                    document.Number = getDocument.Number;
                    document.Key = getDocument.Key;
                    document.StorageKey = getDocument.StorageKey;
                    if (document.IsLocked == true && documentRequest.UserID == getDocument.LockedByUserID)
                    {
                        document.IsLocked = false;
                        document.LockedByUserID = null;
                    }
                    document.FileExtensionTypeID = getDocument.FileExtensionTypeID;
                    document.FileSize = getDocument.FileSize;
                    document.LastUploadedDate = getDocument.LastUploadedDate;


                    this._documentRepository.SaveDocument(document);

                    //Activity log added for deleted document
                    ActivityLog activityLog = new ActivityLog();
                    repositoryUser = _userProvider.GetUser(documentRequest.UserID);
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Deleted.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Project.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                    activityLog.KeyValue = getDocument.Projects.Select(x => x.ProjectID).FirstOrDefault().ToString();
                    activityLog.Custom1 = activityLog.KeyValue;
                    activityLog.Custom2 = "Document : " + getDocument.Name;
                    this._actionLogger.LogUserActivity(activityLog);

                    documentResponse.Message = "Document deleted successfully";
                    documentResponse.IsSuccess = true;

                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error in DeleteDocumentAsync", ex));
                throw new BusinessException("Error in DeleteDocumentAsync", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error in DeleteDocumentAsync", ex));
                throw new BusinessException("Error in DeleteDocumentAsync", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error in DeleteDocumentAsync", ex));
                throw new BusinessException("Error in DeleteDocumentAsync", ex);
            }
            return documentResponse;

        }

        public async Task<DocumentResponse> DownloadDocumentAsync(DocumentRequest documentRequest)
        {
            DocumentResponse documentResponse = new DocumentResponse();
            DateTime currentDateTime = DateTime.Now;
            try
            {
                var document = this._documentRepository.GetDocumentById(documentRequest.Id);
                documentRequest.PhysicalPath = this._documentInformationProvider.GetDocumentPhysicalPath(documentRequest, document.StorageKey);
                documentRequest.Name = document.Name;
                documentResponse = await _storageProvider.DownloadDocumentAsync(documentRequest);


                if (document != null && documentResponse.IsSuccess)
                {

                    //Activity log added for Viewed document
                    ActivityLog activityLog = new ActivityLog();
                    repositoryUser = _userProvider.GetUser(documentRequest.UserID);
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Viewed.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    activityLog.KeyValue = document.DocumentID.ToString();
                    activityLog.Custom1 = document.DocumentID.ToString();
                    this._actionLogger.LogUserActivity(activityLog);


                    documentResponse.Message = "Document downloaded successfully";
                    documentResponse.IsSuccess = true;

                }
            }
            // catch (ExternalStorageException ex)
            // {
            //     Logger.LogError(String.Format("Error while downloading document{0}", ex));
            //     throw new BusinessException("", ex);
            // }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while downloading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while downloading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while downloading document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentResponse;
        }

        public async Task<DocumentResponse> DownloadDocumentVersionAsync(DocumentRequest documentRequest)
        {
            DocumentResponse documentResponse = new DocumentResponse();
            DateTime currentDateTime = DateTime.Now;
            try
            {

                var document = this._documentRepository.GetDocumentVersionById(documentRequest.Id);

                if (document != null)
                {
                    documentRequest.ParentId = document.Projects.Select(x => x.ProjectID).FirstOrDefault();
                    documentRequest.PhysicalPath = this._documentInformationProvider.GetDocumentPhysicalPath(documentRequest, document.StorageKey);
                    documentRequest.Name = document.Name;
                    documentResponse = null;//await _storageProvider.DownloadDocumentAsync(documentRequest);


                    if (document != null && documentResponse.IsSuccess)
                    {

                        //Activity log added for Viewed document
                        ActivityLog activityLog = new ActivityLog();
                        repositoryUser = _userProvider.GetUser(documentRequest.UserID);
                        if (repositoryUser != null)
                            activityLog.UserGuID = repositoryUser.UserGuID;
                        activityLog.ActivityName = ActivityNameEnumeration.Viewed.ToString();
                        activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                        activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                        activityLog.KeyValue = document.DocumentID.ToString();
                        activityLog.Custom1 = document.DocumentID.ToString();
                        this._actionLogger.LogUserActivity(activityLog);


                        documentResponse.Message = "Document downloaded successfully";
                        documentResponse.IsSuccess = true;

                    }
                }
                else
                {
                    documentResponse.Message = "Failed to download successfully";
                    documentResponse.IsSuccess = false;
                }
            }
            // catch (ExternalStorageException ex)
            // {
            //     Logger.LogError(String.Format("Error while downloading document{0}", ex));
            //     throw new BusinessException("", ex);
            // }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while downloading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while downloading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while downloading document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentResponse;
        }
    }
}
