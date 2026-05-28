namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IApplicationStatusRepository : IEFApplicationBaseRepository<ApplicationStatus>
    {
        #region Methods

        List<ApplicationStatus> GetAllApplicationStatusData();

        #endregion Methods
    }
}