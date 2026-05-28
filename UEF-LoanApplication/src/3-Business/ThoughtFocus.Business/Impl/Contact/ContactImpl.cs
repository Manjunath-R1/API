namespace ThoughtFocus.Business.Impl.Contact
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Enumeration;
    using ThoughtFocus.Business.Interfaces.Contact;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.Repository.Interfaces.Contact;
    using ThoughtFocus.Domain.Params;
    using ThoughtFocus.Domain.User; 
    using ThoughtFocus.DataAccess.Models.Contact;
    using ThoughtFocus.Domain;
    using ThoughtFocus.Constants;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.Common.Exceptions.BusinessException; 
    using ThoughtFocus.Validations.ValidationModels;
    using ThoughtFocus.Validations.InputParameterValidation.Contact;
    using ThoughtFocus.Validations.Impl.RuleHandler; 
    using System.Text;
    using ThoughtFocus.Domain.CustomView;
    using ThoughtFocus.Domain.Contact;
    using ThoughtFocus.Domain.Response;
    using ThoughtFocus.Domain.Common;
    using System.Linq;
    using ThoughtFocus.DataAccess.Models.User;
    using ThoughtFocus.Repository.Interfaces.Master;
    using ThoughtFocus.Repository.Interfaces.User;
    using ThoughtFocus.DataAccess.Models.Master;
    using System.Transactions;

    public class ContactImpl : AbstractBusiness, IContact
    {
        #region Fields

        /// <summary>
        /// ILog instance for logging.
        /// </summary>
        private readonly ILogger<ContactImpl> _logger;      
        private IContactRepository _contactRepository; 
        private IRUserRoleRepository _rUserRoleRepository;
        private IUserRepository _userRepository;


        #endregion Fields

        #region Constructors

        public ContactImpl(
        IContactRepository contactRepository, 
        IRUserRoleRepository rUserRoleRepository,
        IUserRepository userRepository,
        ILogger<ContactImpl> logger)
        { 
            this._contactRepository = contactRepository;
            this._logger = logger;
            this._userRepository=userRepository;
            this._rUserRoleRepository=rUserRoleRepository;


        }

        #endregion Constructors

        #region Methods

         public ContactListResponse GetAllContactInformation(PageFilterEntity PageFilter)
        {
            ContactListResponse contactListResponse = new ContactListResponse();
            contactListResponse.ContactPageResultEntity = new PageResultEntity<ContactListingViewEntity>();
            List<ContactListingViewEntity> listOfContactListingViewEntity = null;
            int totalRecordCount = 0;
            string sortExpression = "";
            try
            {
                IQueryable<ThoughtFocus.DataAccess.Models.Contact.Contact> query = this._contactRepository.GetContacts();
                IQueryable<ThoughtFocus.DataAccess.Models.Contact.Contact> ContactCountQuery = this._contactRepository.GetContacts();
                totalRecordCount = ContactCountQuery.Count();
                if (PageFilter.SortBy != null)
                {
                    if (PageFilter.SortDirection == "ascending")
                    {
                        sortExpression = PageFilter.SortBy;
                    }
                    else if (PageFilter.SortDirection == "descending")
                    {
                        sortExpression = PageFilter.SortBy + " DESC";
                    }
                }
                else
                {
                    sortExpression = "FirstName" + " ASC";
                }
                if (PageFilter.IsColumnFilter)
                    PageFilter.PageNumber = 1;

                if (PageFilter.FilterParameters != null)
                {
                    //First Name filter
                    if (PageFilter.FilterParameters.Any(a => a.Key == ContactListFilters.FirstName.ToString()))
                    {
                        string pageFilterFirstName = PageFilter.FilterParameters.Where(a => a.Key == ContactListFilters.FirstName.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterFirstName != null)
                        {
                            string FirstName = pageFilterFirstName.Trim();
                            query = query.Where(a => a.FirstName.Contains(FirstName));
                        }
                    }
                    if (PageFilter.FilterParameters.Any(a => a.Key == ContactListFilters.LastName.ToString()))
                    {
                        string pageFilterLastName = PageFilter.FilterParameters.Where(a => a.Key == ContactListFilters.LastName.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterLastName != null)
                        {
                            string LastName = pageFilterLastName.Trim();
                            query = query.Where(a => a.LastName.Contains(LastName));
                        }
                    }
                    // if (PageFilter.FilterParameters.Any(a => a.Key == ContactListFilters.EmailAddress.ToString()))
                    // {
                    //     string pageFilterEmailAddress = PageFilter.FilterParameters.Where(a => a.Key == ContactListFilters.EmailAddress.ToString()).Select(a => a.Value).FirstOrDefault();
                    //     if (pageFilterEmailAddress != null && pageFilterEmailAddress != "")
                    //     {
                    //         query = query.Where(a => a.ContactEmailAddresses.Any(x => x.IsPrimary == true && x.EmailAddress.Contains(pageFilterEmailAddress)));
                    //     }
                    // }
                    
                    if (PageFilter.FilterParameters.Any(a => a.Key == ContactListFilters.AccountStatus.ToString()))
                    {
                        string accountStatusID = PageFilter.FilterParameters.Where(a => a.Key == ContactListFilters.AccountStatus.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (accountStatusID != null)
                        {
                            long AccountStatusID = Convert.ToInt64(accountStatusID);
                            query = query.Where(m => m.AccountStatusID == AccountStatusID);
                        }
                    }
                }

                contactListResponse.ContactPageResultEntity.FilteredRecord = query.Count();
                listOfContactListingViewEntity = query
                .Select(x => new ContactListingViewEntity
                {
                    ContactID = x.ContactID,
                    UserID = x.Users.UserID,
                    //UserID = x.Users.FirstOrDefault() == null ? 0 : x.Users.FirstOrDefault().UserID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    // EmailAddress = x.ContactEmailAddresses.FirstOrDefault(ce => ce.IsActive == true && ce.IsPrimary == true).EmailAddress,
                    // CountryCallingCode = x.ContactContactInfoes.FirstOrDefault(ci => ci.ContactType == "P" && ci.IsPrimary == true && ci.IsActive == true).Country.CountryCallingCode.ToString(),
                    // PhoneNumber = x.ContactContactInfoes.FirstOrDefault(ci => ci.ContactType == "P" && ci.IsPrimary == true && ci.IsActive == true).Number,
                    //  AccountStatus = x.AccountStatus.Description,
                    //Ethnicity = x.Ethnicity.EthnicityName,
                   

                })
                //ToBeUncommented
                //.OrderBy(sortExpression)
                .Skip((PageFilter.PageNumber - 1) * PageFilter.TakeRecordCount)
                .Take(PageFilter.TakeRecordCount).ToList();

                contactListResponse.ContactPageResultEntity.DataList = listOfContactListingViewEntity;
                contactListResponse.ContactPageResultEntity.TotalRecordCount = totalRecordCount;


            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Exception in ContactImpl-> GetAllContactInformation", ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Exception in ContactImpl-> GetAllContactInformation", ex);
            }
            contactListResponse.IsSuccess = true;
            return contactListResponse;
        }


        #endregion Methods
    }
}