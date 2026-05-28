using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
  [Table("ProgramStatus", Schema = "Master")]
    public class ProgramStatus
    {
        [Key]
        public long ProgramStatusID { get; set; }
        public string ProgramStatusName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    
    }
}