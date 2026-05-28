using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{  

    [Table("NAICS", Schema = "Master")]
    public class NAICS
    {
        [Key]
        public long ID { get; set; }
        public string Code { get; set; }
        public string IndustryTitle { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    
    }
}