using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.Common.Exceptions.BusinessException;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class AccessRoleManager : IAccessRoleManager
    {
        #region Fields

        private readonly ILogger<AccessRoleManager> _logger;
        private IAccessRoleRepository _accessRoleRepository;
        private IUserRepository _userProvider;
        private RepositoryUser repositoryUser = null;
        private IAuthorizer _authorizer;
        private IActionLogger _actionLogger;

        #endregion Fields

        #region Constructors

        public AccessRoleManager(IAccessRoleRepository accessRoleRepository, IUserRepository userProvider,
            IAuthorizer authorizer,IActionLogger actionLogger, ILogger<AccessRoleManager> logger)
        {
            _accessRoleRepository = accessRoleRepository;
            _userProvider = userProvider;
            _authorizer = authorizer;
            _actionLogger = actionLogger;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        public List<PermissionEntity> GetPermissions()
        {
            try
            {
                List<PermissionEntity> permissionList = new List<PermissionEntity>();
                var permissions = _accessRoleRepository.GetPermissions();

                foreach (var permission in permissions)
                {
                    PermissionEntity permissionEntity = new PermissionEntity();
                    permissionEntity.PermissionID = permission.PermissionID;
                    permissionEntity.Name = permission.Name;
                    permissionEntity.Description = permission.Description;
                    permissionList.Add(permissionEntity);
                }

                return permissionList;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public List<AccessRoleEntity> GetAccessRoles()
        {
            try
            {
                List<AccessRoleEntity> accessRolelist = new List<AccessRoleEntity>();
                var accessRoles = _accessRoleRepository.GetAccessRoles();

                foreach (var accessRole in accessRoles)
                {
                    List<PermissionEntity> permissionList = new List<PermissionEntity>();
                    AccessRoleEntity accessRoleEntity = new AccessRoleEntity();
                    accessRoleEntity.AccessRoleID = accessRole.AccessRoleID;
                    accessRoleEntity.Name = accessRole.Name;
                    accessRoleEntity.Description = accessRole.Description;

                    foreach (var permission in accessRole.permissions)
                    {
                        PermissionEntity permissionEntity = new PermissionEntity();
                        permissionEntity.PermissionID = permission.PermissionID;
                        permissionEntity.Name = permission.Name;
                        permissionEntity.Description = permission.Description;
                        permissionList.Add(permissionEntity);
                    }
                    accessRoleEntity.Permissions = permissionList;
                    accessRolelist.Add(accessRoleEntity);
                }

                return accessRolelist;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching accessRoles -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching accessRoles -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching accessRoles -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse AddAccessRole(AccessRoleRequest accessRoleRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                #region Validation
                ValidationResponse groupNameValidation = this.ValidateAccessRole(accessRoleRequest.Name);
                if (!(groupNameValidation.IsSuccess && groupNameValidation.IsValid))
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "AccessRole Name already exists";
                    return documentBaseResponse;

                }
                #endregion Validation

                AccessRole accessRole = new AccessRole();
                accessRole.AccessRoleID = accessRoleRequest.AccessRoleID;
                accessRole.CreatedByUserID = accessRoleRequest.LoggerInUserID;
                accessRole.CreatedDateTime = current;
                accessRole.LastModifiedDateTime = current;
                accessRole.LastModifiedByUserID = accessRoleRequest.LoggerInUserID;
                accessRole.IsActive = true;
                accessRole.Name = accessRoleRequest.Name;
                accessRole.Description = accessRoleRequest.Description;

                this._accessRoleRepository.AddOrUpdateAccessRole(accessRole);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "AccessRole added successfully.";

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding accessrole -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding accessrole -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding accessrole -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public List<PermissionEntity> GetPermissionsByAccessRole(Guid accessRoleID)
        {
            try
            {
                List<PermissionEntity> permissionList = new List<PermissionEntity>();
                var permissions = _accessRoleRepository.GetPermissionsByAccessRole(accessRoleID);

                foreach (var permission in permissions)
                {
                    PermissionEntity permissionEntity = new PermissionEntity();
                    permissionEntity.PermissionID = permission.PermissionID;
                    permissionEntity.Name = permission.Name;
                    permissionEntity.Description = permission.Description;
                    permissionList.Add(permissionEntity);
                }

                return permissionList;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse AddOrUpdatePermissions(List<PermissionEntity> permissionEntity, Guid accessRoleID)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                List<Permission> permissions = new List<Permission>();

                foreach (var _permission in permissionEntity)
                {
                    Permission permission = new Permission();
                    permission.PermissionID = _permission.PermissionID;
                    permissions.Add(permission);
                }

                this._accessRoleRepository.DeleteRelationsForAccessRole(accessRoleID);


                this._accessRoleRepository.AddOrUpdatePermissions(permissions, accessRoleID);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "Permissions added successfully.";

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public DocumentBaseResponse AssignProjectPermission(AccessPermissionRequest accessPermissionRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                AccessPermission accessPermission = new AccessPermission();

                if (accessPermissionRequest.UserID != Guid.Empty)
                {
                    accessPermission.AssigneeID = accessPermissionRequest.UserID;
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.RepositoryUser.ToString();
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.UserGuID.ToString();
                }
                else
                {
                    accessPermission.AssigneeID = accessPermissionRequest.GroupID;
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.Group.ToString();
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.GroupID.ToString();
                }

                accessPermission.ContentNodeName = NodeNameEnumeration.Project.ToString();
                accessPermission.ContentKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                accessPermission.ContentID = accessPermissionRequest.ProjectID;
                accessPermission.RoleName = accessPermissionRequest.AccessRoleName;

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(accessPermissionRequest.LoggedInUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = accessPermission.ContentNodeName;
                authorizeRequest.ContentKeyName = accessPermission.ContentKeyName;
                authorizeRequest.ContentID = accessPermission.ContentID;
                authorizeRequest.ActionName = ActionNameEnumeration.ModifyPermission.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    documentBaseResponse.ID = accessPermission.ContentID;
                    return documentBaseResponse;
                }
                #endregion Authorize

                this._accessRoleRepository.AssignPermission(accessPermission);
                documentBaseResponse.ID = accessPermission.ContentID;
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "Permissions added successfully.";

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public DocumentBaseResponse AssignDocumentPermission(AccessPermissionRequest accessPermissionRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                AccessPermission accessPermission = new AccessPermission();

                if (accessPermissionRequest.UserID != Guid.Empty)
                {
                    accessPermission.AssigneeID = accessPermissionRequest.UserID;
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.RepositoryUser.ToString();
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.UserGuID.ToString();
                }
                else
                {
                    accessPermission.AssigneeID = accessPermissionRequest.GroupID;
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.Group.ToString();
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.GroupID.ToString();
                }

                accessPermission.ContentNodeName = NodeNameEnumeration.Document.ToString();
                accessPermission.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                accessPermission.ContentID = accessPermissionRequest.DocumentID;
                accessPermission.RoleName = accessPermissionRequest.AccessRoleName;

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(accessPermissionRequest.LoggedInUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = accessPermission.ContentNodeName;
                authorizeRequest.ContentKeyName = accessPermission.ContentKeyName;
                authorizeRequest.ContentID = accessPermission.ContentID;
                authorizeRequest.ActionName = ActionNameEnumeration.ModifyPermission.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    documentBaseResponse.ID = accessPermission.ContentID;
                    return documentBaseResponse;
                }
                #endregion Authorize

                this._accessRoleRepository.AssignPermission(accessPermission);
                documentBaseResponse.ID = accessPermission.ContentID;
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "Permission to document added successfully.";

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while adding permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public List<AccessPermissionResponse> GetPermissionsForProject(AccessPermissionEntity accessPermissionEntity)
        {
            try
            {
                accessPermissionEntity.ContentNodeName = NodeNameEnumeration.Project.ToString();
                accessPermissionEntity.ContentkeyName = NodeKeyNameEnumeration.ProjectID.ToString();


                var accessPermission = new AccessPermission
                {
                    AssigneeID = accessPermissionEntity.AssigneeID,
                    AssigneeKeyName = accessPermissionEntity.AssigneekeyName,
                    AssigneeNodeName = accessPermissionEntity.AssigneeNodeName,
                    ContentID = accessPermissionEntity.ContentID,
                    ContentKeyName = accessPermissionEntity.ContentkeyName,
                    ContentNodeName = accessPermissionEntity.ContentNodeName,
                    RoleName = accessPermissionEntity.RoleName
                };

                return this._accessRoleRepository.GetPermissionsForDocumentOrProject(accessPermission);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public List<AccessPermissionResponse> GetPermissionsForDocument(AccessPermissionEntity accessPermissionEntity)
        {
            try
            {
                accessPermissionEntity.ContentNodeName = NodeNameEnumeration.Document.ToString();
                accessPermissionEntity.ContentkeyName = NodeKeyNameEnumeration.DocumentID.ToString();


                var accessPermission = new AccessPermission
                {
                    AssigneeID = accessPermissionEntity.AssigneeID,
                    AssigneeKeyName = accessPermissionEntity.AssigneekeyName,
                    AssigneeNodeName = accessPermissionEntity.AssigneeNodeName,
                    ContentID = accessPermissionEntity.ContentID,
                    ContentKeyName = accessPermissionEntity.ContentkeyName,
                    ContentNodeName = accessPermissionEntity.ContentNodeName,
                    RoleName = accessPermissionEntity.RoleName
                };



                return this._accessRoleRepository.GetPermissionsForDocumentOrProject(accessPermission);
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching permissions -{0}", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse DeletePermission(DeleteRequest deleteRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();
            DateTime current = DateTime.Now;
            try
            {
                AccessPermission accessPermission = new AccessPermission();

                if (deleteRequest.UserID != Guid.Empty)
                {
                    accessPermission.AssigneeID = deleteRequest.UserID;
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.RepositoryUser.ToString();
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.UserGuID.ToString();
                }
                else
                {
                    accessPermission.AssigneeID = deleteRequest.GroupID;
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.Group.ToString();
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.GroupID.ToString();
                }

                if (deleteRequest.Type =="Document")
                {
                    accessPermission.ContentNodeName = NodeNameEnumeration.Document.ToString();
                    accessPermission.ContentKeyName = NodeKeyNameEnumeration.DocumentID.ToString();
                    accessPermission.ContentID = deleteRequest.ContentID;
                }
                else
                {
                    accessPermission.ContentNodeName = NodeNameEnumeration.Project.ToString();
                    accessPermission.ContentKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                    accessPermission.ContentID = deleteRequest.ContentID;
                }

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(deleteRequest.LoggedInUser);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = accessPermission.ContentNodeName;
                authorizeRequest.ContentKeyName = accessPermission.ContentKeyName;
                authorizeRequest.ContentID = deleteRequest.ContentID;

                authorizeRequest.ActionName = ActionNameEnumeration.ModifyPermission.ToString();
                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    documentBaseResponse.IsSuccess = false;
                    documentBaseResponse.ID = accessPermission.ContentID;
                    documentBaseResponse.Type = accessPermission.ContentNodeName;
                    documentBaseResponse.Message = "You are not authorized to perform this action";
                    return documentBaseResponse;
                }
                #endregion Authorize

                accessPermission.RoleName = deleteRequest.AccessRoleName;


                this._accessRoleRepository.DeletePermission(accessPermission);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.ID = accessPermission.ContentID;
                documentBaseResponse.Type = accessPermission.ContentNodeName;
                documentBaseResponse.Message = "Permission deleted successfully.";

            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while deleting permission -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while deleting permission -{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while deleting permission -{0}", ex));
                throw new BusinessException("", ex);
            }
            return documentBaseResponse;
        }

        public DocumentBaseResponse DeleteAccessRole(DeleteRequest deleteRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();

            try
            {
                AccessRole accessRole = _accessRoleRepository.GetAccessRoleByID(deleteRequest.AccessRoleID);

                if (accessRole != null)
                {
                    this._accessRoleRepository.DeleteAccessRoleRelations(accessRole);

                    accessRole.IsActive = false;
                    accessRole.LastModifiedDateTime = DateTime.Now;
                    accessRole.LastModifiedByUserID = deleteRequest.LoggedInUser;

                    this._accessRoleRepository.AddOrUpdateAccessRole(accessRole);
                }

                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.Message = "AccessRole deleted successfully.";

                //Activity log added with Uploaded document
                ActivityLog activityLog = new ActivityLog();
                repositoryUser = _userProvider.GetUser(deleteRequest.LoggedInUser);
                if (repositoryUser != null)
                    activityLog.UserGuID = repositoryUser.UserGuID;
                activityLog.ActivityName = ActivityNameEnumeration.Deleted.ToString();
                activityLog.NodeName = NodeNameEnumeration.AccessRole.ToString();
                activityLog.NodeKeyName = NodeKeyNameEnumeration.AccessRoleID.ToString();
                activityLog.KeyValue = deleteRequest.AccessRoleID.ToString();
                this._actionLogger.LogUserActivity(activityLog);

                return documentBaseResponse;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
        }

        public ValidationResponse ValidateAccessRole(string name)
        {
            ValidationResponse validationResponse = new ValidationResponse();
            validationResponse.IsSuccess = true;
            try
            {
                List<AccessRole> accessRole = new List<AccessRole>();
                accessRole = this._accessRoleRepository.GetAccessRoles().ToList();
                if (accessRole.Any(a => a.Name.ToLower() == name.ToLower()))
                {
                    validationResponse.IsValid = false;
                }
                else
                {
                    validationResponse.IsValid = true;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return validationResponse;
        }

        public DocumentBaseResponse RemoveInheritance(InheritanceRequest inheritanceRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();

            try
            {

                this._accessRoleRepository.RemoveInheritnace(inheritanceRequest);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.ID = inheritanceRequest.ContentID;
                documentBaseResponse.Type = inheritanceRequest.ContentNodeName;
                documentBaseResponse.Message = "Inheritance removed successfully.";

                return documentBaseResponse;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
        }

        public DocumentBaseResponse RemoveUniquePermissions(InheritanceRequest inheritanceRequest)
        {
            DocumentBaseResponse documentBaseResponse = new DocumentBaseResponse();

            try
            {

                this._accessRoleRepository.RemoveUniquePermissions(inheritanceRequest);
                documentBaseResponse.IsSuccess = true;
                documentBaseResponse.ID = inheritanceRequest.ContentID;
                documentBaseResponse.Type = inheritanceRequest.ContentNodeName;
                documentBaseResponse.Message = "Unique permissions deleted successfully.";

                return documentBaseResponse;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while deleting the accessRole", ex));
                throw new BusinessException("", ex);
            }
        }

        #endregion Methods
    }
}
