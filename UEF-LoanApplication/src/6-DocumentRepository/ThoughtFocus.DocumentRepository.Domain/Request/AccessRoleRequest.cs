using System;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class AccessRoleRequest
    {
        public Guid AccessRoleID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long LoggerInUserID { get; set; }

    }
}
