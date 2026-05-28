namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class AffiliateRepositoryImpl : AbstractEFApplicationBaseRepository<UrbanLeagueAffiliate>, IAffiliateRepository
    {
        #region Constructors

        public AffiliateRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public UrbanLeagueAffiliate GetAffiliateByAffiliateID(long AffiliateID)
        {
            var query = GetAll().FirstOrDefault(x => x.AffiliateID == AffiliateID);
               return query;
        }

        #endregion Methods
    }
}