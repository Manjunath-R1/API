namespace ThoughtFocus.Domain.User
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class UserEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long ContactID
        {
            get;
            set;
        }
        public Guid IdentityId { get; set; }

        public string ContactStatusID
        {
            get;
            set;
        }

        [Required]
        [Compare("UserName", ErrorMessage = "Username and Email Address Don't match")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string EmailAddress
        {
            get;
            set;
        }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "First Name must be between 3 and 50 characters!")]
        public string FirstName
        {
            get;
            set;
        }

        // public long GenderID
        // {
        //     get;
        //     set;
        // }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(150, MinimumLength = 3,
           ErrorMessage = "Last Name must be between 3 and 50 characters!")]
        public string LastName
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public string PasswordSalt
        {
            get;
            set;
        }

        public long SalutationID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        [Required]
        [EmailAddress(ErrorMessage = "Enter correct UserName")]
        public string UserName
        {
            get;
            set;
        }
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

        public DateTime LastPasswordChangedDateTime
        {
            get;
            set;
        }

        #endregion Properties
    }
}