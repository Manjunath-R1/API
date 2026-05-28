namespace ThoughtFocus.Repository.Interfaces.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Contact;

    public interface IBusinessContactRepository : IEFApplicationBaseRepository<BusinessUser>
    {
        #region Methods

        /// <summary>
        /// This method is used to get contact by using ID
        /// </summary>
        /// <param name="BusinessUserID">Business User ID</param>    
        /// <returns>BusinessUser</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<BusinessUser> GetBusinessContactsByID(long BusinessID);



        /// <summary>
        /// This method is used to get business contacts
        /// </summary>   
        /// <returns>IQueryable BusinessUser</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<BusinessUser> GetBusinessContacts();


        /// <summary>
        /// This method is used to get business contacts
        /// </summary>   
        /// <param name="BusinessID">Business ID</param>  
        /// <returns>IQueryable BusinessUser</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<BusinessUser> GetBusinessContacts(long BusinessID);

        /// <summary>
        /// This method is used to get business users by contact ID
        /// </summary>   
        /// <param name="contactId">Contact ID</param>  
        /// <returns>IQueryable BusinessUser</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<BusinessUser> GetBusinessUserByContactID(long contactId);

        /// <summary>
        /// This method is used to save/update business user
        /// </summary>   
        /// <param name="businessUser">businessUser</param>  
        /// <returns>NA</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateBusinessUser(BusinessUser businessUser, long? userID);






        #endregion Methods
    }
}