using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("DocumentTypes", Schema = "Master")]
    public partial class DocumentType
    {
        public DocumentType()
        {
            this.ApplicationDocuments = new List<ApplicationDocument>();
        }
        [Key]
        public int DocumentTypeID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> DocumentCategoryID { get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }
        public long DisplayOrder { get; set; }
        public virtual ICollection<ApplicationDocument> ApplicationDocuments { get; set; }
    }
}
