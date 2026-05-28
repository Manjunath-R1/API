namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class RUserRoleEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long RoleID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        public long UserRoleID
        {
            get;
            set;
        }

        #endregion Properties
    }
}