using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("ResponseType", Schema = "Master")]
    public partial class ResponseType			
    {
        [Key]
        public int TypeID { get; set; }
        public string QuestionType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    }
}