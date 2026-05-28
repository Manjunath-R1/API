namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThoughtFocus.Domain.Contact;

    [Serializable]
    public class TransactionNotifyRequest
    {
        #region Properties

       
        public long ApplicationID { get; set; }

        public System.DateTime TransactionDate { get; set; }
        public long BusinessId { get; set; }
        public long ProgramId { get; set; }
        public long ContactID { get; set; }
        #endregion Properties
    }
    public class TransactionsDate
    {
        #region Properties        
        public System.DateTime TransactionDate { get; set; }

        #endregion Properties
    }

}