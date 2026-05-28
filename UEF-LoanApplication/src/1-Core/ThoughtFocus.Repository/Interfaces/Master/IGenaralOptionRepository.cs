namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Domain.Master;

    public interface IGenaralOptionRepository : IEFApplicationBaseRepository<GenaralOption>
    {
        #region Methods

       List<GenaralOption> GetMasterOption(string category);
        bool IsPaymentSchedule(long applicationLoanId);
        #endregion Methods
    }
}