namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master;

    public interface IVeteranRepository : IEFApplicationBaseRepository<Veteran>
    {
        #region Methods

        Veteran GetVeteranByVeteranID(long VeteranID);

        #endregion Methods
    }
}