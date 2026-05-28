using System;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundTransactions", Schema = "FundingSource")]
    public partial class FundUtilization : FundTransaction
    {
        [ForeignKey("LoanApplication")]
        public long ApplicationID { get; set; }  
        public System.DateTime DateofDisbursement { get; set; }
        public string DestinationBankAccount { get; set; }
        public string BankRoutingNumber { get; set; }
        public virtual LoanApplication LoanApplication { get; set; } 

        
    
    }
}