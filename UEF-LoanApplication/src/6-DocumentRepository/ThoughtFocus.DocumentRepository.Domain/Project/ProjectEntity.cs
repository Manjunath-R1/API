using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class ProjectEntity
    {
        public Guid ProjectID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string PhysicalPath { get; set; }
        public string VirtualPath { get; set; }
        public string StorageKey { get; set; }
        public bool IsInherit { get; set; }

        public long ProjectTypeID { get; set; }

    }
}
