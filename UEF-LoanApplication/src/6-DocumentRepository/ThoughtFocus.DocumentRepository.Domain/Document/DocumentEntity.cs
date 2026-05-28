using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class DocumentEntity
    {
        public Guid DocumentID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

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
        public Guid ProjectID { get; set; }

    }
}
