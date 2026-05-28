using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class FilterRequest
    {
        public List<FilterItem> Filters { get; set; }
        public int PageIndex { get; set; }
        public int PageLength { get; set; }
        public string Sort { get; set; }
        public bool Ascending { get; set; }
    }
}
