namespace ThoughtFocus.Repository.Impl.User
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.User;

    public class UserProfileLoginInfoRepositoryImpl : AbstractEFApplicationBaseRepository<UserProfileLoginInfo>, IUserProfileLoginInfoRepository
    {
        #region Constructors

        public UserProfileLoginInfoRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public UserProfileLoginInfo GetUserProfileLoginInfo(Int64 userID)
        {
            return this.FirstOrDefault(x => x.UserID == userID);
        }

    #endregion Methods
  }
}