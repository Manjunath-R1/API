using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class TagValue
    {
        public Guid TagValueID { get; set; }
        public Guid TagID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string Value { get; set; }
    }
}
