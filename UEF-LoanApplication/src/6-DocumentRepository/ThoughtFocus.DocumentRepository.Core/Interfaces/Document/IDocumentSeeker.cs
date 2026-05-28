using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IDocumentSeeker
    {
        //DocumentListResponse GetDocuments(long userID);
        ContentSearchResult SearchText(string searchText);
        FilterResponse SearchDocuments(FilterRequest filterRequest);
    }
}
