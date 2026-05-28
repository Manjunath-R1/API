namespace ThoughtFocus.Repository.Interfaces.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ThoughtFocus.DataAccess.Models.Admin;

    public interface IProgramInvitationRepository : IEFApplicationBaseRepository<ProgramInvitation>
    {
        #region Methods


        /// <summary>
        /// This method is used to fetch program invitation details using id
        /// </summary>
        /// <param name="contact">ProgramInvitationID</param>  
        /// <returns>ProgramInvitation</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramInvitation GetProgramInvitation(long ProgramInvitationID);

        /// <summary>
        /// This method is used to fetch program invitation details using id
        /// </summary>
        /// <param name="contact">ProgramInvitationID</param>  
        /// <returns>ProgramInvitation</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<ProgramInvitation> GetProgramInvitationByBusinessID(long[] businessIDs);


        /// <summary>
        /// This method is used to save or update program invitation
        /// </summary>
        /// <param name="programInvitation">Program Invitation</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateProgramInvitation(ProgramInvitation programInvitation, long? userID);


        /// <summary>
        /// This method is used to fetch program invitation details using program id
        /// </summary>
        /// <param name="programID">programID</param>  
        /// <returns>ProgramInvitation</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramInvitation GetProgramInvitationById(long? programID);

        ProgramInvitationEmailPlaceHolder GetProgramInvitationEmailPlaceHolder(long programID);
        #endregion Methods
    }
}