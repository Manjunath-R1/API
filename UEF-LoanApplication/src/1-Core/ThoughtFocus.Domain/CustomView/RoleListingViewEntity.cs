namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Master;

    [Serializable]
    public class RoleListingViewEntity
    {
        #region Properties

        public List<MenuEntity> listOfMenuEnity
        {
            get; set;
        }

        public string MenuID
        {
            get;
            set;
        }

        public string MenuName
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

        public bool IsPresent
        {
            get;
            set;
        }

        public bool IsRemoved
        {
            get;
            set;
        }

        public bool IsAdded
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