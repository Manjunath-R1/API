using System;


namespace ThoughtFocus.Domain.Params
{
    public class BusinessOwnerParam
    {
        #region Properties
        public Nullable<long> ID { get; set; }
        public Nullable<long> LoanApplicationID { get; set; }
        public string BusinessOwnerName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber  { get; set; }
        public Nullable<decimal> OwnedPercentage { get; set; }
        public Nullable<long> VeteranID { get; set; }
        public string VeteranName { get; set; }
        public Nullable<long> GenderID { get; set; }
        public string GenderName { get; set; }
        public Nullable<long> RaceID { get; set; }
        public string RaceName { get; set; }
        public Nullable<long> EthnicityID { get; set; }
        public string EthnicityName { get; set; }
        public string Demographic  { get; set; }

        #endregion Properties
    }
}
