using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Action", Schema = "Master")]
    public partial class Action
    {
        [Key]
        public int ActionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActionClass { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    
    }

}