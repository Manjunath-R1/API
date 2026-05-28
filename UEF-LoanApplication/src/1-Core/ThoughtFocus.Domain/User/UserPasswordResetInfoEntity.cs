namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class UserPasswordResetInfoEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public string IsAdminOrSelf
        {
            get;
            set;
        }

        public bool IsComplete
        {
            get;
            set;
        }

        public bool IsExpiry
        {
            get;
            set;
        }

        public DateTime? ResetPasswordDate
        {
            get;
            set;
        }

        public string TokenID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        public long UserPasswordResetInfoID
        {
            get;
            set;
        }

        #endregion Properties
    }
}