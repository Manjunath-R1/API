namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class AffiliateContactRequest
    {
        #region Properties

         public long AffiliateID { get; set; }

        [Required(ErrorMessage="Please enter affiliate name")]
        public string AffiliateName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

       public string PhoneNumber { get; set; }

       public string AffiliateAddress { get; set; }
 

        
       
        #endregion Properties
    }
}