using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IDistributionManager
    {
        DocumentBaseResponse ShareDocument(DocumentShareRequest documentShareRequest);

        DocumentDownloadResponse AccessSharedDocument(DocumentShareRequest documentShareRequest);
    }
}
