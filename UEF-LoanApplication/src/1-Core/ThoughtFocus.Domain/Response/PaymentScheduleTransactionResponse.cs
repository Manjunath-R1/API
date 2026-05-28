using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;
using System;

namespace ThoughtFocus.Domain.Response
{
    public class PaymentScheduleTransactionResponse
    {
        public List<PaymentScheduleTransactionDetail> PaymentScheduleTransactionList { get; set; }
        public int ApplicationStatusId { get; set; } 
        public bool IsValidationError { get; set; }
        public ValidationErrorResponse ValidationError { get; set; }
        public bool IsSuccess { get; set; }       
        public string Message { get; set; }

    }

    public class PaymentScheduleTransactionDetail: AuditBase
    {
        public long PaymentScheduleID { get; set; }
        public long LoanApplicationID { get; set; }
        public long BusinessID { get; set; }      
        public long ProgramID { get; set; }
        public long ContactID { get; set; }
        public long FundingTypeID { get; set; }
        public decimal FundedAmount { get; set; }
        public long TransactionStatusID { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionStatusName { get; set; }
        public string FundingTypeName { get; set; }
        public string FundedAmountFormat { get; set; }
        public bool IsEnabled { get; set; }
    }
    public class PaymentScheduleItemResponse
    {
        public PaymentScheduleTransactionDetail PaymentScheduleTransaction { get; set; }
        public bool IsValidationError { get; set; }
        public ValidationErrorResponse ValidationError { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }    
}
