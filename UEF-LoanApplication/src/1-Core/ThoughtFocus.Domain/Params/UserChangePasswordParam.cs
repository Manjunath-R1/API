namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class UserChangePasswordParam
    {
        #region Fields

        private string statusMessage;
        private UserChangePasswordStatus userChangePasswordStatus;

        #endregion Fields

        #region Constructors

        public UserChangePasswordParam()
        {
        }

        public UserChangePasswordParam(UserChangePasswordStatus userChangePasswordStatus, string statusMessage)
        {
            this.userChangePasswordStatus = userChangePasswordStatus;
            this.statusMessage = statusMessage;
        }

        #endregion Constructors

        #region Properties

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

        public String OldPassword
        {
            get;
            set;
        }

        public String NewPassword
        {
            get;
            set;
        }

        public String ConfirmPassword
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please select a secutity question.")]
        public long SecurityQuestionID
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter a secutity answer.")]
        public string SecurityAnswer
        {
            get;
            set;
        }

        public string StatusMessage
        {
            get;
            set;
        }

        public UserChangePasswordStatus UserChangePasswordStatus
        {
            get;
            set;
        }

        #endregion Properties
    }
}
