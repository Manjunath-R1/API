using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class UserEntity
    {
        public long UserID { get; set; }
        public Guid UserGuID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

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
