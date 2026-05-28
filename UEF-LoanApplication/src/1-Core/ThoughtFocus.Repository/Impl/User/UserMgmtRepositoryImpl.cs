namespace ThoughtFocus.Repository.Impl.User
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.User;

    public class UserMgmtRepositoryImpl : AbstractEFApplicationBaseRepository<User>, IUserMgmtRepository
    {
        #region Constructors

        public UserMgmtRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public User Get(string userName)
        {
            var query = GetAll().FirstOrDefault(x => x.UserName == userName&& x.IsActive==true);
            return query;
        } 

        #endregion Methods
    }
}