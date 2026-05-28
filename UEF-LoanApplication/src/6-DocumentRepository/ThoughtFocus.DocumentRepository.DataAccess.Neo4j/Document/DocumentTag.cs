using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public partial class DocumentTag
    {
        public Guid DocumentTagID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        public Guid DocumentID { get; set; }
        public Guid TagID { get; set; }
        public string Value { get; set; }

        public virtual Document Document { get; set; }
        public virtual Tag Tag { get; set; }         
    }
}
