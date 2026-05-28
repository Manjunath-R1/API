namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class RaceRepositoryImpl : AbstractEFApplicationBaseRepository<Race>, IRaceRepository
    {
        #region Constructors

        public RaceRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public Race GetRaceByRaceID(long RaceID)
        {
            var query = GetAll().FirstOrDefault(x => x.RaceID == RaceID);
               return query;
        }

        #endregion Methods
    }
}