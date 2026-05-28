using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Request
{
    public class DashboardDetailRequest
    {
        public string ReportType { get; set; }
        public long ProgramID { get; set; }
    }
    public class StatusRequest
    {
        public string Status { get; set; }
        public long ProgramID { get; set; }
    }
}
