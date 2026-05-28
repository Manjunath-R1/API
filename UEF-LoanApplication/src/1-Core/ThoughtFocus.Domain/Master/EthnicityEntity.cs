namespace ThoughtFocus.Domain.Master
{
    public class EthnicityEntity : IEntity
    {
        #region Properties

        public long DisplayOrder
        {
            get;
            set;
        }

        public long EthnicityID
        {
            get;
            set;
        }

        public string EthnicityName
        {
            get;
            set;
        }

        #endregion Properties
    }
}