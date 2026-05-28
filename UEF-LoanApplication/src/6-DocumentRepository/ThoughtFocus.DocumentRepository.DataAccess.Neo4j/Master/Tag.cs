using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class Tag
    {
        public Guid TagID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public long TagTypeID { get; set; }
        public virtual TagType TagType { get; set; }
        public List<TagValue> TagValues { get; set; }
        public List<TagHas> TagHasRelations { get; set; }
        public List<TagBelongsTo> TagBelongsToRelations { get; set; }
    }
}
