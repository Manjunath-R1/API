using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("ContactPersonType", Schema = "Master")]
    public partial class ContactPersonType
    {
        [Key]
        public long ContactPersonTypeID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string ContactPersonTypeName { get; set; }
        public long DisplayOrder { get; set; }
    }
}