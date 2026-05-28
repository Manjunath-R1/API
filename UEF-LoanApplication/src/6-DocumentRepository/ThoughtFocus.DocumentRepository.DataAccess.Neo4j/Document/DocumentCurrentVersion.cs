using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public partial class DocumentCurrentVersion
    {
        public Guid DocumentID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
    }
}
