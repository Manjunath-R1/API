namespace ThoughtFocus.Domain.Master
{
    public class VeteranEntity :  IEntity
    {
        #region Properties

        public long? DisplayOrder
        {
            get;
            set;
        }

        public long VeteranID
        {
            get;
            set;
        }

        public string VeteranType
        {
            get;
            set;
        }
        
        #endregion Properties
    }
}