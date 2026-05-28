using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.CustomView
{
    public class SMSSenderRequest
    {
        public string LoanNumber { get; set; }
        public string ProgramName { get; set; }
        public long ApplicationStatusID { get; set; }
        public string PhoneNumber { get; set; }
        //public string URL { get; set; }
        public long UserID { get; set; }
        public long BusinessUserID { get; set; }
        public long LoanApplicationID { get; set; }
    }
}
