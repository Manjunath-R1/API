using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class DocumentTagViewModel
    {
        public Guid DocumentTagID { get; set; }

        public Guid DocumentID { get; set; }

        public Guid TagID { get; set; }

        public string TagName { get; set; }

        public string TagTypeName { get; set; }

        public long TagTypeID { get; set; }

        public string Value { get; set; }

        public bool IsEdited { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsDefault { get; set; }

        public List<TagValueModel> TagValues { get; set; }

    }
}
