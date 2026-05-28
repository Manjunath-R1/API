using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class FundTransactionResponse
    {
        public List<FundTransactionDetail> FundTransactionList  { get; set; }
        public bool IsValidationError { get; set; }
        public ValidationErrorResponse ValidationError { get; set; }
        public bool IsSuccess { get; set; }
        public decimal TotalFundedAmount { get; set; }
        public string Message {get; set;}

    }

    public class FundTransactionDetail 
    {
        public long ID { get; set; }
        public string TransactionType { get; set; } 
        public string TransactionAmount { get; set; }
        public string Comment { get; set;}
        public string CreatedDateTime { get; set; }
        public string TransactionDate { get; set; }
        public long FundTransactionDocumentID  { get; set; } 
        public string physicalFileStorageKey { get; set; }
        public string FileName { get; set; }
    }
}
