using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.Params
{
    public class FundPaymentScheduleParam
    {
        #region Properties
        public long FundPaymentScheduleID { get; set; }
        public long LoanApplicationID { get; set; }
        public long BusinessID { get; set; }
        public long ProgramID { get; set; }
        public decimal FundRequestedAmount { get; set; }
        public decimal FundAllocatedAmount { get; set; }
        public decimal FundDisbursedAmount { get; set; }
        public decimal FundPendingAmount { get; set; }  
        public long ContactID { get; set; }
        public string AdditionalNotesAgreement { get; set; }

        public long DocumentID { get; set; }
        public string DocumentGUID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }

        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }
        public string FileUploadedSourceUrl { get; set; }    

        #endregion Properties
    }
}
