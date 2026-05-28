using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Master;
using System.ComponentModel.DataAnnotations;

namespace ThoughtFocus.DataAccess.Models.Admin
{
    [Table("Questions", Schema = "Question")]
    public partial class Question
    {
        [Key]
        public long QuestionID { get; set; }        
        public string QuestionText { get; set; }
        public int Version { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("ResponseType")]
        public int ResponseTypeID { get; set; } 
        public virtual ResponseType ResponseType{ get; set; }

    }
}