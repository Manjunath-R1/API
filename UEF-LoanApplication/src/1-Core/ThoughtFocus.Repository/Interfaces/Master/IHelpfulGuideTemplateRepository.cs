using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.DataAccess.Models.Master;

namespace ThoughtFocus.Repository.Interfaces.Master
{
    public interface IHelpfulGuideTemplateRepository : IEFApplicationBaseRepository<HelpfulGuideTemplate>
    {
        #region Methods

        /// <summary>
        /// This method is used to fetch the program helpfulguide text
        /// </summary>
        /// <param name="tamplateID">tamplateID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        HelpfulGuideTemplate GetHelpfulGuideById(long tamplateID);
        
        /// <summary>
        /// This method is used to save and update  the helpfulguide text
        /// </summary>
        /// <param name="programHelpfulGuide">programHelpfulGuide</param>   
        /// <param name="userID">userID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateHelpfulGuideTemplate(HelpfulGuideTemplate helpfulGuideTemplate, long userID);

        #endregion Methods
    }
}