using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("RolePermission", Schema = "Master")]
    public partial class RolePermission
    {
        [Key]
        public long RolePermissionID { get; set; }
        public long RoleID { get; set; }
        public int? ActionID { get; set; }
        public string Subject { get; set; }
        public string Condition { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
        
        public virtual Action Action { get; set; }
        public virtual Role Role { get; set; }
    }
}