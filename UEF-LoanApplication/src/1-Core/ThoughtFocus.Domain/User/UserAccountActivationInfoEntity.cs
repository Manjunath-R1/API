namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class UserAccountActivationInfoEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public DateTime? AccountActivationDate
        {
            get;
            set;
        }

        public bool IsComplete
        {
            get;
            set;
        }

        public bool IsExpiry
        {
            get;
            set;
        }

        public string TokenID
        {
            get;
            set;
        }

        public long UserAccountActivationInfoID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        //public bool IsActive
        //{
        //    get;
        //    set;
        //}

        #endregion Properties
    }
}