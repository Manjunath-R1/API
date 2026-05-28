using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.User
{
    public class UserGroupRepository: AbstractNeo4jBaseRepository<RepositoryUserGroupMapping>, IUserGroupRepository
    {
        private readonly IGraphClient _graphClient;

        //private static readonly ILogger<UserGroupRepository> Logger;

        #region Constructors

        public UserGroupRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public void SaveOrUpdate(List<RepositoryUserGroupMapping> repositoryUserGroup)
        {
            try
            {
                foreach (var userGroup in repositoryUserGroup)
                {
                    if (userGroup.RepositoryUserGroupID == 0)
                    {
                        //this._context.RepositoryUserGroup.Add(userGroup);
                    }
                    else
                    {
                        //this._context.Entry(userGroup).State = EntityState.Modified;
                    }
                    //_context.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            //catch (DbEntityValidationException ex)
            //{
            //    throw new RepositoryException("", ex);
            //}
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }
    }
    
}
