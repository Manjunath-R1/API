namespace ThoughtFocus.Domain.Master
{
    public class AffiliateEntity :  IEntity
    {
        #region Properties

        public long? DisplayOrder
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
        
        #endregion Properties
    }
}