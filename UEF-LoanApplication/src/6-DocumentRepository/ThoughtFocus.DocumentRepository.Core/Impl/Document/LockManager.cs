using Microsoft.Extensions.Logging;
using System;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class LockManager : ILockManager
    {
        #region Fields

        private readonly ILogger<LockManager> _logger;
        private IDocumentRepository _documentRepositoryImpl;
        private IDocumentInformationProvider _documentInformationProvider;
        private IAuthorizer _authorizer;
        private IActionLogger _actionLogger;
        private IUserRepository _userProvider;
        private IAccessRoleRepository _accessRoleRepository;
        #endregion

        #region Constructor

        public LockManager(IDocumentRepository documentRepositoryImpl, IDocumentInformationProvider documentInformationProvider, 
            IAuthorizer authorizer, IActionLogger actionLogger, IUserRepository userProvider, 
            IAccessRoleRepository accessRoleRepository,
            ILogger<LockManager> logger)
        {
            _documentRepositoryImpl = documentRepositoryImpl;
            _documentInformationProvider = documentInformationProvider;
            _authorizer = authorizer;
            _actionLogger = actionLogger;
            _userProvider = userProvider;
            _accessRoleRepository = accessRoleRepository;
            _logger = logger;
        }

        #endregion Constructor

        #region Methods

        public DocumentBaseResponse LockDocument(long ImpersonatedUser, long userID, Guid documentID)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            var currentDate = DateTime.Now;
            RepositoryUser repositoryUser = null;
            try
            {
                #region DocumentStateValidation
                ValidationResponse stateValidationResponse = this._documentInformationProvider.IsDocumentLocked(documentID);
                if (stateValidationResponse.IsSuccess && stateValidationResponse.IsValid)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "The file is locked.You cannot modify the file until its unlocked.";
                    return documentBaseResponse;

                }
                #endregion

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();

                repositoryUser = _userProvider.GetUser(ImpersonatedUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Document.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                authorizeRequest.ContentID = documentID;
                authorizeRequest.ActionName = ActionNameEnumeration.Lock.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    return documentBaseResponse;
                }
                #endregion

                Document getDocument = this._documentRepositoryImpl.GetDocumentById(documentID);

                if (getDocument != null)
                {

                    Document document = new Document();

                    document.DocumentID = getDocument.DocumentID;
                    document.CreatedDateTime = getDocument.CreatedDateTime;
                    document.LastModifiedDateTime = currentDate;
                    document.CreatedByUserID = getDocument.CreatedByUserID;
                    document.LastModifiedByUserID = userID;
                    document.IsActive = getDocument.IsActive;
                    document.Name = getDocument.Name;
                    document.Path = getDocument.Path;
                    document.MajorVersion = getDocument.MajorVersion;
                    document.MinorVersion = getDocument.MinorVersion;
                    document.Number = getDocument.Number;
                    document.Key = getDocument.Key;
                    document.StorageKey = getDocument.StorageKey;
                    document.IsLocked = true;
                    document.LockedByUserID = userID;
                    document.FileExtensionTypeID = getDocument.FileExtensionTypeID;
                    document.FileSize = getDocument.FileSize;
                    document.LastUploadedDate = getDocument.LastUploadedDate;

                    _documentRepositoryImpl.SaveDocument(document);
                    documentBaseResponse.IsSuccess = true;
                    documentBaseResponse.Message = "Document locked successfully.";


                    //Activity log added with Locked document
                    ActivityLog activityLog = new ActivityLog();
                    repositoryUser = _userProvider.GetUser(userID);
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Locked.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    activityLog.KeyValue = document.DocumentID.ToString();
                    activityLog.Custom1 = document.DocumentID.ToString();
                    this._actionLogger.LogUserActivity(activityLog);

                }
                else
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "Unable to lock the document.";
                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while locking document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while locking document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while locking document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public DocumentBaseResponse UnlockDocument(long ImpersonatedUser, long userID, Guid documentID)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            var currentDate = DateTime.Now;
            RepositoryUser repositoryUser = null;
            try
            {
                #region DocumentStateValidation
                ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentID, userID);
                if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                {

                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "The file is locked by some other user. Please try after sometime.";
                    return documentBaseResponse;
                }
                #endregion

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(ImpersonatedUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Document.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                authorizeRequest.ContentID = documentID;
                authorizeRequest.ActionName = ActionNameEnumeration.Unlock.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    return documentBaseResponse;
                }
                #endregion

                Document getDocument = this._documentRepositoryImpl.GetDocumentById(documentID);

                if (getDocument != null)
                {
                    Document document = new Document();

                    document.DocumentID = getDocument.DocumentID;
                    document.CreatedDateTime = getDocument.CreatedDateTime;
                    document.LastModifiedDateTime = currentDate;
                    document.CreatedByUserID = getDocument.CreatedByUserID;
                    document.LastModifiedByUserID = userID;
                    document.IsActive = getDocument.IsActive;
                    document.Name = getDocument.Name;
                    document.Path = getDocument.Path;
                    document.MajorVersion = getDocument.MajorVersion;
                    document.MinorVersion = getDocument.MinorVersion;
                    document.Number = getDocument.Number;
                    document.Key = getDocument.Key;
                    document.StorageKey = getDocument.StorageKey;
                    document.IsLocked = false;
                    document.LockedByUserID = userID;
                    document.FileExtensionTypeID = getDocument.FileExtensionTypeID;
                    document.FileSize = getDocument.FileSize;
                    document.LastUploadedDate = getDocument.LastUploadedDate;



                    _documentRepositoryImpl.SaveDocument(document);
                    documentBaseResponse.IsSuccess = true;
                    documentBaseResponse.Message = "Document unlocked successfully.";

                    //Activity log added with Unlocked document
                    ActivityLog activityLog = new ActivityLog();
                    repositoryUser = _userProvider.GetUser(userID);
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Unlocked.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    activityLog.KeyValue = document.DocumentID.ToString();
                    activityLog.Custom1 = document.DocumentID.ToString();
                    this._actionLogger.LogUserActivity(activityLog);
                }
                else
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "Unable to unlock the document.";
                }

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while unlocking document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while unlocking document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while unlocking document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        #endregion Methods
    }
}
