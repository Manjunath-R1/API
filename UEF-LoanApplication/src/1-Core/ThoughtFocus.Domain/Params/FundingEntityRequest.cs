namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Master;

    [Serializable]
    public class FundingEntityRequest
    {
        #region Properties
        public long FundingEntityID { get; set; }       
        public string FundingEntityName { get; set; }
        public string Address { get; set; } 
        public string EIN { get; set; }
        public string TIN { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long StateID { get; set; }
        public DocumentRequest UploadFundingEntityLogo { get; set; }
        
        #endregion Properties
    }
}