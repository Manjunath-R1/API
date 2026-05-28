using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("BusinessType", Schema = "Master")]
    public partial class BusinessType
    {
        [Key]
        public long BusinessTypeID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    }
}