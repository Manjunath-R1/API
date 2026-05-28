namespace ThoughtFocus.Domain.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class UserViewEntity : BaseAbstractEntity
    {
        #region Properties

        

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string UserName
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

        [Compare("Password", ErrorMessage = "Password and confirm password Don't match.")]
        public String ConfirmPassword
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

        [Required(ErrorMessage = "Please enter Security Answer.")]
        public string SecurityAnswer
        {
            get;
            set;
        }
        
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
    }
}