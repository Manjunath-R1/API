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

    public class CiviCRMDataExportLogRepository : AbstractEFApplicationBaseRepository<CiviCRMDataExportLog>, ICiviCRMDataExportLogRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public CiviCRMDataExportLogRepository(ApplicationDBContext context) : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods

        public void SaveCiviCRMLog(CiviCRMDataExportLog civiCRMLog, long? userID)
        {
            try
            {                 
                if (civiCRMLog.ID > 0)
                {          
                    _Context.Entry(civiCRMLog).State = EntityState.Modified;
                }                   
                else
                {
                    _Context.CiviCRMDataExportLogs.Add(civiCRMLog);                
                }
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in CiviCRMDataExportLogRepository-> SaveCiviCRMLog", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in CiviCRMDataExportLogRepository-> SaveCiviCRMLog", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in CiviCRMDataExportLogRepository-> SaveCiviCRMLog", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in CiviCRMDataExportLogRepository-> SaveCiviCRMLog", ex);
            }
        }

        #endregion Methods
    }
}