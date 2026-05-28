namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class NewPasswordChangeRequest
    {
        #region Properties

     [Required(ErrorMessage="Please enter your new password.")]
    public string NewPassword { get; set; }

    public String TokenID  { get; set; }

        #endregion Properties
    }
}