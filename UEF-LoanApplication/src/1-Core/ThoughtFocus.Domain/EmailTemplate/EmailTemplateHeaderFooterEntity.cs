using System;

namespace ThoughtFocus.Domain.EmailTemplate
{
    public class EmailTemplateHeaderFooterEntity
    {
        public int EmailNotificationHeaderFooterID { get; set; }
        public Guid LogoID { get; set; }
        public long DocumentTypeID { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }

        public bool IsActive { get; set; }

        public string LogoName { get; set; }

        public string Footer { get; set; }

        public string LogoPath { get; set; }

        public decimal? CurrentVersion { get; set; }

        public long UserID { get; set; }

        public string FolderName { get; set; }

        public string ImageKey { get; set; }

    }
}
