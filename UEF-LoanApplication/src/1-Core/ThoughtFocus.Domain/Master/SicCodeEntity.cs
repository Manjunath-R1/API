namespace ThoughtFocus.Domain.Master
{
    public class SicCodeEntity :  IEntity
    {
        #region Properties

        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public long? DisplayOrder
        {
            get;
            set;
        }

        #endregion Properties
    }
}