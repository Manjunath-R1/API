namespace ThoughtFocus.Domain.CustomView
{
    using System;

    [Serializable]
    public class UserListingViewEntity
    {
        #region Properties

        public long ContactID
        {
            get;
            set;
        }

        public string ContactStatus
        {
            get;
            set;
        }

        public string CountryCallingCode
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public bool? IsActive
        {
            get;
            set;
        }

        public bool? IsComplete
        {
            get;
            set;
        }

        public bool? IsAccountActivated
        {
            get;
            set;
        }

        public bool? IsLockedOut
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string MobileNumber
        {
            get;
            set;
        }

        public string OrganizationName
        {
            get;
            set;
        }

        public string RoleName
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

        public long UserID
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        #endregion Properties
    }
}