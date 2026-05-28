using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Repository.Interfaces.Master;

namespace ThoughtFocus.Repository.Impl.Master
{
    public class RolePermissionRepositoryImpl : AbstractEFApplicationBaseRepository<RolePermission>,IRolePermissionRepository
    {
        private ApplicationDBContext context;

        #region Constructors

        public RolePermissionRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this.context = context;
        }

        #endregion Constructors

        #region Methods

        public List<RolePermission> GetGlobalRolePermissions(List<long> rUserRoles)
        {
            var rolePermissions = (from p in this.GetAll() where rUserRoles.Contains(p.RoleID) && p.IsActive select p).Include(a =>a.Action).ToList();
            return rolePermissions;

        }

        public void SaveOrUpdate(List<RolePermission> permissions)
        {
            try
            {
                foreach (var permission in permissions)
                {
                    if (permission.RolePermissionID > 0)
                        this.context.Entry(permission).State = EntityState.Modified;
                    else
                        this.context.RolePermissions.Add(permission);

                }
                this.context.SaveChanges();
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
