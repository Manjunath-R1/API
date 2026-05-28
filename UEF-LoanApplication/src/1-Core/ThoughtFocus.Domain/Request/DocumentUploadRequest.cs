using System;
using System.IO;

namespace ThoughtFocus.Domain.Request
{
    public class DocumentUploadRequest
    {
        public Guid DocumentID { get; set; }

        public long ContentLength { get; set; }

        public string ContentType { get; set; }

        public byte[] FileContent { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Stream InputStream { get; set; }

        public string TargetFilePath { get; set; }

        public Guid ProjectID { get; set; }

        public long UserID { get; set; }

        public string Key { get; set; }

        public long FileSize { get; set; }

        public string Number { get; set; }

        public bool Overwrite { get; set; }

    }
}
