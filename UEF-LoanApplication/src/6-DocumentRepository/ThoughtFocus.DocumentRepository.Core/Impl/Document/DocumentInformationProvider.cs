using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class DocumentInformationProvider : IDocumentInformationProvider
    {
        private readonly ILogger<DocumentInformationProvider> _logger;
        private IFileExtensionTypeRepository _fileExtensionTypeRepository;
        private IDocumentRepository _documentRepository;
        private IProjectRepository _projectRepository;
        private ITagTypeRepository _tagTypeRepository;
        private ITagRepository _tagRepository;
        private IFileExtensionCategoryRepository _fileExtensionCategory;
        private IDocumentVersionHistoryRepository _documentVersionHistoryRepository;
        private IUserRepository _userProvider;
        private RepositoryUser repositoryUser = null;
        private IDocumentTagRepository _documentTagRepository;
        private ITagValueRepository _tagValueRepository;
        private IProjectManager _projectManager;

        public DocumentInformationProvider(IFileExtensionTypeRepository fileExtensionTypeRepository, 
            IDocumentRepository documentRepository, ITagTypeRepository tagTypeRepository, 
            ITagRepository tagRepository, IFileExtensionCategoryRepository fileExtensionCategory, 
            IProjectRepository projectRepository, IDocumentVersionHistoryRepository documentVersionHistoryRepository,
            IUserRepository userProvider, IDocumentTagRepository documentTagRepository,
            ITagValueRepository tagValueRepository, IProjectManager projectManager,
            ILogger<DocumentInformationProvider> logger)
        {
            _fileExtensionTypeRepository = fileExtensionTypeRepository;
            _documentRepository = documentRepository;
            _projectRepository = projectRepository;
            _tagTypeRepository = tagTypeRepository;
            _tagRepository = tagRepository;
            _fileExtensionCategory = fileExtensionCategory;
            _documentVersionHistoryRepository = documentVersionHistoryRepository;
            _userProvider = userProvider;
            _documentTagRepository = documentTagRepository;
            _tagValueRepository = tagValueRepository;
            _projectManager = projectManager;
            _logger = logger;
        }

        public List<FileExtensionType> GetSupportedFileExtensions()
        {
            List<FileExtensionType> fileExtensions = null;
            try
            {
                fileExtensions = this._fileExtensionTypeRepository.GetAll().ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions{0}", ex));
                throw new BusinessException("", ex);
            }
            return fileExtensions;
        }

        public DocumentEntity GetDocumentByID(Guid documentID)
        {
            DocumentEntity documentEntity = new DocumentEntity();
            Document document = null;
            try
            {
                document = this._documentRepository.GetDocumentById(documentID);
                if (document != null)
                {
                    documentEntity.DocumentID = document.DocumentID;
                    documentEntity.CreatedDateTime = document.CreatedDateTime;
                    documentEntity.LastModifiedDateTime = document.LastModifiedDateTime;
                    documentEntity.CreatedByUserID = document.CreatedByUserID;
                    documentEntity.LastModifiedByUserID = document.LastModifiedByUserID;
                    documentEntity.IsActive = document.IsActive;
                    documentEntity.Name = document.Name;
                    documentEntity.Path = document.Path;
                    documentEntity.MajorVersion = document.MajorVersion;
                    documentEntity.MinorVersion = document.MinorVersion;
                    documentEntity.Number = document.Number;
                    documentEntity.Key = document.Key;
                    documentEntity.StorageKey = document.StorageKey;
                    documentEntity.LockedByUserID = document.LockedByUserID;
                    documentEntity.IsLocked = document.IsLocked;
                    documentEntity.FileExtensionTypeID = document.FileExtensionTypeID;
                    documentEntity.FileSize = document.FileSize;
                    documentEntity.LastUploadedDate = document.LastUploadedDate;
                    documentEntity.ProjectID = document.Projects.FirstOrDefault().ProjectID;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching FileExtensions{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentEntity;
        }

        public string GetTheNextDocumentNumber()
        {
            long documentNumber = 1;
            try
            {
                Document document = this._documentRepository.GetLastDocument();
                if (document != null)
                {
                    documentNumber = document.Number == null ? documentNumber : Convert.ToInt64(document.Number);
                    documentNumber = documentNumber + 1;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching NextDocumentNumber {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching NextDocumentNumber {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching NextDocumentNumber {0}", ex));
                throw new BusinessException("", ex);
            }
            return documentNumber.ToString();
        }

        public string GetStorageKey(DocumentUploadRequest documentUploadRequest, Document document)
        {
            string storageKey = String.Empty;
            try
            {
                string cleanedFileName = documentUploadRequest.FileName.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
                storageKey = String.Format("{0}_{1}", document.Key, cleanedFileName);
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

        public string GetDocumentPhysicalPath(DocumentRequest documentRequest, string storageKey)
        {
            string documentPath = storageKey;
            try
            {
                if (documentRequest.ParentId != Guid.Empty)
                {
                    documentPath = GetProjectPath(documentPath, documentRequest.ParentId);

                }
                else
                {
                    var document = this._documentRepository.GetDocumentPrimaryProject(documentRequest.Id);

                    documentPath = GetProjectPath(documentPath, document.Projects.First().ProjectID);

                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            return documentPath;
        }

        public string GetVersionSalt(DocumentVersionHistory documentVersionHistory)
        {
            string versionSalt = String.Empty;
            try
            {
                string versionSaltBase = String.Format("Ver_{0}.{1}{2}", documentVersionHistory.MajorVersion, documentVersionHistory.MinorVersion, documentVersionHistory.Key);
                versionSalt = "";//StringEncryption.Encrypt(versionSaltBase, ConfigurationManager.AppSettings["VersionSaltKey"]);
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching VersionSalt {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching VersionSalt {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching VersionSalt {0}", ex));
                throw new BusinessException("", ex);
            }
            return versionSalt;
        }

        public string GetFolderPath(DocumentVersionHistory documentVersionHistory)
        {
            string documentPath = documentVersionHistory.StorageKey;
            try
            {
                var document = this._documentRepository.GetDocumentById(documentVersionHistory.DocumentID);

                documentPath = GetProjectPath(documentPath, document.Projects.First().ProjectID);
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            return documentPath;
        }

        public ValidationResponse CanModifyDocumentState(Guid documentID, long userID)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsValid = false;
            validationResponse.IsSuccess = true;
            try
            {
                Document document = this._documentRepository.GetDocumentById(documentID);
                if (document != null)
                {
                    validationResponse = this.IsDocumentLocked(documentID);
                    //If Validation.Isvalid is true -Document is locked
                    validationResponse.IsValid = (!validationResponse.IsValid) || document.LockedByUserID == userID;
                }
                else
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        public ValidationResponse IsDocumentLocked(Guid documentID)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsValid = false;
            validationResponse.IsSuccess = true;
            try
            {

                Document document = this._documentRepository.GetDocumentById(documentID);
                if (document != null)
                {
                    validationResponse.IsValid = document.IsLocked;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        public ValidationResponse ValidateFileExtension(string fileName)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            string extensionValue = Path.GetExtension(fileName).ToLower();
            try
            {
                List<FileExtensionType> fileExtensions = new List<FileExtensionType>();
                fileExtensions = this.GetSupportedFileExtensions();
                if (fileExtensions.Any(a => a.Value == extensionValue))
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        public ValidationResponse ValidateTagType(long tagTypeID)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                TagType tagType = new TagType();
                tagType = this._tagTypeRepository.FirstOrDefault(a => a.TagTypeID == tagTypeID);
                if (tagType != null)
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;

        }

        public ValidationResponse IsDuplicateTag(string tagName)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = false;
            try
            {
                List<Tag> tags = new List<Tag>();
                var activeDuplicateTags = this._tagRepository.GetTagByName(tagName);
                if (activeDuplicateTags != null)
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        public string GetExtensionImagePath(Guid FileExtensionTypeID)
        {
            string imagePath = String.Empty;
            try
            {
                FileExtensionType extension = _fileExtensionTypeRepository.GetExtensionTypeByID(FileExtensionTypeID);
                if (extension == null)
                {
                    _logger.LogError(String.Format("FileExtension is nullfor ExtensionID {0}", FileExtensionTypeID));
                    return imagePath;
                }
                imagePath = extension.ImagePath;
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching FolderPath {0}", ex));
                throw new BusinessException("", ex);
            }
            return imagePath;
        }

        public List<FileExtensionEntity> GetDocumentExtension()
        {
            List<FileExtensionEntity> extensionModels = new List<FileExtensionEntity>();
            try
            {
                 var fileEntensionCategories = this._fileExtensionCategory.GetAll().ToList();
                 if (fileEntensionCategories != null)
                 {
                     foreach (var extension in fileEntensionCategories)
                     {
                         FileExtensionEntity extensionModel = new FileExtensionEntity();
                         extensionModel.Description = extension.Description;
                         extensionModel.FileExtensionCategoryID = extension.FileExtensionCategoryID;
                         extensionModels.Add(extensionModel);
                     }

                 }

                 return extensionModels;
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching EntensionCategories {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing EntensionCategories {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching EntensionCategories {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching EntensionCategories {0}", ex));
                throw new BusinessException("", ex);
            }
        }

        private string GetProjectPath(string documentPath, Guid projectId)
        {
            var project = this._projectRepository.GetProjectById(projectId);

            if (project != null)
            {
                if (String.IsNullOrEmpty(documentPath))
                {
                    documentPath = project.StorageKey;
                }
                else
                {
                    documentPath = String.Format("{0}/{1}", project.PhysicalPath, documentPath);
                }
            }

            return documentPath;
        }

        public string GetLastDocument()
        {
            return _documentRepository.GetLastDocument().Number;
        }

        public DocumentResponse GetDocumentsByProjectId(Guid projectId,long userID)
        {
            DocumentResponse documentResponse = new DocumentResponse();
            documentResponse.DocumentEntityList = new List<DocumentEntity>();
            try
            {
                repositoryUser = _userProvider.GetUser(userID);
                var documentList = this._documentRepository.GetDocumentsByParentId(projectId, repositoryUser.UserGuID).OrderBy(d => d.Name);
                if (documentList != null)
                {
                    foreach (var document in documentList)
                    {
                        DocumentEntity documentEntity = new DocumentEntity();
                        documentEntity.DocumentID = document.DocumentID;
                        documentEntity.CreatedDateTime = document.CreatedDateTime;
                        documentEntity.LastModifiedDateTime = document.LastModifiedDateTime;
                        documentEntity.CreatedByUserID = document.CreatedByUserID;
                        documentEntity.LastModifiedByUserID = document.LastModifiedByUserID;
                        documentEntity.IsActive = document.IsActive;
                        documentEntity.Name = document.Name;
                        documentEntity.Path = document.Path;
                        documentEntity.MajorVersion = document.MajorVersion;
                        documentEntity.MinorVersion = document.MinorVersion;
                        documentEntity.Number = document.Number;
                        documentEntity.Key = document.Key;
                        documentEntity.StorageKey = document.StorageKey;
                        documentEntity.LockedByUserID = document.LockedByUserID;
                        documentEntity.IsLocked = document.IsLocked;
                        documentEntity.FileExtensionTypeID = document.FileExtensionTypeID;
                        documentEntity.FileSize = document.FileSize;
                        documentEntity.LastUploadedDate = document.LastUploadedDate;
                        documentEntity.ProjectID = document.Projects.FirstOrDefault().ProjectID;
                        documentResponse.DocumentEntityList.Add(documentEntity);
                    }

                    documentResponse.IsSuccess = true;
                }
                else
                {
                    documentResponse.IsSuccess = false;
                    documentResponse.Message = "Unable to fetch the documents. Please try after some time.";
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetDocumentsByParentId", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return documentResponse;
        }


        public string GetFileExtensionById(Guid fileExtensionTypeID)
        {
            try
            {
                return this._fileExtensionTypeRepository.GetExtensionTypeByID(fileExtensionTypeID).Value;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetFileExtensionById", ex);
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


        public DocumentResponse GetDocument(Guid documentId)
        {
            DocumentResponse documentResponse = new DocumentResponse();
            try
            {
                var document = this._documentRepository.GetDocumentById(documentId);

                if (document != null)
                {
                    documentResponse.DocumentEntity = new DocumentEntity();
                    documentResponse.DocumentEntity.DocumentID = document.DocumentID;
                    documentResponse.DocumentEntity.CreatedDateTime = document.CreatedDateTime;
                    documentResponse.DocumentEntity.LastModifiedDateTime = document.LastModifiedDateTime;
                    documentResponse.DocumentEntity.CreatedByUserID = document.CreatedByUserID;
                    documentResponse.DocumentEntity.LastModifiedByUserID = document.LastModifiedByUserID;
                    documentResponse.DocumentEntity.IsActive = document.IsActive;
                    documentResponse.DocumentEntity.Name = document.Name;
                    documentResponse.DocumentEntity.Path = document.Path;
                    documentResponse.DocumentEntity.MajorVersion = document.MajorVersion;
                    documentResponse.DocumentEntity.MinorVersion = document.MinorVersion;
                    documentResponse.DocumentEntity.Number = document.Number;
                    documentResponse.DocumentEntity.Key = document.Key;
                    documentResponse.DocumentEntity.StorageKey = document.StorageKey;
                    documentResponse.DocumentEntity.LockedByUserID = document.LockedByUserID;
                    documentResponse.DocumentEntity.IsLocked = document.IsLocked;
                    documentResponse.DocumentEntity.FileExtensionTypeID = document.FileExtensionTypeID;
                    documentResponse.DocumentEntity.FileSize = document.FileSize;
                    documentResponse.DocumentEntity.LastUploadedDate = document.LastUploadedDate;
                    documentResponse.DocumentEntity.ProjectID = document.Projects.FirstOrDefault().ProjectID;


                    documentResponse.IsSuccess = true;
                }
                else
                {
                    documentResponse.IsSuccess = false;
                    documentResponse.Message = "Unable to fetch the documents. Please try after some time.";
                }

                return documentResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetDocumentByID", ex);
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


        public string GetDocumentVersionkeyById(Guid documentId)
        {
            try
            {
                var documentVersionHistory = this._documentVersionHistoryRepository.GetDocumentVersionHistoryById(documentId);

                return documentVersionHistory.VersionSalt.ToString();
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetDocumentVersionHistoryById", ex);
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


        public List<DocumentVersionHistoryEntity> GetDocumentVersionHistoryById(Guid documentId)
        {
            List<DocumentVersionHistoryEntity> documentVersionHistoryEntityList = new List<DocumentVersionHistoryEntity>();
            try
            {
                var documentVersionHistoryList = this._documentVersionHistoryRepository.GetDocumentVersionHistoryId(documentId);

                if (documentVersionHistoryList != null)
                {
                    foreach (var documentVersionHistory in documentVersionHistoryList)
                    {
                        DocumentVersionHistoryEntity documentVersionHistoryEntity = new DocumentVersionHistoryEntity();
                        documentVersionHistoryEntity.DocumentVersionHistoryID = documentVersionHistory.DocumentVersionHistoryID;
                        documentVersionHistoryEntity.DocumentID = documentVersionHistory.DocumentID;
                        documentVersionHistoryEntity.CreatedDateTime = documentVersionHistory.CreatedDateTime;
                        documentVersionHistoryEntity.LastModifiedDateTime = documentVersionHistory.LastModifiedDateTime;
                        documentVersionHistoryEntity.CreatedByUserID = documentVersionHistory.CreatedByUserID;
                        documentVersionHistoryEntity.LastModifiedByUserID = documentVersionHistory.LastModifiedByUserID;
                        documentVersionHistoryEntity.IsActive = documentVersionHistory.IsActive;
                        documentVersionHistoryEntity.Name = documentVersionHistory.Name;
                        documentVersionHistoryEntity.Path = documentVersionHistory.Path;
                        documentVersionHistoryEntity.MajorVersion = documentVersionHistory.MajorVersion;
                        documentVersionHistoryEntity.MinorVersion = documentVersionHistory.MinorVersion;
                        documentVersionHistoryEntity.Key = documentVersionHistory.Key;
                        documentVersionHistoryEntity.StorageKey = documentVersionHistory.StorageKey;
                        documentVersionHistoryEntity.FileExtensionTypeID = documentVersionHistory.FileExtensionTypeID;
                        documentVersionHistoryEntity.FileSize = documentVersionHistory.FileSize;
                        documentVersionHistoryEntity.UploadedDate = documentVersionHistory.UploadedDate;
                        documentVersionHistoryEntity.VersionSalt = documentVersionHistory.VersionSalt;

                        documentVersionHistoryEntityList.Add(documentVersionHistoryEntity);
                    }
                }
                else
                {
                    return null;
                }

                return documentVersionHistoryEntityList;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetDocumentVersionHistoryById", ex);
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

        public Properties GetProperties(Guid iD,string requestType)
        {
            Properties properties = new Properties();
            List<DocumentTagViewModel> documentTagViewModels = new List<DocumentTagViewModel>();
            try
            {
                var documentInfo = new Document();
                var projectInfo = new Project();

                if (requestType == "Document")
                {
                    Document document = _documentRepository.GetDocumentById(iD);

                    repositoryUser = _userProvider.GetUser(document.CreatedByUserID);
                    
                    List<DocumentTag> documentTags = _documentTagRepository.GetTagListByDocumentId(iD);
                    if (documentTags != null)
                    {
                        foreach (var documentTag in documentTags)
                        {
                            List<TagValue> tagsValues = new List<TagValue>();
                            List<TagValueModel> tagsValueEntities = new List<TagValueModel>();
                            DocumentTagViewModel documentTagViewModel = new DocumentTagViewModel();
                            documentTagViewModel.DocumentTagID = documentTag.DocumentTagID;
                            documentTagViewModel.DocumentID = documentTag.DocumentID;
                            documentTagViewModel.TagID = documentTag.TagID;
                            documentTagViewModel.TagTypeID = documentTag.Tag == null ? 0 : documentTag.Tag.TagTypeID;
                            documentTagViewModel.TagTypeName = documentTag.Tag == null ? String.Empty : documentTag.Tag.TagType == null ? String.Empty : documentTag.Tag.TagType.Name;
                            documentTagViewModel.TagName = documentTag.Tag == null ? String.Empty : documentTag.Tag.Name;
                            documentTagViewModel.Value = documentTag.Value;
                            documentTagViewModel.IsDefault = documentTag.IsDefault;

                            if (documentTagViewModel.TagTypeID == (long)ThoughtFocus.DocumentRepository.Domain.Enumeration.TagTypeEnumeration.List)
                            {
                                Guid value = new Guid(documentTag.Value);
                                TagValue tagvalue = _tagValueRepository.GetTagValueById(documentTag.TagID, value);
                                documentTagViewModel.Value = tagvalue.Value;
                            }
                            documentTagViewModels.Add(documentTagViewModel);
                        }
                    }
                    if (document != null)
                    {
                        properties.Name = document.Name;
                        properties.Size = document.FileSize;
                        properties.Type = GetFileExtensionById(document.FileExtensionTypeID);
                        properties.DocumentTags = documentTagViewModels;
                        properties.CreatedBy = repositoryUser.FirstName + " " + repositoryUser.LastName;
                        properties.DocumentNumber = document.Number;
                        var projectDetails = document.Projects.FirstOrDefault();
                        var folderPath = _projectManager.GetVirtualPath(projectDetails.ProjectID, projectDetails.Name);
                        if (folderPath != null)
                        properties.Path = folderPath + "/" + document.Name;
                        properties.IsInherit = document.IsInherit;
                    }
                    
                }
                else
                {
                    Project project = _projectRepository.GetProjectById(iD);
                    var user = _userProvider.GetUser(project.CreatedByUserID);
                    if (project != null)
                    {
                        properties.Name = project.Name;
                        properties.Path = _projectManager.GetVirtualPath(project.ProjectID,project.Name);
                        properties.Type = Domain.Enumeration.ProjectDocTypeEnumeration.Folder.ToString();
                        properties.Size = 0;
                        properties.CreatedBy = user.FirstName + " " + user.LastName;
                        properties.IsInherit = project.IsInherit;
                    }
                    
                }


            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching Properties{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Properties{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing Properties {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Properties{0}", ex));
                throw new BusinessException("", ex);
            }
            return properties;
        }

        public DocumentEntity GetDocumentByNameAndProjectID(string FileName, Guid ProjectId)
        {
            DocumentEntity documentEntity = null;
            Document document = null;
            try
            {
                document = this._documentRepository.GetDocumentByNameAndProjectID(FileName, ProjectId);
                if (document != null)
                {
                    documentEntity = new DocumentEntity();
                    documentEntity.DocumentID = document.DocumentID;
                    documentEntity.CreatedDateTime = document.CreatedDateTime;
                    documentEntity.LastModifiedDateTime = document.LastModifiedDateTime;
                    documentEntity.CreatedByUserID = document.CreatedByUserID;
                    documentEntity.LastModifiedByUserID = document.LastModifiedByUserID;
                    documentEntity.IsActive = document.IsActive;
                    documentEntity.Name = document.Name;
                    documentEntity.Path = document.Path;
                    documentEntity.MajorVersion = document.MajorVersion;
                    documentEntity.MinorVersion = document.MinorVersion;
                    documentEntity.Number = document.Number;
                    documentEntity.Key = document.Key;
                    documentEntity.StorageKey = document.StorageKey;
                    documentEntity.LockedByUserID = document.LockedByUserID;
                    documentEntity.IsLocked = document.IsLocked;
                    documentEntity.FileExtensionTypeID = document.FileExtensionTypeID;
                    documentEntity.FileSize = document.FileSize;
                    documentEntity.LastUploadedDate = document.LastUploadedDate;                    
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while fetching Document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching Document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching Document {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching Document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentEntity;
        }
    }
}
