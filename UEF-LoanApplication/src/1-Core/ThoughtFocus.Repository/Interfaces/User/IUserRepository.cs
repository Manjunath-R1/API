namespace ThoughtFocus.Repository.Interfaces.User
{
    using System;

    using ThoughtFocus.DataAccess.Models.User;

    public interface IUserRepository : IEFApplicationBaseRepository<User>
    {
        #region Methods

        User GetByUserID(Int64 userID);

        User GetByUserName(string userName);

        User GetActiveByUserName(string userName);

        User GetUserLogin(string UserName, string Password);

        User GetUserByContactID(long contactID);

        void SaveUser(User user);

        void UpdateUser(User user);

        String GetUserIDByContactID(long ContactID);

        bool CheckUserExists(Int64 userID);

        #endregion Methods
    }
}