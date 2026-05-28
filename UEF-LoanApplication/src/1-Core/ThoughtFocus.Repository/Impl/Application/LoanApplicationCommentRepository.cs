namespace ThoughtFocus.Repository.Impl.Application
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess.Models;

    public class LoanApplicationCommentRepository : AbstractEFApplicationBaseRepository<LoanApplicationComment>, ILoanApplicationCommentRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public LoanApplicationCommentRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods

        public void SaveLoanApplicantComments(LoanApplicationComment loanApplicationComments)
        {
            try
            {
                if (loanApplicationComments.LoanApplicationCommentID > 0)
                {                    
                    _Context.Entry(loanApplicationComments).State = EntityState.Modified;
                }                   
                else
                    _Context.LoanApplicationComments.Add(loanApplicationComments);

                 this._Context.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationCommentRepository-> SaveLoanApplicantComments", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationCommentRepository-> SaveLoanApplicantComments", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationCommentRepository-> SaveLoanApplicantComments", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationCommentRepository-> SaveLoanApplicantComments", ex);
            }
            
        }

        #endregion Methods

    }
}