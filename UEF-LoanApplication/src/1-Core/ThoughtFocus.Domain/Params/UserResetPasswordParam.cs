namespace ThoughtFocus.Domain.Params
{
    using System; 
    using ThoughtFocus.Domain.User;

    [Serializable]
    public class UserResetPasswordParam
    {
        #region Properties

        public String ConfirmPassword
        {
            get;
            set;
        }

        public string DecryptedTokenID
        {
            get;
            set;
        }

        public string EncryptedTokenID
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

        public string StatusMessage
        {
            get;
            set;
        }

        public UserEntity UserEntity
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public UserResetPasswordStatus UserResetPasswordStatus
        {
            get;
            set;
        }
        public UserPasswordResetInfoEntity UserPasswordResetInfoEntity
        {
            get;
            set;
        }

        public UserProfileLoginInfoEntity UserProfileLoginInfoEntity
        {
            get;
            set;
        }
        // public AuditTrailPasswordHistoryEntity AuditTrailPasswordHistoryEntity
        // {
        //     get;
        //     set;
        // }


        #endregion Properties
    }
}