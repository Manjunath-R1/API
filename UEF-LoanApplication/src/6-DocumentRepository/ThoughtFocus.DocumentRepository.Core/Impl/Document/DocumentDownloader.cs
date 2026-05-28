using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.IO;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DocumentRepository.StorageService;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class DocumentDownloader : IDocumentDownloader
    {
        private readonly ILogger<DocumentDownloader> _logger;
        private IDocumentVersionHistoryRepository _documentVersionHistoryRepository;
        private IDocumentInformationProvider _documentInformationProvider;
        private IStorageService _storageProvider;
        private IActionLogger _actionLogger;
        private IUserRepository _userProvider;

        public DocumentDownloader(IDocumentVersionHistoryRepository documentVersionHistoryRepository, IDocumentInformationProvider documentInformationProvider,
             IActionLogger actionLogger, IUserRepository userProvider,IStorageService storageProvider,
             ILogger<DocumentDownloader> logger
             )
        {
            _documentVersionHistoryRepository = documentVersionHistoryRepository;
            _documentInformationProvider = documentInformationProvider;
            _storageProvider = storageProvider;
            _actionLogger = actionLogger;
            _userProvider = userProvider;
            _logger = logger;
        }

        public DocumentDownloadResponse DownloadDocument(string documentVersionKey, long userID)
        {
            DocumentDownloadResponse documentDownloadResponse = new DocumentDownloadResponse();
            ActivityLog activityLog = new ActivityLog();
            string folderPath = String.Empty;
            DocumentVersionHistory documentVersionHistory = null;
            RepositoryUser repositoryUser = null;
            try
            {
                
                documentVersionHistory = this._documentVersionHistoryRepository.GetDocumentVersionHistoryByVersionKey(documentVersionKey);
                if (documentVersionHistory != null)
                {
                    folderPath = this._documentInformationProvider.GetFolderPath(documentVersionHistory);
                    if (!String.IsNullOrEmpty(folderPath))
                    {
                        Stream stream = null;//this._storageProvider.DownloadFile(folderPath);
                        if (stream != null)
                        {
                            documentDownloadResponse.FileStream = stream;
                            documentDownloadResponse.FileName = documentVersionHistory.Name;
                            documentDownloadResponse.IsSuccess = true;
                           
                           //Activity log added with downloaded document
                            repositoryUser = _userProvider.GetUser(userID);
                            if (repositoryUser != null)
                                activityLog.UserGuID = repositoryUser.UserGuID;
                            activityLog.ActivityName = ActivityNameEnumeration.Viewed.ToString();
                            activityLog.NodeName = NodeNameEnumeration.Document.ToString();
                            activityLog.NodeKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                            activityLog.KeyValue = documentVersionHistory.Key.ToString();
                            activityLog.Custom1 = documentVersionHistory.Key.ToString();
                            activityLog.Custom2 = documentVersionHistory.MajorVersion.ToString()+"."+ documentVersionHistory.MinorVersion.ToString();                             
                            this._actionLogger.LogUserActivity(activityLog);
                        }
                        else
                        {
                            _logger.LogError("Input Stream returned from Storage service is null while trying to download document");
                            documentDownloadResponse.IsSuccess = false;
                            documentDownloadResponse.Message = "Unable to download the document. Please try after sometime.";
                            return documentDownloadResponse;
                        }
                    }
                    else
                    {
                        _logger.LogError("Folder path is empty while trying to download document");
                        documentDownloadResponse.IsSuccess = false;
                        return documentDownloadResponse;
                    }
                }
                else
                {
                    _logger.LogError("documentVersionHistory is null while trying to download document");
                    documentDownloadResponse.IsSuccess = false;
                    return documentDownloadResponse;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while executing DownloadDocument {0}", ex));
                throw new BusinessException("", ex);
            }
            return documentDownloadResponse;

        }
    }
}
