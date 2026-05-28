using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
  [Table("AccountStatus", Schema = "Master")]
    public class AccountStatus
    {
        [Key]
        public long AccountStatusID { get; set; }
        public string AccountStatusName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
    
    }
}