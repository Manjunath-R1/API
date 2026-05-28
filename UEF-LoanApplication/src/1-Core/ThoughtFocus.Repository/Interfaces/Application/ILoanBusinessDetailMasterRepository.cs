namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Application;

    public interface ILoanBusinessDetailMasterRepository : IEFApplicationBaseRepository<LoanBusinessDetailMaster>
    {
        #region Methods

        /// <summary>
        /// This method is used to save or update LoanBusinessDetail
        /// </summary>
        /// <param name="LoanBusinessDetail">BusinessDetail</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateLoanBusinessDetailsMaster(LoanBusinessDetailMaster BusinessDetail, long? userID);
        long GetProgramStatusId(long programInvitationId);
        #endregion Methods
    }
}