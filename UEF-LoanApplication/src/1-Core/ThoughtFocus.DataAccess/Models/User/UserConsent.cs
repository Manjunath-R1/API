using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Table("UserConsent", Schema = "User")]
    public partial class UserConsent
    {
        [Key]
        public long UserConsentID { get; set; }
        public long UserID { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> ConsentDateTime { get; set; }
        public virtual User User { get; set; }
    }
}

