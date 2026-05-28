using System;
using System.Collections.Generic;


namespace ThoughtFocus.Domain.Params
{
    public class LoanApplicationRequest
    {
        #region Properties
        public long ProgramInvitationID { get; set; }
        public LoanBusinessDetailParam LoanBusinessDetails { get; set; }
        public List<BusinessOwnerParam> BusinessOwners { get; set; }
        public List<LoanApplicantDetailsParam> LoanApplicantDetails { get; set; }
        public string CommandName { get; set; }
        public long LoanApplicationID​​​​​​​​ { get; set; }
        public long ApplicationStatusID { get; set; }
        public FundingApplicationParam FundingApplication { get; set; }
        public List<DocumentRequest> ApplicationDocuments { get; set; }
        public FundUtilizationParam FundUtilization { get; set; }
        public string TransitionComments { get; set; }
        public bool IsBusinessProfile { get; set; }
        public bool IsFundingApp { get; set; }
        public bool IsApplicationDocuments { get; set; }
        public bool IsCfoORControllerAcceptByRMI { get; set; }

        #endregion Properties
    }
}
