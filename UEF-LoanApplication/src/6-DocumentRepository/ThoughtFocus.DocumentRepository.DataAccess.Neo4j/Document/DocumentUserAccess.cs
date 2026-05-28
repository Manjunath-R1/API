using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public partial class DocumentUserAccess
    {
        public long DocumentUserAccessID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public long RepositoryUserID { get; set; }
        public Guid DocumentID { get; set; }
        public int Permission { get; set; }
        public bool IsShared { get; set; }
        public RepositoryUser RepositoryUser { get; set; }
        public virtual Document Document { get; set; }

    }
}
