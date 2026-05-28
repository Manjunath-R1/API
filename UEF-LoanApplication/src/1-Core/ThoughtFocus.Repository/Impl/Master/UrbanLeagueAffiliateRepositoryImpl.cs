namespace ThoughtFocus.Repository.Impl.Master
{
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.Repository.Interfaces.Master;
    using System.Data.SqlClient;
    using ThoughtFocus.Common.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using ThoughtFocus.DataAccess.Models.Admin;

    public class UrbanLeagueAffiliateRepositoryImpl : AbstractEFApplicationBaseRepository<UrbanLeagueAffiliate>, IUrbanLeagueAffiliateRepository
    {
        private ApplicationDBContext _Context;

        #region Constructors

        public UrbanLeagueAffiliateRepositoryImpl(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;
        }

        #endregion Constructors

        #region Methods

        public UrbanLeagueAffiliate GetUrbanLeagueAffiliateById(long AffiliateId)
        {
            var query = GetAll().FirstOrDefault(x => x.AffiliateID == AffiliateId);
            return query;
        }


        public UrbanLeagueAffiliate GetUrbanLeagueAffiliate()
        {
            var query = GetAll().Where(a => a.IsActive == true);
            return (UrbanLeagueAffiliate)query;
        }
        public UrbanLeagueAffiliateContact GetUrbanLeagueAffiliateContacts(long AffiliateId)
        {
            try
            {
                return this._Context.UrbanLeagueAffiliateContacts.FirstOrDefault(x => x.AffiliateID == AffiliateId);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in UrbanLeagueAffiliateRepositoryImpl-> GetUrbanLeagueAffiliateContacts", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in UrbanLeagueAffiliateRepositoryImpl-> GetUrbanLeagueAffiliateContacts", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in UrbanLeagueAffiliateRepositoryImpl-> GetUrbanLeagueAffiliateContacts", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> GetUrbanLeagueAffiliateContacts", ex);
            }
        }

        public void SaveOrUpdateAffiliate(UrbanLeagueAffiliate affiliateInfo, long? userID)
        {
            try
            {
                if (affiliateInfo.AffiliateID > 0)
                {
                    _Context.Entry(affiliateInfo).State = EntityState.Modified;
                }
                else
                    _Context.UrbanLeagueAffiliates.Add(affiliateInfo);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliate", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliate", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliate", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliate", ex);
            }
        }
        public void SaveOrUpdateAffiliatContact(UrbanLeagueAffiliateContact AffiliateContactInfo, long? userID)
        {
            try
            {
                if (AffiliateContactInfo.AffiliateContactID > 0)
                {
                    _Context.Entry(AffiliateContactInfo).State = EntityState.Modified;
                }
                else
                    _Context.UrbanLeagueAffiliateContacts.Add(AffiliateContactInfo);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliatContact", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliatContact", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliatContact", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> SaveOrUpdateAffiliatContact", ex);
            }
        }

        public UrbanLeagueAffiliate GetAffiliateName(string EmailAddress)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.AffiliateName == EmailAddress && x.IsActive==true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);

            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in UrbanLeagueAffiliateRepositoryImpl-> GetAffiliateName", ex);
            }
        }
        #endregion Methods
    }
}