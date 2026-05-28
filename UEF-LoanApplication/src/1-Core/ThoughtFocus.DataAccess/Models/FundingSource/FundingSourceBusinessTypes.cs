using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundingSourceBusinessTypes", Schema = "FundingSource")]
    public partial class FundingSourceBusinessTypes : AuditBase
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("FundingSource")]
        public long FundingSourceID { get; set; }

        [ForeignKey("BusinessType")]
        public long BusinessTypeID { get; set; }

        public virtual FundingSource FundingSource { get; set; }
        public virtual BusinessType BusinessType { get; set; }
    }
}