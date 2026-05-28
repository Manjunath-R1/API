using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ProjectMapping
    {
        public long ProjectMappingID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public Guid ProjectID { get; set; }
        public Guid? ParentProjectID { get; set; }

        public virtual Project Project { get; set; }
        public virtual Project ParentProject { get; set; }

    }
}
