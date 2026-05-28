using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IGroupManager
    {
        List<GroupEntity> GetGroups();

        DocumentBaseResponse AddGroup(GroupRequest groupRequest);

        DocumentBaseResponse SaveUserGroupRelation(Guid groupID, Guid userID);

        List<UserGroupEntity> GetUserGroupRelations();

        DocumentBaseResponse DeleteGroup(DeleteRequest deleteRequest);

        DocumentBaseResponse DeleteUserGroup(DeleteRequest deleteRequest);

        List<UserGroupEntity> GetUsersByGroup(Guid groupID);
    }
}
