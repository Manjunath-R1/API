namespace ThoughtFocus.Domain.Master
{
    using System;

    [Serializable]
    public class SubMenuEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public int DisplayOrder
        {
            get;
            set;
        }

        public long MenuID
        {
            get;
            set;
        }

        public string SubMenuDescription
        {
            get;
            set;
        }

        public string SubMenuIconClass
        {
            get;
            set;
        }

        public long SubMenuID
        {
            get;
            set;
        }

        public string SubMenuName
        {
            get;
            set;
        }

        public string SubMenuURL
        {
            get;
            set;
        }

        #endregion Properties
    }
}