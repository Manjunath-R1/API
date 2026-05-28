namespace ThoughtFocus.Repository.Interfaces.Admin
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Admin;

    public interface IBusinessEntityRepository : IEFApplicationBaseRepository<BusinessEntity>
    {
        #region Methods

        /// This method is used to save or update Business Entity
        /// </summary>
        /// <param name="businessEntity">Save Business Entity</param>
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateBusinessEntity(BusinessEntity businessEntity,long? userID);
   
        /// <summary>
        /// This method is used to get all the Business Entity Information
        /// </summary>
        /// <returns>IQueryable BusinessEntity</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<BusinessEntity> GetBusinessEntity();

        /// <summary>
        /// This method is used to get the Business Entity Information from the ID
        /// </summary>
        /// <returns>BusinessEntity</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessEntity GetBusinessEntity(long businessEntityID);

        /// <summary>
        /// This method is used to get the business entity by using Business name
        /// </summary>
        /// <param name="ein">EIN</param>    
        /// <returns>Business Entity Name</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessEntity GetByEIN(string ein);

        #endregion Methods
    }
}