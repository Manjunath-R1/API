using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IAccessRoleManager
    {
        List<PermissionEntity> GetPermissions();

        List<AccessRoleEntity> GetAccessRoles();

        DocumentBaseResponse AddAccessRole(AccessRoleRequest accessRoleRequest);

        List<PermissionEntity> GetPermissionsByAccessRole(Guid accessRoleID);

        DocumentBaseResponse AddOrUpdatePermissions(List<PermissionEntity> permissions, Guid accessRoleID);

        DocumentBaseResponse AssignProjectPermission(AccessPermissionRequest accessPermissionRequest);

        DocumentBaseResponse AssignDocumentPermission(AccessPermissionRequest accessPermissionRequest);

        List<AccessPermissionResponse> GetPermissionsForProject(AccessPermissionEntity accessPermission);

        List<AccessPermissionResponse> GetPermissionsForDocument(AccessPermissionEntity accessPermission);

        DocumentBaseResponse DeletePermission(DeleteRequest deleteRequest);

        DocumentBaseResponse DeleteAccessRole(DeleteRequest deleteRequest);

        DocumentBaseResponse RemoveInheritance(InheritanceRequest inheritanceRequest);

        DocumentBaseResponse RemoveUniquePermissions(InheritanceRequest inheritanceRequest);
    }
}
