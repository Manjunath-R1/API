namespace ThoughtFocus.Domain.CustomView
{
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Contact;
    using ThoughtFocus.Domain.Params;
    using ThoughtFocus.Domain.Response;
    using ThoughtFocus.Domain.User;

    public class FundingEntityViewEntity
    {
        #region Properties
        public long FundingEntityID { get; set; }
        public string FundingEntityName { get; set; }
        public string Address { get; set; }
        public string EIN { get; set; }
        public string TIN { get; set; }
        public long StateID { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string LogoFileName { get; set; }
        public string LogoPhysicalFileStorageKey { get; set; }
        public string LogoName { get; set; }
        public long? LogoID { get; set; }
        public string LogSource { get; set; }
        public List<FundingSourceListResponse> FundingSources { get; set; }

        #endregion Properties



    }
}