using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class ProjectResponse : DocumentBaseResponse
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime timestamp { get; set; }
        public string StorageKey { get; set; }
        public long ProjectTypeID { get; set; }
        public Guid ParentID { get; set; }
        public ProjectEntity ProjectEntity { get; set; }
        public List<ProjectEntity> ProjectEntityList { get; set; }

    }
}
