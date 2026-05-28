using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain.Response
{
    public class FilterResponse : DocumentBaseResponse
    {
        public IList<DataAccess.Neo4j.Document> Documents { get; set; }
        public ContentSearchResult ContentSearchResult { get; set; }
        public long TotalCount { get; set; }
        public IList<DocumentEntity> DocumentList { get; set; }

    }
}
