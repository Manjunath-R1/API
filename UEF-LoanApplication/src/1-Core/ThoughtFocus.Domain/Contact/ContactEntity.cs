namespace ThoughtFocus.Domain.Contact
{
    using System;

    [Serializable]
    public class ContactEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long ContactID
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
        public long SalutationID
        {
            get;
            set;
        }
        public string SalutationName
        {
            get;
            set;
        }
        public string BusinessName
        {
            get;
            set;
        }
        public long AccountStatusID
        {
            get;
            set;
        }
        public string AccountStatusName
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
        public long AffiliateID
        {
            get;
            set;
        }
        public string AffiliateName
        {
            get;
            set;
        }
        public long BusinessRoleID
        {
            get;
            set;
        }
        public long BusinessRoleName
        {
            get;
            set;
        }
        public string ProgramName
        {
            get;
            set;
        }
        
        #endregion Properties
    }
}