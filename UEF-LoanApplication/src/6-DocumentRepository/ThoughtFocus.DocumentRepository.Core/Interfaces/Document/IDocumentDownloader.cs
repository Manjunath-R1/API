using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IDocumentDownloader
    {
        DocumentDownloadResponse DownloadDocument(string documentVersionKey,long userID);
    }
}
