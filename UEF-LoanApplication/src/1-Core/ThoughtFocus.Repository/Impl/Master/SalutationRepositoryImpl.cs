namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;
    using ThoughtFocus.Common.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class SalutationRepositoryImpl : AbstractEFApplicationBaseRepository<Salutation>, ISalutationRepository
    {
        #region Constructors

        public SalutationRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public Salutation GetSalutationBySalutationId(long SalutationId)
        {
            var query = GetAll().FirstOrDefault(x => x.SalutationID == SalutationId);
               return query;
        }

        public List<Salutation> GetAllSalutationData() 
        {
            try 
            {
                return GetAll().ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in SalutationRepositoryImpl-> GetAllSalutationData", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in SalutationRepositoryImpl-> GetAllSalutationData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in SalutationRepositoryImpl-> GetAllSalutationData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SalutationRepositoryImpl-> GetAllSalutationData", ex);
            }
        }

        #endregion Methods
    }
}