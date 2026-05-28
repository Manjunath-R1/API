using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.User
{
    [Table("UserRole", Schema = "User")]
    public partial class UserRole : AuditBase
    {
        [Key]
        public long UserRoleID { get; set; }
        public long UserID { get; set; }
        public long RoleID { get; set; }
        public long? DisplayOrder { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}