using System;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.Common;

namespace ThoughtFocus.Business.Interfaces.Application
{
    public interface ILoanApplication
    {
        #region Methods

        /// <summary>
        /// This method is used to GenerateLoanNumber
        /// </summary>
        /// <param name="LoanApplicationParam">Loan Application Param</param>         
        /// <returns>string</returns>       
        /// <exception cref="Exception">Exception</exception>
        string GenerateLoanNumber(LoanApplicationRequest LoanApplicationParam, long applicationCount);

        /// <summary>
        /// This method is used to GetAllLoanApplicationInformation
        /// </summary>
        /// <param name="ApplicationListResponse">PageFilter</param>     
        /// <param name="UserID">UserID</param>       
        /// <returns>string</returns>       
        /// <exception cref="Exception">Exception</exception>
        ApplicationListResponse GetAllLoanApplicationInformation(PageFilterEntity PageFilter, long UserID);

        #endregion Methods
    }
}