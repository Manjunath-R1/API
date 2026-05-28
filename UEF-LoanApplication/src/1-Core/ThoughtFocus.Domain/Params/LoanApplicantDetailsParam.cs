
namespace ThoughtFocus.Domain.Params
{
    public class LoanApplicantDetailsParam
    {

        #region Properties
        public long BusinessRoleID { get; set; }
        public string SSN { get; set; } 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long SalutationID { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrentAddress { get; set; }
        public long StateID { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long ContactID { get; set; }

        #endregion Properties
    
    }
}
