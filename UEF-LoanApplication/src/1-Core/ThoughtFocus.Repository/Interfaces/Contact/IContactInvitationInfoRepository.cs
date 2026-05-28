namespace ThoughtFocus.Repository.Interfaces.ContactManagement
{
    using ThoughtFocus.DataAccess.Models.Contact;

    public interface IContactInvitationInfoRepository : IEFApplicationBaseRepository<ContactInvitationInfo>
    {

                /// <summary>
        /// This method is used to get  ContactInvitationInfo By Token
        /// </summary>
        /// <param name="TokenID">TokenID</param>    
          /// <param name="contactId">contactId</param>    
        /// <returns>ContactInvitationInfo</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ContactInvitationInfo GetContactInvitationInfoByToken(string TokenID,long contactId);

         /// <summary>
        /// This method is used to save or update ContactInvitationInfo
        /// </summary>
        /// <param name="contact"> ContactInvitationInfo</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateContactInvitation( ContactInvitationInfo contactInvitationInfo,long? userID);

        
    }
}