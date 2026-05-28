namespace ThoughtFocus.Repository.Impl.Application
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq; 

    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;

    public class LoanBusinessDetailRepository : AbstractEFApplicationBaseRepository<LoanBusinessDetail>, ILoanBusinessDetailRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public LoanBusinessDetailRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods
         public void SaveOrUpdateLoanBusinessDetails(LoanBusinessDetail BusinessDetail,long? userID)
        {
            try
            {                 
                if (BusinessDetail.ID > 0)
                {
                    //_Context.Entry(BusinessDetail).State = EntityState.Modified;
                    var local = _Context.Set<LoanBusinessDetail>()
                            .Local
                            .FirstOrDefault(entry => entry.ID.Equals(BusinessDetail.ID));

                    // check if local is not null 
                    if (local != null)
                    {
                        // detach
                        _Context.Entry(local).State = EntityState.Detached;
                    }

                    _Context.Entry(BusinessDetail).State = EntityState.Modified;
                }                   
                else
                    _Context.LoanBusinessDetails.Add(BusinessDetail);                
                 
               this._Context.SaveChanges(userID);
               

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanBusinessDetailRepository-> SaveOrUpdateLoanBusinessDetails", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanBusinessDetailRepository-> SaveOrUpdateLoanBusinessDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanBusinessDetailRepository-> SaveOrUpdateLoanBusinessDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanBusinessDetailRepository-> SaveOrUpdateLoanBusinessDetails", ex);
            }
        }
        public long GetProgramStatusId(long programIvitationId)
        {
            try
            {
                var programStatusID = _Context.ProgramInvitations.Where(a => a.ProgramInvitationID == programIvitationId && a.IsActive == true).FirstOrDefault()?.ProgramStatusID;

                return (long)programStatusID;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramStatusId", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramStatusId", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramStatusId", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramStatusId", ex);
            }
        }
        #endregion Methods

    }
}