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
    using System.Collections.Generic;

    public class FundingEntityRepository : AbstractEFApplicationBaseRepository<FundingEntity>, IFundingEntityRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public FundingEntityRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        #endregion Constructors

        #region Methods
        public IQueryable<FundingEntity> GetAllFundingEntities()
        {
            try
            {
                var query = GetAll().Where(x => x.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at FundingEntityRepository-> GetAllFundingEntities", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at FundingEntityRepository-> GetAllFundingEntities", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at FundingEntityRepository-> GetAllFundingEntities", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at FundingEntityRepository-> GetAllFundingEntities", ex);
            }
        }

        public FundingEntity GetFundingEntityByID(long fundingEntityID)
        {
            try
            {
                var query = GetAll().Where(x => x.IsActive == true && x.FundingEntityID == fundingEntityID).FirstOrDefault();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at FundingEntityRepository-> GetFundingEntityByID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at FundingEntityRepository-> GetFundingEntityByID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at FundingEntityRepository-> GetFundingEntityByID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at FundingEntityRepository-> GetFundingEntityByID", ex);
            }
        }

        public FundingEntity GetFundingEntityEin(string fundingEntityEIN)
        {
            try
            {
                var query = GetAll().Where(x => x.IsActive == true && x.EIN == fundingEntityEIN).FirstOrDefault();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at FundingEntityRepository-> GetFundingEntityEin", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at FundingEntityRepository-> GetFundingEntityEin", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception at FundingEntityRepository-> GetFundingEntityEin", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at FundingEntityRepository-> GetFundingEntityEin", ex);
            }
        }

        public void SaveOrUpdateFundingEntity(FundingEntity fundingEntity, long userID)
        {
            try
            {
                if (fundingEntity.FundingEntityID > 0)
                {
                    fundingEntity.LastModifiedDateTime = DateTime.Now;
                    fundingEntity.LastModifiedByUserID = userID;
                    _Context.Entry(fundingEntity).State = EntityState.Modified;
                }
                else
                {
                    fundingEntity.CreatedDateTime = DateTime.Now;
                    fundingEntity.CreatedByUserID = userID;
                    fundingEntity.LastModifiedDateTime = DateTime.Now;
                    fundingEntity.LastModifiedByUserID = userID;
                    _Context.FundingEntities.Add(fundingEntity);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingEntityRepository-> SaveOrUpdateFundingEntity", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingEntityRepository-> SaveOrUpdateFundingEntity", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingEntityRepository-> SaveOrUpdateFundingEntity", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at FundingEntityRepository-> SaveOrUpdateFundingEntity", ex);
            }
        }

        #endregion Methods
    }
}