using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Params
{
    [Serializable]
    public class CumulativeReportRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<long?> FundingEntityID { get; set; }
        public List<long?>ProgramID { get; set; }
    }
    public class ReportDetailRequest: CumulativeReportRequest
    {
        public string ReportType { get; set; }
       
    }
}
