using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Table("UserProfileLoginInfo", Schema = "User")]
    public partial class UserProfileLoginInfo : AuditBase
    {
        [Key]
        public long UserID { get; set; }
        public bool IsAccountActivated { get; set; }
        public bool IsLockedOut { get; set; }
        public System.DateTime LastPasswordChangedDateTime { get; set; }
        public Nullable<System.DateTime> FirstLoginDateTime { get; set; }
        public Nullable<System.DateTime> LastLoginDateTime { get; set; }
        public Nullable<System.DateTime> LastLogoutDateTime { get; set; }
        public Nullable<System.DateTime> LastLockoutDateTime { get; set; }
        public Nullable<long> FailedLoginAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedLoginAttemptDateTime { get; set; }
        public Nullable<long> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptDateTime { get; set; }
        public Nullable<System.DateTime> AccountActivationDate { get; set; }
    }
}