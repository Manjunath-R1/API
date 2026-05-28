using System;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class InheritanceRequest
    {
        public string ContentKeyName { get; set; }

        public string ContentNodeName { get; set; }

        public Guid ContentID { get; set; }

        public long LoggedInUserID { get; set; }
    }
}
