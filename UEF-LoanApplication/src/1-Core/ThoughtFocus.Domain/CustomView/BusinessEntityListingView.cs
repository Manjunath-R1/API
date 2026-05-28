namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;

    //[Serializable]
    public class BusinessEntityListingView
    {
        #region Properties

        public long ID
        {
            get;
            set;
        }       

        public string BusinessName
        {
            get;
            set;
        }
        public string EIN
        {
            get;
            set;
        }
        public long AffiliateID
        {
            get;
            set;
        }
         public string AffiliateName
        {
            get;
            set;
        }
        public long BusinessTypeID
        {
            get;
            set;
        }
    
        public string BusinessTypeName
        {
            get;
            set;
        }
    

        #endregion Properties
    }
}