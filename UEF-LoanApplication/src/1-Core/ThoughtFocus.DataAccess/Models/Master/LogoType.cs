using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Master

{
    [Table("LogoTypes", Schema = "Master")]
    public partial class LogoType 

    {
        [Key]
        public long LogoTypeID { get; set; }
        public string LogoTypeName { get; set; }
        public bool IsActive { get; set; }

        public long DisplayOrder { get; set; }

    }
}