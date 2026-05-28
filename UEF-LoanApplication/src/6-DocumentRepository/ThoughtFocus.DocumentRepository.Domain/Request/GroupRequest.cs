using System;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class GroupRequest
    {
        public Guid GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long LoggerInUserID { get; set; }
    }
}
