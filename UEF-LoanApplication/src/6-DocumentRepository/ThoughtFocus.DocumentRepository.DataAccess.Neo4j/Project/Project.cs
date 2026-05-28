using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class Project
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
        public long ID { get; set; }
        public long ProjectTypeID { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectBelongsTo> ProjectRelations { get; set; }

    }
}
