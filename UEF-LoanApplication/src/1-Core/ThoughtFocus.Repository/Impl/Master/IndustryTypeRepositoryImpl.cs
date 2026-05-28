namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class IndustryTypeRepositoryImpl : AbstractEFApplicationBaseRepository<IndustryType>, IIndustryTypeRepository
    {
        #region Constructors

        public IndustryTypeRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public IndustryType GetIndustryTypeByIndustryTypeID(long IndustryTypeID)
        {
            var query = GetAll().FirstOrDefault(x => x.IndustryTypeID == IndustryTypeID);
               return query;
        }

        #endregion Methods
    }
}