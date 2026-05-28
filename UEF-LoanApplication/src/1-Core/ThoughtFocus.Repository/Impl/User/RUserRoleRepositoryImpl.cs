namespace ThoughtFocus.Repository.Impl.User
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.User;
    using Microsoft.EntityFrameworkCore;
    using ThoughtFocus.Common.Exceptions;



    // using ThoughtFocus.Common.Exceptions;


    public class RUserRoleRepositoryImpl : AbstractEFApplicationBaseRepository<UserRole>, IRUserRoleRepository
    {
        private readonly ILogger<RUserRoleRepositoryImpl> _logger;
        private ApplicationDBContext _Context;
        #region Constructors

        public RUserRoleRepositoryImpl(ApplicationDBContext context, ILogger<RUserRoleRepositoryImpl> logger)
                : base(context)
        {
            _logger = logger;
            _Context = context;
        }

        #endregion Constructors

        #region Methods

        public List<string> GetRoleIdByUserId(long userId)
        {
            List<string> roleList = new List<string>();
            List<UserRole> rUserRoles = new List<UserRole>();
            try
            {
                rUserRoles = GetAll().Where(c => c.UserID == userId && c.IsActive == true) == null ? rUserRoles : GetAll().Where(c => c.UserID == userId && c.IsActive == true).ToList();
                if (rUserRoles.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no roles assigned for UserID-{0}", userId));
                }
                foreach (var ruserRole in rUserRoles)
                {
                    if (ruserRole.Role != null)
                    {
                        _logger.LogDebug(String.Format("Role entity is null in {0}", ruserRole));
                        roleList.Add(ruserRole.Role.RoleName);
                    }
                }
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

            return roleList;

        }

        public List<UserRole> GetRoleByUserId(long userId)
        {
            List<UserRole> rUserRoles = new List<UserRole>();
            try
            {
                rUserRoles = GetAll().Where(x => x.UserID == userId && x.IsActive == true) == null ? rUserRoles : GetAll().Where(x => x.UserID == userId && x.IsActive == true).ToList();
                if (rUserRoles != null && rUserRoles.Count == 0)
                {
                    _logger.LogDebug(String.Format("There is no roles assigned for UserID-{0}", userId));
                }
                else if (rUserRoles == null)
                {
                    _logger.LogDebug(String.Format("There is no roles assigned for UserID-{0}", userId));
                }

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
            return rUserRoles;
        }

        public List<long> GetGlobalRoles(long userId)
        {
            var query = GetAll().Where(x => x.UserID == userId && x.IsActive == true).Select(x => x.RoleID).ToList();
            return query;
        }

        public void SaveRole(UserRole rUserRole)
        {
            try
            {
                _Context.RUserRoles.Add(rUserRole);
                this._Context.SaveChanges(rUserRole.UserID);
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