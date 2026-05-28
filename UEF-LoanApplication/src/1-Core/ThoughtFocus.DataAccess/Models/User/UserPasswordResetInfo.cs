using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Table("UserPasswordResetInfo", Schema = "User")]
    public partial class UserPasswordResetInfo : AuditBase
    {
        [Key]
        public long UserPasswordResetInfoID { get; set; }
        public long UserID { get; set; }
        public Nullable<System.DateTime> PasswordResetDate { get; set; }
        public string TokenID { get; set; }
        public bool IsExpiry { get; set; }
        public bool IsComplete { get; set; }
        public string IsAdminOrSelf { get; set; }

    }
}