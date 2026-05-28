namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using ThoughtFocus.DataAccess.Models.Application;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models;
    using System.Collections.Generic;

    public interface ILoanApplicationRepository : IEFApplicationBaseRepository<LoanApplication>
    {
        #region Methods
        /// <summary>
        /// This method is used to save Loan application Detail
        /// </summary>
        /// <param name="LoanApplication">LoanApplication</param> 

        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveLoanApplicationDetails(LoanApplication LoanApplication, long? userID);

        /// <summary>
        /// This method is used to get Loan application Detail by ID
        /// </summary>
        /// <param name="applicationID">applicationID</param> 

        /// <returns>LoanApplication</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        LoanApplication GetLoanApplicationByApplicationID(Int64 applicationID);
        LoanApplication GetLoanApplicationByApplicationIDForNotifications(Int64 applicationID);
        /// <summary>
        /// This method is used to get all Loan application Detail
        /// </summary>
        /// <param name="LoanApplication">LoanApplication</param> 
        /// <param name="UserID">UserID</param> 
        /// <param name="programId">programId</param> 
        /// <returns>IQueryable<LoanApplication></returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<LoanApplication> GetLoanApplications(long UserID, List<long> programIds);
        long GetProgramInvitationByLoanApplicationID(long loanAllicationID);

        bool UpdateLoanApplicationApplicationStatus(LoanApplication LoanApplication, long? userID, int applicationStatusID);
        void SaveOrUpdateLoanPaymentScheduledStatus(PaymentScheduleStatus paymentScheduleStatus, long? userID);
        PaymentScheduleStatus GetPaymentScheduleStatusById(long id);
        int GetProgressReportId(string name);
        #endregion Methods

    }
}
