namespace ThoughtFocus.Repository.Interfaces.User
{
    using System;

    using ThoughtFocus.DataAccess.Models.User;

    public interface IUserProfileLoginInfoRepository : IEFApplicationBaseRepository<UserProfileLoginInfo>
    {
    #region Methods

    UserProfileLoginInfo GetUserProfileLoginInfo(Int64 userID);

    #endregion Methods
  }
}