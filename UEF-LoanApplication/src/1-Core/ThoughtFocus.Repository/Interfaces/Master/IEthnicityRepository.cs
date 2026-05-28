namespace ThoughtFocus.Repository.Interfaces.Master
{

 
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IEthnicityRepository : IEFApplicationBaseRepository<Ethnicity>
    {
        #region Methods

        Ethnicity GetEthnicityByEthnicityId(long EthnicityId);

        #endregion Methods
    }
}