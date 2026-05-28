using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("EmailTemplatePreCondition", Schema = "Master")]
    public class EmailTemplatePreCondition
    {
        [Key]
        public long EmailTemplatePreConditionID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }        
        public bool IsActive { get; set; }
    }
}
