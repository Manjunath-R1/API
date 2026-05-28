namespace ThoughtFocus.Domain.CustomView
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Admin;
    using ThoughtFocus.DataAccess.Models.FundingSource;
    using ThoughtFocus.Domain.Params;
    using ThoughtFocus.Domain.Response;

    //[Serializable]
    public class ApplicationViewEntity
    {
        public ApplicationViewEntity()
        {
            LoanBusinessDetails = new LoanBusinessDetailParam();
            BusinessOwners = new List<BusinessOwnerParam>();
            FundingApplication = new FundingApplicationParam();
            ApplicationDocuments = new List<ProgramDocuments>();
            DisbursedTransactionDetails = new List<TransactionAccountDisbursed>();
            PendingTransactionDetails = new List<TransactionAccountDisbursed>();
        }
        #region Properties
        public long ApplicationID { get; set; }
        public string LoanNumber { get; set; }
        public long ApplicationStatusID { get; set; }
        public string ApplicationStatus { get; set; }
        public string ApplicationTypeName { get; set; }
        public string DateApplied { get; set; }
        public long ProgramInvitationID { get; set; }
        public string ProgramName { get; set; }
        public string FundingEntityName { get; set; }
        public string ProgramLogoSource { get; set; }
        public bool showProgramLogo { get; set; }
        public string FundingEntityLogoSource { get; set; }
        public bool showFundingEntityLogo { get; set; }
        public bool showAccountDisbursedInfo { get; set; }
        public bool IsPaymentRequested { get; set; }
        public string FundedDate { get; set; }
        public string FundedAmount { get; set; }
        public string AgreementAcceptedBy { get; set; }
        public string AgreementAcceptanceDateTime { get; set; }
        public bool ShowUserAgreementdetailsInfo { get; set; }
        public LoanBusinessDetailParam LoanBusinessDetails { get; set; }
        public List<BusinessOwnerParam> BusinessOwners { get; set; }
        public FundingApplicationParam FundingApplication { get; set; }
        public List<ProgramDocuments> ApplicationDocuments { get; set; }
       
        
        //SPA
        public bool ShowAccountDisbursedInfoIfSPA { get; set; }
        public List<TransactionAccountDisbursed> DisbursedTransactionDetails { get; set; }

        public bool ShowUserSPAdetailsInfo { get; set; }
        public string SPAAcceptedBy { get; set; }
        public string SPAAcceptanceDateTime { get; set; }
        public string GrantAgreementName { get; set; }
        public string SPAName { get; set; }

        public int ProgressReportId { get; set; }

        public long? BusinessId { get; set; }
        public long? ProgramId { get; set; }

        public List<TransactionAccountDisbursed> PendingTransactionDetails { get; set; }
        #endregion Properties
    }
}