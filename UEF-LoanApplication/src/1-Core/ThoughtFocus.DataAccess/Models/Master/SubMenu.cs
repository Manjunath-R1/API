using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("SubMenu", Schema = "Master")]
    public partial class SubMenu
    {
        public SubMenu()
        {
            this.RMRoleMenuSubMenus = new List<RMRoleMenuSubMenu>();
        }
        [Key]
        public long SubMenuID { get; set; }
        public long MenuID { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuDescription { get; set; }
        public string SubMenuURL { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
        public string SubMenuIconClass { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<RMRoleMenuSubMenu> RMRoleMenuSubMenus { get; set; }
    }
}