
namespace ThoughtFocus.Domain.CustomView
{
    public class RolePermissionViewEntity
    {
        #region Properties

        public long RolePermissionID { get; set; }
        public bool IsActive { get; set; }
        public long RoleID { get; set; }
        public int ActionID { get; set; }
        public string Subject { get; set; }
        public string ActionName { get; set; }
        public string RoleName { get; set; }
        public bool IsAllowed { get; set; }

        // public virtual ThoughtFocus..Models.Action Action { get; set; }
        // public virtual Role Role { get; set; }

        #endregion Properties
    }
}
