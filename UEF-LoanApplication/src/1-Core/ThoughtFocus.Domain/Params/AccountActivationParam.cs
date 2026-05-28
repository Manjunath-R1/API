namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class AccountActivationParam
    {
        #region Properties

        [Required(ErrorMessage = "Please enter ContactID.")]
        public long ContactID { get; set; }

        [Required(ErrorMessage = "Please enter UserID.")]
         public long UserID { get; set; }
       

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

        
        [Required(ErrorMessage = "Please enter TokenID.")]
        public String TokenID
        {
            get;
            set;
        }


        
        #endregion Properties
    }
}