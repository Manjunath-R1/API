using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Enumeration;
using ThoughtFocus.DocumentRepository.Domain.Response;
using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Document;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DocumentRepository.StorageService;

namespace ThoughtFocus.DocumentRepository.Core.Impl
{
    public class ProjectManager : IProjectManager
    {
        private readonly ILogger<ProjectManager> _logger;
        private IStorageService _storageProvider;
        private IAuthorizer _authorizer;
        private IActionLogger _actionLogger;
        private IProjectRepository _projectRepository;
        private IDocumentRepository _documentRepository;
        private IUserRepository _userProvider;
        private IAccessRoleRepository _accessRoleRepository;
        private RepositoryUser repositoryUser = null;

        public ProjectManager(
            IAuthorizer authorizer, 
            IActionLogger actionLogger,
            IProjectRepository projectRepository, 
            IDocumentRepository documenRepository, 
            IUserRepository userProvider,
            IAccessRoleRepository accessRoleRepository, 
            IStorageService storageProvider,
            ILogger<ProjectManager> logger
            )
        {
            _storageProvider = storageProvider;
            _authorizer = authorizer;
            _actionLogger = actionLogger;
            _projectRepository = projectRepository;
            _documentRepository = documenRepository;
            _userProvider = userProvider;
            _accessRoleRepository = accessRoleRepository;
            _logger = logger;
        }

        public async Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            DateTime currentDateTime = DateTime.Now;

