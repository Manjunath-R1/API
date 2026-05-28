namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master; 

    public interface ICountryRepository : IEFApplicationBaseRepository<Country>
    {
        #region Methods

        Country GetCountryByCountryId(long CountryId);

        #endregion Methods
    }
}