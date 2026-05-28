namespace ThoughtFocus.Repository.Interfaces.Application
{
    using System;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Application;
    using System.Collections.Generic;

    public interface IQuestionResponseRepository : IEFApplicationBaseRepository<QuestionResponse>
    {
        #region Methods

        /// <summary>
        /// This method is used to save or update QuestionResponses
        /// </summary>
        /// <param name="QuestionResponse">QuestionResponse</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateQuestionResponses(List<QuestionResponse> questionResponses, long? userID);

        #endregion Methods
    }
}