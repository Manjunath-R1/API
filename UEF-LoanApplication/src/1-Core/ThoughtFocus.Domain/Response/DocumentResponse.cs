using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class DocumentResponse : BaseResponse
    {
        public DocumentViewModel Document { get; set; }
        public List<TagViewModel> documentTagModel { get; set; }
    }
}
