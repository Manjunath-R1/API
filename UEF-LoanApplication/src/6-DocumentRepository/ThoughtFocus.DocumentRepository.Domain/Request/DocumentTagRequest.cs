using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain.Request
{
    public class DocumentTagRequest
    {
        public Guid DocumentID { get; set; }

        public long UserID { get; set; }

        public List<DocumentTagViewModel> DocumentTagViewModels { get; set; }
    }
}
