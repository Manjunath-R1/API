namespace ThoughtFocus.Repository.Impl.User
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.Repository.Interfaces.User;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class UserRepositoryImpl : AbstractEFApplicationBaseRepository<User>, IUserRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
        private readonly ILogger<UserRepositoryImpl> _logger;

        #endregion Fields
        #region Constructors

        public UserRepositoryImpl(ApplicationDBContext context, ILogger<UserRepositoryImpl> logger)
            : base(context)
        {
            this._Context = context;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        public User GetByUserID(Int64 userID)
        {
            var query = GetAll().FirstOrDefault(x => x.UserID == userID);
            return query;
        }

        public User GetByUserName(string userName)
        {
            var query = GetAll().FirstOrDefault(x => x.UserName == userName && x.Contact.IsActive==true);
            return query;
        }

        public User GetActiveByUserName(string userName)
        {
            var query = GetAll().FirstOrDefault(x => x.UserName == userName && x.IsActive == true);
            return query;
        }

        public User GetUserLogin(string UserName, string Password)
        {
            var query = GetAll().FirstOrDefault(x => x.UserName == UserName && x.PasswordHash == Password && x.Contact.IsActive == true);
            return query;
        }

        public User GetUserByContactID(long contactID)
        {
            try
            {

                return this.GetAll().Where(x => x.Contact.ContactID == contactID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        public String GetUserIDByContactID(long ContactID)
        {
            try
            {
                var user = GetAll().FirstOrDefault(a => a.ContactID == ContactID);
                if (user != null)
                {
                    return user.UserID.ToString();
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        public void SaveUser(User user)
        {
            try
            {
                if (user.UserID > 0)
                    _Context.Entry(user).State = EntityState.Modified;
                else
                    _Context.Users.Add(user);
                this._Context.SaveChanges(user.UserID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                this._Context.SaveChanges(user.UserID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        public bool CheckUserExists(Int64 userID)
        {
            try
            {
                var count = this.FindBy(e => e.UserID == userID).Count();
                return count > 0 ? true : false;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("", ex);
            }
        }

        #endregion Methods
    }
}