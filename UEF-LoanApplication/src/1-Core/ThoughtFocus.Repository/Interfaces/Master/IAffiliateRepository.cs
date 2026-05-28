namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master;

    public interface IAffiliateRepository : IEFApplicationBaseRepository<UrbanLeagueAffiliate>
    {
        #region Methods

        UrbanLeagueAffiliate GetAffiliateByAffiliateID(long AffiliateID);

        #endregion Methods
    }
}