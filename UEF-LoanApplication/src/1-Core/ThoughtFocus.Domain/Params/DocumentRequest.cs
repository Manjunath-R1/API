
using System;

namespace ThoughtFocus.Domain.Params
{
    public class DocumentRequest
    {

        #region Properties
        public Guid DocumentGUID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }
        public bool IsActive { get; set; }
        public long LoanApplicationID​​​​​​​​ {get; set;}
        public long ApplicationDocumentID {get;set;}
        public string FileSource { get; set; }

        #endregion Properties
    
    }
}
