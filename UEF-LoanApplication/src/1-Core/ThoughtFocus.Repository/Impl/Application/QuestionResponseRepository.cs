namespace ThoughtFocus.Repository.Impl.Application
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq; 
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;

    public class QuestionResponseRepository : AbstractEFApplicationBaseRepository<QuestionResponse>, IQuestionResponseRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public QuestionResponseRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods
        public void SaveOrUpdateQuestionResponses(List<QuestionResponse> questionResponses,long? userID)
        {
            try
            {      
                foreach (var questionResponse in questionResponses)
                {
                    if (questionResponse.ID > 0)
                    {                    
                        _Context.Entry(questionResponse).State = EntityState.Modified;
                    }                   
                    else
                        _Context.QuestionResponses.Add(questionResponse);
                }
               this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in QuestionResponseRepository-> SaveOrUpdateQuestionResponses", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in QuestionResponseRepository-> SaveOrUpdateQuestionResponses", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in QuestionResponseRepository-> SaveOrUpdateQuestionResponses", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in QuestionResponseRepository-> SaveOrUpdateQuestionResponses", ex);
            }
        }

        #endregion Methods

    }
}