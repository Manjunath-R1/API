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

    public class StateRepositoryImpl : AbstractEFApplicationBaseRepository<State>, IStateRepository
    {
        #region Constructors

        public StateRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
        }

        public List<State> GetAllStateData()
        {
            try 
            {
                return GetAll().ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in StateRepositoryImpl-> GetAllStateData", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in StateRepositoryImpl-> GetAllStateData", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in StateRepositoryImpl-> GetAllStateData", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in StateRepositoryImpl-> GetAllStateData", ex);
            }
        }

        #endregion Constructors

        #region Methods

        public State GetStateByStateId(long StateId)
        {
            var query = GetAll().FirstOrDefault(x => x.StateID == StateId);
             return query;
        }
        public List<State> GetStates()
        {
            var query = GetAll().ToList();
             return query;
        }

        #endregion Methods
    }
}