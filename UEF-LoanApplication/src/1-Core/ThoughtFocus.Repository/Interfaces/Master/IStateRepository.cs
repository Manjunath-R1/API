namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface IStateRepository : IEFApplicationBaseRepository<State>
    {
        #region Methods

        State GetStateByStateId(long StateId);

        List<State> GetAllStateData();

        #endregion Methods
    }
}