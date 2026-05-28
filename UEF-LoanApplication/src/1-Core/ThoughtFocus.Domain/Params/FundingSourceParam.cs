namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Master;

    [Serializable]
    public class FundingSourceParam
    {
        #region Properties
        public long FundingSourceID { get; set; }
        public string ProgramName { get; set; }
        public string FundingEntityName { get; set; }

        public long FundingEntityID { get; set; }
        public string Address { get; set; }
        public int FundingTypeID { get; set; }
        public bool IsActive { get; set; }
        public List<long> States { get; set; }
        public List<long> BusinessTypes { get; set; }
        public decimal MinimumLoanAmount { get; set; }
        public decimal MaximumLoanAmount { get; set; }
        public string InitialFundedAmount { get; set; }
        public string AvailableLimit { get; set; }
        public string UtilizedAmount { get; set; }
        public string TotalFundedAmount { get; set; }
        public string LogoFileName { get; set; }
        public string LogoPhysicalFileStorageKey { get; set; }
        public string LogoName { get; set; }
        public long? LogoID { get; set; }
        public string LogSource { get; set; }
        public DocumentRequest UploadProgramLogo { get; set; }

        #endregion Properties
    }
}