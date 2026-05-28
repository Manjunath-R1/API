namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class BusinessRoleRepositoryImpl : AbstractEFApplicationBaseRepository<BusinessRole>, IBusinessRoleRepository
    {
        #region Constructors

        public BusinessRoleRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public BusinessRole GetBusinessRoleById(long BusinessRoleId)
        {
            var query = GetAll().FirstOrDefault(x => x.BusinessRoleID == BusinessRoleId);
               return query;
        }

        #endregion Methods
    }
}