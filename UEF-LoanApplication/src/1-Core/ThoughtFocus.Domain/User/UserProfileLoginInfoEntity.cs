namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class UserProfileLoginInfoEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public DateTime? AccountActivationDate
        {
            get;
            set;
        }

        public long? FailedLoginAttemptCount
        {
            get;
            set;
        }

        public DateTime? FailedLoginAttemptDateTime
        {
            get;
            set;
        }

        public long? FailedPasswordAttemptCount
        {
            get;
            set;
        }

        public DateTime? FailedPasswordAttemptDate
        {
            get;
            set;
        }

        public DateTime FailedPasswordAttemptDateTime
        {
            get;
            set;
        }

        public DateTime? FirstLoginDateTime
        {
            get;
            set;
        }

        public bool IsAccountActivated
        {
            get;
            set;
        }

        public bool IsLockedOut
        {
            get;
            set;
        }

        public DateTime? LastLockoutDateTime
        {
            get;
            set;
        }

        public DateTime? LastLoginDateTime
        {
            get;
            set;
        }

        public DateTime? LastLogoutDateTime
        {
            get;
            set;
        }

        //public long LastModifiedByUserID
        //{
        //    get;
        //    set;
        //}

        public DateTime LastPasswordChangedDateTime
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        #endregion Properties
    }
}