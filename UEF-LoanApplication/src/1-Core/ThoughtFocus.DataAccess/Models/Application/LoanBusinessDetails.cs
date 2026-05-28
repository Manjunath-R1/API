using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Application
{
    [Table("LoanBusinessDetail", Schema = "Application")]
    public partial class LoanBusinessDetail			
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }
        public string BusinessName { get; set; }
        
        [ForeignKey("BusinessType")]
        public long BusinessTypeID { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EIN { get; set; }
        public string DBA { get; set; }

        public string Url { get; set; }
        public string FacebookUrl { get; set; }

        [ForeignKey("SIC")]
        public Nullable<long> SIC_ID { get; set; }

        [ForeignKey("NAICS")]
        public Nullable<long> NAICS_ID { get; set; }
        public DateTime StartDate { get; set; }

        [ForeignKey("IndustryType")]
        public long IndustryTypeID { get; set; }
        public long EmployeeStrength { get; set; }
        public long NumberOfYearsInBusiness { get; set; }
        public long AverageMonthlyPayroll { get; set; }
        public string DUNS { get; set; }

        [ForeignKey("UrbanLeagueAffiliate")]
        public long AffiliateID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [ForeignKey("State")]
        public long StateID { get; set; }
        public string Zip { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankRoutingNumber { get; set; }
        public string Comment { get; set; }

        public virtual BusinessType BusinessType { get; set; }
        public virtual IndustryType IndustryType { get; set; }
        public virtual UrbanLeagueAffiliate Affiliate { get; set; }
        public virtual State State { get; set; }
        public virtual LoanApplication LoanApplication { get; set; }

        public virtual NAICS NAICS { get; set; }
        public virtual SIC SIC { get; set; }
        public string NaicsCode { get; set; }
        
    }
}