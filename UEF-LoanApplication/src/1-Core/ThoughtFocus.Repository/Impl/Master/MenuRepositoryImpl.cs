namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;
    using System.Linq;

    public class MenuRepositoryImpl : AbstractEFApplicationBaseRepository<Menu>, IMenuRepository
    {
        #region Fields
        private ApplicationDBContext _Context;
        #endregion Fields

        #region Constructors

        public MenuRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods
         public List<Menu> GetMenuInformationByRoleID(long id)
        {
            List<Menu> listOfMenus = new List<Menu>();
             listOfMenus = (from a in this._Context.Menus
                                join b in this._Context.RMRoleMenuSubMenus on a.MenuID equals b.MenuID
                                where b.RoleID == id
                                select new
                                {
                                    MenuID = a.MenuID,
                                    MenuName = a.MenuName 

                                }).ToList()
                                .Select(x => new Menu()
                                {
                                    MenuID = x.MenuID,
                                    MenuName = x.MenuName
                                }).ToList();

            return listOfMenus;
        }
        #endregion Methods
    }
}