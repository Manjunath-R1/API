namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IFundingTypeRepository : IEFApplicationBaseRepository<FundingType>
    {
        #region Methods

        FundingType GetFundingTypeByFundingTypeID(long FundingTypeID);
        List<FundingType> GetAllFundingType();
        
        #endregion Methods
    }
}