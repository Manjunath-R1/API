using System.Threading.Tasks;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.DocumentManager.Interfaces
{
    public interface IDocumentUploadManager
    {
        Task<DocumentResponse> UploadDocument(DocumentEntity documentEntity);
        
    }
}
