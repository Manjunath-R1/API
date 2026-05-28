using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("SecurityQuestion", Schema = "Master")]
    public partial class SecurityQuestion
    {
        [Key]
        public long SecurityQuestionID { get; set; }
        public string SecurityQuestionName { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    }
}