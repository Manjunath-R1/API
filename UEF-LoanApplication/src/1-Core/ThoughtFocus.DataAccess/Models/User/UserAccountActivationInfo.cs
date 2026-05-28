using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Table("UserAccountActivationInfo", Schema = "User")]
    public partial class UserAccountActivationInfo : AuditBase
    {
        [Key]
        public long UserAccountActivationInfoID { get; set; }
        public long UserID { get; set; }
        public Nullable<System.DateTime> AccountActivationDate { get; set; }
        public string TokenID { get; set; }
        public bool IsExpiry { get; set; }
        public bool IsComplete { get; set; }
    }
}