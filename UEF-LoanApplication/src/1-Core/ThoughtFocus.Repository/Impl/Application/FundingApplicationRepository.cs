namespace ThoughtFocus.Repository.Impl.Application
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq; 

    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;

    public class FundingApplicationRepository : AbstractEFApplicationBaseRepository<FundingApplication>, IFundingApplicationRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public FundingApplicationRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods
        public void SaveOrUpdateLoanFundingApplication(FundingApplication fundingApplication,long? userID)
        {
            try
            {                 
                if (fundingApplication.ID > 0)
                {                    
                    _Context.Entry(fundingApplication).State = EntityState.Modified;
                }                   
                else
                    _Context.FundingApplications.Add(fundingApplication);                
                 
               this._Context.SaveChanges(userID);
               

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingApplicationRepository-> SaveOrUpdateLoanFundingApplication", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingApplicationRepository-> SaveOrUpdateLoanFundingApplication", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingApplicationRepository-> SaveOrUpdateLoanFundingApplication", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingApplicationRepository-> SaveOrUpdateLoanFundingApplication", ex);
            }
        }

        #endregion Methods

    }
}