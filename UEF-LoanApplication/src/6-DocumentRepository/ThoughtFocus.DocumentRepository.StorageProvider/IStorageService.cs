using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.StorageService
{
    public interface IStorageService
    {
        Task<DocumentUploadResponse> UploadFileAsync(DocumentUploadRequest request);
        Task<DocumentResponse> DownloadDocumentAsync(DocumentRequest request);
        Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest);
        Task<IEnumerable<string>> ListBlobsAsync();
    }
}
