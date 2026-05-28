using System;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace ThoughtFocus.Domain.CustomView
{
    public class ApplicationDocumentViewModel
    {
        public string FileName { get; set; } //Uploaded FileName

        public string versionNumber { get; set; } //Uploaded Version

        public string FilePath { get; set; } // Uploaded FilePath

        public string UploadDate { get; set; } //Uploaded Date

        public Int64 ApplicationDocumentID { get; set; }

        public int DocumentTypeID { get; set; }

        public string DocumentName { get; set; }

        public string DocumentTypeName { get; set; }

        public bool Uploading { get; set; }

        public bool CanUpload { get; set; }

        public bool CanView { get; set; }

        public bool CanViewHistory { get; set; }

        public Int64 MemberID { get; set; }

        public string MemberFullName { get; set; }

        public Guid DocumentKey { get; set; }

        public Guid? DocumentVersionID { get; set; }

        public Guid DocumentID { get; set; }

        public bool SaveUploaded { get; set; }

        public bool CancelUploaded { get; set; }

        public bool DocumentNameEditable { get; set; }

        public bool DeleteIcon { get; set; }

        public string DocumentVersionkey { get; set; }

        public IFormFile UploadedFile { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string StorageKey { get; set; }

        public bool IsLocked { get; set; }

        public long LockedByUserID { get; set; }

        public long LoggedInUserID { get; set; }

        public bool HasDocument { get; set; }

    }
}
