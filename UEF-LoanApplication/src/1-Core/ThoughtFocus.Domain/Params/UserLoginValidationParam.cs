namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class UserLoginValidationParam
    {
        #region Fields

        private string statusMessage;
        private UserLoginStatus userLoginStatus;

        #endregion Fields

        #region Constructors

        public UserLoginValidationParam(UserLoginStatus userLoginStatus, string statusMessage)
        {
            this.userLoginStatus = userLoginStatus;
            this.statusMessage = statusMessage;
        }

        #endregion Constructors

        #region Properties

        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }
            set
            {
                this.statusMessage = value;
            }
        }

        public UserLoginStatus UserLoginStatus
        {
            get
            {
                return this.userLoginStatus;
            }
            set
            {
                this.userLoginStatus = value;
            }
        }

        #endregion Properties
    }
}