using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("DocumentCategorys", Schema = "Master")]
    public partial class DocumentCategory
    {
        public DocumentCategory()
        {
            this.DocumentTypes = new List<DocumentType>();
        }
        [Key]
        public int DocumentCategoryID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
