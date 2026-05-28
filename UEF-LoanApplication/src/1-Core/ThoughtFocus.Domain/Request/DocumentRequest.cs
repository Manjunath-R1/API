using System;

namespace ThoughtFocus.Domain.Request
{
    public class DocumentRequest
    {
        public Guid ParentId { get; set; }
        public Guid Id { get; set; }
        public long UserID { get; set; }
        public string NewName { get; set; }
        public string Name { get; set; }
    }
}
