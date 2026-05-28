using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace ThoughtFocus.Domain
{
    public class DocumentViewModel
    {
        public string FileName { get; set; } //Uploaded FileName

        public string VersionNumber { get; set; } //Uploaded Version

        public string FilePath { get; set; } // Uploaded FilePath

        public string UploadDate { get; set; } //Uploaded Date

        public int DocumentTypeID { get; set; }

        public string DocumentName { get; set; }

        public string DocumentTypeName { get; set; }

        public string DocumentVersionkey { get; set; }

        public Guid DocumentKey { get; set; }

        public Guid? DocumentVersionID { get; set; }

        public Guid DocumentID { get; set; }

        public Guid FileExtensionTypeID { get; set; }

        public IFormFile UploadedFile { get; set; }

        public string NavigationUrl { get; set; }

        public Guid ParentId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }

        public string FileImagePath { get; set; }

        public string Number { get; set; }

        public string Content { get; set; }

        public bool IsLocked { get; set; }

        public long LockedByUserID { get; set; }

        public long LoggedInUserID { get; set; }

        public double FileSize { get; set; }

        public string FileContent { get; set; }

        public Stream FileStream { get; set; }
    }
}
