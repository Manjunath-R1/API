using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IUserGroupRepository : IBaseRepository<RepositoryUserGroupMapping>
    {
        void SaveOrUpdate(List<RepositoryUserGroupMapping> repositoryUserGroup);
    }
}
