namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class VeteranRepositoryImpl : AbstractEFApplicationBaseRepository<Veteran>, IVeteranRepository
    {
        #region Constructors

        public VeteranRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public Veteran GetVeteranByVeteranID(long VeteranID)
        {
            var query = GetAll().FirstOrDefault(x => x.VeteranID == VeteranID);
               return query;
        }

        #endregion Methods
    }
}