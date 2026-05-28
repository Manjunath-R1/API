using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtFocus.Domain.Request
{
    // public class FileManagerRequest
    // {
    //     public string Disk { get; set; }
    //     public string Path { get; set; }
    // }

    // public class DirectoryRequest : FileManagerRequest
    // {
    //     public long id { get; set; }
    //     public string Name { get; set; }
    //     public long? parentId { get; set; }
    //     public long UserID { get; set; }
    //     public bool isRootFolder { get; set; }
    // }

    public class FileUploadRequest //: FileManagerRequest
    {
        // private readonly string _RawValue;

        // public bool Overwrite { get; set; }
        // //public Guid DocumentID { get; set; }
        // public long ContentLength { get; set; }
        // public string MediaType { get; set; }
        // public string FileName { get; set; }
        // // public Stream InputStream { get; set; }
        // // public string Value { get; set; }
        // // //public long LoanApplicationID { get; set; }
        //  public int DocumentTypeID { get; set; }

        public long fileContentLength { get; set; }
        public Stream inputStream { get; set; }
        public string mediaType { get; set; }
        

        // public FileUploadRequest()
        // {

        // }

        // public FileUploadRequest(string path, long fileContentLength, bool overwriteFile, Stream inputStream, string mediaType, string value)
        // {
        //     InputStream = inputStream;
        //     MediaType = mediaType;
        //     FileName = path;
        //     //ProjectID = fileProjectId;
        //     ContentLength = fileContentLength;
        //     _RawValue = value;
        //     Overwrite = overwriteFile;
        //     Value = _RawValue;
        // }


    }
}
