namespace ThoughtFocus.RoleProvider.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.CustomView;
    using ThoughtFocus.DataAccess.Models.Master;
    using Microsoft.Extensions.Logging;

    public interface IListRole 
    {
        #region Methods

        List<RoleListingViewEntity> GetRoleList();

        List<RolePermission> GetGlobalRolePermissions(Int64 userId);

        long GetRoleIDByRoleName(string roleName);

        List<long> GetRolesByID(long userID);

        #endregion Methods
    }
}