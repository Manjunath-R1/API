namespace ThoughtFocus.Repository.Interfaces.Master
{
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;

    public interface ISalutationRepository : IEFApplicationBaseRepository<Salutation>
    {
        #region Methods

        Salutation GetSalutationBySalutationId(long SalutationId);

        List<Salutation> GetAllSalutationData();

        #endregion Methods
    }
}