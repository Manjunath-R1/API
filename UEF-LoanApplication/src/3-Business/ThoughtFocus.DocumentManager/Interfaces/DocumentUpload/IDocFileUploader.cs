using System;
using System.Threading.Tasks;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Request;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.DocumentManager.Interfaces
{
    public interface IDocFileUploader
    {
        Task<DocumentResponse> UploadDocument(DocumentEntity documentEntity, FileEntity fileEntity);
        DocumentResponse RenameDocument(DocumentRequest projectRequest);
        DocumentResponse DeleteDocument(DocumentRequest projectRequest);
        Task<DocumentResponse> DownloadDocument(string storageKey, UserSessionEntity userSessionEntity);
        DocumentResponse DownloadDocumentVersionID(Guid documentVersionId, UserSessionEntity userSessionEntity);
        long GetMaxFileSizeLimit();
    }
}
