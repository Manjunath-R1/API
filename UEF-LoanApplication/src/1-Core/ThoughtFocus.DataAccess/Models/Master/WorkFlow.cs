using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("WorkFlow", Schema = "Master")]
    public partial class WorkFlow
    {
        public WorkFlow()
        {
            this.ApplicationStatus = new List<ApplicationStatus>();
        }

        public int WorkFlowID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationStatus> ApplicationStatus { get; set; }
    }
}
