using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("TemplateTypes", Schema = "Master")]
    public partial class TemplateType
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

    }

}