namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IGenderRepository : IEFApplicationBaseRepository<Gender>
    {
        #region Methods

        Gender GetGenderByGenderId(long GenderId);
        List<Gender> GetAllGenderData();

        #endregion Methods
    }
}