namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IApplicationTypeRepository : IEFApplicationBaseRepository<ApplicationType>
    {
        #region Methods

        List<ApplicationType> GetAllApplicationTypeData();

        #endregion Methods
    }
}