using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.DataAccess.Models.FundingSource
{
    [Table("FundAgreementDocuments", Schema = "FundingSource")]
    public partial class FundAgreementDocuments : AuditBase
    {
    
        [Key]
        public long DocumentID { get; set; }
        [ForeignKey("LoanApplication")]
        public long LoanApplicationID { get; set; }

        [ForeignKey("FundingSource")]
        public long ProgramID { get; set; }

        public long BusinessID { get; set; }
        public Guid DocumentGUID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }
        public string FileUploadedSourceUrl { get; set; }
        public long ContactID { get; set; }
      

    }
}