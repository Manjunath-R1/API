namespace ThoughtFocus.Domain.CustomView
{
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;
    using ThoughtFocus.Domain.User;

    public class ContactViewEntity
    {
        #region Properties

        public ContactViewEntity()
        {
            AssociatedBusinesses = new List<BusinessInfo>();

        }
        public long ContactID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long SalutationID { get; set; }
        public string SalutationName { get; set; }
        public long AccountStatusID { get; set; }
        public string AccountStatusName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
         public bool CanDelete { get; set; }

        public List<BusinessInfo> AssociatedBusinesses { get; set; }
        #endregion Properties
    }

    public class BusinessInfo
    {
        public long BusinessID { get; set; }
        public string BusinessName { get; set; }
        public string BusinessRole { get; set; }
        public string Status { get; set; }

    }
}