using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;

using ThoughtFocus.DocumentRepository.Repository.Core;
using Neo4jClient;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl
{
    public class ProjectRepositoryImpl : AbstractNeo4jBaseRepository<Project>, IProjectRepository
    {
        private IGraphClient _graphClient;
        //private static readonly ILogger<ProjectRepositoryImpl> Logger;

        #region Constructors

        public ProjectRepositoryImpl(IGraphClient graphClient)
        {
            _graphClient = graphClient;

        }

        #endregion Constructors

        public void SaveProject(Project project)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                //if (project.ProjectID == Guid.Empty)
                //{

                var query = _graphClient.Cypher
                            .Merge("(project:Project { ProjectID: {id} })")
                            .OnCreate().Set("project = {project}")
                            .OnMatch().Set("project = {project}")
                            .WithParams(new
                             {
                               id = project.ProjectID,
                               project
                             });

                  query.ExecuteWithoutResultsAsync();

                    //this._context.Projects.Add(project);
                //}
                //else
                //{
                //    //this._context.Entry(project).State = EntityState.Modified;
                //}
                //_context.SaveChanges();

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the projects", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the projects", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the projects ", ex);
            }
        }

        public void SaveProjects(List<DataAccess.Neo4j.Project> projects)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                    .Unwind(projects, "project")
                    .Match("(projectType:ProjectType)")
                    .Where("projectType.ProjectTypeID=project.ProjectTypeID")
                    .Merge("(p:Project { ProjectID : project.ProjectID })")
                    .CreateUnique("(p)-[r:IS_OF]->(projectType)")
                    .Set("p = project");

                  query.ExecuteWithoutResultsAsync();

                _graphClient.Cypher.CreateUniqueConstraint("p:Project", "p.ProjectID").ExecuteWithoutResultsAsync();

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the projects", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the projects", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the projects ", ex);
            }
        }

        public void SaveProjectMapping(ProjectMapping projectMapping)
        {
            try
            {

                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (projectMapping != null)
                {
                    var query = _graphClient.Cypher
                                       .Match("(project:Project)", "(parentproject:Project)")
                                       .Where((Project project) => project.ProjectID.ToString() == projectMapping.ProjectID.ToString())
                                       .AndWhere((Project parentproject) => parentproject.ProjectID.ToString() == projectMapping.ParentProjectID.ToString())
                                       .CreateUnique("(project)-[r:BELONGS_TO]->(parentproject)");

                    query.ExecuteWithoutResultsAsync();

                }



            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in SaveProjectMapping repository", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveProjectMapping repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveProjectMapping repository", ex);
            }
        }

        public List<Project> GetRootProjects(Guid userID)
        {
            try
            {

                var projectsQuery = _graphClient.Cypher
                                        .Match("(repositoryUser:RepositoryUser)" + "-[r:IS_OWNER]->" + "(project:Project)")
                                        .Where((DataAccess.Neo4j.Project project) => project.ProjectTypeID == (int)ProjectTypeEnumeration.Workspace)
                                        .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                        .Return((project) => project.As<DataAccess.Neo4j.Project>())
                                        .Union()
                                        .Match("(repositoryUser:RepositoryUser)" + "-[r1:CAN_ACCESS]->" + "(project:Project)")
                                        .Where((DataAccess.Neo4j.Project project) => project.ProjectTypeID == (int)ProjectTypeEnumeration.Workspace)
                                        .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                        .With("r1, project")
                                        .Match("(accessRole:AccessRole{Name:r1.RoleName})" + "-[m:CONTAINS]->" + "(permission:Permission{Name:'View'})")
                                        .Return((project) => project.As<DataAccess.Neo4j.Project>())
                                        .Union()
                                        .Match("(repositoryUser:RepositoryUser)" + "-[r2:BELONGS_TO]->" + "(group:Group)")
                                        .Where((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                        .With("group")
                                        .Match("(group:Group{GroupID:group.GroupID})" + "-[r3:CAN_ACCESS]->" + "(project:Project)")
                                        .Where((DataAccess.Neo4j.Project project) => project.ProjectTypeID == (int)ProjectTypeEnumeration.Workspace)
                                        .With("r3,project")
                                        .Match("(accessRole:AccessRole{Name:r3.RoleName})" + "-[m:CONTAINS]->" + "(permission:Permission{Name:'View'})")
                                        .Return((project) => project.As<DataAccess.Neo4j.Project>());

                var projectCollection = projectsQuery.ResultsAsync.Result.ToList();

                return projectCollection;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in GetRootProjects repository", ex);
            }

            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetRootProjects repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetRootProjects repository", ex);
            }
        }        

        public List<Project> GetProjectsByParentId(Guid parentId, Guid userID)
        {
            List<Project> projects = new List<Project>();
            try
            {
                var projectsQuery = _graphClient.Cypher
                                    .Match(string.Format("(project:Project)-[relation: BELONGS_TO]->(parentProject:Project)"))
                                    .Where((Project parentProject) => parentProject.ProjectID == parentId)
                                    .AndWhere((Project project) => project.IsActive == true && project.IsInherit == true)
                                    .Return((project, relations) => new
                                    {
                                        Project = project.As<Project>()
                                    })
                                    .Union()
                                    .Match(string.Format("(repositoryUser:RepositoryUser)-[r:IS_OWNER]->(project:Project)-[relation: BELONGS_TO]->(parentProject:Project)"))
                                    .Where((Project parentProject) => parentProject.ProjectID == parentId)
                                    .AndWhere((Project project) => project.IsActive == true && project.IsInherit==false)
                                    .AndWhere((RepositoryUser repositoryUser)=> repositoryUser.UserGuID==userID)
                                    .Return((project, relations) => new
                                    {
                                        Project = project.As<Project>()
                                    })
                                    .Union()
                                    .Match(string.Format("(repositoryUser:RepositoryUser)-[r1:CAN_ACCESS]->(project:Project)-[relation: BELONGS_TO]->(parentProject:Project)"))
                                    .Where((Project parentProject) => parentProject.ProjectID == parentId)
                                    .AndWhere((Project project) => project.IsActive == true && project.IsInherit == false)
                                    .AndWhere((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                    .With("r1, project")
                                    .Match("(accessRole:AccessRole{Name:r1.RoleName})" + "-[m:CONTAINS]->" + "(permission:Permission{Name:'View'})")
                                    .Return((project, relations) => new
                                    {
                                        Project = project.As<Project>()
                                    })
                                    .Union()
                                    .Match("(repositoryUser:RepositoryUser)" + "-[r2:BELONGS_TO]->" + "(group:Group)")
                                    .Where((RepositoryUser repositoryUser) => repositoryUser.UserGuID == userID)
                                    .With("group")
                                    .Match("(group:Group{GroupID:group.GroupID})" + "-[r3:CAN_ACCESS]->" + "(project:Project)" + "-[relation: BELONGS_TO]->" + "(parentProject:Project)")
                                    .Where((Project parentProject) => parentProject.ProjectID == parentId)
                                    .AndWhere((Project project) => project.IsActive == true && project.IsInherit == false)
                                    .With("r3,project")
                                    .Match("(accessRole:AccessRole{Name:r3.RoleName})" + "-[m:CONTAINS]->" + "(permission:Permission{Name:'View'})")
                                    .Return((project, relations) => new
                                    {
                                        Project = project.As<Project>()
                                    });

                var projectCollection = projectsQuery.ResultsAsync.Result.Select(p =>
                    new Project
                    {
                        ProjectID = p.Project.ProjectID,
                        CreatedDateTime = p.Project.CreatedDateTime,
                        LastModifiedDateTime = p.Project.LastModifiedDateTime,
                        CreatedByUserID = p.Project.CreatedByUserID,
                        LastModifiedByUserID = p.Project.LastModifiedByUserID,
                        IsActive = p.Project.IsActive,
                        Name = p.Project.Name,
                        VirtualPath = p.Project.VirtualPath,
                        Description =  p.Project.Description,
                        PhysicalPath =  p.Project.PhysicalPath,
                        StorageKey =  p.Project.StorageKey,
                        IsInherit =  p.Project.IsInherit,
                        ProjectTypeID =  p.Project.ProjectTypeID,
                        ID = p.Project.ID
                      }).ToList();

                return projectCollection;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while Getting the projects", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while Getting the projects", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while Getting the projects ", ex);
            }
        }

        public List<Project> GetProjectsByParentIdForDirectoryKey(Guid parentId)
        {
            List<Project> projects = new List<Project>();
            try
            {
                var projectsQuery = _graphClient.Cypher
                                    .Match(string.Format("(project:Project)-[relation: BELONGS_TO]->(parentProject:Project)"))
                                    .Where((Project parentProject) => parentProject.ProjectID == parentId)
                                    .AndWhere((Project project) => project.IsActive == true)
                                    .Return((project) => new
                                    {
                                        Project = project.As<Project>()
                                    });

                var projectCollection = projectsQuery.ResultsAsync.Result.Select(p =>
                    new Project
                    {
                        ProjectID = p.Project.ProjectID,
                        CreatedDateTime = p.Project.CreatedDateTime,
                        LastModifiedDateTime = p.Project.LastModifiedDateTime,
                        CreatedByUserID = p.Project.CreatedByUserID,
                        LastModifiedByUserID = p.Project.LastModifiedByUserID,
                        IsActive = p.Project.IsActive,
                        Name = p.Project.Name,
                        VirtualPath = p.Project.VirtualPath,
                        Description = p.Project.Description,
                        PhysicalPath = p.Project.PhysicalPath,
                        StorageKey = p.Project.StorageKey,
                        IsInherit = p.Project.IsInherit,
                        ProjectTypeID = p.Project.ProjectTypeID,
                        ID = p.Project.ID
                    }).ToList();

                return projectCollection;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while Getting the projects", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while Getting the projects", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while Getting the projects ", ex);
            }
        }

        public Project GetProjectById(Guid projectId)
        {
            try
            {
                var result =
                _graphClient
                       .Cypher
                       .Match("(project:Project)")
                       .Where((Project project) => project.ProjectID.ToString() == projectId.ToString())
                       .Return(project => project.As<Project>())
                       .ResultsAsync.Result
                       .SingleOrDefault();

                return result;
                //return _context.Projects.Where(p => p.ProjectId == projectId).FirstOrDefault();
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the projects", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the projects", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the projects ", ex);
            }
        }

        public Project GetParentProjectById(Guid projectId)
        {
            try
            {
                var projectsQuery = _graphClient.Cypher
                    .Match(string.Format("(project:Project)-[relation: BELONGS_TO]->(parentProject:Project)"))
                    .Where((DataAccess.Neo4j.Project project) => project.ProjectID.ToString() == projectId.ToString())
                    .AndWhere((DataAccess.Neo4j.Project project) => project.IsActive == true)
                    .Return((parentProject, relations) => new
                    {
                        Project = parentProject.As<DataAccess.Neo4j.Project>()
                    });

                var parentProjectCollection = projectsQuery.ResultsAsync.Result.Select(p =>
                    new Project
                    {
                        ProjectID = p.Project.ProjectID,
                        CreatedDateTime = p.Project.CreatedDateTime,
                        LastModifiedDateTime = p.Project.LastModifiedDateTime,
                        CreatedByUserID = p.Project.CreatedByUserID,
                        LastModifiedByUserID = p.Project.LastModifiedByUserID,
                        IsActive = p.Project.IsActive,
                        Name = p.Project.Name,
                        VirtualPath = p.Project.VirtualPath,
                        Description = p.Project.Description,
                        PhysicalPath = p.Project.PhysicalPath,
                        StorageKey = p.Project.StorageKey,
                        IsInherit = p.Project.IsInherit,
                        ProjectTypeID = p.Project.ProjectTypeID,
                        ID = p.Project.ID
                    }).SingleOrDefault(); 

                return parentProjectCollection;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while GetParentProjectById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-whileGetParentProjectById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while GetParentProjectById", ex);
            }
        }

        public long GetNextFolderId(Guid ProjectID)
        {
            try
            {
                var result = _graphClient.Cypher
                       .Match("(project:Project)")
                       .Where((Project project) => project.ProjectID.ToString() == ProjectID.ToString())
                       .Return(project => project.As<Project>())
                       .ResultsAsync.Result
                       .SingleOrDefault();

                return Convert.ToInt64(result.StorageKey);
                //return _context.Projects.Where(p => p.ProjectId == projectId).FirstOrDefault();
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in GetNextFolderId", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in GetNextFolderId", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in GetNextFolderId ", ex);
            }
        }

        public bool HasProjects(Guid projectId)
        {

            try
            {
                var projectsQuery = _graphClient.Cypher
                   .Match(string.Format("(project:Project)-[relation: BELONGS_TO]->(parent:Project)"))
                   .Where((Project parent) => parent.ProjectID.ToString() == projectId.ToString())
                   .AndWhere((DataAccess.Neo4j.Project project) => project.IsActive == true)
                   .Return((project, relations) => new
                    {
                        Project = project.As<DataAccess.Neo4j.Project>()
                    });
                   

                var projectHasSubProjects = projectsQuery.ResultsAsync.Result.Any() ? true : false;

                return projectHasSubProjects;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException in HasProjects", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in HasProjects", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error in HasProjects ", ex);
            }
        }

        public void SaveProjectType(ProjectType projectType)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                if (projectType != null)
                {
                    var query = _graphClient
                          .Cypher
                          .Merge("(projectType:ProjectType { ProjectTypeID: {ProjectTypeID} })")
                          .OnCreate()
                          .Set("projectType = {projectType}")
                          .WithParams(new
                          {
                              ProjectTypeID = projectType.ProjectTypeID,
                              projectType
                          });
                        query.ExecuteWithoutResultsAsync();                    
                }
                

            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while inserting the ProjectType", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while inserting the ProjectType", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while inserting the ProjectType ", ex);
            }
        }
        public Project GetProjectByID(long ID)
        {
            try
            {
                var result =
                _graphClient
                       .Cypher
                       .Match("(project:Project)")
                       .Where((Project project) => project.ID == ID)
                       .Return(project => project.As<Project>())
                       .ResultsAsync.Result
                       .SingleOrDefault();

                return result;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while getting the project", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while getting the project", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while getting the project", ex);
            }
        }

        public Project GetParentProjectByName(string name)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();
                var result =
                _graphClient
                       .Cypher
                       .Match("(project:Project)")
                       .Where((Project project) => project.Name.ToLower() == name.ToLower() && project.ProjectTypeID == (int)ProjectTypeEnumeration.Workspace)
                       .Return(project => project.As<Project>())
                       .ResultsAsync.Result
                       .SingleOrDefault();

                return result;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while getting the project", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while getting the project", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while getting the project", ex);
            }
        }



    }
}
