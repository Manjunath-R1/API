using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Interfaces.Contact
{
    public interface IContact : IBaseBusiness
    {
        #region Methods

        /// <summary>
        /// This method is used to get all the contact information with filters
        /// </summary>
        /// <param name="PageFilter">Page Filter</param>    
        /// <returns>ContactListResponse</returns>
        /// <exception cref="RepositoryException">Repository Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ContactListResponse GetAllContactInformation(PageFilterEntity PageFilter);
        
        #endregion Methods
    }
}