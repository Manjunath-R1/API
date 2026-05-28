using System;

namespace ThoughtFocus.Domain.Params
{
    public class FundUtilizationParam
    {
        #region Properties
        public long BusinessID { get; set; }
        public string BusinessName { get; set; }
        public long BusinessTypeID { get; set; }
        public string BusinessType { get; set; }
        public int FundingTypeID { get; set; }
        public string FundingType { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public Nullable<DateTime> DateofDisbursement { get; set; }
        public string OriginatingBankAccount { get; set; }
        public string Comment { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
        public DocumentRequest ApplicationDocument { get; set; }
        public string DestinationBankAccount { get; set; }
        public string BankRoutingNumber { get; set; }
        public string PhysicalFileStorageKey { get; set; }
        public string FileName { get; set; }
        public string FundUtilizedTransactionAmount { get; set; }
        public string FundUtilizedTransactionDate { get; set; }

        #endregion Properties
    }
}
