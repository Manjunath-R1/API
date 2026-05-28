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

    public class ProgramInviteeRepository : AbstractEFApplicationBaseRepository<ProgramInvitee>, IProgramInviteeRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public ProgramInviteeRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods
        
        public IQueryable<ProgramInvitee> GetProgramInvitees()
        {
            try
            {
                var query = GetAll().Where(a => a.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> GetBusinessContacts",ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> GetBusinessContacts",ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> GetBusinessContacts",ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> GetBusinessContacts",ex);
            }
        }

        public long GetUserIDByProgramInvitation(long programInvitationID)
        {
            long contactID = 0;

            var query = new ProgramInvitee();
            try
            {
                query = _Context.ProgramInvitees.Where(a => a.IsActive == true && a.ProgramInvitationID == programInvitationID).FirstOrDefault();

                if (query != null)
                {
                    // Detach the object to remove it from context’s cache.
                    _Context.Entry(query).State = EntityState.Detached;
                    // Then load it. We will get a new object with data
                    // freshly loaded from the database.
                    query = _Context.ProgramInvitees.Find(query.ID);
                    contactID = query.ContactID;
                }
                                
                return contactID;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in ProgramInviteeRepository-> GetUserByProgramInvitation", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in ProgramInviteeRepository-> GetUserByProgramInvitation", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in ProgramInviteeRepository-> GetUserByProgramInvitation", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> GetBusinessContacts", ex);
            }
        }
        #endregion Methods
    }
}