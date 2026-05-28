namespace ThoughtFocus.Repository.Impl.Master
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Repository.Interfaces.Master;

    public class GenderRepositoryImpl : AbstractEFApplicationBaseRepository<Gender>, IGenderRepository
    {
        #region Constructors

        public GenderRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public Gender GetGenderByGenderId(long GenderId)
        {
            var query = GetAll().FirstOrDefault(x => x.GenderID == GenderId);
            return query;
        }

        public List<Gender> GetAllGenderData()
        {
            try 
            {
                return GetAll().ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in GenderRepositoryImpl-> GetAllGenderData", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in GenderRepositoryImpl-> GetAllGenderData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in GenderRepositoryImpl-> GetAllGenderData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GenderRepositoryImpl-> GetAllGenderData", ex);
            }
        }

        #endregion Methods
    }
}