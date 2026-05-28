namespace ThoughtFocus.Domain.CustomView
{
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;
    using ThoughtFocus.Domain.User;

    public class BusinessContactViewEntity
    {
        #region Properties

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
        public long BusinessID { get; set; }
        public long BusinessRoleID { get; set; }
        public string BusinessRoleName { get; set; }


        #endregion Properties




    }
}