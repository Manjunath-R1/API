using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class DocumentVersionHistoryEntity
    {
        public Guid DocumentVersionHistoryID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public Guid Key { get; set; }
        public string StorageKey { get; set; }
        public Guid FileExtensionTypeID { get; set; }

        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public string VersionSalt { get; set; }

        public Guid DocumentID { get; set; }

        public Nullable<long> FileSize { get; set; }
        public Nullable<DateTime> UploadedDate { get; set; }

    }
}
