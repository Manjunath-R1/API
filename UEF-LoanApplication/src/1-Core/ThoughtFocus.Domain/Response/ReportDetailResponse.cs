using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Response
{
    public class ReportDetailResponse : BaseResponse
    {
        public List<InvitationDetail> InvitationDetail { get; set; }
        public List<ActiveAccountDetail> ActiveAccountDetail { get; set; }
        public List<ApplicationSubmittedDetail> ApplicationSubmittedDetail { get; set; }
        public List<ApplicationStartedDetail> ApplicationStartedDetail { get; set; }
        public List<ApplicationFundedDetail> ApplicationFundedDetail { get; set; }
        public List<FundReleasedDetail> FundReleasedDetail { get; set; }
        
    }
    public class CommonReport
    {
        public string FundingEntityName { get; set; }
        public string BusinessName { get; set; }
        public string AffiliateName { get; set; }
        public string ProgramName { get; set; }
     
    }
    public class BasicDetail
    {
        public long BusinessID { get; set; }
        public long ProgramInvitationID { get; set; }
        public long ProgramID { get; set; }
        public long FundingEntityID { get; set; }
        public string FundingEntityName { get; set; }        
        public string BusinessName { get; set; }
        public string AffiliateName { get; set; }
        public string ProgramName { get; set; }
        public string FundingEIN { get; set; }
        public string FundingTIN { get; set; }
    }
    public class InvitationDetail: CommonReport
    {
        
        public string InvitationEmailAddreess { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string InvitationSentDateTime { get; set; }
        public long ProgramInvitationID { get; set; }
        
    }
    public class ActiveAccountDetail
    {

        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string FirstLoginDateTime { get; set; }
        public string LastLoginDateTime { get; set; }
        public string AccountActivationDate { get; set; }
        public string IsAccountActivated { get; set; }
        public string IsLockedOut { get; set; }

    }
    public class ApplicationSubmittedDetail : CommonReport
    {  
        public string LoanNumber { get; set; }
        public string ConcentAcceptedDate { get; set; }
        public string IsConcentAccepted { get; set; }
        
    }
    public class ApplicationStartedDetail : CommonReport
    {  
        public string LoanNumber { get; set; }
        public string ConcentAcceptedDate { get; set; }
        public string ApplicationStartedDate { get; set; }
        public string IsConcentAccepted { get; set; }
    }
    public class ApplicationFundedDetail : CommonReport
    {
        public long LoanApplicationID { get; set; }
        public string LoanNumber { get; set; }
        public string FundingEIN { get; set; }
        public string FundingTIN { get; set; }
        public string FundedAmount { get; set; }
        public string DateOfDisbursement { get; set; }
    }
    public class FundReleasedDetail: CommonReport
    {
        public long LoanApplicationID { get; set; }
        public string LoanNumber { get; set; }
        public string ReleasedAmount { get; set; }
        public string DateOfDisbursement { get; set; }
        
    }
}
