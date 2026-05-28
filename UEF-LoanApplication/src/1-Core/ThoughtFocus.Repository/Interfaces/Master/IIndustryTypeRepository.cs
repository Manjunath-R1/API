namespace ThoughtFocus.Repository.Interfaces.Master
{

    using ThoughtFocus.DataAccess.Models.Master;

    public interface IIndustryTypeRepository : IEFApplicationBaseRepository<IndustryType>
    {
        #region Methods

        IndustryType GetIndustryTypeByIndustryTypeID(long IndustryTypeID);

        #endregion Methods
    }
}