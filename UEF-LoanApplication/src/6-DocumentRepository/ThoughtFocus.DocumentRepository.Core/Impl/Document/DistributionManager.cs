using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Mail;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class DistributionManager : IDistributionManager
    {
        private readonly ILogger<DistributionManager> _logger;
        private ISharedDocumentLogRepository _sharedDocumentLogRepository;
        private IAuthorizer _authorizer;
        private IActionLogger _actionLogger;
        private IDocumentVersionHistoryRepository _documentVersionHistoryRepository;
        private IDocumentInformationProvider _documentInformationProvider;
       
        private IUserRepository _userProvider;
        private IDocumentUploader _documentUploader;
        private IAccessRoleRepository _accessRoleRepository;


        public DistributionManager(IDocumentUploader documentUploader, 
            ISharedDocumentLogRepository sharedDocumentLogRepository,
            IAuthorizer authorizer, IActionLogger actionLogger,
            IDocumentVersionHistoryRepository documentVersionHistoryRepository,
            IDocumentInformationProvider documentInformationProvider,
            IUserRepository userProvider, 
            IAccessRoleRepository accessRoleRepository,
            ILogger<DistributionManager> logger)
        {
            _sharedDocumentLogRepository = sharedDocumentLogRepository;
            _authorizer = authorizer;
            _actionLogger = actionLogger;
            _documentVersionHistoryRepository = documentVersionHistoryRepository;
            _documentInformationProvider = documentInformationProvider;
           
            _userProvider = userProvider;
            _documentUploader = documentUploader;
            _accessRoleRepository = accessRoleRepository;
            _logger = logger;
        }

        public DocumentBaseResponse ShareDocument(Domain.Request.DocumentShareRequest documentShareRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            try
            {
                RepositoryUser repositoryUser = null;
                #region Validation
                if (documentShareRequest.DocumentID == null || String.IsNullOrEmpty(documentShareRequest.DocumentVersionKey))
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.IsValid = false;
                    documentBaseResponse.Message = "Unable to share document at this moment. Please contact administrator.";
                    _logger.LogError(String.Format("DocumentVersionKey is empty or DocumentID is 0", documentShareRequest.DocumentVersionKey));
                    return documentBaseResponse;
                }
                if (documentShareRequest.EmailAddresses == null)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.IsValid = false;
                    documentBaseResponse.Message = "Please enter the recipients.";
                    _logger.LogError(String.Format("EmailAddresses is null", documentShareRequest.DocumentVersionKey));
                    return documentBaseResponse;
                }
                if (documentShareRequest.EmailAddresses.Count <= 0)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.IsValid = false;
                    documentBaseResponse.Message = "Please enter the recipients.";
                    _logger.LogError(String.Format("EmailAddresses is empty", documentShareRequest.DocumentVersionKey));
                    return documentBaseResponse;
                }
                #endregion

                #region DocumentStateValidation
                ValidationResponse stateValidationResponse = this._documentInformationProvider.CanModifyDocumentState(documentShareRequest.DocumentID, documentShareRequest.LoggedInUser);
                if (!(stateValidationResponse.IsSuccess && stateValidationResponse.IsValid))
                {

                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "The file is locked by some other user. Please try after sometime.";
                    return documentBaseResponse;
                }
                #endregion

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(documentShareRequest.ImpersonatedUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Document.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                authorizeRequest.ContentID = documentShareRequest.DocumentID;
                authorizeRequest.ActionName = ActionNameEnumeration.Share.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    return documentBaseResponse;
                }
                #endregion

                List<SharedDocumentLog> sharedDocumentLogs = new List<SharedDocumentLog>();
                ActivityLog activityLog = new ActivityLog();
                
                DateTime currentTime = DateTime.Now;
                string To = String.Empty;
                string body = String.Empty;
                string subject = String.Empty;
                DocumentVersionHistory documentVersionHistory = null;
                MailMessage mailMessage = new MailMessage();
                //bool emailSent = false;

                documentVersionHistory = this._documentVersionHistoryRepository.GetDocumentVersionHistoryByVersionKey(documentShareRequest.DocumentVersionKey);

                #region DocumentValidation
                if (documentVersionHistory == null)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.IsValid = false;
                    documentBaseResponse.Message = "Unable to share document at this moment. Please contact administrator.";
                    _logger.LogError(String.Format("DocumentVersion for versionSalt {0} is null",documentShareRequest.DocumentVersionKey));
                    return documentBaseResponse;
                }
                #endregion

                foreach (var email in documentShareRequest.EmailAddresses)
                {
                    SharedDocumentLog sharedDocumentLog = new SharedDocumentLog();
                    sharedDocumentLog.SharedDocumentLogID = Guid.NewGuid();
                    sharedDocumentLog.CreatedByUserID = documentShareRequest.LoggedInUser;
                    sharedDocumentLog.CreatedDateTime = currentTime;
                    sharedDocumentLog.DocumentID = documentShareRequest.DocumentID;
                    sharedDocumentLog.DocumentVersionSalt = documentShareRequest.DocumentVersionKey;
                    sharedDocumentLog.EmailAddress = email;
                    sharedDocumentLogs.Add(sharedDocumentLog);
                    mailMessage.To.Add(String.IsNullOrEmpty(To) ? email : String.Format("{0},{1}", To, email));
                }
                mailMessage.Body = this.FormEmailBody(documentShareRequest, documentVersionHistory);
                mailMessage.Subject = this.FormEmailSubject(documentVersionHistory);
               
                
                foreach (var email in documentShareRequest.EmailAddresses)
                {
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Shared.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    activityLog.KeyValue = documentVersionHistory.DocumentID.ToString();
                    activityLog.Custom1 = documentVersionHistory.DocumentID.ToString();
                    activityLog.Custom2 = email;                   
                    this._actionLogger.LogUserActivity(activityLog);
                }

                _logger.LogDebug(String.Format("Document with ID {0} of versionsalt {1} is shared with {2} by {3}", documentShareRequest.DocumentID, documentShareRequest.DocumentVersionKey, To, documentShareRequest.LoggedInUser));
                documentBaseResponse.IsValid = true;
                documentBaseResponse.IsSuccess = true;
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while sharing document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while sharing document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while sharing document{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        private string FormEmailBody(Domain.Request.DocumentShareRequest documentShareRequest, DocumentVersionHistory documentVersionHistory)
        {

            string body = String.Empty;
            DateTime current = DateTime.Now;
            string storageKey=String.Empty;
            string imagePath = String.Empty;
            try
            {
                // imagePath = this._documentInformationProvider.GetExtensionImagePath(documentVersionHistory.FileExtensionTypeID);
                // using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/email_templates/ShareTemplate.html")))
                    
                // {
                //     body = reader.ReadToEnd();
                // }

                // body = body.Replace("[[Currentyear]]", current.Year.ToString());
                // body = body.Replace("[[FileName]]", documentVersionHistory.Name);
                // body = body.Replace("[[ClickHere]]", String.Format(ConfigurationManager.AppSettings["DocumentShareUrl"], HttpUtility.UrlEncode(documentVersionHistory.VersionSalt))); 
                // body = body.Replace("[[ImageSource]]", imagePath);
                // body = body.Replace("[[BodyByUser]]", documentShareRequest.Body);

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while forming email body {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while forming email body {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while forming email body {0}", ex));
                throw new BusinessException("", ex);
            }
            return body;
        }

        private string FormEmailSubject(DocumentVersionHistory documentVersionHistory)
        {
            string subject = "{0} is shared.";
            try
            {
                subject = String.Format(subject, documentVersionHistory.Name);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while forming email subject {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while forming email subject {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while forming email subject {0}", ex));
                throw new BusinessException("", ex);
            }
            return subject;
        }


        public DocumentDownloadResponse AccessSharedDocument(Domain.Request.DocumentShareRequest documentShareRequest)
        {
            DocumentDownloadResponse documentDownloadResponse = new DocumentDownloadResponse();
            string folderPath = String.Empty;
            DocumentVersionHistory documentVersionHistory = null;

            try
            {
                #region Validation

                if(String.IsNullOrEmpty(documentShareRequest.DocumentVersionKey))
                {
                    _logger.LogError("DocumentVersionKey is null ");
                    documentDownloadResponse.IsSuccess = false;
                    documentDownloadResponse.Message = "Unable to download the document. Please contact administrator.";
                    return documentDownloadResponse;
                }

                if(documentShareRequest.EmailAddresses==null)
                {
                    _logger.LogError(String.Format("EmailAddress is null for DocumentVersionKey {0}", documentShareRequest.DocumentVersionKey));
                    documentDownloadResponse.IsSuccess = false;
                    documentDownloadResponse.Message = "Unable to download the document. Please contact administrator.";
                    return documentDownloadResponse;
                }

               // SharedDocumentLog sharedDocumentLog = _sharedDocumentLogRepository.FirstOrDefault(a => a.DocumentVersionSalt == documentShareRequest.DocumentVersionKey && a.EmailAddress == documentShareRequest.EmailAddresses.FirstOrDefault());

                SharedDocumentLog sharedDocumentLog = _sharedDocumentLogRepository.GetSharedDocumentLog(documentShareRequest.DocumentVersionKey, documentShareRequest.EmailAddresses.FirstOrDefault());

                if (sharedDocumentLog==null)
                {
                    _logger.LogError(String.Format("sharedDocumentLog is null for DocumentVersionKey {0}", documentShareRequest.DocumentVersionKey));
                    documentDownloadResponse.IsSuccess = false;
                    documentDownloadResponse.Message = "You are not authorized to download this document.";
                    return documentDownloadResponse;
                }
                #endregion

                //documentVersionHistory = this._documentVersionHistoryRepository.FirstOrDefault(a => a.IsActive == true && a.VersionSalt == documentShareRequest.DocumentVersionKey);

                documentVersionHistory = this._documentVersionHistoryRepository.GetDocumentVersionHistoryByVersionKey(documentShareRequest.DocumentVersionKey);
               
                if (documentVersionHistory != null)
                {
                    var request = new DocumentRepository.Domain.DocumentRequest { Id = documentVersionHistory.DocumentID, UserID = documentVersionHistory.LastModifiedByUserID };

                        var uploaderResponseTask = _documentUploader.DownloadDocumentAsync(request);
                        var uploaderResponse = uploaderResponseTask.Result;
                      //  Stream stream = this._storageProvider.DownloadFile(folderPath);
                        if (uploaderResponseTask != null)
                        {
                            //documentDownloadResponse.InputStream = stream;
                            documentDownloadResponse.FileName = documentVersionHistory.Name;
                            documentDownloadResponse.ID = uploaderResponse.DocumentID;
                            documentDownloadResponse.DocumentKey = uploaderResponse.DocumentID;
                            documentDownloadResponse.FileName = uploaderResponse.Name;
                            documentDownloadResponse.FilePath = uploaderResponse.PhysicalPath;
                            documentDownloadResponse.FileContent = uploaderResponse.Content;
                            documentDownloadResponse.FileStream = uploaderResponse.OutputStream;
                            documentDownloadResponse.IsSuccess = true;

                            #region LogSharedAccess
                            SharedDocumentAccessLog accesslog = new SharedDocumentAccessLog();
                            accesslog.CreatedDateTime = DateTime.Now;
                            accesslog.DocumentID = documentVersionHistory.DocumentID;
                            accesslog.DocumentVersionSalt = documentShareRequest.DocumentVersionKey;
                            accesslog.EmailAddress = documentShareRequest.EmailAddresses.FirstOrDefault();
                            //this._sharedDocumentAccessLogRepository.SaveLog(accesslog);
                            #endregion

                        }
                        else
                        {
                            _logger.LogError("Input Stream returned from Storage serice is null while trying to download document");
                            documentDownloadResponse.IsSuccess = false;
                            documentDownloadResponse.Message = "Unable to download the document. Please contact administrator.";
                            return documentDownloadResponse;
                        }
                  
                }
                else
                {
                    _logger.LogError("documentVersionHistory is null while trying to download document");
                    documentDownloadResponse.IsSuccess = false;
                    documentDownloadResponse.Message = "Unable to download the document. Please contact administrator.";
                    return documentDownloadResponse;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while executing AccessSharedDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing AccessSharedDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while executing AccessSharedDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while executing AccessSharedDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while executing AccessSharedDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            return documentDownloadResponse;
        }
    }
}
