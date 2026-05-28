using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.App.ViewModels
{
    public class RoleFunctionResponse : BaseResponse
    {
        public List<AccessPermissionViewEntity> RoleStatePermission { get; set; }
    }
    public class RolePermission : BaseResponse
    {
        public List<AccessPermissionViewEntity> RolePermissions { get; set; }
    }
}