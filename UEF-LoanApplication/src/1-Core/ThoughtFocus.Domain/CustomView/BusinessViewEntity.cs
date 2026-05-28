namespace ThoughtFocus.Domain.CustomView
{
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;
    using ThoughtFocus.Domain.User;

    public class BusinessViewEntity
    {
        #region Properties

        public long ID { get; set; }
        public string EIN { get; set; }
        public int AffiliateID { get; set; }
        public string BusinessName { get; set; }
        public int BusinessTypeID { get; set; }

        
        #endregion Properties

    }
}