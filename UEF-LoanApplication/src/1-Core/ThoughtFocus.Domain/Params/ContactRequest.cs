namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class ContactRequest
    {
        #region Properties

        public long ContactID { get; set; }
    
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long SalutationID { get; set; }
        public long AccountStatusID { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public long BusinessID { get; set; }
        public long BusinessRoleID { get; set; }
        public bool IsActive { get; set; }
        public string AdditionalMessage { get; set; }
        public List<int> UserRoles {get; set;}
        public List<long> ProgramInvitations { get; set; }
        
       
        #endregion Properties
    }

    public class ProgramInvitationInfo
    {
        public long ProgramID { get; set; }
        public bool IsSelected { get; set; }
    }
}