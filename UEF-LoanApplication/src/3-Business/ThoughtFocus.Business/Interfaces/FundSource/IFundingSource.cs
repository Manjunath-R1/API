using System;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Business.Interfaces.FundSource
{
    public interface IFundingSource : IBaseBusiness
    {
        #region Methods

        /// <summary>
        /// This method is used to calculate the total funded amount
        /// </summary>
        /// <param name="FundTransactionList">Fund Transactions List</param>        
        /// <returns>decimal</returns>       
        /// <exception cref="Exception">Exception</exception>
        decimal CalculateTotalFundedAmount(List<FundTransaction> FundTransactionList);
        FundingEntityListResponse GetAllFundingEntitiesInformation(PageFilterEntity PageFilter, long UserID);
        #endregion Methods
    }
}