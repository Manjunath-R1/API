namespace ThoughtFocus.Repository.Interfaces.Admin
{
    using System;
    using ThoughtFocus.DataAccess.Models.Admin;

    public interface ICiviCRMDataExportLogRepository : IEFApplicationBaseRepository<CiviCRMDataExportLog>
    {
        #region Methods

        /// <summary>
        /// This method is used to save the CiviCRM Exported Log details.
        /// </summary>
        /// <param name="civiCRMLog">CiviCRMDataExportLog</param> 
        /// <param name="userID">long</param>
        /// <returns>void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveCiviCRMLog(CiviCRMDataExportLog civiCRMLog, long? userID);
          
        #endregion Methods
    }
}