using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.DataAccess.Models.Application
{
    [Table("QuestionResponse", Schema = "Application")]
    public partial class QuestionResponse
    {
        [Key]
        public long ID { get; set; } 

        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }

        [ForeignKey("Question")]
        public long QuestionID { get; set; }
        public string Response { get; set; } 

        public virtual LoanApplication LoanApplication{ get; set; }
        public virtual Question Question{ get; set; }

    }
}