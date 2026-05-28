namespace ThoughtFocus.Repository.Interfaces.FundingSource
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.FundingSource;

    public interface IFundingEntityRepository : IEFApplicationBaseRepository<FundingEntity>
    {
        #region Methods
        IQueryable<FundingEntity> GetAllFundingEntities();
        FundingEntity GetFundingEntityByID(long fundingEntityID);
        FundingEntity GetFundingEntityEin(string fundingEntityName);
        void SaveOrUpdateFundingEntity(FundingEntity fundingEntity, long userID);
        
        #endregion Methods
    }
}