namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IMenuRepository : IEFApplicationBaseRepository<Menu>
    {
        List<Menu> GetMenuInformationByRoleID(long id);
    }
}