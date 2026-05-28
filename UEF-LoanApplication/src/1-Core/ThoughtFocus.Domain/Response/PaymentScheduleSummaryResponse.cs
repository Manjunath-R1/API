using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;
using System;

namespace ThoughtFocus.Domain.Response
{
    public class PaymentScheduleSummaryResponse
    {
        public PaymentScheduleSummaryDetails paymentScheduleSummary { get; set; }
        public bool IsValidationError { get; set; }
        public ValidationErrorResponse ValidationError { get; set; }
        public bool IsSuccess { get; set; }       
        public string Message { get; set; }
        public PaymentScheduleSummaryDocument paymentScheduleDocument { get; set; }
        public int ApplicationStatusId;
    }

    public class PaymentScheduleSummaryDetails
    {
        public long FundPaymentScheduleID { get; set; }
        public long ContactID { get; set; }
        public string FundAllocatedAmount { get; set; }

        public decimal FundAllocatedSummaryAmount {get;set;}

        public string FundRequestedAmount { get; set; }
        public decimal FundRequestedSummaryAmount { get; set; }
        public string FundDisbursedAmount { get; set; }
        public decimal FundDisbursedSummaryAmount { get; set; }
        public string FundPendingAmount { get; set; }
        public decimal FundPendingSummaryAmount { get; set; }
        public long LoanApplicationID { get; set; }
        public long ProgramID { get; set; }
        public long BusinessID { get; set; }
        public string AdditionalNotesAgreement { get; set; }
        public string ProgramName { get; set; }
    }
    public class PaymentScheduleSummaryDocument
    {


        public long DocumentID { get; set; }
        public string DocumentGUID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }

        public string PhysicalFileStorageKey { get; set; }
        public long FileSize { get; set; }
        public string FileUploadedSourceUrl { get; set; }

    }
}
