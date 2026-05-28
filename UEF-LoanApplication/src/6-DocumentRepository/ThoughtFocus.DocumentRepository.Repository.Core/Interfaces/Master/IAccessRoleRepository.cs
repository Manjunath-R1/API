using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IAccessRoleRepository : IBaseRepository<AccessRole>
    {
        List<AccessRole> GetAccessRoles();

        List<Permission> GetPermissions();

        void AddOrUpdateAccessRole(AccessRole accessRole);

        AccessRole GetAccessRoleByID(Guid accessRoleID);

        void AddOrUpdatePermissions(List<Permission> permissions, Guid accessRoleID);

        void DeleteRelationsForAccessRole(Guid accessRoleID);

        List<Permission> GetPermissionsByAccessRole(Guid accessRoleID);

        void AssignPermission(AccessPermission accessPermission);

        List<AccessPermissionResponse> GetPermissionsForDocumentOrProject(AccessPermission accessPermission);

        List<AccessPermissionResponse> GetPermissionsByUserID(AuthorizeRequest authorizeRequest);

        List<AccessPermissionResponse> GetPermissionsByGroupID(AuthorizeRequest authorizeRequest);

        bool IsAllowed(AuthorizeRequest authorizeRequest, List<AccessPermissionResponse> assignedRoles);

        void AssignOwnerPermission(AccessPermission accessPermission);

        bool IsOwnerPermissionExists(AuthorizeRequest authorizeRequest);

        void DeletePermission(AccessPermission accessPermission);

        void DeleteAccessRoleRelations(AccessRole accessRoleRequest);

        InheritanceResponse GetInheritedProjectOrDocument(InheritanceRequest inheritanceRequest);

        void RemoveInheritnace(InheritanceRequest inheritanceRequest);

        void RemoveUniquePermissions(InheritanceRequest inheritanceRequest);

    }
}
