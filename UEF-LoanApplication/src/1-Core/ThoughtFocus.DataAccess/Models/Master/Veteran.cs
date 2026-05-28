using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Veteran", Schema = "Master")]
    public partial class Veteran			
    {
        [Key]
        public long VeteranID { get; set; }
        public string VeteranType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    }
}