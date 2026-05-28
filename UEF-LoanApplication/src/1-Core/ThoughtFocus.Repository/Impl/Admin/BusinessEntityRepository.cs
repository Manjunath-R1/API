namespace ThoughtFocus.Repository.Impl.Contact
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

    public class BusinessEntityRepository : AbstractEFApplicationBaseRepository<BusinessEntity>, IBusinessEntityRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public BusinessEntityRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods


        public void SaveOrUpdateBusinessEntity(BusinessEntity businessEntity,long? userID)
        {
            try
            {                 
                if (businessEntity.ID > 0)
                {          
                    _Context.Entry(businessEntity).State = EntityState.Modified;
                }                   
                else
                {
                    _Context.BusinessEntity.Add(businessEntity);                
                }
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessEntityRepository-> SaveOrUpdateBusinessEntity", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessEntityRepository-> SaveOrUpdateBusinessEntity", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessEntityRepository-> SaveOrUpdateBusinessEntity", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessEntityRepository-> SaveOrUpdateBusinessEntity", ex);
            }
        }
        
        public BusinessEntity GetBusinessEntity(long businessEntityID)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.ID == businessEntityID && x.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessEntityRepository-> GetBusinessEntity",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessEntityRepository-> GetBusinessEntity",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessEntityRepository-> GetBusinessEntity",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessEntityRepository-> GetContacts",ex);
            }
        }
        public IQueryable<BusinessEntity> GetBusinessEntity()
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessEntityRepository-> GetBusinessEntity",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessEntityRepository-> GetBusinessEntity",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessEntityRepository-> GetBusinessEntity",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessEntityRepository-> GetContacts",ex);
            }
        }

        public BusinessEntity GetByEIN(string ein)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.EIN == ein && x.IsActive==true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactEmailAddressRepositoryImpl-> GetByEmailAddress", ex);

            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactEmailAddressRepositoryImpl-> GetByEmailAddress", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactEmailAddressRepositoryImpl-> GetByEmailAddress", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactEmailAddressRepositoryImpl-> GetByEmailAddress", ex);
            }
        }

        #endregion Methods
    }
}