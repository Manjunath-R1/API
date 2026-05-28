using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Response
{
    public class DashboardReportDetailResponse : BaseResponse
    {

        public List<ProgramResponse> Programs { get; set; }
        public List<FundDetailReport> FundAllocationDetail { get; set; }
        public List<ApplicationStatsDetailReport> ApplicationStatsDetail { get; set; }

        public List<FundDetailReport> FundDetailProgramWise { get; set; }
        public List<ApplicationStatsDetailReport> ApplicationStatsDetailProgramWise { get; set; }
        public List<FundDetailReport> FundDetailAffiliateWise { get; set; }        
        public List<ApplicationStatsDetailReport> ApplicationStatsDetailAffiliateWise { get; set; }
    }
    public class BasicReport
    {
        public string FundingEntityName { get; set; }
        public string AffiliateName { get; set; }
        public string ProgramName { get; set; }

    }
    public class FundDetailReport: BasicReport
    {
        public string AvailableLimit { get; set; }
        public string UtilizedAmount { get; set; }
    }
    
    public class ApplicationStatsDetailReport: BasicReport
    {

        public string NoAction { get; set; }
        public string Started { get; set; }
        public string Inprogress { get; set; }
        public string Funded { get; set; }
    }
}
