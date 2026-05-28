namespace ThoughtFocus.Repository.Impl.Master
{

  using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class RoleRepositoryImpl : AbstractEFApplicationBaseRepository<Role>, IRoleRepository
    {
        #region Fields

        private ApplicationDBContext context;

        #endregion Fields

        #region Constructors

        public RoleRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this.context = context;
        }

        #endregion Constructors


    }
}