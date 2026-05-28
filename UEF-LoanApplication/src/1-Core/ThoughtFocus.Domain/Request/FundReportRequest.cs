using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Request
{
    [Serializable]
    public class FundReportRequest
    {
        public List<long?> BusinessEntityID { get; set; }
        public List<long?> ProgramID { get; set; }
    }
}
