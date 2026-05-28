using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        void SaveGroup(Group group);

        List<Group> GetGroups();

        void SaveUserGroupRelation(Guid groupID, Guid userID);

        Group GetGroupByID(Guid groupID);

        List<RepositoryUserGroupMapping> GetUserGroupRelations();

        List<Group> GetUserBelongsTo(Guid userID);

        void DeleteGroupRelations(Group groupRequest);

        void DeleteUserGroupRelations(RepositoryUserGroupMapping userGroupMapping);

        List<RepositoryUserGroupMapping> GetUsersByGroup(Guid groupID);
    }
}
