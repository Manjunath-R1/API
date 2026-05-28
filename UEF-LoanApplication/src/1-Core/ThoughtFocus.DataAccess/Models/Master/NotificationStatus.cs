using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("NotificationStatus", Schema = "Master")]
    public class NotificationStatus
    {
        [Key]
        public long NotificationStatusID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}