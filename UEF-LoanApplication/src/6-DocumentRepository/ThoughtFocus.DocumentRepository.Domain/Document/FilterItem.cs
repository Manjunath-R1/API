using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class FilterItem
    {
        public string Property { get; set; }
        public object Value { get; set; }
        public Dictionary<string, string> ListValues { get; set; }
    }
}
