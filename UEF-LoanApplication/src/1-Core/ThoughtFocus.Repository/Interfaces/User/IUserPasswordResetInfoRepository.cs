namespace ThoughtFocus.Repository.Interfaces.User
{
    using System;

    using ThoughtFocus.DataAccess.Models.User;

    public interface IUserPasswordResetInfoRepository : IEFApplicationBaseRepository<UserPasswordResetInfo>
    {
        #region Methods

        UserPasswordResetInfo GetUserPasswordResetInfo(Int64 userPasswordResetInfoID);

         void SaveOrUpdateForgetPassword(UserPasswordResetInfo UserPasswordResetInfo,long? userID);
        UserPasswordResetInfo GetForgetPasswordInfoByToken(string TokenID);

        #endregion Methods
    }
}