using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class ProgramDocumentResponse : BaseResponse
    {
        public List<ProgramDocumentViewEntity> ProgramDocumentsResponse { get; set; }

    }

    public class ProgramDocumentViewEntity
    {
        #region Properties
        public long? ProgramDocumentID { get; set; }
        public long DocumentID { get; set; }
        public string DocumentName { get; set; }
        public long? ProgramID { get; set; }
        public bool IsMandatory { get; set; }
        public long DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        #endregion Properties


    }
}
