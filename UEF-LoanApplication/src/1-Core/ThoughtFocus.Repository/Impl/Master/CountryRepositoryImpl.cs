namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class CountryRepositoryImpl : AbstractEFApplicationBaseRepository<Country>, ICountryRepository
    {
        #region Constructors

        public CountryRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public Country GetCountryByCountryId(long CountryId)
        {
            var query = GetAll().FirstOrDefault(x => x.CountryID == CountryId);
            return query;
        }

        #endregion Methods
    }
}