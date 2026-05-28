namespace ThoughtFocus.Business.Impl.Master
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.CustomView;
    using ThoughtFocus.Domain.Master;
    using ThoughtFocus.Business;
    using ThoughtFocus.Business.Interfaces.Master;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class ViewRoleImpl : AbstractBusiness, IViewRole
    {
        #region Fields

         /// <summary>
        /// ILog instance for logging.
        /// </summary>
        private readonly ILogger<ViewRoleImpl> _logger;
        private IMenuRepository _menuRepository;

        #endregion Fields

        #region Constructors

        public ViewRoleImpl(IMenuRepository menuRepository, ILogger<ViewRoleImpl> logger)
        {
            this._menuRepository = menuRepository;
            this._logger = logger;
        }

        #endregion Constructors

        #region Methods

        public RoleListingViewEntity GetMenuViewEntity(long id)
        {
            RoleListingViewEntity menuViewEntity = new RoleListingViewEntity();
            try
            {
                List<MenuEntity> menuEntity = this.GetListOfMenuNameByRoleID(id);
                if (menuEntity != null)
                {
                    menuViewEntity.listOfMenuEnity = new List<MenuEntity>();
                    foreach (var item in menuEntity)
                    {
                        menuViewEntity.listOfMenuEnity.Add(new MenuEntity
                            {
                                MenuID=item.MenuID,
                                MenuName=item.MenuName

                            });
                        }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at MenuViewEntity GetMenuViewEntity(string id) >>", ex);
                throw ex;
            }
            return menuViewEntity;
        }

        public List<MenuEntity> GetListOfMenuNameByRoleID(long id)
        {
            List<MenuEntity> listOfMenuEntity = null;
            
            var menuList = this._menuRepository.GetMenuInformationByRoleID(id);
                if (menuList != null)
                {
                    listOfMenuEntity = new List<MenuEntity>();
                    foreach (var item in menuList)
                    {

                        listOfMenuEntity.Add(new MenuEntity
                        {
                            MenuID = item.MenuID,
                            MenuName = item.MenuName
                        });
                    }
                }
            
            return listOfMenuEntity;
        }

        #endregion Methods
    }
}