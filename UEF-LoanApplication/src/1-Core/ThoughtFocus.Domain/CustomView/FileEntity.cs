using System;
using System.IO;

namespace ThoughtFocus.Domain.CustomView
{
    public class FileEntity
    {

        public long ContentLength { get; set; }

        public string ContentType { get; set; }

        public string FileContent { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Stream InputStream { get; set; }

        public string TargetFilePath { get; set; }

        public long FileSize { get; set; }

        public bool Overwrite { get; set; }

        public int DocumentTypeID { get; set; }

        public Guid DocumentID { get; set; }

    }
}
