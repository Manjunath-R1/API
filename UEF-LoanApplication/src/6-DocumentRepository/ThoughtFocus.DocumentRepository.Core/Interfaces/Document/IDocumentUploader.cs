using System.Threading.Tasks;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IDocumentUploader
    {
        Task<DocumentUploadResponse> UploadDocument(DocumentUploadRequest documentUploadRequest);
        DocumentResponse DeleteDocumentAsync(DocumentRequest request);
        DocumentResponse RenameDocument(DocumentRequest request);
        Task<DocumentResponse> DownloadDocumentAsync(DocumentRequest request);
        Task<DocumentResponse> DownloadDocumentVersionAsync(DocumentRequest request);
        long GetMaxFileSizeLimit();
    }
}
