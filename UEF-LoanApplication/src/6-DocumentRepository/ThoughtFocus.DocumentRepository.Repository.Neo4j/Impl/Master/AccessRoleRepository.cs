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
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Request;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl
{
    public class AccessRoleRepository : AbstractNeo4jBaseRepository<AccessRole>, IAccessRoleRepository
    {
       // private static readonly ILogger<AccessRoleRepository> Logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public AccessRoleRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        #endregion Constructors

        public List<AccessRole> GetAccessRoles()
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var permissionResults = new List<Permission>();

                var documentsQuery = _graphClient.Cypher
                   .Match("(accessRole:AccessRole) ")
                   .Where((AccessRole accessRole) => accessRole.IsActive == true)
                   .Return(accessRole => accessRole.As<AccessRole>());
                var results = documentsQuery.ResultsAsync.Result.ToList();


                foreach (var role in results)
                {
                    var query = _graphClient.Cypher
                   .Match("(accessRole:AccessRole)-[r:CONTAINS]->(permission:Permission)")
                   .Where((AccessRole accessRole) => accessRole.AccessRoleID == role.AccessRoleID && accessRole.IsActive == true)
                   .Return(permission => permission.As<Permission>());
                    var permissions = query.ResultsAsync.Result.ToList();

                    role.permissions = permissions;

                }

                return results;

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

        public List<Permission> GetPermissions()
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var documentsQuery = _graphClient.Cypher
                   .Match("(permission:Permission) ")
                   .Where((Permission permission) => permission.IsActive == true)
                   .Return(permission => permission.As<Permission>());
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

        public void AddOrUpdateAccessRole(AccessRole accessRole)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                _graphClient
                        .Cypher
                        .Merge("(accessRole:AccessRole { AccessRoleID: {id} })")
                        .OnCreate().Set("accessRole = {accessRole}")
                        .OnMatch().Set("accessRole = {accessRole}")
                        .WithParams(new
                        {
                            id = accessRole.AccessRoleID,
                            accessRole = accessRole
                        })
                        .ExecuteWithoutResultsAsync();


                _graphClient.Cypher.Create("INDEX ON :AccessRole(AccessRoleID)").ExecuteWithoutResultsAsync();
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

        public AccessRole GetAccessRoleByID(Guid accessRoleID)
        {
            if (!_graphClient.IsConnected)
                _graphClient.ConnectAsync().Wait();

            var documentsQuery = _graphClient.Cypher
               .Match("(accessRole:AccessRole)")
               .Where((AccessRole accessRole) => accessRole.AccessRoleID == accessRoleID && accessRole.IsActive == true)
               .Return(accessRole => accessRole.As<AccessRole>());

            var result = documentsQuery.ResultsAsync.Result.Single();

            return result;
        }

        public void AddOrUpdatePermissions(List<Permission> permissions, Guid accessRoleID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();


                foreach (var per in permissions)
                {
                    var query = _graphClient.Cypher
                   .Match("(permission:Permission)", "(accessRole:AccessRole)")
                   .Where((AccessRole accessRole) => accessRole.AccessRoleID == accessRoleID)
                   .AndWhere((Permission permission) => permission.PermissionID == per.PermissionID)
                   .Create("(accessRole)-[r:CONTAINS]->(permission)");
                    query.ExecuteWithoutResultsAsync();


                }

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

        public void DeleteRelationsForAccessRole(Guid accessRoleID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                           .Match("(accessRole:AccessRole)-[r:CONTAINS]->(permission:Permission)")
                           .Where((AccessRole accessRole) => accessRole.AccessRoleID == accessRoleID);

                var permissionList = query.Return(permission => permission.As<Permission>()).ResultsAsync.Result.ToList();

                if (permissionList.Count > 0)
                {
                    query.Delete("r").ExecuteWithoutResultsAsync();
                }

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

        public List<Permission> GetPermissionsByAccessRole(Guid accessRoleID)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var documentsQuery = _graphClient.Cypher
                   .Match("(accessRole:AccessRole)-[r:CONTAINS]->(permission:Permission)")
                   .Where((AccessRole accessRole) => accessRole.AccessRoleID == accessRoleID && accessRole.IsActive == true)
                   .Return(permission => permission.As<Permission>());
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

        public void AssignPermission(AccessPermission accessPermission)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                            .Match("(c:" + accessPermission.ContentNodeName + ")", "(a:" + accessPermission.AssigneeNodeName + ")")
                            .Where("c." + accessPermission.ContentKeyName + "=" + "'" + accessPermission.ContentID + "'")
                            .AndWhere("a." + accessPermission.AssigneeKeyName + "=" + "'" + accessPermission.AssigneeID + "'")
                            .CreateUnique("(a)-[r:CAN_ACCESS{RoleName:'" + accessPermission.RoleName + "'}]->(c)");

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

        public List<AccessPermissionResponse> GetPermissionsForDocumentOrProject(AccessPermission accessPermission)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                   .Match("(c:" + accessPermission.ContentNodeName + ")" + "<-[r:CAN_ACCESS]-" + "(n)")
                   .Where("c." + accessPermission.ContentKeyName + "=" + "'" + accessPermission.ContentID + "'")
                   .Return((n, r) => new
                   {
                       User = n.As<RepositoryUser>(),
                       Group = n.As<Group>(),
                       AccessRole = r.As<AccessPermission>().RoleName
                   });

                var documentOrProjectPermissions = query.ResultsAsync.Result.Select(p =>
                    new AccessPermissionResponse
                    {
                        GroupID = p.Group.GroupID,
                        GroupName = p.Group.Name,
                        UserName = p.User.FirstName + " " + p.User.LastName,
                        UserID = p.User.UserGuID,
                        RoleName = p.AccessRole
                    }).ToList();

                return documentOrProjectPermissions;
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

        public List<AccessPermissionResponse> GetPermissionsByUserID(AuthorizeRequest authorizeRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                   .Match("(c:" + authorizeRequest.ContentNodeName + ")" + "<-[r:CAN_ACCESS]-" + "(n)")
                   .Where("c." + authorizeRequest.ContentKeyName + "=" + "'" + authorizeRequest.ContentID + "'")
                   .AndWhere("n." + authorizeRequest.AssigneeKeyName + "=" + "'" + authorizeRequest.LoggedInUserID + "'")
                   .Return((n, r) => new
                   {
                       AccessRole = r.As<AccessPermission>().RoleName
                   });

                var documentOrProjectPermissions = query.ResultsAsync.Result.Select(p =>
                    new AccessPermissionResponse
                    {
                        RoleName = p.AccessRole
                    }).ToList();

                return documentOrProjectPermissions;
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

        public void DeletePermission(AccessPermission accessPermission)
        {
            var query = _graphClient.Cypher
                        .Match("(c:" + accessPermission.ContentNodeName + ")" + "<-[r:CAN_ACCESS]-" + "(a)")
                        .Where("c." + accessPermission.ContentKeyName + "=" + "'" + accessPermission.ContentID + "'")
                        .AndWhere("a." + accessPermission.AssigneeKeyName + "=" + "'" + accessPermission.AssigneeID + "'")
                        .AndWhere("r.RoleName" + "=" + "'" + accessPermission.RoleName + "'")
                        .Delete("r");
            query.ExecuteWithoutResultsAsync();

        }

        public List<AccessPermissionResponse> GetPermissionsByGroupID(AuthorizeRequest authorizeRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                   .Match("(c:" + authorizeRequest.ContentNodeName + ")" + "<-[r:CAN_ACCESS]-" + "(n)")
                   .Where("c." + authorizeRequest.ContentKeyName + "=" + "'" + authorizeRequest.ContentID + "'")
                   .AndWhere("n." + authorizeRequest.AssigneeKeyName + "=" + "'" + authorizeRequest.GroupID + "'")
                   .Return((n, r) => new
                   {
                       AccessRole = r.As<AccessPermission>().RoleName
                   });

                var documentOrProjectPermissions = query.ResultsAsync.Result.Select(p =>
                    new AccessPermissionResponse
                    {
                        RoleName = p.AccessRole
                    }).ToList();

                return documentOrProjectPermissions;
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

        public bool IsAllowed(AuthorizeRequest authorizeRequest, List<AccessPermissionResponse> assignedRoles)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
                AccessRole accessRole = new AccessRole();


                if (assignedRoles != null && assignedRoles.Count > 0)
                {
                    foreach (var role in assignedRoles)
                    {
                        List<AccessRole> accessRoles = GetAccessRoles();
                        accessRole = accessRoles.Where(a => a.Name.ToLower() == role.RoleName.ToLower()).LastOrDefault();
                        if (accessRole.permissions.Any(p => p.Name.ToLower() == authorizeRequest.ActionName.ToLower()))
                        {
                            return true;
                            //break;
                        }
                    }
                }
                return false;
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

        public void AssignOwnerPermission(AccessPermission accessPermission)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                            .Match("(c:" + accessPermission.ContentNodeName + ")", "(a:" + accessPermission.AssigneeNodeName + ")")
                            .Where("c." + accessPermission.ContentKeyName + "=" + "'" + accessPermission.ContentID + "'")
                            .AndWhere("a." + accessPermission.AssigneeKeyName + "=" + "'" + accessPermission.AssigneeID + "'")
                            .CreateUnique("(a)-[r:IS_OWNER]->(c)");

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

        public bool IsOwnerPermissionExists(AuthorizeRequest authorizeRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                   .Match("(c:" + authorizeRequest.ContentNodeName + ")" + "<-[r:IS_OWNER]-" + "(n)")
                   .Where("c." + authorizeRequest.ContentKeyName + "=" + "'" + authorizeRequest.ContentID + "'")
                   .AndWhere("n." + authorizeRequest.AssigneeKeyName + "=" + "'" + authorizeRequest.LoggedInUserID + "'")
                   .Return(r => r.As<AccessPermission>());
                var result = query.ResultsAsync.Result.ToList();
                if (result.Count > 0)
                {
                    return true;
                }
                return false;
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

        public void DeleteAccessRoleRelations(AccessRole accessRoleRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                                .Match("(n)-[r:CAN_ACCESS]->(c)")
                                .Where("toLower(r.RoleName)" + "=" + "'" + accessRoleRequest.Name.ToLower() + "'");
                    query.Delete("r").ExecuteWithoutResultsAsync();
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

        public InheritanceResponse GetInheritedProjectOrDocument(InheritanceRequest inheritanceRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                InheritanceResponse inheritanceResponse = new InheritanceResponse();

                Project projectDetails = new Project();

                if (inheritanceRequest.ContentNodeName == Domain.Enumeration.NodeNameEnumeration.Project.ToString())
                {
                    projectDetails = _graphClient
                                       .Cypher
                                       .Match("(project:Project)")
                                       .Where((Project project) => project.ProjectID == inheritanceRequest.ContentID)
                                       .Return(project => project.As<Project>())
                                       .ResultsAsync.Result
                                       .SingleOrDefault();
                }

                if (projectDetails.ProjectTypeID != (long)Domain.Enumeration.ProjectTypeEnumeration.Workspace)
                {
                    var query = _graphClient.Cypher
                                    .Match(@"(c:" + inheritanceRequest.ContentNodeName + "{" + inheritanceRequest.ContentKeyName + ":" + "'" + inheritanceRequest.ContentID + "'" + "}),"
                                            + "(p:Project),s=(c)-[:BELONGS_TO*]->(p)with p,filter (node in nodes(s)[0..] where node.IsInherit = false)[0]as Content");

                    var result = query.ReturnDistinct(Content => new { Project = Content.As<Project>(), Document = Content.As<Document>() }).ResultsAsync.Result.Where(x => x.Document != null && x.Project != null).FirstOrDefault();

                    if (result.Project.ProjectID != Guid.Empty)
                    {
                        inheritanceResponse.ContentNodeName = Domain.Enumeration.NodeNameEnumeration.Project.ToString();
                        inheritanceResponse.ContentKeyName = Domain.Enumeration.NodeKeyNameEnumeration.ProjectID.ToString();
                        inheritanceResponse.ContentID = result.Project.ProjectID;
                    }
                    else
                    {
                        inheritanceResponse.ContentNodeName = Domain.Enumeration.NodeNameEnumeration.Document.ToString();
                        inheritanceResponse.ContentKeyName = Domain.Enumeration.NodeKeyNameEnumeration.DocumentID.ToString();
                        inheritanceResponse.ContentID = result.Document.DocumentID;
                    }
                }
                else
                {
                    inheritanceResponse.ContentNodeName = Domain.Enumeration.NodeNameEnumeration.Project.ToString();
                    inheritanceResponse.ContentKeyName = Domain.Enumeration.NodeKeyNameEnumeration.ProjectID.ToString();
                    inheritanceResponse.ContentID = inheritanceRequest.ContentID;
                }

                return inheritanceResponse;
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

        public void RemoveInheritnace(InheritanceRequest inheritanceRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                            .Match("(c:" + inheritanceRequest.ContentNodeName + ")")
                            .Where("c." + inheritanceRequest.ContentKeyName + "=" + "'" + inheritanceRequest.ContentID + "'")
                            .Set("c.IsInherit" + "=" + false);

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

        public void RemoveUniquePermissions(InheritanceRequest inheritanceRequest)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                            .Match("(u)-[r:CAN_ACCESS]->(c:" + inheritanceRequest.ContentNodeName + ")")
                            .Where("c." + inheritanceRequest.ContentKeyName + "=" + "'" + inheritanceRequest.ContentID + "'");
                       query.Delete("r").ExecuteWithoutResultsAsync();

                var contentQuery = _graphClient.Cypher
                                    .Match("(c:" + inheritanceRequest.ContentNodeName + ")")
                                    .Where("c." + inheritanceRequest.ContentKeyName + "=" + "'" + inheritanceRequest.ContentID + "'")
                                    .Set("c.IsInherit" + "=" + true);
                    contentQuery.ExecuteWithoutResultsAsync();

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
    }
}
