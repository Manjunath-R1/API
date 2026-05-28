namespace ThoughtFocus.Repository.Impl.User
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.User;

    public class UserPasswordResetInfoRepositoryImpl : AbstractEFApplicationBaseRepository<UserPasswordResetInfo>, IUserPasswordResetInfoRepository
    {
        #region Constructors
       private ApplicationDBContext _Context;
        public UserPasswordResetInfoRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods

        public UserPasswordResetInfo GetUserPasswordResetInfo(Int64 UserPasswordResetInfoID)
        {
            return this.FirstOrDefault(x => x.UserPasswordResetInfoID == UserPasswordResetInfoID);
        }

        public void SaveOrUpdateForgetPassword(UserPasswordResetInfo UserPasswordResetInf,long? userID)
        {
             
            try
            {
                if(UserPasswordResetInf.UserPasswordResetInfoID > 0)
                {
                _Context.Entry(UserPasswordResetInf).State = EntityState.Modified;
                this._Context.SaveChanges(userID);
                }
                else
                {
                _Context.UserPasswordResetInfoes.Add(UserPasswordResetInf);
                 this._Context.SaveChanges(userID);
                }
            }
            catch (SqlException ex)
            {
                 throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> SaveOrUpdateForgetPassword", ex);
            }
            catch (DbUpdateException ex)
            {
               throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> SaveOrUpdateForgetPassword", ex);
            }
            catch (ObjectDisposedException ex)
            {
                 throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> SaveOrUpdateForgetPassword", ex);
            }
            catch (Exception ex)
            {
               throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> SaveOrUpdateForgetPassword", ex);
            }
        }

        public UserPasswordResetInfo GetForgetPasswordInfoByToken(string TokenID)
        {
            try
             {
                var query =  FirstOrDefault(x => x.TokenID == TokenID);
                return query;
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> GetForgetPasswordInfoByToken", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> GetForgetPasswordInfoByToken", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> GetForgetPasswordInfoByToken", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in UserPasswordResetInfoRepositoryImpl-> GetForgetPasswordInfoByToken", ex);
             }
        }
      
    
        #endregion Methods
    }
}