namespace ThoughtFocus.Repository.Interfaces.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Admin;
    using ThoughtFocus.DataAccess.Models.Contact;
 
    public interface IContactRepository : IEFApplicationBaseRepository<Contact>
    {
        #region Methods

        /// <summary>
        /// This method is used to get contact by using ID
        /// </summary>
        /// <param name="ContactId">Contact ID</param>    
        /// <returns>Contact</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        Contact GetContactsByID(long ContactID);

        /// <summary>
        /// This method is used to get internal contact by using ID
        /// </summary>
        /// <param name="ContactId">Contact ID</param>    
        /// <returns>Contact</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        Contact GetInternalContactsByID(long ContactID);

        /// <summary>
        /// This method is used to get business contact by using ID
        /// </summary>
        /// <param name="ContactId">Contact ID</param>    
        /// <param name="BusinessID">Business ID</param> 
        /// <returns>BusinessUser</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessUser GetBusinessContactsByID(long ContactID, long BusinessID);

        /// <summary>
        /// This method is used to get contacts
        /// </summary>   
        /// <returns>IQueryable Contact</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<Contact> GetContacts();

        /// <summary>
        /// This method is used to save or update contact
        /// </summary>
        /// <param name="contact">Contact</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateContact(Contact contact,long? userID);

        /// <summary>
        /// This method is used to check if contact exists or not
        /// </summary> 
        /// <param name="contactID">Contact ID</param>   
        /// <returns>Boolean</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        bool CheckContactExists(long contactID);

        /// <summary>
        /// This method is used to get the contact by using Email address
        /// </summary>
        /// <param name="EmailAddress">Email Address</param>    
        /// <returns>ContactEmailAddress</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        Contact GetByEmailAddress(string EmailAddress);

        /// <summary>
        /// This method is used to save or update business contact
        /// </summary>
        /// <param name="contact">Contact</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateBusinessContact(BusinessUser contact,long? userID);

        /// <summary>
        /// This method is used to get business contacts
        /// </summary>   
        /// <returns>IQueryable BusinessUser</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<BusinessUser> GetBusinessContacts();
        List<ProgramInvitationContactRole> GetProgramInvitationContactRoles(long contactID);




        #endregion Methods
    }
}