using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        void SaveProject(Project project);
        void SaveProjects(List<DataAccess.Neo4j.Project> projects);
        void SaveProjectMapping(ProjectMapping projectMapping);
        List<Project> GetRootProjects(Guid userID);        
        List<Project> GetProjectsByParentId(Guid parentId,Guid userID);
        Project GetProjectById(Guid projectId);
        long GetNextFolderId(Guid projectId);
        bool HasProjects(Guid projectId);
        Project GetParentProjectById(Guid ProjectID);
        List<Project> GetProjectsByParentIdForDirectoryKey(Guid parentId);
        Project GetProjectByID(long ID);
        Project GetParentProjectByName(string projectName);
    }
}
