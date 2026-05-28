using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class TagRequest
    {
        public long TagTypeID { get; set; }

        public string TagName { get; set; }

        public List<string> Values { get; set; }

        public long LoggerInUserID { get; set; }
    }
}
