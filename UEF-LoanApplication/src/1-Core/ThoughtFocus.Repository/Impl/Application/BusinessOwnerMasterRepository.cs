using System;
using System.Collections.Generic;
using System.Text;


namespace ThoughtFocus.Repository.Impl.Application
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;

    public class BusinessOwnerMasterRepository : AbstractEFApplicationBaseRepository<BusinessOwnerMaster>, IBusinessOwnerMasterRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public BusinessOwnerMasterRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        #endregion Constructors

        #region Methods
        public void SaveOrUpdateBusinessOwnerMaster(List<BusinessOwnerMaster> BusinessOwners, long? userID)
        {
            try
            {
                foreach (var BusinessOwner in BusinessOwners)
                {
                    if (BusinessOwner.ID > 0)
                    {
                        var local = _Context.Set<BusinessOwnerMaster>()
                            .Local
                            .FirstOrDefault(entry => entry.ID.Equals(BusinessOwner.ID));

                        // check if local is not null 
                        if (local != null)
                        {
                            // detach
                            _Context.Entry(local).State = EntityState.Detached;
                        }
                        
                        _Context.Entry(BusinessOwner).State = EntityState.Modified;
                    }
                    else
                        _Context.BusinessOwnerMasters.Add(BusinessOwner);
                }
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in BusinessOwnerRepository-> SaveOrUpdateBusinessOwner", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in BusinessOwnerRepository-> SaveOrUpdateBusinessOwner", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in BusinessOwnerRepository-> SaveOrUpdateBusinessOwner", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in BusinessOwnerRepository-> SaveOrUpdateBusinessOwner", ex);
            }
        }

      


        #endregion Methods

    }
}