using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("NotificationPreCondition", Schema = "Notification")]
    public class NotificationPreCondition
    {
        [Key]
        public long NotificationPreConditionID { get; set; }
          [ForeignKey("Notification")]
        public long NotificationID { get; set; }

        [ForeignKey("EmailTemplatePreCondition")]
        public long? EmailTemplatePreConditionID { get; set; }
        public bool IsActive { get; set; }
        
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        
        public virtual ThoughtFocus.DataAccess.Models.Master.Notification Notification { get; set; }
        public virtual EmailTemplatePreCondition EmailTemplatePreCondition { get; set; }
    }
}