using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("SMSNotificationTemplate", Schema = "Master")]
    public partial class SMSNotificationTemplate
    {
        [Key]
        public long ID { get; set; }
        public string Template { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ApplicationStatus")]
        public int ApplicationStatusID { get; set; }

        public virtual ThoughtFocus.DataAccess.Models.Master.ApplicationStatus ApplicationStatus { get; set; }

    }
}
