namespace ThoughtFocus.Repository.Interfaces.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Admin;

    public interface IProgramInviteeRepository : IEFApplicationBaseRepository<ProgramInvitee>
    {
        #region Methods


        /// <summary>
        /// This method is used to fetch program invitees
        /// </summary>  
        /// <returns>List of Program Invitees</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        IQueryable<ProgramInvitee> GetProgramInvitees();

        long GetUserIDByProgramInvitation(long programInvitationID);

        #endregion Methods
    }
}