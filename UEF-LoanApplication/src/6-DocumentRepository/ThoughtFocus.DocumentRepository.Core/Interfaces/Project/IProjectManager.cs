using System;
using System.Threading.Tasks;
using ThoughtFocus.DocumentRepository.Domain;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IProjectManager
    {
        Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest);
        Task<ProjectResponse> DeleteProjectAsync(ProjectRequest projectRequest);
        Task<ProjectResponse> RenameProjectAsync(ProjectRequest projectRequest);
        bool HasProjects(Guid projectId);
        ProjectResponse GetRootProjects(long userID);
        ProjectResponse GetProjectById(Guid projectId);
        ProjectResponse GetProjectsByParentId(Guid parentId,long userID);
        string GetVirtualPath(Guid projectID, string projectName);
        ProjectResponse GetRootProjectDetails(Guid projectId);
        ProjectResponse GetParentProjectById(Guid projectId);
        ProjectResponse GetProjectByDocumentTypeID(int documentTypeID,long ID, string workspaceName);
        ProjectResponse GetParentProjectByName(string projectName);
    }
}
