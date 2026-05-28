namespace ThoughtFocus.Repository.Impl.Admin
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Admin;
    using ThoughtFocus.Repository.Interfaces.Admin;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class QuestionsRepository : AbstractEFApplicationBaseRepository<Question>, IQuestionsRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public QuestionsRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        #endregion Constructors

        #region Methods

        public void SaveOrUpdateQuestion(Question questionInfo, long? userID)
        {
            try
            {
                if (questionInfo.QuestionID > 0)
                {
                    _Context.Entry(questionInfo).State = EntityState.Modified;
                }
                else
                    _Context.Questions.Add(questionInfo);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in QuestionsRepository-> SaveOrUpdateQuestion", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in QuestionsRepository-> SaveOrUpdateQuestion", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in QuestionsRepository-> SaveOrUpdateQuestion", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in QuestionsRepository-> SaveOrUpdateQuestion", ex);
            }
        }
        
        #endregion Methods
    }
}