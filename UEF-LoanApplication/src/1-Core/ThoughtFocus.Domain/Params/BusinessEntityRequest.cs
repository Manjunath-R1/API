namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class BusinessEntityRequest
    {
        #region Properties
        public long ID {get; set;}
        public string BusinessName { get; set; }
        public string EIN { get; set; }
        public Nullable<int> BusinessTypeID { get; set; }
        public int AffiliateID { get; set; }
               
        #endregion Properties
    }
}