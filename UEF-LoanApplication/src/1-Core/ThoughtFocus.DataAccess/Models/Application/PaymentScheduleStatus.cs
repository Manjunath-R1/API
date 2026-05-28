using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThoughtFocus.DataAccess.Models.Application
{
    [Table("PaymentScheduleStatus", Schema = "Application")]
    public class PaymentScheduleStatus
    {
        public long ID { get; set; }

        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }
        public long DisbursementCount { get; set; }
        public string Status { get; set; }
        public long ApplicationStatusID { get; set; }
    }
}
