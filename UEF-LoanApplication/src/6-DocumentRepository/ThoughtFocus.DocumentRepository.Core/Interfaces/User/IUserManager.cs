using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IUserManager
    {
        DocumentBaseResponse SaveUser(UserEntity user);

        DocumentBaseResponse UpdateUserGroups(List<RepositoryUserGroupMapping> repositoryUserGroups);

        List<UserEntity> GetUsers();

        UserEntity GetUser(long userID);

        UserEntity GetUser(Guid userID);
        
    }
}
