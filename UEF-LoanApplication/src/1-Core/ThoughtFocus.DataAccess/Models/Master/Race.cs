using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Race", Schema = "Master")]
    public partial class Race			
    {
        [Key]
        public long RaceID { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    }
}