namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class BusinessTypeRepositoryImpl : AbstractEFApplicationBaseRepository<BusinessType>, IBusinessTypeRepository
    {
        #region Constructors

        public BusinessTypeRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public BusinessType GetBusinessTypeByBusinessTypeID(long BusinessTypeID)
        {
            var query = GetAll().FirstOrDefault(x => x.BusinessTypeID == BusinessTypeID);
               return query;
        }

        #endregion Methods
    }
}