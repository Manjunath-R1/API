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

    public class ApplicationTypeRepositoryImpl : AbstractEFApplicationBaseRepository<ApplicationType>, IApplicationTypeRepository
    {
        #region Constructors

        public ApplicationTypeRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        public List<ApplicationType> GetAllApplicationTypeData()
        {
            try 
            {
                return GetAll().ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in ApplicationTypeRepositoryImpl-> GetAllApplicationTypeData", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in ApplicationTypeRepositoryImpl-> GetAllApplicationTypeData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in ApplicationTypeRepositoryImpl-> GetAllApplicationTypeData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in ApplicationTypeRepositoryImpl-> GetAllApplicationTypeData", ex);
            }
        }

        #endregion Constructors
    }
}