using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Domain.Response
{
    public class TagListResponse : DocumentBaseResponse
    {
        public IList<Tag> Tags { get; set; }

        public IList<DocumentTagViewModel> DocumentTags { get; set; }
    }
}
