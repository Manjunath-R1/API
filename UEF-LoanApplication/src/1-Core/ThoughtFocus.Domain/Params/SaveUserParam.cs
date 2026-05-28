namespace ThoughtFocus.Domain.Params
{
    using System; 
    using ThoughtFocus.Domain.User; 

    [Serializable]
    public class SaveUserParam
    {
        #region Properties

        // public AccountActivationQueueEntity AccountActivationQueueEntity
        // {
        //     get;
        //     set;
        // }

        public long ContactID
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public long GenderID
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public long RoleID
        {
            get;
            set;
        }

        public RUserContactEntity RUserContactEntity
        {
            get;
            set;
        }

        public RUserRoleEntity RUserRoleEntity
        {
            get;
            set;
        }

        public long SalutationID
        {
            get;
            set;
        }

        public UserAccountActivationInfoEntity UserAccountActivationInfoEntity
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

        public UserProfileLoginInfoEntity UserProfileLoginInfoEntity
        {
            get;
            set;
        }

        public UserSecurityQuestionInfoEntity UserSecurityQuestionInfoEntity
        {
            get;
            set;
        }

        #endregion Properties
    }
}