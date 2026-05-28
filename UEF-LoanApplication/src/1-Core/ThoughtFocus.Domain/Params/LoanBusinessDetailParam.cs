using System;
using System.Collections.Generic;


namespace ThoughtFocus.Domain.Params
{
    public class LoanBusinessDetailParam
    {

        #region Properties
        public long ID {get; set;}
        public long LoanApplicationID​​​​​​​​ {get; set;}
        public string BusinessName { get; set; }
        public Nullable<long> BusinessTypeID { get; set; }
        public string BusinessTypeName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EIN { get; set; }
        public string DBA { get; set; }
        public string StartDate { get; set; }
        public Nullable<long> IndustryTypeID { get; set; }
        public string IndustryTypeName { get; set; }
        public Nullable<long> EmployeeStrength { get; set; }
        public Nullable<long> NumberOfYearsInBusiness { get; set; }
        public Nullable<long> AverageMonthlyPayroll { get; set; }
        public string DUNS { get; set; }
        public Nullable<long> AffiliateID { get; set; }
        public string AffiliateName { get; set; }
        public string Url { get; set; }
        public Nullable<long> SIC_ID { get; set; }
        public string SIC_Name { get; set; }
        public Nullable<long> NAICS_ID { get; set; }
        public string NaicsCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<long> StateID { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankRoutingNumber { get; set; }
        public string Comment { get; set; }
        

        #endregion Properties
    }
}
