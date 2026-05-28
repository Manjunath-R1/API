namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Application;

    public interface ILoanApplicantRepository : IEFApplicationBaseRepository<LoanApplicantDetails>
    {
        #region Methods
        /// <summary>
        /// This method is used to save Loan Applicant Details
        /// </summary>
        /// <param name="LoanApplicantDetails">LoanApplicantDetails</param> 
  
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveLoanApplicantDetails(LoanApplicantDetails LoanApplicantDetails);
        #endregion
    }
}