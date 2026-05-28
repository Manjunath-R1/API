using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class FundAllocationViewEntity : BaseResponse
    {
        #region Properties
        public string ProgramName { get; set; }
        public decimal AvailableLimit { get; set; }
        public decimal UtilizedAmount { get; set; }
        public long FundingSourceID { get; set; }

        public decimal TotalFundAllocation { get; set; }

        #endregion Properties
    }
}
