namespace ThoughtFocus.Repository.Interfaces.User
{
    using System;
    using System.Collections.Generic; 
    using ThoughtFocus.DataAccess.Models.User;

    public interface IRUserRoleRepository : IEFApplicationBaseRepository<UserRole>
    {
        List<string> GetRoleIdByUserId(Int64 userId);
        //string GetRoleIdByUserId(Int64 userId);
        List<UserRole> GetRoleByUserId(Int64 userId);
       // RUserRole GetRoleByUserId(Int64 userId);
        List<long> GetGlobalRoles(Int64 userId);
        void SaveRole(UserRole rUserRole);
    }
}