using System;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class AuthorizeRequest
    {
        public string ActionName { get; set; }

        public string ContentKeyName { get; set; }

        public string ContentNodeName { get; set; }

        public Guid ContentID { get; set; }

        public string AssigneeKeyName { get; set; }

        public string AssigneeNodeName { get; set; }

        public string RoleName { get; set; }

        public Guid LoggedInUserID { get; set; }

        public Guid GroupID { get; set; }
    }
}
