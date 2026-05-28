namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.ComponentModel.DataAnnotations; 
    using ThoughtFocus.Domain.User;

    [Serializable]
    public class UserForgotPasswordParam
    {
        #region Fields

        private string statusMessage;
        private UserForgotPasswordStatus userForgotPasswordStatus;

        #endregion Fields

        #region Constructors

        public UserForgotPasswordParam()
        {
        }

        public UserForgotPasswordParam(UserForgotPasswordStatus userForgotPasswordStatus, string statusMessage)
        {
            this.userForgotPasswordStatus = userForgotPasswordStatus;
            this.statusMessage = statusMessage;
        }

        #endregion Constructors

        #region Properties

        public bool IsAdmin
        {
            get;
            set;
        }

        // public PasswordResetQueueEntity PasswordResetQueueEntity
        // {
        //     get;
        //     set;
        // }

        [Required(ErrorMessage = "Please enter Security Answer")]
        public string SecurityAnswer
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please select Security Question")]
        public long SecurityQuestionID
        {
            get;
            set;
        }

        public string StatusMessage
        {
            get;
            set;
        }

        public UserForgotPasswordStatus UserForgotPasswordStatus
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        [Required(ErrorMessage="Please enter Username")]
        public string UserName
        {
            get;
            set;
        }

        public UserPasswordResetInfoEntity UserPasswordResetInfoEntity
        {
            get;
            set;
        }

        #endregion Properties
    }
}