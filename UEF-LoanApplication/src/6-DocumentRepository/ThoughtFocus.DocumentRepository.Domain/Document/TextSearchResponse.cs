using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class ContentSearchResult : DocumentBaseResponse
    {
        public List<SearchResult> SearchResults { get; set; }
        public Guid[] CloudSearchedDocuments { get; set; }
    }
}
