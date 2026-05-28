using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundingSourceStates", Schema = "FundingSource")]
    public partial class FundingSourceStates : AuditBase
    {
        [Key]
        public long ID { get; set; }

        [ForeignKey("FundingSource")]
        public long FundingSourceID { get; set; }

        [ForeignKey("State")]
        public long StateID { get; set; }
        public virtual FundingSource FundingSource { get; set; }
        public virtual State State { get; set; }
    }
}