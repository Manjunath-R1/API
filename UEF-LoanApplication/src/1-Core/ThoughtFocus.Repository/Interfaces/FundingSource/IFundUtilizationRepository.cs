using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DataAccess.Models.FundingSource;

namespace ThoughtFocus.Repository.Interfaces.FundingSource
{
    public interface IFundUtilizationRepository : IEFApplicationBaseRepository<FundUtilization>
    {
        #region Methods

        /// <summary>
        /// This method is used to get FundUtilization by using ID
        /// </summary>
        /// <param name="FundUtilization">FundUtilization</param> 
        /// <param name="ID">ID</param>   
        /// <returns>FundUtilization</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        FundUtilization GetFundUtilizationID(Int64 ID);

        /// <summary>
        /// This method is used to get GetFundUtilization
        /// </summary>
        /// <param name="fundingSourceID">fundingSourceID</param>    
        /// <returns>IQueryable FundUtilization</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<FundUtilization> GetFundUtilization(long fundingSourceID);

        /// <summary>
        /// This method is used to save FundUtilization
        /// </summary>
        /// <param name="FundUtilization">Fund Utilization</param> 
        /// <param name="userID">userID</param> 
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateFundUtilization(FundUtilization fundUtilization, long userID);

        List<FundTransaction> GetAllFundTransaction();
        #endregion Methods
    }
}
