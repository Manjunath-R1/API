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

    public class ProgramInvitationRepository : AbstractEFApplicationBaseRepository<ProgramInvitation>, IProgramInvitationRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public ProgramInvitationRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods
        public ProgramInvitation GetProgramInvitation(long ProgramInvitationID)
        {
            try
            {                 
                var query = GetAll().Where(x => x.ProgramInvitationID == ProgramInvitationID && x.IsActive)?.FirstOrDefault();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at ProgramInvitationRepository-> GetProgramInvitation", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at ProgramInvitationRepository-> GetProgramInvitation", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ProgramInvitationRepository-> GetProgramInvitation", ex);
            }
        }

         public List<ProgramInvitation> GetProgramInvitationByBusinessID(long[] businessIDs)
        {
            try
            {                 
                var query = GetAll().Where(x =>businessIDs.Contains(x.BusinessID) && x.IsActive)?.ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at ProgramInvitationRepository-> GetProgramInvitation", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at ProgramInvitationRepository-> GetProgramInvitation", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ProgramInvitationRepository-> GetProgramInvitation", ex);
            }
        }

        public void SaveOrUpdateProgramInvitation(ProgramInvitation programInvitation,long? userID)
        {
            try
            {                 
                if (programInvitation.ProgramInvitationID > 0)
                {                    
                    _Context.Entry(programInvitation).State = EntityState.Modified;
                }                   
                else
                {
                    _Context.ProgramInvitations.Add(programInvitation);                
                }
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in BusinessContactRepository-> SaveOrUpdateProgramInvitation", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in BusinessContactRepository-> SaveOrUpdateProgramInvitation", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in BusinessContactRepository-> SaveOrUpdateProgramInvitation", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessContactRepository-> SaveOrUpdateProgramInvitation", ex);
            }
        }

        public ProgramInvitation GetProgramInvitationById(long? programID)
        {
             try
            {                 
                var query = GetAll().Where(x => x.ProgramInvitationID == programID && x.IsActive)?.FirstOrDefault();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at ProgramInvitationRepository-> GetProgramInvitationById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at ProgramInvitationRepository-> GetProgramInvitationById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ProgramInvitationRepository-> GetProgramInvitationById", ex);
            }
        }
        public ProgramInvitationEmailPlaceHolder GetProgramInvitationEmailPlaceHolder(long programID)
        {
            try
            {
                var query = _Context.ProgramInvitationEmailPlaceHolder.Where(x => x.ProgramID == programID).FirstOrDefault();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at ProgramInvitationRepository-> GetProgramInvitationEmailPlaceHolder", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at ProgramInvitationRepository-> GetProgramInvitationEmailPlaceHolder", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception at ProgramInvitationRepository-> GetProgramInvitationEmailPlaceHolder", ex);
            }
        }
            #endregion Methods
        }
}