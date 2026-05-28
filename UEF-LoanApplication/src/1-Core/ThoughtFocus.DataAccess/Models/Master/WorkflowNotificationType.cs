using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("WorkflowNotificationType", Schema = "Master")]
    public partial class WorkflowNotificationType
    {
        public long WorkflowNotificationTypeID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string WorkflowNotificationTypeName { get; set; }
    }
}
