using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("EmailTemplatePlaceholders", Schema = "Notification")]
    public partial class EmailTemplatePlaceholders
    {
        [Key]
        public long PlaceholderID { get; set; }        
        public string DisplayName { get; set; }
        public string Placeholder { get; set; }
        public string Description { get; set; }

        [ForeignKey("PlaceHolderType")]
        public Nullable<long> PlaceHolderTypeID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public virtual EmailTemplatePlaceholderType PlaceHolderType { get; set; }
    }
}
