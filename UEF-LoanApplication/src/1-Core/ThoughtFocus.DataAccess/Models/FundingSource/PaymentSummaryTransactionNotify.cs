using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("PaymentSummaryTransactionNotify", Schema = "FundingSource")]
    public partial class PaymentSummaryTransactionNotify : AuditBase
    {
        

        [Key]
        public long NotifyID { get; set; }          
        public long ApplicationID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public long ToNotifyUserId { get; set; }
        public bool IsSent { get; set; }
    }
}



