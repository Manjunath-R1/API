using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain.Response
{
    public class DocumentTagResponse:DocumentBaseResponse
    {
        public List<DocumentTagViewModel> DocumentTags { get; set; }
    }
}
