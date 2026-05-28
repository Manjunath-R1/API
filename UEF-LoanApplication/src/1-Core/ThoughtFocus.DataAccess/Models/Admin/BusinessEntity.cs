using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("BusinessEntity", Schema = "Admin")]
    public partial class BusinessEntity
    {
        [Key]
        public long ID { get; set; }
        public string BusinessName { get; set; }
        
        [ForeignKey("BusinessType")]
        public Nullable<long> BusinessTypeID { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EIN { get; set; }
        public string DBA { get; set; }
        public string Url { get; set; }
        public string SIC { get; set; }
        public string NAICS { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

        [ForeignKey("IndustryType")]
        public Nullable<long> IndustryTypeID { get; set; }
        public long EmployeeStrength { get; set; }
        public long NumberOfYearsInBusiness { get; set; }
        public long AverageMonthlyPayroll { get; set; }
        public string DUNS { get; set; }

        [ForeignKey("UrbanLeagueAffiliate")]
        public long AffiliateID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [ForeignKey("State")]
        public Nullable<long> StateID { get; set; }
        public string Zip { get; set; }
        public long BankAccountNumber { get; set; }
        public long BankRoutingNumber { get; set; }
        public string Comment { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public bool CiviCRMExportFlag {get; set;}
        public virtual BusinessType BusinessType { get; set; }
        public virtual IndustryType IndustryType { get; set; }
        public virtual UrbanLeagueAffiliate Affiliate { get; set; }
        public virtual State State { get; set; }
    }
}