            try
            {

                var storageKey = GetNextFolderId(projectRequest);
                projectRequest.PhysicalPath = GetProjectPhysicalPathByParentId(projectRequest, storageKey);
                var virtualPath = GetProjectVirtualPathByParentId(projectRequest);

                projectResponse = await _storageProvider.CreateProjectAsync(projectRequest);

                if (projectResponse.IsSuccess)
                {
                    var project = new Project();
                    project.ProjectID = Guid.NewGuid();
                    project.CreatedDateTime = currentDateTime;
                    project.LastModifiedDateTime = currentDateTime;
                    project.CreatedByUserID = projectRequest.UserID;
                    project.LastModifiedByUserID = projectRequest.UserID;
                    project.IsActive = true;
                    project.Name = projectRequest.Name;
                    project.Description = projectRequest.Name;
                    project.PhysicalPath = projectRequest.PhysicalPath;
                    project.VirtualPath = virtualPath;
                    project.StorageKey = storageKey;
                    project.IsInherit = true;
                    project.ID = projectRequest.ID;
                    project.ProjectTypeID = projectRequest.RootFolder == true ? 1 : 2;

                    this._projectRepository.SaveProject(project);
                    projectResponse.ProjectID = project.ProjectID;
                    projectResponse.Path = project.VirtualPath;
                    projectResponse.timestamp = project.CreatedDateTime;
                    projectResponse.IsSuccess = true;

                    if (projectRequest.ParentId != null)
                    {
                        var projectMapping = new ProjectMapping();
                        projectMapping.CreatedDateTime = currentDateTime;
                        projectMapping.LastModifiedDateTime = currentDateTime;
                        projectMapping.CreatedByUserID = projectRequest.UserID;
                        projectMapping.LastModifiedByUserID = projectRequest.UserID;
                        projectMapping.IsActive = true;
                        projectMapping.ProjectID = projectResponse.ProjectID;
                        projectMapping.ParentProjectID = projectRequest.ParentId;

                        this._projectRepository.SaveProjectMapping(projectMapping);
                    }

                    AccessPermission accessPermission = new AccessPermission();
                    accessPermission.ContentNodeName = NodeNameEnumeration.Project.ToString();
                    accessPermission.AssigneeNodeName = NodeNameEnumeration.RepositoryUser.ToString();
                    accessPermission.ContentKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                    accessPermission.ContentID = projectResponse.ProjectID;
                    accessPermission.AssigneeKeyName = NodeKeyNameEnumeration.UserGuID.ToString();
                    accessPermission.AssigneeID = _userProvider.GetUser(projectRequest.UserID).UserGuID;

                    this._accessRoleRepository.AssignOwnerPermission(accessPermission);

                    //*Adding the super admin permission as a owner for the newly uploaded project*//
                    repositoryUser = _userProvider.GetUser((long)SystemAdminUserEnumeration.SuperAdmin);
                    if (repositoryUser.UserGuID != Guid.Empty)
                        accessPermission.AssigneeID = repositoryUser.UserGuID;

                    this._accessRoleRepository.AssignOwnerPermission(accessPermission);

                    //Activity log added with Created Project
                    ActivityLog activityLog = new ActivityLog();
                    repositoryUser = _userProvider.GetUser(projectRequest.UserID);
                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.AddedContent.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Project.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                    activityLog.KeyValue = project.ProjectID.ToString();
                    activityLog.Custom1 = project.ProjectID.ToString();
                    this._actionLogger.LogUserActivity(activityLog);


                    projectResponse.IsSuccess = true;
                    projectResponse.Message = "Folder created successfully";

                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            return projectResponse;

        }

        public async Task<ProjectResponse> RenameProjectAsync(ProjectRequest projectRequest)
        {
            var projectResponse = new ProjectResponse();
            ActivityLog activityLog = new ActivityLog();
            DateTime currentDateTime = DateTime.Now;
            try
            {
                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();
                repositoryUser = _userProvider.GetUser(projectRequest.UserID);
                if (repositoryUser != null)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Project.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                authorizeRequest.ContentID = projectRequest.Id;
                authorizeRequest.ActionName = ActionNameEnumeration.Rename.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "You are not authorized to perform this action";
                    return projectResponse;
                }
                #endregion
                var virtualPath = GetProjectVirtualPathByParentId(projectRequest);
                var project = this._projectRepository.GetProjectById(projectRequest.Id);
                string projectOldName = string.Empty;
                if (project != null)
                {
                    projectOldName = project.Name;
                    project.ProjectID = projectRequest.Id;
                    project.LastModifiedDateTime = currentDateTime;
                    project.LastModifiedByUserID = projectRequest.UserID;
                    project.Name = projectRequest.Name;
                    project.Description = projectRequest.Name;
                    project.VirtualPath = virtualPath;

                    this._projectRepository.SaveProject(project);
                    projectResponse.ProjectID = project.ProjectID;
                    projectResponse.ProjectName = project.Name;
                    projectResponse.Path = project.VirtualPath;
                    projectResponse.IsSuccess = true;

                    //Activity log added with Renamed Project

                    if (repositoryUser != null)
                        activityLog.UserGuID = repositoryUser.UserGuID;
                    activityLog.ActivityName = ActivityNameEnumeration.Renamed.ToString();
                    activityLog.NodeName = NodeNameEnumeration.Project.ToString();
                    activityLog.NodeKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                    activityLog.KeyValue = project.ProjectID.ToString();
                    activityLog.Custom1 = project.ProjectID.ToString();
                    activityLog.Custom2 = projectOldName.ToString();
                    activityLog.Custom3 = projectRequest.Name.ToString();
                    this._actionLogger.LogUserActivity(activityLog);

                    projectResponse.IsSuccess = true;
                    projectResponse.Message = "Folder renamed successfully";


                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            return projectResponse;

        }

        public async Task<ProjectResponse> DeleteProjectAsync(ProjectRequest projectRequest)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            DateTime currentDateTime = DateTime.Now;

            try
            {

                #region Authorize
                AuthorizeRequest authorizeRequest = new AuthorizeRequest();

                repositoryUser = _userProvider.GetUser(projectRequest.UserID);
                if (repositoryUser.UserGuID != Guid.Empty)
                    authorizeRequest.LoggedInUserID = repositoryUser.UserGuID;
                authorizeRequest.ContentNodeName = NodeNameEnumeration.Project.ToString();
                authorizeRequest.ContentKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                authorizeRequest.ContentID = projectRequest.Id;
                authorizeRequest.ActionName = ActionNameEnumeration.Delete.ToString();

                AuthorizationResponse authorizationResponse = this._authorizer.AuthorizeUser(authorizeRequest);
                if (!authorizationResponse.IsAllowed)
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "You are not authorized to perform this action";
                    return projectResponse;
                }
                #endregion
                var project = this._projectRepository.GetProjectById(projectRequest.Id);
                projectRequest.PhysicalPath = project.PhysicalPath;

                var subProjectByProjectId = _projectRepository.GetProjectsByParentId(projectRequest.Id, repositoryUser.UserGuID);

                if (subProjectByProjectId.Any())
                {
                    projectResponse.Message = "Please delete sub folders, if any";
                    projectResponse.IsSuccess = false;
                    return projectResponse;
                }
                projectResponse = null;//await _storageProvider.DeleteProjectAsync(projectRequest);

                if (project != null && projectResponse.IsSuccess)
                {
                    var parentProject = this._projectRepository.GetParentProjectById(projectRequest.Id);
                    project.ProjectID = projectRequest.Id;
                    project.LastModifiedDateTime = currentDateTime;
                    project.LastModifiedByUserID = projectRequest.UserID;
                    project.IsActive = false;
                    this._projectRepository.SaveProject(project);
                    projectResponse.Message = "Folder deleted successfully";
                    projectResponse.IsSuccess = true;


                    //Activity log added with Deleted Project                   
                    if (parentProject != null)
                    {
                        ActivityLog activityLog = new ActivityLog();
                        if (repositoryUser != null)
                            activityLog.UserGuID = repositoryUser.UserGuID;
                        activityLog.ActivityName = ActivityNameEnumeration.Deleted.ToString();
                        activityLog.NodeName = NodeNameEnumeration.Project.ToString();
                        activityLog.NodeKeyName = NodeKeyNameEnumeration.ProjectID.ToString();
                        activityLog.KeyValue = parentProject.ProjectID.ToString();
                        activityLog.Custom1 = parentProject.ProjectID.ToString();
                        activityLog.Custom2 = "Folder : " + project.Name.ToString();
                        this._actionLogger.LogUserActivity(activityLog);
                    }

                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while uploading document{0}", ex));
                throw new BusinessException("", ex);
            }
            return projectResponse;

        }

        private string GetNextFolderId(ProjectRequest projectRequest)
        {
            if (projectRequest.ParentId == null)
            {
                return projectRequest.Name;
            }
            var projects = _projectRepository.GetProjectsByParentIdForDirectoryKey(projectRequest.ParentId);

            if (!projects.Any())
                return GetInitialFolderId();

            long outId;

            var maxDirectoryId = projects.ToList().Where(f => f.StorageKey != null && long.TryParse(f.StorageKey, out outId))
                                         .Select(f => long.Parse(f.StorageKey)).Max();


            //var maxDirectoryId = _projectRepository.GetNextFolderId(projects);


            return (++maxDirectoryId).ToString();
        }

        private string GetInitialFolderId()
        {
            //TODO: Write the logic to determine first project in the parent project
            return "1";
        }

        private string GetProjectPhysicalPathByParentId(ProjectRequest projectRequest, string storageKey)
        {
            if (projectRequest.ParentId == null)
                return string.Empty;

            var project = _projectRepository.GetProjectById(projectRequest.ParentId);

            if (project == null)
                return string.Empty;

            return (projectRequest.ParentId != null ? project.PhysicalPath : string.Empty) + "/" + storageKey; //end the folder name with "/"

        }

        private string GetProjectVirtualPathByParentId(ProjectRequest projectRequest)
        {
            if (projectRequest.ParentId == null)
                return string.Empty;

            var project = _projectRepository.GetProjectById(projectRequest.ParentId);

            if (project == null)
                return projectRequest.Name;

            return (projectRequest.ParentId != null ? project.VirtualPath : string.Empty) + "/" + projectRequest.Name;

        }


        public ProjectResponse GetRootProjects(long userID)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            projectResponse.ProjectEntityList = new List<ProjectEntity>();

            try
            {
                repositoryUser = _userProvider.GetUser(userID);
                var rootProjects = this._projectRepository.GetRootProjects(repositoryUser.UserGuID).OrderBy(p => p.Name);

                if (rootProjects != null)
                {
                    foreach (var project in rootProjects)
                    {
                        ProjectEntity projectEntity = new ProjectEntity();
                        projectEntity.ProjectID = project.ProjectID;
                        projectEntity.CreatedDateTime = project.CreatedDateTime;
                        projectEntity.LastModifiedDateTime = project.LastModifiedDateTime;
                        projectEntity.CreatedByUserID = project.CreatedByUserID;
                        projectEntity.LastModifiedByUserID = project.LastModifiedByUserID;
                        projectEntity.IsActive = project.IsActive;
                        projectEntity.Name = project.Name;
                        projectEntity.VirtualPath = project.VirtualPath;
                        projectEntity.Description = project.Description;
                        projectEntity.PhysicalPath = project.PhysicalPath;
                        projectEntity.StorageKey = project.StorageKey;
                        projectEntity.IsInherit = project.IsInherit;
                        projectEntity.ProjectTypeID = project.ProjectTypeID;
                        projectResponse.ProjectEntityList.Add(projectEntity);
                    }

                    projectResponse.IsSuccess = true;
                }
                else
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "Unable to fetch the projects. Please try after some time.";
                }

                return projectResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetRootProjects", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ProjectResponse GetProjectsByParentId(Guid parentId, long userID)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            projectResponse.ProjectEntityList = new List<ProjectEntity>();

            try
            {
                repositoryUser = _userProvider.GetUser(userID);
                var projectsByParentId = this._projectRepository.GetProjectsByParentId(parentId, repositoryUser.UserGuID).OrderBy(p => p.Name);

                if (projectsByParentId != null)
                {
                    foreach (var project in projectsByParentId)
                    {
                        ProjectEntity projectEntity = new ProjectEntity();
                        projectEntity.ProjectID = project.ProjectID;
                        projectEntity.CreatedDateTime = project.CreatedDateTime;
                        projectEntity.LastModifiedDateTime = project.LastModifiedDateTime;
                        projectEntity.CreatedByUserID = project.CreatedByUserID;
                        projectEntity.LastModifiedByUserID = project.LastModifiedByUserID;
                        projectEntity.IsActive = project.IsActive;
                        projectEntity.Name = project.Name;
                        projectEntity.VirtualPath = project.VirtualPath;
                        projectEntity.Description = project.Description;
                        projectEntity.PhysicalPath = project.PhysicalPath;
                        projectEntity.StorageKey = project.StorageKey;
                        projectEntity.IsInherit = project.IsInherit;
                        projectEntity.ProjectTypeID = project.ProjectTypeID;
                        projectResponse.ProjectEntityList.Add(projectEntity);
                    }

                    projectResponse.IsSuccess = true;
                }
                else
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "Unable to fetch the projects. Please try after some time.";
                }

                return projectResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetProjectsByParentId", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProjectResponse GetProjectById(Guid projectId)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            projectResponse.ProjectEntity = new ProjectEntity();

            try
            {
                var project = this._projectRepository.GetProjectById(projectId);
                if (project != null)
                {
                    projectResponse.ProjectEntity.ProjectID = project.ProjectID;
                    projectResponse.ProjectEntity.CreatedDateTime = project.CreatedDateTime;
                    projectResponse.ProjectEntity.LastModifiedDateTime = project.LastModifiedDateTime;
                    projectResponse.ProjectEntity.CreatedByUserID = project.CreatedByUserID;
                    projectResponse.ProjectEntity.LastModifiedByUserID = project.LastModifiedByUserID;
                    projectResponse.ProjectEntity.IsActive = project.IsActive;
                    projectResponse.ProjectEntity.Name = project.Name;
                    projectResponse.ProjectEntity.VirtualPath = project.VirtualPath;
                    projectResponse.ProjectEntity.Description = project.Description;
                    projectResponse.ProjectEntity.PhysicalPath = project.PhysicalPath;
                    projectResponse.ProjectEntity.StorageKey = project.StorageKey;
                    projectResponse.ProjectEntity.IsInherit = project.IsInherit;
                    projectResponse.ProjectEntity.ProjectTypeID = project.ProjectTypeID;
                    projectResponse.IsSuccess = true;
                }
                else
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "Unable to fetch the projects. Please try after some time.";
                }

                return projectResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetProjectById", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProjectResponse GetParentProjectById(Guid projectId)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            projectResponse.ProjectEntity = new ProjectEntity();

            try
            {
                var project = _projectRepository.GetParentProjectById(projectId);
                if (project != null)
                {
                    projectResponse.ProjectEntity.ProjectID = project.ProjectID;
                    projectResponse.ProjectEntity.CreatedDateTime = project.CreatedDateTime;
                    projectResponse.ProjectEntity.LastModifiedDateTime = project.LastModifiedDateTime;
                    projectResponse.ProjectEntity.CreatedByUserID = project.CreatedByUserID;
                    projectResponse.ProjectEntity.LastModifiedByUserID = project.LastModifiedByUserID;
                    projectResponse.ProjectEntity.IsActive = project.IsActive;
                    projectResponse.ProjectEntity.Name = project.Name;
                    projectResponse.ProjectEntity.VirtualPath = project.VirtualPath;
                    projectResponse.ProjectEntity.Description = project.Description;
                    projectResponse.ProjectEntity.PhysicalPath = project.PhysicalPath;
                    projectResponse.ProjectEntity.StorageKey = project.StorageKey;
                    projectResponse.ProjectEntity.IsInherit = project.IsInherit;
                    projectResponse.ProjectEntity.ProjectTypeID = project.ProjectTypeID;

                    projectResponse.IsSuccess = true;
                }
                else
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "Unable to fetch the projects. Please try after some time.";
                }

                return projectResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetParentProjectById", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HasProjects(Guid projectId)
        {
            return _projectRepository.HasProjects(projectId);
        }

        public string GetVirtualPath(Guid projectID, string projectName)
        {
            try
            {
                bool isRootFolder = false;
                string childVirtualPath = null;
                string virtualPath = null;

                var projectDetails = _projectRepository.GetProjectById(projectID);

                isRootFolder = projectDetails.ProjectTypeID == 1 ? true : false;

                while (!isRootFolder)
                {

                    var project = _projectRepository.GetParentProjectById(projectID);

                    if (project != null)
                        childVirtualPath = project.Name + "/" + childVirtualPath;
                    projectID = project.ProjectID;
                    isRootFolder = project.ProjectTypeID == 1 ? true : false;
                }
                virtualPath = childVirtualPath + "" + projectName;

                return virtualPath;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetVirtualPath", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ProjectResponse GetRootProjectDetails(Guid projectId)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            projectResponse.ProjectEntity = new ProjectEntity();

            try
            {
                bool isRootFolder = false;
                Project project = null;

                var projectDetails = this.GetProjectById(projectId);
                projectResponse.ProjectEntity = projectDetails.ProjectEntity;

                if (projectResponse.ProjectEntity.ProjectTypeID == (int)ProjectTypeEnumeration.Workspace)
                {
                    isRootFolder = true;
                    projectResponse.IsSuccess = true;
                    return projectResponse;
                }
                else
                {
                    while (!isRootFolder)
                    {

                        project = _projectRepository.GetParentProjectById(projectId);

                        if (project != null)
                        {
                            projectId = project.ProjectID;
                            isRootFolder = project.ProjectTypeID == 1 ? true : false;

                        }

                    }
                }


                if (project != null)
                {
                    projectResponse.ProjectEntity.ProjectID = project.ProjectID;
                    projectResponse.ProjectEntity.CreatedDateTime = project.CreatedDateTime;
                    projectResponse.ProjectEntity.LastModifiedDateTime = project.LastModifiedDateTime;
                    projectResponse.ProjectEntity.CreatedByUserID = project.CreatedByUserID;
                    projectResponse.ProjectEntity.LastModifiedByUserID = project.LastModifiedByUserID;
                    projectResponse.ProjectEntity.IsActive = project.IsActive;
                    projectResponse.ProjectEntity.Name = project.Name;
                    projectResponse.ProjectEntity.VirtualPath = project.VirtualPath;
                    projectResponse.ProjectEntity.Description = project.Description;
                    projectResponse.ProjectEntity.PhysicalPath = project.PhysicalPath;
                    projectResponse.ProjectEntity.StorageKey = project.StorageKey;
                    projectResponse.ProjectEntity.IsInherit = project.IsInherit;
                    projectResponse.ProjectEntity.ProjectTypeID = project.ProjectTypeID;
                    projectResponse.IsSuccess = true;
                }
                else
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "Unable to fetch the projects. Please try after some time.";
                }
                return projectResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetRootProjectDetails", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProjectResponse GetProjectByDocumentTypeID(int documentTypeID, long ID, string workSpaceName)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            try
            {
                List<Project> parentProjects = new List<Project>();
                Project parentProject = new Project();
                Project project = new Project();
                if (documentTypeID != 0)
                {
                    var workSpaceDetails = _projectRepository.GetParentProjectByName(workSpaceName);

                    if (workSpaceDetails != null)
                    {
                        parentProjects = _projectRepository.GetProjectsByParentIdForDirectoryKey(workSpaceDetails.ProjectID).ToList();

                        if (parentProjects.Count > 0)
                        {
                            parentProject = parentProjects.Where(x => x.ID == ID).FirstOrDefault();

                            if (parentProject.ProjectID != Guid.Empty)
                            {
                                List<Project> projects = _projectRepository.GetProjectsByParentIdForDirectoryKey(parentProject.ProjectID).ToList();

                                if (projects.Count > 0)
                                {
                                    project = projects.Where(x => x.ID == documentTypeID).FirstOrDefault();
                                    if(project!=null)
                                        projectResponse.ProjectID = project.ProjectID;
                                }
                                else
                                {
                                    projectResponse.ProjectID = default(Guid);
                                }
                            }
                        }
                        else
                        {
                            projectResponse = null;
                        }
                    }
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetProjectByDocumentType", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projectResponse;
        }

        public ProjectResponse GetParentProjectByName(string name)
        {
            ProjectResponse projectResponse = new ProjectResponse();
            projectResponse.ProjectEntity = new ProjectEntity();

            try
            {
                var project = this._projectRepository.GetParentProjectByName(name);
                if (project != null)
                {
                    projectResponse.ProjectID = project.ProjectID;
                    projectResponse.ProjectName = project.Name;
                    projectResponse.Description = project.Description;
                    projectResponse.StorageKey = project.StorageKey;
                    projectResponse.ProjectTypeID = project.ProjectTypeID;
                    projectResponse.IsSuccess = true;
                }
                else
                {
                    projectResponse.IsSuccess = false;
                    projectResponse.Message = "Unable to fetch the projects. Please try after some time.";
                }

                return projectResponse;
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("RepositoryException in GetProjectById", ex);
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
