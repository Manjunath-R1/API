using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class DocumentShareRequest
    {
        public Guid DocumentID { get; set; }

        public string DocumentVersionKey { get; set; }

        public List<string> EmailAddresses { get; set; }

        public string Body { get; set; }

        public long LoggedInUser { get; set; }

        public long ImpersonatedUser { get; set; }
    }
}
