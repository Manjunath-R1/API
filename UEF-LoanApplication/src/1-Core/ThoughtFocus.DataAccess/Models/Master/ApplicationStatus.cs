using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Application;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Master
{
    
    [Table("ApplicationStatus", Schema = "Master")]
    public partial class ApplicationStatus
    {
         public ApplicationStatus()
        {
            this.LoanApplications = new List<LoanApplication>();
        }

        [Key]
        public int ApplicationStatusID { get; set; }
        public string ApplicationStatusName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }

        [ForeignKey("WorkFlow")]
        public int WorkFlowID { get; set; }

        public virtual ICollection<LoanApplication> LoanApplications { get; set; }
        public virtual WorkFlow WorkFlow { get; set; }    
   
    }
}