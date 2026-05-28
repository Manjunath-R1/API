using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.DataAccess.Models
{
    [Table("ApplicationDocuments", Schema = "Application")]
    public partial class ApplicationDocument : AuditBase
    {
        public long ApplicationDocumentID { get; set; }

        public Guid DocumentGUID { get; set; }
        public long LoanApplicationID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual LoanApplication LoanApplication { get; set; }

    }
}
