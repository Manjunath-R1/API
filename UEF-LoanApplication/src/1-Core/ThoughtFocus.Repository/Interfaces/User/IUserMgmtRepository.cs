namespace ThoughtFocus.Repository.Interfaces.User
{

    using ThoughtFocus.DataAccess.Models.User;

    public interface IUserMgmtRepository : IEFApplicationBaseRepository<User>
    {
        #region Methods

        User Get(string userName);

        #endregion Methods
    }
}