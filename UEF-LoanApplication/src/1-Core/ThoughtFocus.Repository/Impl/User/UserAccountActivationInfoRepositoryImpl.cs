namespace ThoughtFocus.Repository.Impl.User
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.User;
    using ThoughtFocus.DataAccess;

    public class UserAccountActivationInfoRepositoryImpl : AbstractEFApplicationBaseRepository<UserAccountActivationInfo>, IUserAccountActivationInfoRepository
    {
        #region Constructors

        public UserAccountActivationInfoRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public UserAccountActivationInfo GetUserAccountActivationInfo(Int64 UserAccountActivationInfoID)
        {
            return this.FirstOrDefault(x => x.UserAccountActivationInfoID == UserAccountActivationInfoID);
        }

        public List<UserAccountActivationInfo> GetUserAccountActivationInfoByUserID(Int64 UserID)
        {
            return this.FindBy(x => x.UserID == UserID).ToList();
        }

        #endregion Methods
    }
}