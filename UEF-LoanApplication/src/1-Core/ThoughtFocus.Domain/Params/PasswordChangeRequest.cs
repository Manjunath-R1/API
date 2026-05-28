namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class PasswordChangeRequest
    {
        #region Properties
    public long ContactID {get; set;}

    [Required(ErrorMessage="Please enter your Cuttent Password")]
    public string ExistingPassword { get; set; }

     [Required(ErrorMessage="Please enter your New Password")]
    public string NewPassword { get; set; }

        #endregion Properties
    }
}