namespace ThoughtFocus.Domain.AffiliateContact
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;

    //[Serializable]
    public class AffiliateContactEntity
    {
        #region Properties
  
      public string AffiliateName
        {
            get;
            set;
        }
      public string AffiliateAddress
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

       public string PhoneNumber
        {
            get;
            set;
        }
        public string EmailAddress
        {
            get;
            set;
        }

        public string Active
        {
            get;
            set;
        }
        public long AffiliateID
        {
            get;
            set;
        }


        #endregion Properties
    }
}