using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class Properties
    {
        public string Name { get; set; }
        public long? Size { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string CreatedBy { get; set; }
        public string DocumentNumber { get; set; }
        public bool IsInherit { get; set; }
        public List<DocumentTagViewModel> DocumentTags { get; set; }
    }
}
