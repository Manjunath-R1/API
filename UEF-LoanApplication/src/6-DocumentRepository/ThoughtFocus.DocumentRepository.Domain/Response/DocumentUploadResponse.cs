using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class DocumentUploadResponse : DocumentBaseResponse
    {
        public Guid DocumentID { get; set; }

        public string StorageKey { get; set; }

        public string ParentFolderName { get; set; }

        public string FileSource {get; set;}
      
    }
}
