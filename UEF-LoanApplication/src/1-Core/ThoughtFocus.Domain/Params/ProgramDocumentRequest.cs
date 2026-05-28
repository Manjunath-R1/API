namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Master;

    [Serializable]
    public class ProgramDocumentRequest
    {
        #region Properties

        public long FundingSourceID { get; set; }
        public List<ProgramWiseDocument> ProgramDocuments { get; set; }

        #endregion Properties
    }
    public class ProgramWiseDocument
    {
        public long ProgramDocumentID { get; set; }
        public int DocumentID { get; set; }
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }

    }
}