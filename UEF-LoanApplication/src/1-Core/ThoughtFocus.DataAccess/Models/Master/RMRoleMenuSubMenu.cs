using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("RMRoleMenuSubMenu", Schema = "Master")]
    public partial class RMRoleMenuSubMenu
    {
        [Key]
        public long RoleMenuSubMenuID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public long RoleID { get; set; }
        public long MenuID { get; set; }
        public Nullable<long> SubMenuID { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
        public virtual SubMenu SubMenu { get; set; }
    }
}