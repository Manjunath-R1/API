namespace ThoughtFocus.Business.Interfaces.Master
{
    using ThoughtFocus.Business;
    using ThoughtFocus.Domain.CustomView;

    public interface IViewRole : IBaseBusiness
    {
        #region Methods

        RoleListingViewEntity GetMenuViewEntity(long id);

        #endregion Methods
    }
}