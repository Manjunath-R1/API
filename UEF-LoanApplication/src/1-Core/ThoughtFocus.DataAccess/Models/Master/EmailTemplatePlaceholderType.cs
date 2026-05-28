using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("EmailTemplatePlaceholderType", Schema = "Master")]
    public partial class EmailTemplatePlaceholderType
    {
        public EmailTemplatePlaceholderType()
        {
            this.Placeholders = new List<EmailTemplatePlaceholders>();
        }

        [Key]
        public long PlaceHolderTypeID { get; set; }       
        public string PlaceHolderType { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmailTemplatePlaceholders> Placeholders { get; set; }
    }
}
