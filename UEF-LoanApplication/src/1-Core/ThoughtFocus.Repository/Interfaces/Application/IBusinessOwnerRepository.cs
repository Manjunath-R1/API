namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Application;
    using System.Collections.Generic;

    public interface IBusinessOwnerRepository : IEFApplicationBaseRepository<BusinessOwner>
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
        void SaveOrUpdateBusinessOwner(List<BusinessOwner> BusinessOwner,long? userID);

        /// <summary>
        /// This method is used to get Business Owners details
        /// </summary>
        /// <param name="applicationID">Application ID</param>    
        /// <returns>IQueryable<BusinessOwner></returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<BusinessOwner> GetBusinessOwnersByApplicationID(long applicationID);

        #endregion Methods
    }
}