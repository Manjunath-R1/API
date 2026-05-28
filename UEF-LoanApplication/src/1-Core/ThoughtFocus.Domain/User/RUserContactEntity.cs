namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class RUserContactEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long ContactID
        {
            get;
            set;
        }

        public long UserContactID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        #endregion Properties
    }
}