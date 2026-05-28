namespace ThoughtFocus.Repository.Impl.Master
{ 
    using DataAccess.Models.Master;
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class GenaralOptionRepository : AbstractEFApplicationBaseRepository<GenaralOption>, IGenaralOptionRepository
    {
        #region Constructors
        private ApplicationDBContext _Context;
        public GenaralOptionRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        public List<GenaralOption> GetMasterOption(string category)
        {
            var query = GetAll().Where(w=>w.OptionCategory== category).ToList();
            return query;
        }
        public bool IsPaymentSchedule(long applicationLoanId)
        {            
            var result = this._Context.FundingApplications.FirstOrDefault(x=>x.LoanApplicationID== applicationLoanId).IsPaymentSchedule;
            if (result !=true)
            {
                return false;
            }
            return true;
        }
        #endregion Constructors
    }
}