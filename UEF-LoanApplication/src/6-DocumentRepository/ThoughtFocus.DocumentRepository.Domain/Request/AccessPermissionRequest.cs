using System;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class AccessPermissionRequest
    {
        public Guid UserID { get; set; }

        public Guid GroupID { get; set; }

        public Guid DocumentID { get; set; }

        public Guid ProjectID { get; set; }

        public string AccessRoleName { get; set; }

        public long LoggedInUser { get; set; }

    }
}
