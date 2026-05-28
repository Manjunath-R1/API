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

    public class LoanBusinessDetailMasterRepository : AbstractEFApplicationBaseRepository<LoanBusinessDetailMaster>, ILoanBusinessDetailMasterRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public LoanBusinessDetailMasterRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        public long GetProgramStatusId(long programInvitationId)
        {
            throw new NotImplementedException();
        }

      

        #endregion Constructors

        #region Methods
        public void SaveOrUpdateLoanBusinessDetailsMaster(LoanBusinessDetailMaster BusinessDetail, long? userID)
        {
            try
            {
                if (BusinessDetail.ID > 0)
                {
                    var local = _Context.Set<LoanBusinessDetailMaster>()
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
                    _Context.LoanBusinessDetailMaster.Add(BusinessDetail);

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
        #endregion Methods

    }
}