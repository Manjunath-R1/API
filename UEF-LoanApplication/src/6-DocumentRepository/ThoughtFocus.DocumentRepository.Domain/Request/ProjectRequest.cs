using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class ProjectRequest
    {
        public bool RootFolder { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public Guid Id { get; set; }
        public long UserID { get; set; }
        public string NewName { get; set; }
        public string PhysicalPath { get; set; }
        public long ID { get; set; }
    }
}
