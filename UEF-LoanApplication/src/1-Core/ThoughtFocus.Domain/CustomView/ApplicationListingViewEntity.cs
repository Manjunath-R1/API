namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    //[Serializable]
    public class ApplicationListingViewEntity
    {
        #region Properties
        public long LoanApplicationID { get; set; }
        [Display(Name = "Loan Number")]
        public string LoanNumber { get; set; }
        public string DateApplied { get; set; }
        public string BusinessName { get; set; }
        public string AffiliateName { get; set; }
        public string LoanProgramName { get; set; }
        [Display(Name = "Grant Type")]
        public string LoanType { get; set; }
        [Display(Name = "Grant Amount")]
        public string LoanAmount { get; set; }
        public string ApplicationStatus { get; set; }
        public long ApplicationStatusID { get; set; }

        [Display(Name = "Grant Amount (USD)")]
        public string LoanAmounts { get; set; }
        public string FundAllocatedAmount { get; set; } = "0";
        public string ContactInfo { get; set; }
        public long BusinessId { get; set; }

        #endregion Properties
    }
}