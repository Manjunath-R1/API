namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class ForgotPasswordRequest
    {
     #region Properties

    [Required(ErrorMessage="Please enter email id.")]
    public string EmailId { get; set; }
    public long ContactID { get; set; }

     #endregion Properties
    }
}