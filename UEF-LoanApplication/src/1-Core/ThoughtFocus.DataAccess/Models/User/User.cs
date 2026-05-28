using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Contact;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Serializable]
    [Table("Users", Schema = "User")]
    public partial class User : AuditBase
    {
        // [Index(IsUnique = true)]
        public long UserID { get; set; }

        public Guid IdentityID { get; set; }
        public string UserName { get; set; }
        
        
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public long ContactID { get; set; }
        
        
        //Profile Information
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

        public virtual ThoughtFocus.DataAccess.Models.Contact.Contact Contact { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual UserConsent UserConsent { get; set; }
    }
}
