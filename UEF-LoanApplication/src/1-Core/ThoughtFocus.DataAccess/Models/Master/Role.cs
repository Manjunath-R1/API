using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Role", Schema = "Master")]
    public partial class Role
    {
        public Role()
        {
            this.RolePermissions = new List<RolePermission>();
            this.RMRoleMenuSubMenus = new List<RMRoleMenuSubMenu>();
            this.RUserRoles = new List<UserRole>();
        }
        [Key]
        public long RoleID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public long DisplayOrder { get; set; }
        public bool IsLoginRole { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<RMRoleMenuSubMenu> RMRoleMenuSubMenus { get; set; }
        public virtual ICollection<UserRole> RUserRoles { get; set; }
    }
}