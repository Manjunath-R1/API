namespace ThoughtFocus.Domain.Master
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class RoleEntity : IEntity
    {
        #region Properties

        public int DisplayOrder
        {
            get;
            set;
        }

        public List<MenuEntity> listOfMenuEntity
        {
            get;
            set;
        }

        public string RoleDescription
        {
            get;
            set;
        }

        public long RoleID
        {
            get;
            set;
        }

        public string RoleName
        {
            get;
            set;
        }
        public bool IsLoginRole
        {
            get;
            set;
        }

        #endregion Properties
    }
}