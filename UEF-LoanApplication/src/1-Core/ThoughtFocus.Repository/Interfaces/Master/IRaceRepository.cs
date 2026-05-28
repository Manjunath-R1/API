namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master;

    public interface IRaceRepository : IEFApplicationBaseRepository<Race>
    {
        #region Methods

        Race GetRaceByRaceID(long RaceID);

        #endregion Methods
    }
}