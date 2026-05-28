using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("HelpfulGuideTemplates", Schema = "Master")]
    public partial class HelpfulGuideTemplate
    {
        [Key]
        public long ID { get; set; }
        public string Body { get; set; }

        [ForeignKey("TemplateType")]
        public long TypeID { get; set; }
        public bool IsActive { get; set; }
        public virtual TemplateType TemplateType { get; set; }

    }

}