using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.DataAccess.Models.Master
{
    
    [Table("GenaralOption", Schema = "Master")]
    public partial class GenaralOption
    {

        [Key]
        public long OptionID { get; set; }
        public string OptionValue { get; set; }
        public string OptionCategory { get; set; }
        public string OptionDescription { get; set; }
        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }
    
        
    }
}