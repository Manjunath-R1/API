namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master;

    public interface IBusinessTypeRepository : IEFApplicationBaseRepository<BusinessType>
    {
        #region Methods

        BusinessType GetBusinessTypeByBusinessTypeID(long BusinessTypeID);

        #endregion Methods
    }
}