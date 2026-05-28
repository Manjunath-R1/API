namespace ThoughtFocus.Repository.Interfaces.User
{
    using System;

    using ThoughtFocus.DataAccess.Models.User;

    public interface IUserSecurityQuestionInfoRepository : IEFApplicationBaseRepository<UserSecurityQuestionInfo>
    {
        #region Methods

        UserSecurityQuestionInfo GetSecurityInformation(Int64 UserId);

        #endregion Methods
    }
}