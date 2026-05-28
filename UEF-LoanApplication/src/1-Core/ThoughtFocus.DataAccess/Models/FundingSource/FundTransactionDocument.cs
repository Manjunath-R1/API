using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundTransactionDocuments", Schema = "FundingSource")]
    public partial class FundTransactionDocument : AuditBase
    {
        [Key]
        public long ID { get; set; }      

        [ForeignKey("FundTransaction")]
        public long FundTransactionID { get; set; }
        public Guid DocumentGUID { get; set; }
        public string DocumentName { get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeID { get; set; }
        public string FileName { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual FundTransaction FundTransaction { get; set; }
        
    }
}