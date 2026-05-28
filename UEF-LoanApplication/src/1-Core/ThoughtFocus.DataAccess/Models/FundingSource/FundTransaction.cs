using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundTransactions", Schema = "FundingSource")]
    public partial class FundTransaction : AuditBase
    {
         public FundTransaction()
        {
            FundTransactionDocuments=new List<FundTransactionDocument>();
            
        }
        [Key]
        public long ID { get; set; }
        
      

        [ForeignKey("TransactionType")]
        public int TransactionTypeID { get; set; } 
        public decimal TransactionAmount { get; set; }
        public string Comment { get; set;}
        public string OriginatingBankAccount { get; set; }
        public System.DateTime TransactionDate { get; set; }
        
        
        public virtual FundingSource FundingSource { get; set; }
        [ForeignKey("FundingSource")]
        public Nullable<long> FundingSourceID { get; set; }
        
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<FundTransactionDocument> FundTransactionDocuments { get; set; }

        
    }
}