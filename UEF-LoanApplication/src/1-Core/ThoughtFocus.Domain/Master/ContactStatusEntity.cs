namespace ThoughtFocus.Domain.Master
{
    public class ContactStatusEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long ContactStatusID
        {
            get;
            set;
        }

        public string ContactStatusName
        {
            get;
            set;
        }

        public long DisplayOrder
        {
            get;
            set;
        }

        #endregion Properties
    }
}