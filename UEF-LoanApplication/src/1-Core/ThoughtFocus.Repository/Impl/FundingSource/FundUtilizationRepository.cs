namespace ThoughtFocus.Repository.Impl.FundingSource
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq; 
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.FundingSource;
    using ThoughtFocus.Repository.Interfaces.FundingSource;
    using System.Threading.Tasks;
    using ThoughtFocus.Domain.Enumeration;
    using System.Collections.Generic;

    public class FundUtilizationRepository : AbstractEFApplicationBaseRepository<FundUtilization>, IFundUtilizationRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public FundUtilizationRepository (ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods


        public FundUtilization GetFundUtilizationID(Int64 ID)
        {
             try
             {
                var query = GetAll().FirstOrDefault(x => x.ID == ID);
                return query;
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("Exception in FundUtilizationRepository-> GetFundUtilizationID", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("Exception in FundUtilizationRepository-> GetFundUtilizationID", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("Exception in FundUtilizationRepository-> GetFundUtilizationID", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in FundUtilizationRepository-> GetFundUtilizationID", ex);
             }
        }

        public List<FundUtilization> GetFundUtilization(long fundingSourceID)
        {
            try
            {
                var query = GetAll()
                                .Where(
                                    a => a.IsActive == true 
                                    && a.FundingSourceID == fundingSourceID 
                                    && a.TransactionTypeID == (int)TransactionTypeEnumeration.Allocated).ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundUtilization",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundUtilization",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundUtilization",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundUtilization",ex);
            }
        }

        public void SaveOrUpdateFundUtilization(FundUtilization fundUtilization, long userID)
        {
            try
            {
                if (fundUtilization.ID > 0)
                {
                    _Context.Entry(fundUtilization).State = EntityState.Modified;
                }
                else
                {
                    _Context.FundUtilizations.Add(fundUtilization);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundUtilizationRepository-> SaveOrUpdateFundUtilization", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundUtilizationRepository-> SaveOrUpdateFundUtilization", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundUtilizationRepository-> SaveOrUpdateFundUtilization", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at FundUtilizationRepository-> SaveOrUpdateFundUtilization", ex);
            }
        }
        public List<FundTransaction> GetAllFundTransaction()
        {
            try
            {
                var query = _Context.FundTransactions
                                .Where(
                                    a => a.IsActive == true).ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundTransaction", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundTransaction", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundTransaction", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundUtilizationRepository-> GetAllFundTransaction", ex);
            }
        }
       
        #endregion Methods
    }
}
