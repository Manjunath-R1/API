using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IUserRepository: IBaseRepository<RepositoryUser>
    {
        void Save(RepositoryUser repositoryUser);

        List<RepositoryUser> GetUsers();

        RepositoryUser GetUser(long userID);

        RepositoryUser GetUser(Guid userID);

        void DeleteUserAccessRoleRelationship(Guid userID);

    }
    
}
