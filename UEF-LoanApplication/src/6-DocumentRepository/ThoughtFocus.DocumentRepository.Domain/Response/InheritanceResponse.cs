using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class InheritanceResponse : DocumentBaseResponse
    {
        public string ContentKeyName { get; set; }

        public string ContentNodeName { get; set; }

        public Guid ContentID { get; set; }

        public long LoggedInUserID { get; set; }
    }
}
