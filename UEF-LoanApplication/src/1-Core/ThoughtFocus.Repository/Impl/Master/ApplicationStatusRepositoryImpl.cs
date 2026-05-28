namespace ThoughtFocus.Repository.Impl.Master
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using DataAccess.Models.Master;
    using Microsoft.EntityFrameworkCore;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class ApplicationStatusRepositoryImpl : AbstractEFApplicationBaseRepository<ApplicationStatus>, IApplicationStatusRepository
    {
        #region Constructors

        public ApplicationStatusRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        public List<ApplicationStatus> GetAllApplicationStatusData()
        {
            try 
            {
                return GetAll().ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in ApplicationStatusRepositoryImpl-> GetAllApplicationStatusData", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in ApplicationStatusRepositoryImpl-> GetAllApplicationStatusData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in ApplicationStatusRepositoryImpl-> GetAllApplicationStatusData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ApplicationStatusRepositoryImpl-> GetAllApplicationStatusData", ex);
            }
        }

        #endregion Constructors
    }
}