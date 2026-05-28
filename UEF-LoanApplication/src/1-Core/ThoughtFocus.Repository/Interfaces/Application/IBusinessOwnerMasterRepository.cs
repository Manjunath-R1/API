using System;
using System.Linq;
using ThoughtFocus.DataAccess.Models.Application;
using System.Collections.Generic;
using ThoughtFocus.Repository;

public interface IBusinessOwnerMasterRepository : IEFApplicationBaseRepository<BusinessOwnerMaster>
{
    #region Methods

    /// <summary>
    /// This method is used to save or update LoanBusinessDetail
    /// </summary>
    /// <param name="BusinessOwner">BusinessOwner</param> 
    /// <param name="userID">User ID</param>   
    /// <returns>Void</returns>
    /// <exception cref="SqlException">SQL Exception</exception>
    /// <exception cref="DbUpdateException">Database update exception</exception>
    /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
    /// <exception cref="Exception">Exception</exception>
    void SaveOrUpdateBusinessOwnerMaster(List<BusinessOwnerMaster> BusinessOwnerMaster, long? userID);
    /// <summary>
    /// This method is used to get Business Owners details
    /// </summary>
    /// <param name="applicationID">Application ID</param>    
    /// <returns>IQueryable<BusinessOwner></returns>
    /// <exception cref="SqlException">SQL Exception</exception>
    /// <exception cref="DbUpdateException">Database update exception</exception>
    /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
    /// <exception cref="Exception">Exception</exception>

    #endregion Methods
}