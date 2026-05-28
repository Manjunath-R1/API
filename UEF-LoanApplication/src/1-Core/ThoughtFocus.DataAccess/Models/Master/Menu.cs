using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ThoughtFocus.DataAccess.Models.Master
{
    [Table("Menu", Schema = "Master")]
    public partial class Menu
    {
        public Menu()
        {
            this.RMRoleMenuSubMenus = new List<RMRoleMenuSubMenu>();
            this.SubMenus = new List<SubMenu>();
        }
        [Key]
        public long MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string MenuURL { get; set; }
        public string MenuIconClass { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
        public virtual ICollection<RMRoleMenuSubMenu> RMRoleMenuSubMenus { get; set; }
        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}