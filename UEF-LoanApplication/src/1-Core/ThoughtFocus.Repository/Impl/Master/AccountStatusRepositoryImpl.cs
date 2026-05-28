namespace ThoughtFocus.Repository.Impl.Master
{ 
    using DataAccess.Models.Master;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class AccountStatusRepositoryImpl : AbstractEFApplicationBaseRepository<AccountStatus>, IAccountStatusRepository
    {
        #region Constructors

        public AccountStatusRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}