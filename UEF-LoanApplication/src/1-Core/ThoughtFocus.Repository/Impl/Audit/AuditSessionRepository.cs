namespace ThoughtFocus.Repository.Impl.Audit
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using ThoughtFocus.Repository.Interfaces.Audit;
    using ThoughtFocus.DataAccess.Models.Audit;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.Repository.Interfaces.User;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class AuditSessionRepository : AbstractEFApplicationBaseRepository<AuditSessionDetails>, ISessionDetailsRepository
    {
        #region Fields

        public ApplicationDBContext _Context;
        

        #endregion Fields
        #region Constructors

        public AuditSessionRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods

        public AuditSessionDetails GetAuditDetailsBySessionID(int ID)
        {
            try
            {
            var query = GetAll().FirstOrDefault(x=>x.Id == ID);
             return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> GetAuditDetailsBySessionID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> GetAuditDetailsBySessionID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> GetAuditDetailsBySessionID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> GetAuditDetailsBySessionID", ex);
            }
        }
        
      
       public void SaveUserSessionDetails(AuditSessionDetails auditSessionDetails)
        {
            try
            {
                if(auditSessionDetails!=null)
                {
                   this._Context.AuditSessionDetails.Add(auditSessionDetails);
                }               
              
                this._Context.SaveChanges(auditSessionDetails.UserID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> SaveUserSessionDetails", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> SaveUserSessionDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> SaveUserSessionDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in AuditSessionRepositoryImpl-> SaveUserSessionDetails", ex);
            }
        }

        #endregion Methods
    }
}