using System.Collections.Generic;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.CustomView
{
    public class ApplicationDocumentResponse:BaseResponse
    {
        public List<ApplicationDocumentViewModel> ApplicationDocumentViewModels { get; set; }

        public bool AddAdditionalDocuments { get; set; }

        public int AdditionalDocumentTypeID { get; set; }

        public FileEntity FileEntity { get; set; }

    }
}
