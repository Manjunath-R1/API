using System;
using System.Collections.Generic;
using System.IO;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class DocumentResponse : DocumentBaseResponse
    {
        public Guid DocumentID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string StorageKey { get; set; }
        public string ParentFolderName { get; set; }
        public string PhysicalPath { get; set; }
        public string Content { get; set; }
        public Stream OutputStream { get; set; }
        public List<DocumentEntity> DocumentEntityList { get; set; }
        public DocumentEntity DocumentEntity { get; set; }

    }
}
