using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("ProgramHelpfulGuides", Schema = "FundingSource")]
    public partial class ProgramHelpfulGuide : AuditBase
    {

        [Key]
        public long ID { get; set; }

        [ForeignKey("FundingSource")]
        public long ProgramID { get; set; }

        [ForeignKey("HelpfulGuideTemplate")]
        public long TamplateID { get; set; }
        public virtual HelpfulGuideTemplate HelpfulGuideTemplate { get; set; }
        public virtual FundingSource FundingSource { get; set; }

    }
}