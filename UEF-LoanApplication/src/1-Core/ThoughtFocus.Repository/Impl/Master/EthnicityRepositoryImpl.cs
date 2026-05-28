namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class EthnicityRepositoryImpl : AbstractEFApplicationBaseRepository<Ethnicity>, IEthnicityRepository
    {
        #region Constructors

        public EthnicityRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public Ethnicity GetEthnicityByEthnicityId(long EthnicityID)
        {
            var query = GetAll().FirstOrDefault(x => x.EthnicityID == EthnicityID);
            return query;
        }

        #endregion Methods
    }
}