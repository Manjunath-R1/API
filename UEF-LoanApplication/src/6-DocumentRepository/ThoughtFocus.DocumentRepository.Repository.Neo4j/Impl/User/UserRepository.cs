using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;

using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl.User
{
    public class UserRepository : AbstractNeo4jBaseRepository<RepositoryUser>, IUserRepository
    {
        //private static readonly ILogger<UserRepository> Logger;
        private readonly IGraphClient _graphClient;
        #region Constructors

        public UserRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public void Save(RepositoryUser repositoryUser)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();


                var query = _graphClient
                        .Cypher
                        .Merge("(repositoryUser:RepositoryUser { UserGuID: {UserGuID} })")                                            
                        .Set("repositoryUser = {repositoryUser}")
                        .WithParams(new
                        {
                            UserGuID = repositoryUser.UserGuID,
                            repositoryUser
                        });

                query.ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("U:RepositoryUser", "U.UserGuID").ExecuteWithoutResultsAsync();

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while getting the Repository User", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while getting the Repository User", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while getting the Repository User ", ex);
            }
        }

        public List<RepositoryUser> GetUsers()
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var userQuery = _graphClient.Cypher
                   .Match("(repositoryUser:RepositoryUser) ")
                   .Where((RepositoryUser repositoryUser) => repositoryUser.IsActive == true)
                   .Return(repositoryUser => repositoryUser.As<RepositoryUser>());
                var results = userQuery.ResultsAsync.Result.ToList();
                return results;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        public RepositoryUser GetUser(long userID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var userQuery = _graphClient.Cypher
                   .Match("(repositoryUser:RepositoryUser) ")
                   .Where((RepositoryUser repositoryUser) => repositoryUser.IsActive == true)
                    .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserID == userID)
                   .Return(repositoryUser => repositoryUser.As<RepositoryUser>());
                var results = userQuery.ResultsAsync.Result.FirstOrDefault();
                return results;
                      
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while getting the Repository User", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while getting the Repository User", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while getting the Repository User ", ex);
            }
        }

        public RepositoryUser GetUser(Guid userID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var userQuery = _graphClient.Cypher
                   .Match("(repositoryUser:RepositoryUser) ")
                   .Where((RepositoryUser repositoryUser) => repositoryUser.IsActive == true)
                    .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                   .Return(repositoryUser => repositoryUser.As<RepositoryUser>());
                var results = userQuery.ResultsAsync.Result.FirstOrDefault();
                return results;

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while getting the Repository User", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while getting the Repository User", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while getting the Repository User ", ex);
            }
        }

        public void DeleteUserAccessRoleRelationship(Guid userID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                                .Match("(repositoryUser:RepositoryUser)-[r:CAN_ACCESS]->(c)")
                                .Where((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID);
                    query.Delete("r").ExecuteWithoutResultsAsync();
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while getting the Repository User", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while getting the Repository User", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while getting the Repository User ", ex);
            }
        }
    }
}
