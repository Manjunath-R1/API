namespace ThoughtFocus.RoleProvider.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Domain.CustomView;
    using ThoughtFocus.Repository.Interfaces.Master;
    using ThoughtFocus.Repository.Interfaces.User;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.RoleProvider.Interfaces;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.DataAccess.Models.User;

    public class ListRoleImpl : IListRole
    {
        #region Fields

        private IRolePermissionRepository rolePermissionRepository;
        private IRUserRoleRepository rUserRoleRepository;
        private IRoleRepository _roleRepository;
        //private IUserRepository userRepository;
       

        /// <summary>
        /// ILog instance for logging.
        /// </summary>
        private readonly ILogger<ListRoleImpl> _logger;

        #endregion Fields

        #region Constructors

        public ListRoleImpl(IRolePermissionRepository _rolePermissionRepository, IRUserRoleRepository _rUserRoleRepository,
             IRoleRepository roleRepository, ILogger<ListRoleImpl> logger)
        {
            this.rolePermissionRepository = _rolePermissionRepository;
            this.rUserRoleRepository = _rUserRoleRepository;
            this._roleRepository = roleRepository;
            this._logger = logger;
        }

        #endregion Constructors

        #region Methods

        public List<RoleListingViewEntity> GetRoleList()
        {
            try
            {
                List<RoleListingViewEntity> listRoleListingViewEntity = null;
                List<Role> roleList = this._roleRepository.FindBy(a => a.IsActive == true && a.IsLoginRole == true).OrderBy(x=>x.RoleName).ToList();
                if (roleList != null)
                {
                    listRoleListingViewEntity = new List<RoleListingViewEntity>();
                    foreach (var item in roleList)
                    {

                        listRoleListingViewEntity.Add(new RoleListingViewEntity
                        {
                            RoleID = item.RoleID,
                            RoleName = item.RoleName,
                            RoleDescription = item.RoleDescription

                        });
                    }
                }
                return listRoleListingViewEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at public List<RoleListingViewEntity> GetRoleList()>>", ex);
                throw;
            }

        }

        
        public List<RolePermission> GetGlobalRolePermissions(Int64 userId)
        {
            List<RolePermission> rolePermissions = new List<RolePermission>();
            //List<RolePermissionViewEntity> rolePermissionViewEntity = new List<RolePermissionViewEntity>();
            List<long> roles = new List<long>();

            //GetGlobal Roles
            roles = this.rUserRoleRepository.GetGlobalRoles(userId);

            //Get RolePermissions
            rolePermissions = this.rolePermissionRepository.GetGlobalRolePermissions(roles);

            return rolePermissions;
        }

       
        public long GetRoleIDByRoleName(string roleName)
        {
            long roleID = 0;
            Role role = this._roleRepository.FirstOrDefault(a => a.RoleName == roleName && a.IsActive == true);
            if (role != null)
            {
                roleID = role.RoleID;
            }
            return roleID;
        }

        
        public List<long> GetRolesByID(long userID)
        {
            List<long> roles = new List<long>();
            if(userID > 0 )
            {
                roles = this.rUserRoleRepository.GetAll().Where(item => item.UserID == userID && item.IsActive == true).Select(u => u.RoleID).ToList();   
                
            }
           return roles;
        }
      


        #endregion Methods


    }
}