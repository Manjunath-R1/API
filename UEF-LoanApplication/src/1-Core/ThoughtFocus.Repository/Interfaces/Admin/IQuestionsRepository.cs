namespace ThoughtFocus.Repository.Interfaces.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Admin;

    public interface IQuestionsRepository : IEFApplicationBaseRepository<Question>
    {
        #region Methods

        /// <summary>
        /// This method is used to save and update the questions
        /// </summary>  
        /// <returns>List of Program Invitees</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateQuestion(Question questions, long? userID);

        #endregion Methods
    }
}