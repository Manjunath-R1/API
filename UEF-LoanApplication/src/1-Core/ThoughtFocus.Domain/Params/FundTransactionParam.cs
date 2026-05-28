namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Master;

    [Serializable]
    public class FundTransactionParam
    {
        public long FundingSourceID { get; set; }
        public int TransactionTypeID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Comment { get; set; }
        public string OriginatingBankAccount { get; set; }
        public System.DateTime dateOfFunding { get; set; }
        public DocumentRequest TransactionDocument { get; set; }
    }
}