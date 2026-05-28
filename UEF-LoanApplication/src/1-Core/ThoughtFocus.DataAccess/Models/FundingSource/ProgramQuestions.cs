using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("ProgramQuestions", Schema = "FundingSource")]
    public partial class ProgramQuestion : AuditBase
    {

        [Key]
        public long ID { get; set; }

        [ForeignKey("FundingSource")]
        public Nullable<long> ProgramID { get; set; }

        [ForeignKey("Question")]
        public Nullable<long> QuestionID { get; set; }
        public bool IsMadatory { get; set; }
        public int DisplayOrder { get; set; }
        public virtual FundingSource FundingSource { get; set; }
        public virtual Question Question { get; set; }

    }
}