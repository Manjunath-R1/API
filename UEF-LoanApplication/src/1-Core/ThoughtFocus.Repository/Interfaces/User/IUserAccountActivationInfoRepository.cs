namespace ThoughtFocus.Repository.Interfaces.User
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.User;

    public interface IUserAccountActivationInfoRepository : IEFApplicationBaseRepository<UserAccountActivationInfo>
    {
        #region Methods

        UserAccountActivationInfo GetUserAccountActivationInfo(Int64 UserAccountActivationInfoID);

        List<UserAccountActivationInfo> GetUserAccountActivationInfoByUserID(Int64 UserID);

        #endregion Methods
    }
}