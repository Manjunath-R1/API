using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public partial class Document
    {
        public Guid DocumentID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public bool IsInherit { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string StorageKey { get; set; }
        public Guid Key { get; set; }
        public string Number { get; set; }

        public Nullable<int> MajorVersion { get; set; }
        public Nullable<int> MinorVersion { get; set; }

        public bool IsLocked { get; set; }
        public Nullable<long> LockedByUserID { get; set; }

        public Guid FileExtensionTypeID { get; set; }

        public Nullable<long> FileSize { get; set; }
        public Nullable<DateTime> LastUploadedDate { get; set; }

        public List<DocumentTag> DocumentTags { get; set; }
        public List<DocumentVersionHistory> DocumentVersionHistories { get; set; }
        public FileExtensionType FileExtensionType { get; set; }
        public List<Project> Projects { get; set; }
        
        //public ICollection<DocumentBelongsTo> DocumentProjectRelations { get; set; }
        //public ICollection<DocumentIsAt> DocumentVersionRelations { get; set; }
        //public ICollection<DocumentHas> DocumentTagRelations { get; set; }
        //public ICollection<DocumentIsOf> DocumentFileExtensionTypeRelations { get; set; }

    }
}
