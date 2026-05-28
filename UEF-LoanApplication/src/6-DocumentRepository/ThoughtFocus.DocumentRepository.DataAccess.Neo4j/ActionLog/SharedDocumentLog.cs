using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class SharedDocumentLog
    {
        public Guid SharedDocumentLogID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }

        public Guid DocumentID { get; set; }
        public string DocumentVersionSalt { get;set; }
        public string EmailAddress { get; set; }

        public virtual Document Document { get; set; }
    }
}
