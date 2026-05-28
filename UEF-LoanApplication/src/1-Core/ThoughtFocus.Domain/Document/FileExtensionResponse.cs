using System.Collections.Generic;

using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain
{
    public class FileExtensionResponse : BaseResponse
    {
        public List<FileExtensionModel> FileExtensions { get; set; }
    }
}
