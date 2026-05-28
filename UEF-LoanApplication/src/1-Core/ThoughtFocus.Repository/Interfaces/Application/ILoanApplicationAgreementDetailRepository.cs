namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models;
    using ThoughtFocus.DataAccess.Models.Application;

    public interface ILoanApplicationAgreementDetailRepository : IEFApplicationBaseRepository<LoanApplicationAgreementDetail>
    {
        #region Methods
        /// <summary>
        /// This method is used to Save LoanApplication Agreement Details
        /// </summary>
        /// <param name="LoanApplicationAgreementDetail">LoanApplicationAgreementDetail</param> 
  
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveLoanApplicationAgreementDetail(LoanApplicationAgreementDetail loanApplicationAgreementDetail);
        LoanApplicationSchedulePaymentAreementDetail GetLoanApplicationSchedulePaymentAreementDetail(long applicationID);
        #endregion
    }
}