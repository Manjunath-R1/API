namespace ThoughtFocus.Domain.Master
{
    public class  IndustryTypeEntity:  IEntity
    {
        #region Properties

        public long? DisplayOrder
        {
            get;
            set;
        }

        public long IndustryTypeID
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }
        
        #endregion Properties
    }
}