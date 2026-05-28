using System;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class DeleteRequest
    {
        public Guid ContentID { get; set; }

        public Guid GroupID { get; set; }

        public Guid UserID { get; set; }

        public Guid AccessRoleID { get; set; }

        public string AccessRoleName { get; set; }

        public string Type { get; set; }

        public long LoggedInUser { get; set; }

    }
}
