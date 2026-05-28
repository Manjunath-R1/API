using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class GroupEntity
    {
        public Guid GroupID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }  

    }
}
