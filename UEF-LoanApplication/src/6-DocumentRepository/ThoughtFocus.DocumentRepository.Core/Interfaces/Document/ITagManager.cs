using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface ITagManager
    {
        DocumentBaseResponse AddTag(TagRequest tagRequest);

        TagListResponse GetTags(long userID);

        DocumentBaseResponse ManageDocumentTags(List<DocumentTagViewModel> documentTagViewModel, long userID, long ImpersonatedUser);

        DocumentTagResponse FetchDocumentTags(DocumentTagRequest documentTagRequest);

        TagListResponse SearchTagsByName(string tagSearchText);

    }
}
