namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class FundingTypeRepositoryImpl : AbstractEFApplicationBaseRepository<FundingType>, IFundingTypeRepository
    {
        #region Constructors

        public FundingTypeRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public FundingType GetFundingTypeByFundingTypeID(long FundingTypeID)
        {
            var query = GetAll().FirstOrDefault(x => x.FundingTypeID == FundingTypeID);
               return query;
        }
        public List<FundingType> GetAllFundingType()
        {
            var query = GetAll().ToList();
            return query;
        }
        #endregion Methods
    }
}