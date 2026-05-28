using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("NotificationModesTypes", Schema = "Notification")]
    public class NotificationModesType
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("NotificationMode")]
        public long NotificationModesID { get; set; }
        [ForeignKey("User")]
        public long UserID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public virtual NotificationMode NotificationMode { get; set; }
        public virtual ThoughtFocus.DataAccess.Models.User.User User { get; set; }

    }
}
