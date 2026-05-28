namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class DocumentsRequest
    {
        #region Properties
        public long DocumentTypeID { get; set; }
        public string DocumentName { get; set; }

        #endregion Properties
    }
}