using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Repository.Core;
using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl
{
    public class GroupRepository : AbstractNeo4jBaseRepository<Group>, IGroupRepository
    {
        #region Filelds

        //private static readonly ILogger<GroupRepository> Logger;
        private readonly IGraphClient _graphClient;

        #endregion Filelds

        #region Constructors

        public GroupRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        #region Methods

        public List<Group> GetGroups()
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait(); 

                var documentsQuery = _graphClient.Cypher
                   .Match("(group:Group) ")
                   .Where((Group group) => group.IsActive == true)
                   .Return(group => group.As<Group>());
                var results = documentsQuery.ResultsAsync.Result.ToList();
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

        public void SaveGroup(Group group)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                    _graphClient
                        .Cypher
                        .Merge("(group:Group { GroupID: {id} })")
                        .OnCreate().Set("group = {group}")
                        .OnMatch().Set("group = {group}")
                        .WithParams(new
                        {
                            id = group.GroupID,
                            group = group
                        })
                        .ExecuteWithoutResultsAsync();

                    _graphClient.Cypher.Create("INDEX ON :Group(GroupID)").ExecuteWithoutResultsAsync();
                
                
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

        public void SaveUserGroupRelation(Guid groupID,Guid userID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                    .Match("(group:Group)", "(repositoryUser:RepositoryUser)")
                    .Where((Group group) => group.GroupID == groupID && group.IsActive==true)
                    .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID && repositoryUser.IsActive == true)
                    .CreateUnique("(repositoryUser)-[r:BELONGS_TO]->(group)");

                query.ExecuteWithoutResultsAsync();
                
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

        public Group GetGroupByID(Guid groupID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var documentsQuery = _graphClient.Cypher
                   .Match("(group:Group)")
                   .Where((Group group) => group.GroupID == groupID && group.IsActive==true)
                   .Return(group => group.As<Group>());

                var result = documentsQuery.ResultsAsync.Result.Single();

                return result;

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

        public List<RepositoryUserGroupMapping> GetUserGroupRelations()
        {
            try
            {
                var query = _graphClient.Cypher
                    .Match(string.Format("(repositoryUser:RepositoryUser)-[relation: BELONGS_TO]->(group:Group)"))
                    .Where((RepositoryUser repositoryUser) => repositoryUser.IsActive == true)
                    .AndWhere((Group group) => group.IsActive == true)
                    .Return((repositoryUser, group) => new
                    {
                        User = repositoryUser.As<RepositoryUser>(),

                        Group = group.As<Group>()
                    });

                var userGroupCollection = query.ResultsAsync.Result.Select(g =>
                    new RepositoryUserGroupMapping
                    {
                        GroupID = g.Group.GroupID,
                        GroupName = g.Group.Name,
                        UserName = g.User.FirstName +" "+ g.User.LastName,
                        RepositoryUserID = g.User.UserGuID
                    }).ToList();

                return userGroupCollection;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentsByParentId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentsByParentId repository", ex);
            }
        }

        public List<Group> GetUserBelongsTo(Guid userID)
        {
            var query = _graphClient.Cypher
                        .Match(string.Format("(repositoryUser:RepositoryUser)-[relation: BELONGS_TO]->(group:Group)"))
                        .Where((RepositoryUser repositoryUser) => repositoryUser.IsActive == true && repositoryUser.UserGuID == userID)
                        .AndWhere((Group group) => group.IsActive == true)
                        .Return(group => group.As<Group>());

            var results = query.ResultsAsync.Result.ToList();

            return results;
        }

        public void DeleteGroupRelations(Group groupRequest)
        {
            if (!_graphClient.IsConnected)
                _graphClient.ConnectAsync().Wait();

            var query = _graphClient.Cypher
                            .Match("(n)-[r:BELONGS_TO]->(group:Group)")
                            .With("r,group")
                            .Match("(group:Group)-[r1:CAN_ACCESS]->(c)")
                            .Where((Group group) => group.GroupID == groupRequest.GroupID)
                            .Delete("r,r1");
                query.ExecuteWithoutResultsAsync();
        }

        public void DeleteUserGroupRelations(RepositoryUserGroupMapping userGroupMapping)
        {
            if (!_graphClient.IsConnected)
                _graphClient.ConnectAsync().Wait();

            var query = _graphClient.Cypher
                            .Match("(repositoryUser:RepositoryUser)-[r:BELONGS_TO]->(group:Group)")
                            .Where((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userGroupMapping.RepositoryUserID)
                            .AndWhere((Group group) => group.GroupID == userGroupMapping.GroupID)
                            .Delete("r");
            query.ExecuteWithoutResultsAsync();
        }

        public List<RepositoryUserGroupMapping> GetUsersByGroup(Guid groupID)
        {
            try
            {
                var query = _graphClient.Cypher
                    .Match(string.Format("(repositoryUser:RepositoryUser)-[relation: BELONGS_TO]->(group:Group)"))
                    .Where((RepositoryUser repositoryUser) => repositoryUser.IsActive == true)
                    .AndWhere((Group group) => group.IsActive == true && group.GroupID == groupID)
                    .Return((repositoryUser, group) => new
                    {
                        User = repositoryUser.As<RepositoryUser>(),

                        Group = group.As<Group>()
                    });

                var userGroupCollection = query.ResultsAsync.Result.Select(g =>
                    new RepositoryUserGroupMapping
                    {
                        GroupID = g.Group.GroupID,
                        GroupName = g.Group.Name,
                        UserName = g.User.FirstName + " " + g.User.LastName,
                        RepositoryUserID = g.User.UserGuID
                    }).ToList();

                return userGroupCollection;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetDocumentsByParentId repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetDocumentsByParentId repository", ex);
            }
        }

        #endregion Methods

    }
}
