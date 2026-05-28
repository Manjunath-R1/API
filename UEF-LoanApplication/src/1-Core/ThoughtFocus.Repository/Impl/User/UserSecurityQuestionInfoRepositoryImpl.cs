namespace ThoughtFocus.Repository.Impl.User
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.User;

    public class UserSecurityQuestionInfoRepositoryImpl : AbstractEFApplicationBaseRepository<UserSecurityQuestionInfo>, IUserSecurityQuestionInfoRepository
    {
        #region Constructors

        public UserSecurityQuestionInfoRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public UserSecurityQuestionInfo GetSecurityInformation(Int64 UserId)
        {
            var query = GetAll().FirstOrDefault(x => x.UserID == UserId);
            return query;
        }

        #endregion Methods
    }
}