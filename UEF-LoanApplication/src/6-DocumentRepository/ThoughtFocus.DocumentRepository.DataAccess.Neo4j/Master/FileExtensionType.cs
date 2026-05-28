using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class FileExtensionType
    {
        public Guid FileExtensionTypeID { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public string ImagePath{ get; set; }
        public string FileMimeType { get; set; }

        public long FileExtensionCategoryID { get; set; }
        public virtual FileExtensionCategory FileEntensionCategory { get; set; }

        public FileExtensionTypeBelongsTo FileExtensionTypeRelation { get; set; }
    }
}
