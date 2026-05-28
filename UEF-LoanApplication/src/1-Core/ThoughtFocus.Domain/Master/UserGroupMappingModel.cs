using System;

namespace ThoughtFocus.Domain
{
    public class UserGroupMappingModel
    {
        public Guid GroupID { get; set; }

        public string Description { get; set; }

        public Guid UserID { get; set; }

        public string Name { get; set; }
    }
}
