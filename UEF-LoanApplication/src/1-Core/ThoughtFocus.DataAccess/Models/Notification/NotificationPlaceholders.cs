using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Notification
{
    [Table("NotificationPlaceholders", Schema = "Notification")]
    public partial class NotificationPlaceholders
    {
        [Key]
        public long NotificationPlaceholderID { get; set; }
      

         [ForeignKey("Notification")]

        public long NotificationID { get; set; }

         [ForeignKey("Placeholders")]
        public long PlaceholderID { get; set; }

         public bool IsActive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
       
       public virtual ThoughtFocus.DataAccess.Models.Master.Notification Notification { get; set; }
       public virtual EmailTemplatePlaceholders Placeholders { get; set; }
    }
}
