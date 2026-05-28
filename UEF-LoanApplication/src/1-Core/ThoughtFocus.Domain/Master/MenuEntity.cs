namespace ThoughtFocus.Domain.Master
{
    public class MenuEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public int DisplayOrder
        {
            get;
            set;
        }

        public string MenuDescription
        {
            get;
            set;
        }

        public string MenuIconClass
        {
            get;
            set;
        }

        public long MenuID
        {
            get;
            set;
        }

        public string MenuName
        {
            get;
            set;
        }

        public string MenuURL
        {
            get;
            set;
        }

        #endregion Properties
    }
}