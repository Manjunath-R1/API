namespace ThoughtFocus.Domain.CustomView
{
    using Newtonsoft.Json;
    using System.ComponentModel;
    
    //[Serializable]
    public class ApplicationListingViewEntityExportToExcel
    {
        #region Properties
        public long LoanApplicationID { get; set; }
        [DisplayName("Grant Number")]
        public string LoanNumber { get; set; }
        public string DateApplied { get; set; }
        public string BusinessName { get; set; }
        public string AffiliateName { get; set; }
        [DisplayName("Grant ProgramName")]
        public string LoanProgramName { get; set; }
        [DisplayName("Grant Type")]
        public string LoanType { get; set; }
            
        [DisplayName("GrantAmount (USD)")]
        public string LoanAmount { get; set; }
        public string ApplicationStatus { get; set; }
        [JsonIgnore]
        public long ApplicationStatusID { get; set; }
        public string FundAllocatedAmount { get; set; } = "0";
        public string ContactInfo { get; set; }


        #endregion Properties
    }
}