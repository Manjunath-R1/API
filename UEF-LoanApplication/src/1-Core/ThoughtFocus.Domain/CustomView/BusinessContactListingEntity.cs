namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;

    //[Serializable]
    public class BusinessContactListingEntity
    {
        #region Properties

        public long BusinessUserID
        {
            get;
            set;
        }

        public long BusinessID
        {
            get;
            set;
        }
        
        public long BusinessRoleID
        {
            get;
            set;
        }
        

        public long ContactID
        {
            get;
            set;
        }       

        public string AccountStatus
        {
            get;
            set;
        }
        
        
        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }


        public string PhoneNo
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public long SalutationID { get; set; }

        public bool IsActive { get; set; }

        
        public string BusinessRoleName
        {
            get;
            set;
        }

        public bool ShowActivationLink { get; set; }

        public long AccountStatusID
        {
            get;
            set;
        }

        #endregion Properties
    }
}