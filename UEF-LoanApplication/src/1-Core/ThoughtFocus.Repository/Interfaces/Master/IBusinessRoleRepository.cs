using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.Repository.Interfaces.Master
{
    public interface IBusinessRoleRepository : IEFApplicationBaseRepository<BusinessRole>
    {
        #region Methods

        BusinessRole GetBusinessRoleById(long BusinessRoleID);

        #endregion Methods
    }
}