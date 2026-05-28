using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class OwnerAuthorizer : IAuthorizerStrategy
    {
        private IAccessRoleRepository _accessRoleRepository;
        private IGroupRepository _groupRepository;

        public OwnerAuthorizer(IAccessRoleRepository accessRoleRepository,IGroupRepository groupRepository)
        {
            this._accessRoleRepository = accessRoleRepository;
            this._groupRepository = groupRepository;
        }

        public AuthorizationResponse GetAuthorization(AuthorizeRequest authorizeRequest)
        {
            AuthorizationResponse authorizationResponse = new AuthorizationResponse();
            AccessPermission accessPermission = new AccessPermission();

            authorizationResponse.IsAllowed = _accessRoleRepository.IsOwnerPermissionExists(authorizeRequest);

            return authorizationResponse;
        }

        public int ExcicutionOrder
        {
            get { return 1; }
        }
     
    }

    public class UserAuthorizer : IAuthorizerStrategy
    {
        private IAccessRoleRepository _accessRoleRepository;
        private IGroupRepository _groupRepository;

        public UserAuthorizer(IAccessRoleRepository accessRoleRepository, IGroupRepository groupRepository)
        {
            this._accessRoleRepository = accessRoleRepository;
            this._groupRepository = groupRepository;
        }

        public AuthorizationResponse GetAuthorization(AuthorizeRequest authorizeRequest)
        {
            AuthorizationResponse authorizationResponse = new AuthorizationResponse();
            List<AccessPermissionResponse> assignedRoles = _accessRoleRepository.GetPermissionsByUserID(authorizeRequest);

            authorizationResponse.IsAllowed = _accessRoleRepository.IsAllowed(authorizeRequest, assignedRoles);
            return authorizationResponse;
        }
        public int ExcicutionOrder
        {
            get { return 2; }
        }
    }

    public class GroupAuthorizer : IAuthorizerStrategy
    {
        private IAccessRoleRepository _accessRoleRepository;
        private IGroupRepository _groupRepository;

        public GroupAuthorizer(IAccessRoleRepository accessRoleRepository, IGroupRepository groupRepository)
        {
            this._accessRoleRepository = accessRoleRepository;
            this._groupRepository = groupRepository;
        }

        public AuthorizationResponse GetAuthorization(AuthorizeRequest authorizeRequest)
        {
            AuthorizationResponse authorizationResponse = new AuthorizationResponse();
            AccessRole accessRole = new AccessRole();

            List<Group> groups = _groupRepository.GetUserBelongsTo(authorizeRequest.LoggedInUserID);
            if (groups != null && groups.Count > 0)
            {
                foreach (var group in groups)
                {
                    authorizeRequest.AssigneeNodeName = NodeNameEnumeration.Group.ToString();
                    authorizeRequest.AssigneeKeyName = NodeKeyNameEnumeration.GroupID.ToString();
                    authorizeRequest.GroupID = group.GroupID;
                    List<AccessPermissionResponse> assignedRoles = _accessRoleRepository.GetPermissionsByGroupID(authorizeRequest);
                    authorizationResponse.IsAllowed = _accessRoleRepository.IsAllowed(authorizeRequest, assignedRoles);
                    if (authorizationResponse.IsAllowed)
                    {
                        break;
                    }

                }
            }
            
            return authorizationResponse;
        }

        public int ExcicutionOrder
        {
            get { return 3; }
        }
    }
}
