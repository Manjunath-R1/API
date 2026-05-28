using System;
using System.Web;

namespace ThoughtFocus.Domain.CustomView
{
    public class EmailTemplateHeaderFooterViewModel
    {
        public long EmailTemplateHeaderFooterID { get; set; }

        public Guid LogoID { get; set; }

        public long? DocumentTypeID { get; set; }

        public string LogoName { get; set; } //Uploaded FileName

        public string versionNumber { get; set; } //Uploaded Version

        public string LogoPath { get; set; } // Uploaded FilePath

        public string UploadDate { get; set; } //Uploaded Date

        public Guid DocumentKey { get; set; }

        public string StorageKey { get; set; }

        public string DocumentVersionkey { get; set; }

        public Guid? DocumentVersionID { get; set; }

        public string Footer { get; set; }

        public bool IsActive { get; set; }

        public bool Uploading { get; set; }

        public bool CanUpload { get; set; }

        public bool CanView { get; set; }

        public bool CanViewHistory { get; set; }

        public bool SaveUploaded { get; set; }

        public bool CancelUploaded { get; set; }

      //  public HttpPostedFileBase UploadedFile { get; set; }

        public DateTime CreatedDateTime { get; set; }

    }
}
