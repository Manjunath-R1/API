using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.Repository.Interfaces.Master
{
    public interface IUrbanLeagueAffiliateRepository : IEFApplicationBaseRepository<UrbanLeagueAffiliate>
    {
        #region Methods

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="AffiliateID">AffiliateID</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">GetUrbanLeagueAffiliateById</exception>
        /// <exception cref="Exception">Exception</exception>
        UrbanLeagueAffiliate GetUrbanLeagueAffiliateById(long AffiliateID);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="AffiliateID">AffiliateID</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">GetUrbanLeagueAffiliateContacts</exception>
        /// <exception cref="Exception">Exception</exception>
        UrbanLeagueAffiliateContact GetUrbanLeagueAffiliateContacts(long AffiliateID);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="urbanLeagueAffiliateContacts">urbanLeagueAffiliateContacts</param> 
        /// <param name="userID">userID</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">SaveOrUpdateAffiliatContact</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateAffiliatContact(UrbanLeagueAffiliateContact urbanLeagueAffiliateContacts, long? userID);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="UrbanLeagueAffiliate">UrbanLeagueAffiliate</param> 
        /// <param name="userID">userID</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">SaveOrUpdateAffiliate</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateAffiliate(UrbanLeagueAffiliate UrbanLeagueAffiliate, long? userID);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="affiliateName">affiliateName</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">GetAffiliateName</exception>
        /// <exception cref="Exception">Exception</exception>
        UrbanLeagueAffiliate GetAffiliateName(string affiliateName);

        #endregion Methods
    }
}