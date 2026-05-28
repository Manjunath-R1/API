namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class QuestionResponseForFunding
    {
        #region Properties
        public Nullable<long> QuestionID { get; set; }       
        public string Response { get; set; }
        public long LoanApplicationID​​​​​​​​ {get; set;}
        public long ID {get; set;}
        
        #endregion Properties
    }
}