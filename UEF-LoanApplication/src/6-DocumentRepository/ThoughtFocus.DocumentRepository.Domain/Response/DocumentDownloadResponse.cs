using System;
using System.IO;

namespace ThoughtFocus.DocumentRepository.Domain.Response
{
    public class DocumentDownloadResponse:DocumentBaseResponse
    {
        public string FileName { get; set; }

        public Guid DocumentKey { get; set; }

        public Stream FileStream { get; set; }

        public string FilePath { get; set; }

        public string FileContent { get; set; }
    }
}
