using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("EmailTemplateHeaderFooter", Schema = "Notification")]
    public class EmailTemplateHeaderFooter
    {
        [Key]
        public long EmailTemplateHeaderFooterID { get; set; }
       
        public Nullable<long> DocumentTypeID { get; set; }
        public Guid LogoGUID { get; set; }
        public long LogoID { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(2000)]
        public string Footer { get; set; }

        public string ImageKey { get; set; }
        public string FolderName { get; set; }

         public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
    }
}
