namespace ThoughtFocus.Repository.Impl.Application
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;

    public class LoanApplicantRepository : AbstractEFApplicationBaseRepository<LoanApplicantDetails>, ILoanApplicantRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public LoanApplicantRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods

        public void SaveLoanApplicantDetails(LoanApplicantDetails LoanApplicantDetails)
        {
            try
            {
                _Context.LoanApplicantDetails.Add(LoanApplicantDetails);
                _Context.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicantRepository-> SaveLoanApplicantDetails", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicantRepository-> SaveLoanApplicantDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicantRepository-> SaveLoanApplicantDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicantRepository-> SaveLoanApplicantDetails", ex);
            }
            
        }

        #endregion Methods

    }
}