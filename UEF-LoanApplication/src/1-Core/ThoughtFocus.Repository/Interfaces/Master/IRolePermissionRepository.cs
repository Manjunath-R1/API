using System.Collections.Generic; 
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.Repository.Interfaces.Master
{
    public interface IRolePermissionRepository:IEFApplicationBaseRepository<RolePermission>
    {

        List<RolePermission> GetGlobalRolePermissions(List<long> ruserRoles);

        void SaveOrUpdate(List<RolePermission> permissions);


    }
}
