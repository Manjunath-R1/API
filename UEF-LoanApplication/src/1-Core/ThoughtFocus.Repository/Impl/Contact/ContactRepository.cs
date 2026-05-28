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

    public class ContactRepository : AbstractEFApplicationBaseRepository<Contact>, IContactRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public ContactRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods


        public Contact GetContactsByID(long ContactID)
        {
             try
             {
                var query = GetAll().FirstOrDefault(x => x.ContactID == ContactID);
                return query;
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("SqlException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in ContactRepositoryImpl-> GetContactsByID", ex);
             }
        }

        public Contact GetInternalContactsByID(long ContactID)
        {
             try
             {
                var _query = _Context.BusinessUsers.FirstOrDefault(x => x.ContactID == ContactID && x.IsActive==true);
                if(_query==null)
                {
                    var query = GetAll().FirstOrDefault(x => x.ContactID == ContactID && x.IsActive==true);
                    return query;
                }
                return null;
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("SqlException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in ContactRepositoryImpl-> GetContactsByID", ex);
             }
        }

        
        public BusinessUser GetBusinessContactsByID(long ContactID, long BusinessID)
        {
             try
             {
                return this._Context.BusinessUsers.FirstOrDefault(x => x.ContactID == ContactID && x.BusinessID== BusinessID);
                
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("SqlException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> GetContactsByID", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in ContactRepositoryImpl-> GetContactsByID", ex);
             }
        }

        public IQueryable<Contact> GetContacts()
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactRepositoryImpl-> GetContacts",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> GetContacts",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> GetContacts",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactRepositoryImpl-> GetContacts",ex);
            }
        }

        public List<ProgramInvitationContactRole> GetProgramInvitationContactRoles(long contactID)
        {
            try
            {
                var query = this._Context.ProgramInvitationContactRole.Where(cr => cr.IsActive == true && cr.ContactID == contactID).ToList();
                return query;

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactRepositoryImpl-> GetProgramInvitationContactRoles", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> GetProgramInvitationContactRoles", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> GetProgramInvitationContactRoles", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactRepositoryImpl-> GetProgramInvitationContactRoles", ex);
            }
        }
        public void SaveOrUpdateContact(Contact contact,long? userID)
        {
            try
            {                 
                if (contact.ContactID > 0)
                {                    
                    _Context.Entry(contact).State = EntityState.Modified;
                    this._Context.SaveChanges(userID);
                }                   
                else
                {
                    _Context.Contacts.Add(contact);                
                    this._Context.SaveChanges(userID);
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
        }

        public bool CheckContactExists(long contactID)
        {
            try
            {
                var count = this.FindBy(e => e.ContactID == contactID && e.IsActive==true).Count();
                return count > 0 ? true : false;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactRepositoryImpl-> CheckContactExists", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> CheckContactExists", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> CheckContactExists", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactRepositoryImpl-> CheckContactExists", ex);
            }
        }        

        public Contact GetByEmailAddress(string EmailAddress)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.EmailAddress == EmailAddress && x.IsActive==true);
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


        public void SaveOrUpdateBusinessContact(BusinessUser contact,long? userID)
        {
            try
            {                 
                if (contact.BusinessUserID > 0)
                {                    
                    _Context.Entry(contact).State = EntityState.Modified;
                    this._Context.SaveChanges(userID);
                }                   
                else
                {
                    _Context.BusinessUsers.Add(contact);                
                    this._Context.SaveChanges(userID);
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactRepositoryImpl-> SaveOrUpdateContact", ex);
            }
        }

        public IQueryable<BusinessUser> GetBusinessContacts()
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true);
                return (IQueryable<BusinessUser>)query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ContactRepositoryImpl-> GetContacts",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ContactRepositoryImpl-> GetContacts",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ContactRepositoryImpl-> GetContacts",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ContactRepositoryImpl-> GetContacts",ex);
            }
        }


        


        #endregion Methods
    }
}