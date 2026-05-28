using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Params
{
    public class GrantApplication
    {
        public string GrantNumber { get; set; }
        public DateTime DateApplied { get; set; }
        public string BusinessName { get; set; }
        public string AffiliateName { get; set; }
        public string GrantProgramName { get; set; }
        public decimal FundsAllocated { get; set; }
        public decimal DisbursedAmount { get; set; }
        public string ApplicationStatus { get; set; }
    }
}
