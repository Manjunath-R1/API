using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class UserGroupEntity
    {
        public long RepositoryUserGroupID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public Guid RepositoryUserID { get; set; }
        public Guid GroupID { get; set; }

        public string GroupName { get; set; }
        public string UserName { get; set; }

    }
}
