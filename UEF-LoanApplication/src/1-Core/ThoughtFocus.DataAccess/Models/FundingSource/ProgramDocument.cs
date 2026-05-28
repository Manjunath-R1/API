using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("ProgramDocuments", Schema = "FundingSource")]
    public partial class ProgramDocument : AuditBase
    {

        [Key]
        public long ID { get; set; }

        [ForeignKey("FundingSource")]
        public Nullable<long> ProgramID { get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeID { get; set; }
        public bool IsMandatory { get; set; }
        public long DisplayOrder { get; set; }
        public virtual FundingSource FundingSource { get; set; }
        public virtual DocumentType DocumentType { get; set; }

    }
}



 