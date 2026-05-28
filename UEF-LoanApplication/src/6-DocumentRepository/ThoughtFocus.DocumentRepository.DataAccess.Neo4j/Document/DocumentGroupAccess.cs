using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public partial class DocumentGroupAccess
    {
        public long DocumentGroupAccessID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public Guid DocumentID { get; set; }
        public long GroupID { get; set; }
        public bool IsShared { get; set; }
        public int Permission { get; set; }
        public virtual Document Document { get; set; }
        public virtual Group Groups { get; set; }

    }
}
