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

    public class BusinessOwnerRepository : AbstractEFApplicationBaseRepository<BusinessOwner>, IBusinessOwnerRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
       
        #endregion Fields

        #region Constructors

        public BusinessOwnerRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
            
        }

        #endregion Constructors

        #region Methods
         public void SaveOrUpdateBusinessOwner(List<BusinessOwner> BusinessOwners,long? userID)
        {
            try
            {      
                foreach (var businessOwner in BusinessOwners)
                {
                    if (businessOwner.ID > 0)
                    {
                        //_Context.Entry(BusinessOwner).State = EntityState.Modified;
                        var local = _Context.Set<BusinessOwner>()
                                .Local
                                .FirstOrDefault(entry => entry.ID.Equals(businessOwner.ID));

                        // check if local is not null 
                        if (local != null)
                        {
                            // detach
                            _Context.Entry(local).State = EntityState.Detached;
                        }

                        _Context.Entry(businessOwner).State = EntityState.Modified;
                    }                   
                    else
                        _Context.BusinessOwners.Add(businessOwner);
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


        public IQueryable<BusinessOwner> GetBusinessOwnersByApplicationID(long applicationID)
        {
             try
             {
                var query = GetAll().Where(a => a.IsActive == true && a.LoanApplicationID == applicationID);
                return query;
             }
             catch (SqlException ex)
             {
                 throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
             }
             catch (DbUpdateException ex)
             {
                 throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
             }
             catch (ObjectDisposedException ex)
             {
                 throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
             }
             catch (Exception ex)
             {
                 throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
             }
        }
        #endregion Methods

    }
}