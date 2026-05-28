namespace ThoughtFocus.Repository.Impl.Contact
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Contact;
    using ThoughtFocus.Repository.Interfaces.Contact;
    using System.Threading.Tasks;
    using ThoughtFocus.DataAccess.Models.Admin;
    using System.Collections.Generic;

    public class BusinessContactRepository : AbstractEFApplicationBaseRepository<BusinessUser>, IBusinessContactRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public BusinessContactRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        #endregion Constructors

        #region Methods


        public List<BusinessUser> GetBusinessContactsByID(long BusinessID)
        {
            try
            {
                var query = GetAll().Where(x => x.BusinessID == BusinessID).ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> GetBusinessContactsByID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> GetBusinessContactsByID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> GetBusinessContactsByID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> GetBusinessContactsByID", ex);
            }
        }

        public IQueryable<BusinessUser> GetBusinessContacts()
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> GetBusinessContacts", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> GetBusinessContacts", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> GetBusinessContacts", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> GetBusinessContacts", ex);
            }
        }

        public IQueryable<BusinessUser> GetBusinessContacts(long BusinessID)
        {
            try
            {
                var query = GetAll().Where(a => a.BusinessID == BusinessID);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> GetBusinessContacts", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> GetBusinessContacts", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> GetBusinessContacts", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> GetBusinessContacts", ex);
            }
        }

        public IQueryable<BusinessUser> GetBusinessUserByContactID(long contactId)
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true && a.ContactID == contactId);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> GetBusinessUserByContactID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> GetBusinessUserByContactID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> GetBusinessUserByContactID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> GetBusinessUserByContactID", ex);
            }
        }

        public void SaveOrUpdateBusinessUser(BusinessUser businessUser, long? userID)
        {
            try
            {
                if (businessUser.BusinessUserID > 0)
                {
                    _Context.Entry(businessUser).State = EntityState.Modified;
                }
                else
                {
                    _Context.BusinessUsers.Add(businessUser);
                }
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> SaveOrUpdateBusinessUser", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> SaveOrUpdateBusinessUser", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> SaveOrUpdateBusinessUser", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> SaveOrUpdateBusinessUser", ex);
            }
        }




        #endregion Methods
    }
}