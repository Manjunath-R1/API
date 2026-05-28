using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtFocus.Domain.Response
{
    public class DashboardResponse : BaseResponse
    {
        public List<FundAllocationViewEntity> FundAllocationResponse { get; set; }

        public decimal AvailableLimit { get; set; }
        public decimal UtilizedAmount { get; set; }
        public decimal Submitted { get; set; }
        public decimal ActivatedAccount { get; set; }
        public long Amountdisbursed { get; set; }
        public long InvitedNoActionTaken { get; set; }
        public int TotalSize { get; set; }
        public int PageNumber { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public int TotalPages { get; set; }

    }
    public class ProgramApplicationStatus
    {
        public decimal Submitted { get; set; }
        public decimal ActivatedAccount { get; set; }
        public long Amountdisbursed { get; set; }
        public long NoActionTaken { get; set; }

       public long TotalProgramwiseApplicationStatus{ get; set; }

    }

    public class DashboardProgramWiseApplicationStatusResponse : BaseResponse
    {
        public Dictionary<string, ProgramApplicationStatus> ProgramApplicationStatus { get; set; }
        public int TotalSize { get; set; }
        public int PageNumber { get; set; }
        public int PageEnd { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public int TotalPages { get; set; }
    }
    public class AffiliateApplicationStatus
    {
        public decimal Submitted { get; set; }
        public decimal ActivatedAccount { get; set; }
        public long Amountdisbursed { get; set; }
        public long NoActionTaken { get; set; }
        public long TotalProgramInvitationCount { get; set; }
    }

    public class DashboardAffiliateWiseApplicationStatusResponse : BaseResponse
    {
        public Dictionary<string, AffiliateApplicationStatus> affiliateApplicationStatus { get; set; }
        public int TotalSize { get; set; }
        public int PageNumber { get; set; }
        public int PageEnd { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class AffiliateFundAllocationStatus
    {
        public decimal UtilizedAmount { get; set; }

    }

    public class DashboardAffiliateWiseFundAllocationStatusResponse : BaseResponse
    {
        public Dictionary<string, AffiliateFundAllocationStatus> affiliateFundAllocationStatus { get; set; }
        public int TotalSize { get; set; }
        public int PageNumber { get; set; }
        public int PageEnd { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public int TotalPages { get; set; }
    }

}
