namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class AgreementRequest
    {
        #region Properties
        public long AgreementID { get; set; }
        public string AgreementName { get; set; }
        public long ProgramID { get; set; }
        public string AgreementBody { get; set; }

        #endregion Properties
    }
}