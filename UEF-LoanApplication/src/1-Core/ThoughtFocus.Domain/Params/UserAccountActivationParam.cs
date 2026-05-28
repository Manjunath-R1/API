namespace ThoughtFocus.Domain.User
{
    using System;
    using System.ComponentModel.DataAnnotations;
 

    [Serializable]
    public class UserAccountActivationParam : BaseAbstractEntity, IEntity
    {
        #region Properties

        [Compare("Password", ErrorMessage = "Password and confirm password Don't match.")]
        public String ConfirmPassword
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter Password.")]
        public String Password
        {
            get;
            set;
        }

        // public PostUserAccountCreationQueueEntity PostUserAccountCreationQueueEntity
        // {
        //     get;
        //     set;
        // }

        [Required(ErrorMessage = "Please enter Security Answer.")]
        public string SecurityAnswer
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please select the Security Question.")]
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

        public string TokenID
        {
            get;
            set;
        }

        public UserAccountActivationStatus UserAccountActivationStatus
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
        public UserAccountActivationInfoEntity UserAccountActivationInfoEntity
        {
            get;
            set;
        }
        // public AuditTrailPasswordHistoryEntity AuditTrailPasswordHistoryEntity
        // {
        //     get;
        //     set;
        // }
        public long ContactID
        {
            get;
            set;
        }
        public long RoleID
        {
            get;
            set;
        }


        #endregion Properties

        public bool ShowCaptcha { get; set; }

        public Guid IdentityId { get; set; }
    }
}