using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.DataAccess.Models.Master
{
    
    [Table("NotificationModes", Schema = "Master")]
    public partial class NotificationMode
    {

        [Key]
        public long ID { get; set; }
        public string ModeType { get; set; }
        public bool IsActive { get; set; }
        
    }
}