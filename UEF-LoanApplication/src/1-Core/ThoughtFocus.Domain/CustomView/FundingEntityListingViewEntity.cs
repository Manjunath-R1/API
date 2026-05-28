namespace ThoughtFocus.Domain.CustomView
{
    public class FundingEntityListingViewEntity
    {
        #region Properties

        public long FundingEntityID { get; set; }
        public string FundingEntityName { get; set; }
        public string City { get; set; } 
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EIN { get; set; }
        public string TIN { get; set; }

        
        #endregion Properties



    }
}