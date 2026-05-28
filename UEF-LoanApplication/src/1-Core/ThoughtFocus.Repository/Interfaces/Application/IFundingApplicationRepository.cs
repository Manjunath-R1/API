namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Application;

    public interface IFundingApplicationRepository : IEFApplicationBaseRepository<FundingApplication>
    {
        #region Methods

        /// <summary>
        /// This method is used to save or update FundingApplication
        /// </summary>
        /// <param name="FundingApplication">FundingApplication</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateLoanFundingApplication(FundingApplication fundingApplication,long? userID);

        #endregion Methods
    }
}