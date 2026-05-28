using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Constants;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.Validations.InputParameterValidation.Contact;
using FluentValidation;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.Repository.Interfaces.ContactManagement;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Common.Utilities;
using Microsoft.Extensions.Options;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.Domain.Notification;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.Service.Impl
{
    public class ContactService : IContactService
    {
        #region Fields
        private readonly ILogger<ContactService> Logger;
        private readonly IMapper _mapper;
        private IContactRepository _contactRepository;
        private IUserRepository _userRepository;
        private IBusinessContactRepository _businessContactRepository;
        private INotificationService _notificationService;
        private IContactInvitationInfoRepository _contactInvitationInfoRepository;
        private readonly AppSettings _appSettings;
        public ILogger<ContactService> Object { get; }
        public IContactRepository ContactRepo { get; }
        public IUserRepository UserRepo { get; }
        public IMapper Mapper { get; }
        private IProgramInvitationRepository _programInvitationRepository;
        public IUserPasswordResetInfoRepository _userPasswordResetInfoRepository;
        private readonly INotificationModeRepository _notificationModeRepository;
        private readonly INotificationModeRepositoryImpl _notificationModeImplRepository;
     
        

        #endregion Fields

        #region Constructors

        public ContactService(ILogger<ContactService> logger,
                              IContactRepository contactRepository,
                              IUserRepository userRepository,
                              IMapper _mapper,
                              INotificationService notificationService,
                              IContactInvitationInfoRepository contactInvitationInfoRepository,
                              IOptions<AppSettings> appSettings,
                              IBusinessContactRepository businessContactRepository,
                              IProgramInvitationRepository programInvitationRepository,
                              IUserPasswordResetInfoRepository userPasswordResetInfoRepository,
                              INotificationModeRepository notificationModeRepository,
                              INotificationModeRepositoryImpl notificationModeImplRepository)

        {
            this.Logger = logger;
            this._mapper = _mapper;
            this._appSettings = appSettings.Value;
            this._contactRepository = contactRepository;
            this._userRepository = userRepository;
            this._notificationService = notificationService;
            this._contactInvitationInfoRepository = contactInvitationInfoRepository;
            this._businessContactRepository = businessContactRepository;
            this._programInvitationRepository = programInvitationRepository;
            this._userPasswordResetInfoRepository = userPasswordResetInfoRepository;
            this._notificationModeRepository = notificationModeRepository;
            this._notificationModeImplRepository = notificationModeImplRepository;
            
        }

        #endregion Constructors

        #region Methods


        public CommonResponse CheckEmailExists(string emailAddress)
        {
            CommonResponse commonResponse = null;

            var savedContact = this._contactRepository.GetByEmailAddress(emailAddress);
            if (savedContact != null && savedContact.ContactID > 0)
            {
                commonResponse = new CommonResponse(
                            ResponseStatus.Success, MessageConstants.EmailExists, null
            );
            }
            else
            {
                commonResponse = new CommonResponse(
                            ResponseStatus.Fail, MessageConstants.Success, null);
            }
            return commonResponse;
        }
        public CommonResponse AddContact(ContactRequest ContactEntityParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (ContactEntityParam == null)
            {
                Logger.LogError("Input Parameter is null");
                validationMessages.Add("Input Parameter is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSession is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            try
            {
                var validator = new ContactInvitationValidation();
                var modelValidationResults = validator.Validate(ContactEntityParam, options =>
                {
                    options.IncludeRuleSets("mandatoryFields", "invalidInput");
                });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                    return commonResponse;
                }
                if (ContactEntityParam.ContactID > 0)
                {
                    Contact contact_ = this._contactRepository.FirstOrDefault(a => a.ContactID == ContactEntityParam.ContactID && a.IsActive);
                    if (contact_ != null)
                    {
                        var contactEditEmailCheck = this._contactRepository.GetAll().Where(x => x.EmailAddress == ContactEntityParam.EmailAddress && x.ContactID != contact_.ContactID && x.IsActive).ToList();
                        if (contactEditEmailCheck != null && contactEditEmailCheck.Count > 0)
                        {
                            validationMessages.Add("Email address is already used for other Contact.");
                            commonResponse = new CommonResponse(
                            ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                            return commonResponse;
                        }

                        contact_.ContactID = ContactEntityParam.ContactID;
                        contact_.FirstName = ContactEntityParam.FirstName;
                        contact_.MiddleName = ContactEntityParam.MiddleName;
                        contact_.LastName = ContactEntityParam.LastName;
                        contact_.SalutationID = ContactEntityParam.SalutationID;
                        contact_.PhoneNo = ContactEntityParam.PhoneNo;
                        contact_.EmailAddress = ContactEntityParam.EmailAddress;
                        contact_.LastModifiedByUserID = userSessionEntity.UserID;
                        contact_.LastModifiedDateTime = DateTime.Now;
                        if (ContactEntityParam.BusinessID == 0)
                            contact_.IsActive = ContactEntityParam.IsActive;

                        if (ContactEntityParam.IsActive == false && ContactEntityParam.BusinessID == 0)
                        {
                            contact_.AccountStatusID = (long)AccountStatusEnumeration.Deactivated;
                            ContactInvitationInfo contactInvitationInfo = _contactInvitationInfoRepository.GetAll().Where(x => x.IsComplete == false && x.ContactID == contact_.ContactID).FirstOrDefault();

                            if (contactInvitationInfo != null)
                            {
                                contactInvitationInfo.IsComplete = true;
                                contactInvitationInfo.LastModifiedDateTime = DateTime.Now;
                                contactInvitationInfo.LastModifiedByUserID = userSessionEntity.UserID;
                                _contactInvitationInfoRepository.SaveOrUpdateContactInvitation(contactInvitationInfo, userSessionEntity.UserID);
                            }
                        }
                        if (contact_.AccountStatusID == (long)AccountStatusEnumeration.Deactivated && ContactEntityParam.IsActive == true && ContactEntityParam.BusinessID == 0)
                            contact_.AccountStatusID = (long)AccountStatusEnumeration.NotInvited;

                        if (contact_.Users != null && contact_.Users.UserID != 0)
                        {
                            contact_.Users.LastModifiedByUserID = userSessionEntity.UserID;
                            contact_.Users.LastModifiedDateTime = DateTime.Now;
                            contact_.Users.UserName = ContactEntityParam.EmailAddress;

                            //Role will be modified only for non business contacts
                            if (ContactEntityParam.BusinessID == 0)
                            {
                                List<UserRole> activeRoles = contact_.Users.UserRoles.Where(a => a.IsActive == true).ToList();
                                foreach (var role in activeRoles)
                                {
                                    bool IsRoleExists = ContactEntityParam.UserRoles.Contains(Convert.ToInt32(role.RoleID));
                                    if (!IsRoleExists)
                                    {
                                        role.IsActive = false;
                                        role.LastModifiedByUserID = userSessionEntity.UserID;
                                        role.LastModifiedDateTime = DateTime.Now;
                                    }
                                }
                                foreach (var role in ContactEntityParam.UserRoles)
                                {
                                    var _role = contact_.Users.UserRoles.FirstOrDefault(a => a.RoleID == role && a.IsActive == true);
                                    if (_role == null)
                                    {
                                        UserRole _userRole = new UserRole();
                                        _userRole.UserID = userSessionEntity.UserID;
                                        _userRole.RoleID = role;
                                        _userRole.IsActive = ContactEntityParam.IsActive;
                                        _userRole.CreatedByUserID = userSessionEntity.UserID;
                                        _userRole.CreatedDateTime = DateTime.Now;
                                        _userRole.LastModifiedByUserID = userSessionEntity.UserID;
                                        _userRole.LastModifiedDateTime = DateTime.Now;
                                        contact_.Users.UserRoles.Add(_userRole);
                                    }
                                }
                            }

                        }
                        var programContactRoleList = new List<ProgramInvitationContactRole>();
                        if (ContactEntityParam.ProgramInvitations != null && ContactEntityParam.ProgramInvitations.Count > 0)
                        {
                            if(ContactEntityParam.ProgramInvitations.FirstOrDefault() == 0)
                            {
                                var programContactRole = new ProgramInvitationContactRole();
                                programContactRole.ContactID = contact_.ContactID;
                                programContactRole.ProgramID = 0;
                                programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                                programContactRole.IsActive = contact_.IsActive;
                                programContactRole.CreatedByUserID = userSessionEntity.UserID;
                                programContactRole.CreatedDateTime = DateTime.Now;
                                programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                programContactRole.LastModifiedDateTime = DateTime.Now;
                                programContactRoleList.Add(programContactRole);

                                var prContactRoles = contact_.ProgramInvitationContactRoles.Where(r => r.IsActive && r.ContactID == contact_.ContactID && r.ProgramID > 0);
                                if(prContactRoles != null && prContactRoles.Count() > 0)
                                {
                                    //prContactRoles = contact_.ProgramInvitationContactRoles.Where(r => r.IsActive && r.ContactID == contact_.ContactID && r.ProgramID > 0);
                                    foreach (var crole in prContactRoles)
                                    {
                                        crole.IsActive = false;
                                        crole.LastModifiedByUserID = userSessionEntity.UserID;
                                        crole.LastModifiedDateTime = DateTime.Now;
                                        programContactRoleList.Add(crole);
                                    }
                                }
                            }
                            else
                            {
                                
                                    var noProgramInvitationContactRoles = contact_.ProgramInvitationContactRoles.Where(p => !ContactEntityParam.ProgramInvitations.Contains(p.ProgramID));
                                    if(noProgramInvitationContactRoles != null && noProgramInvitationContactRoles.Count() > 0)
                                    {
                                        foreach(var cr in noProgramInvitationContactRoles)
                                        {
                                            cr.IsActive = false;
                                            cr.LastModifiedByUserID = userSessionEntity.UserID;
                                            cr.LastModifiedDateTime = DateTime.Now;
                                            programContactRoleList.Add(cr);
                                        }
                                    }
                                    var programInvitationContactRoles = contact_.ProgramInvitationContactRoles.Where(p => ContactEntityParam.ProgramInvitations.Contains(p.ProgramID));
                                    foreach (var cRole in ContactEntityParam.ProgramInvitations)
                                    {
                                        var pInvitationContactRole = contact_.ProgramInvitationContactRoles.FirstOrDefault(cr1 =>  cr1.ProgramID == cRole);
                                        if (pInvitationContactRole == null)
                                        {
                                            var programContactRole = new ProgramInvitationContactRole();
                                            programContactRole.ContactID = contact_.ContactID;
                                            programContactRole.ProgramID = cRole;
                                            programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                                            programContactRole.IsActive = contact_.IsActive;
                                            programContactRole.CreatedByUserID = userSessionEntity.UserID;
                                            programContactRole.CreatedDateTime = DateTime.Now;
                                            programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                            programContactRole.LastModifiedDateTime = DateTime.Now;
                                            programContactRoleList.Add(programContactRole);
                                        }
                                        else
                                        {
                                            pInvitationContactRole.IsActive = true;
                                            pInvitationContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                            pInvitationContactRole.LastModifiedDateTime = DateTime.Now;
                                            programContactRoleList.Add(pInvitationContactRole);
                                        }
                                    }
                                    /*
                                        foreach (var cRole in ContactEntityParam.ProgramInvitations)
                                    {
                                        var prContactRoles1 = contact_.ProgramInvitationContactRoles.Where(r => r.IsActive && r.ContactID == contact_.ContactID);
                                        if(prContactRoles1 == null && prContactRoles1.Count() > 0)
                                        {
                                            foreach(var cr in prContactRoles1)
                                            {
                                                if(cr.ProgramID != cRole)
                                                {
                                                    var programContactRole = new ProgramInvitationContactRole();
                                                    programContactRole.ContactID = contact_.ContactID;
                                                    programContactRole.ProgramID = cRole;
                                                    programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                                                    programContactRole.IsActive = contact_.IsActive;
                                                    programContactRole.CreatedByUserID = userSessionEntity.UserID;
                                                    programContactRole.CreatedDateTime = DateTime.Now;
                                                    programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                                    programContactRole.LastModifiedDateTime = DateTime.Now;
                                                    programContactRoleList.Add(programContactRole);
                                                }
                                                else
                                                {

                                                }

                                            }
                                            
                                        }
                                        else
                                        {
                                            var prContactRoles2 = contact_.ProgramInvitationContactRoles.Where(r => r.IsActive && r.ContactID == contact_.ContactID && r.ProgramID == cRole).FirstOrDefault();
                                            if(prContactRoles2 == null)
                                            {
                                                //prContactRoles1.IsActive = false;
                                                //prContactRoles1.LastModifiedByUserID = userSessionEntity.UserID;
                                                //prContactRoles1.LastModifiedDateTime = DateTime.Now;
                                                //programContactRoleList.Add(prContactRoles1);
                                            }
                                           
                                        }
                                    }
                                        */
                               
                            }

/*
                            foreach (var cRole in ContactEntityParam.ProgramInvitations)
                            {
                                var prContactRoles = contact_.ProgramInvitationContactRoles.Where(r => r.IsActive && r.ContactID == contact_.ContactID);
                                if (prContactRoles != null && prContactRoles.Count() > 0)
                                {
                                    var pcRole = prContactRoles.FirstOrDefault(p => p.ProgramID == cRole.ProgramID);
                                    //foreach (var pcRole in prContactRoles)
                                    //{
                                       // var isExist = ContactEntityParam.ProgramInvitations.Find(p => p.ProgramID == pcRole.ProgramID);
                                        if (pcRole != null && cRole.IsSelected)
                                        {
                                        //var programInvitation = ContactEntityParam.ProgramInvitations.Find(pi => pi.ProgramID == pcRole.ProgramID);
                                            pcRole.ProgramID = cRole.ProgramID;
                                            pcRole.LastModifiedByUserID = userSessionEntity.UserID;
                                            pcRole.LastModifiedDateTime = DateTime.Now;
                                            programContactRoleList.Add(pcRole);
                                        }
                                        else if(pcRole != null && !cRole.IsSelected)
                                        {
                                            pcRole.IsActive = false;
                                            pcRole.LastModifiedByUserID = userSessionEntity.UserID;
                                            pcRole.LastModifiedDateTime = DateTime.Now;
                                            programContactRoleList.Add(pcRole);
                                        }
                                        else
                                        {
                                        var programContactRole = new ProgramInvitationContactRole();
                                        programContactRole.ContactID = contact_.ContactID;
                                        programContactRole.ProgramID = cRole.ProgramID;
                                        programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                                        programContactRole.IsActive = contact_.IsActive;
                                        programContactRole.CreatedByUserID = userSessionEntity.UserID;
                                        programContactRole.CreatedDateTime = DateTime.Now;
                                        programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                        programContactRole.LastModifiedDateTime = DateTime.Now;
                                        programContactRoleList.Add(pcRole);
                                        }
                                        if(ContactEntityParam.ProgramInvitations.FirstOrDefault().ProgramID == 0)
                                        {
                                            var programContactRole = new ProgramInvitationContactRole();
                                            programContactRole.ContactID = contact_.ContactID;
                                            programContactRole.ProgramID = cRole.ProgramID;
                                            programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                                            programContactRole.IsActive = contact_.IsActive;
                                            programContactRole.CreatedByUserID = userSessionEntity.UserID;
                                            programContactRole.CreatedDateTime = DateTime.Now;
                                            programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                            programContactRole.LastModifiedDateTime = DateTime.Now;
                                            programContactRoleList.Add(programContactRole);
                                        }
                                    //}
                                }
                                else
                                {
                                    var programContactRole = new ProgramInvitationContactRole();
                                    foreach (var cr in ContactEntityParam.ProgramInvitations)
                                    {
                                        programContactRole.ContactID = contact_.ContactID;
                                        programContactRole.ProgramID = cr.ProgramID;
                                        programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                                        programContactRole.IsActive = contact_.IsActive;
                                        programContactRole.CreatedByUserID = userSessionEntity.UserID;
                                        programContactRole.CreatedDateTime = DateTime.Now;
                                        programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                                        programContactRole.LastModifiedDateTime = DateTime.Now;
                                        programContactRoleList.Add(programContactRole);
                                    }

                                }
                                //var prContactRoles = this._contactRepository.GetProgramInvitationContactRoles(cRole, contact_.ContactID, ContactEntityParam.UserRoles[0]);


                            }
                            */
                            contact_.ProgramInvitationContactRoles = programContactRoleList;
                        }
                        this._contactRepository.SaveOrUpdateContact(contact_, userSessionEntity.UserID);


                        if (ContactEntityParam.BusinessID > 0)
                        {

                            //Contact Business mapping, used for only business users
                            BusinessUser businessUser = this._contactRepository.GetBusinessContactsByID(contact_.ContactID, ContactEntityParam.BusinessID);
                            if (businessUser != null && businessUser.BusinessUserID > 0)
                            {
                                businessUser.BusinessRoleID = ContactEntityParam.BusinessRoleID;
                                businessUser.LastModifiedByUserID = userSessionEntity.UserID;
                                businessUser.LastModifiedDateTime = DateTime.Now;
                                businessUser.IsActive = ContactEntityParam.IsActive;

                                this._contactRepository.SaveOrUpdateBusinessContact(businessUser, userSessionEntity.UserID);

                            }

                        }
                        validationMessages.Add("Profile updated successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        return commonResponse;
                    }
                }

                var _contactEmailCheck = this._contactRepository.GetByEmailAddress(ContactEntityParam.EmailAddress);
                if (ContactEntityParam.ContactID == 0 && _contactEmailCheck != null && _contactEmailCheck.ContactID > 0)
                {
                    validationMessages.Add("Email address already exists.");
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonResponse;
                }

                Contact contact = this._mapper.Map<Contact>(ContactEntityParam);
                contact.AccountStatusID = (long)AccountStatusEnumeration.NotInvited;
                contact.CreatedByUserID = userSessionEntity.UserID;
                contact.CreatedDateTime = System.DateTime.Now;
                contact.LastModifiedByUserID = userSessionEntity.UserID;
                contact.LastModifiedDateTime = System.DateTime.Now;

                if (ContactEntityParam.IsActive == false)
                    contact.AccountStatusID = (long)AccountStatusEnumeration.Deactivated;

                User user = new User();
                user.ContactID = contact.ContactID;
                user.IdentityID = Guid.NewGuid();
                user.UserName = contact.EmailAddress;
                user.IsActive = contact.IsActive;
                user.CreatedByUserID = contact.CreatedByUserID;
                user.CreatedDateTime = DateTime.Now;
                user.LastModifiedByUserID = contact.LastModifiedByUserID;
                user.LastModifiedDateTime = DateTime.Now;
                user.IsAccountActivated = false;

                List<UserRole> userRoles = new List<UserRole>();
                foreach (var role in ContactEntityParam.UserRoles)
                {
                    UserRole _userRole = new UserRole();
                    _userRole.UserID = userSessionEntity.UserID;
                    _userRole.RoleID = role;
                    _userRole.IsActive = contact.IsActive;
                    _userRole.CreatedByUserID = userSessionEntity.UserID;
                    _userRole.CreatedDateTime = DateTime.Now;
                    _userRole.LastModifiedByUserID = userSessionEntity.UserID;
                    _userRole.LastModifiedDateTime = DateTime.Now;
                    userRoles.Add(_userRole);
                }
                user.UserRoles = userRoles;
                contact.Users = user;

                var programContactRoles = new List<ProgramInvitationContactRole>();
                if (ContactEntityParam.ProgramInvitations != null && ContactEntityParam.ProgramInvitations.Count > 0)
                {
                    foreach (var cRole in ContactEntityParam.ProgramInvitations)
                    {
                        var programContactRole = new ProgramInvitationContactRole();
                        programContactRole.ContactID = contact.ContactID;
                        programContactRole.ProgramID = cRole;
                        programContactRole.RoleID = ContactEntityParam.UserRoles[0];
                        programContactRole.IsActive = contact.IsActive;
                        programContactRole.CreatedByUserID = userSessionEntity.UserID;
                        programContactRole.CreatedDateTime = DateTime.Now;
                        programContactRole.LastModifiedByUserID = userSessionEntity.UserID;
                        programContactRole.LastModifiedDateTime = DateTime.Now;
                        programContactRoles.Add(programContactRole);
                    }
                    contact.ProgramInvitationContactRoles = programContactRoles;
                }
                

                this._contactRepository.SaveOrUpdateContact(contact, userSessionEntity.UserID);
                ContactEntityParam.ContactID = contact.ContactID;
                if (ContactEntityParam.BusinessID > 0)
                {
                    //Contact Business mapping, activation email will be triggered along with the program invitation
                    BusinessUser businessUser = new BusinessUser();
                    businessUser.ContactID = contact.ContactID;
                    businessUser.BusinessID = ContactEntityParam.BusinessID;
                    businessUser.BusinessRoleID = ContactEntityParam.BusinessRoleID;
                    businessUser.CreatedByUserID = userSessionEntity.UserID;
                    businessUser.CreatedDateTime = DateTime.Now;
                    businessUser.LastModifiedByUserID = userSessionEntity.UserID;
                    businessUser.LastModifiedDateTime = DateTime.Now;
                    businessUser.IsActive = contact.IsActive;

                    this._contactRepository.SaveOrUpdateBusinessContact(businessUser, userSessionEntity.UserID);
                }
                else
                {
                    //sending email notification for non business user(NUL user)
                    sendContactInvitation(ContactEntityParam, userSessionEntity);
                }
                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> AddContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AddContact ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> AddContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AddContact", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public ContactViewEntityResponse GetContactInfoById(long contactID, UserSessionEntity userSessionEntity)
        {
            ContactViewEntityResponse contactViewEntityResponse = new ContactViewEntityResponse();
            contactViewEntityResponse.contactViewEntity = new ContactViewEntity();

            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter user session is null");
                contactViewEntityResponse.IsSuccess = false;
                contactViewEntityResponse.Message = "Unable to retrieve Contact as user session entity is null.";
            }
            if (contactID == 0)
            {
                Logger.LogError("contactID is 0");
                contactViewEntityResponse.IsSuccess = false;
                contactViewEntityResponse.Message = "Contact doesn't exist.";
            }
            try
            {
                Contact contact = this._contactRepository.FirstOrDefault(a => a.ContactID == contactID);
                if (contact == null)
                {
                    contactViewEntityResponse.IsSuccess = false;
                    contactViewEntityResponse.Message = "Contact is empty.";
                    return contactViewEntityResponse;
                }
                ContactViewEntity contactViewEntity = this._mapper.Map<ContactViewEntity>(contact);
                contactViewEntity.CanDelete = true;

                List<BusinessUser> businessContacts = this._businessContactRepository.GetAll().Where(x => x.ContactID == contact.ContactID).ToList();

                if (businessContacts != null)
                {
                    BusinessInfo businessInfo = null;
                    foreach (var businessUser in businessContacts)
                    {
                        businessInfo = new BusinessInfo();
                        businessInfo.BusinessID = businessUser.BusinessID;
                        businessInfo.BusinessName = businessUser.BusinessEntity.BusinessName;
                        businessInfo.BusinessRole = businessUser.BusinessRole.BusinessRoleName;
                        businessInfo.Status = businessUser.IsActive == true ? "Active" : "Inactive";
                        contactViewEntity.AssociatedBusinesses.Add(businessInfo);
                        if (businessUser.IsActive == true)
                            contactViewEntity.CanDelete = false;
                    }
                }


                if (contact.Salutation != null)
                {
                    contactViewEntity.SalutationName = contact.Salutation.SalutationName;
                }
                if (contact.AccountStatusID > 0)
                {
                    contactViewEntity.AccountStatusName = contact.AccountStatus?.AccountStatusName;
                }
                contactViewEntity.IsActive = contact.IsActive;

                User user = this._userRepository.FirstOrDefault(a => a.ContactID == contactID && a.IsActive == true);

                if (user != null)
                {
                    UserRole userRole = user.UserRoles == null ? null : user.UserRoles.Where(a => a.IsActive == true).FirstOrDefault();
                    if (userRole != null)
                    {
                        contactViewEntity.RoleID = userRole.RoleID;
                        contactViewEntity.RoleName = userRole.Role.RoleName;
                    }
                }
                contactViewEntityResponse.contactViewEntity = contactViewEntity;
                contactViewEntityResponse.IsSuccess = true;

                if (contactViewEntityResponse != null && contactViewEntityResponse.IsSuccess)
                {
                    contactViewEntityResponse.IsSuccess = true;
                }
                else
                {
                    contactViewEntityResponse.IsSuccess = false;
                    contactViewEntityResponse.Message = "Contact ID doesn't exist.";
                }

                if(contact.ProgramInvitationContactRoles != null && contact.ProgramInvitationContactRoles.Count() > 0)
                {
                    var programInvitations = contact.ProgramInvitationContactRoles.Where(p=>p.IsActive == true);
                    contactViewEntityResponse.ProgramInvitations = new List<ProgramInvitationContactRole>();
                    contactViewEntityResponse.ProgramInvitations.AddRange(programInvitations);
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> GetContactInfoById " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                contactViewEntityResponse.IsSuccess = false;
                contactViewEntityResponse.Message = "Unable to retrieve Contact Information.";

            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> GetContactInfoById " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                contactViewEntityResponse.IsSuccess = false;
                contactViewEntityResponse.Message = "Unable to retrieve Contact Information.";
            }
            return contactViewEntityResponse;
        }

        public CommonResponse DeleteContact(long ContactID, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (ContactID == 0)
            {
                Logger.LogError("Input Parameter UserID is null");
                validationMessages.Add("Contact ID is 0.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }

            //checking if contact is part of any business
            List<BusinessUser> businessContacts = this._businessContactRepository.GetAll().Where(x => x.ContactID == ContactID && x.IsActive == true).ToList();
            if (businessContacts != null && businessContacts.Count > 0)
            {
                Logger.LogError("Contact is actively associated with businessess");
                validationMessages.Add("Contact is actively associated with " + businessContacts.Count.ToString() + "businessess");
                commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }


            try
            {
                Contact contact = this._contactRepository.FirstOrDefault(a => a.ContactID == ContactID);

                if (contact != null && contact.IsActive == true)
                {
                    contact.IsActive = false;
                    contact.LastModifiedByUserID = userSessionEntity.UserID;
                    contact.LastModifiedDateTime = DateTime.Now;
                    if (contact.Users != null && contact.Users.UserID != 0)
                    {
                        contact.Users.IsActive = false;
                        contact.Users.LastModifiedByUserID = userSessionEntity.UserID;
                        contact.Users.LastModifiedDateTime = DateTime.Now;
                    }

                    foreach (var role in contact.Users?.UserRoles)
                    {
                        role.IsActive = false;
                        role.LastModifiedByUserID = userSessionEntity.UserID;
                        role.LastModifiedDateTime = DateTime.Now;
                    }

                    this._contactRepository.SaveOrUpdateContact(contact, userSessionEntity.UserID);

                    contact.AccountStatusID = (long)AccountStatusEnumeration.Deactivated;
                    ContactInvitationInfo contactInvitationInfo = _contactInvitationInfoRepository.GetAll().Where(x => x.IsComplete == false && x.ContactID == contact.ContactID).FirstOrDefault();

                    if (contactInvitationInfo != null)
                    {
                        contactInvitationInfo.IsComplete = true;
                        contactInvitationInfo.LastModifiedDateTime = DateTime.Now;
                        contactInvitationInfo.LastModifiedByUserID = userSessionEntity.UserID;
                        _contactInvitationInfoRepository.SaveOrUpdateContactInvitation(contactInvitationInfo, userSessionEntity.UserID);
                    }



                    validationMessages.Add("Account deleted successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                }
                else
                {
                    validationMessages.Add("Contact doesn't exist.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> DeleteContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> DeleteContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonResponse;
        }

        public bool sendContactInvitation(ContactRequest contact, UserSessionEntity userSessionEntity)
        {
            try
            {
                //Generate call back URL for activate Account
                var tokenID = CommonUtility.CreateUniqueID(contact.ContactID.ToString());
                //var _t = StringCipher.Encrypt(tokenID, _appSettings.PasswordSalt);
                //var _c = StringCipher.Encrypt(contact.ContactID.ToString(), _appSettings.PasswordSalt);
                var callBack = _appSettings.BaseUrl + "/ActivateAccount/" + tokenID + "/" + contact.ContactID.ToString();

                //Update ContactInvitation Status   
                List<ContactInvitationInfo> contactInvitationInfoList = _contactInvitationInfoRepository.GetAll().Where(x => x.IsComplete == false && x.ContactID == contact.ContactID).ToList();

                foreach (var contactInvInfo in contactInvitationInfoList)
                {
                    contactInvInfo.IsComplete = true;
                    contactInvInfo.LastModifiedDateTime = DateTime.Now;
                    contactInvInfo.LastModifiedByUserID = userSessionEntity.UserID;
                    _contactInvitationInfoRepository.SaveOrUpdateContactInvitation(contactInvInfo, userSessionEntity.UserID);
                }

                //sending invitation 
                _notificationService.SendContactEmail("Account Activation Email", callBack, (long)EmailTemplateNameID.CONTACTINVITATION, contact.ContactID, contact.AdditionalMessage, userSessionEntity);

                //Save Contact Invitation Info
                ContactInvitationInfo contactInvitationInfo = new ContactInvitationInfo();
                contactInvitationInfo.ContactID = contact.ContactID;
                contactInvitationInfo.CreatedDateTime = DateTime.Now;
                contactInvitationInfo.CreatedByUserID = userSessionEntity.UserID;
                contactInvitationInfo.LastModifiedDateTime = DateTime.Now;
                contactInvitationInfo.LastModifiedByUserID = userSessionEntity.UserID;
                contactInvitationInfo.IsActive = true;
                contactInvitationInfo.ContactInvitationStatusID = (long)ContactInvitationStatus.PENDING;
                contactInvitationInfo.ContactInvitedDateTime = DateTime.Now;
                contactInvitationInfo.InvitationDescription = "Account Activation Email";
                contactInvitationInfo.InvitationEmailAddreess = contact.EmailAddress;
                contactInvitationInfo.TokenID = tokenID;



                _contactInvitationInfoRepository.SaveOrUpdateContactInvitation(contactInvitationInfo, userSessionEntity.UserID);

                //update  account status in contact table 
                Contact _contact = _contactRepository.GetContactsByID(contact.ContactID);
                if (_contact != null & _contact.ContactID > 0)
                {
                    _contact.LastModifiedByUserID = userSessionEntity.UserID;
                    _contact.LastModifiedDateTime = DateTime.Now;
                    if (_contact.AccountStatusID != (long)AccountStatusEnumeration.Active)
                        _contact.AccountStatusID = (long)AccountStatusEnumeration.Invited;
                    _contactRepository.SaveOrUpdateContact(_contact, userSessionEntity.UserID);
                }

                return true;

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> sendInvitation " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> sendInvitation " + ex.InnerException == null
           ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                return false;
            }
        }

        public AccountActivationResponse GetActivateAccountInfo(string tokenID, string contactId)
        {
            AccountActivationResponse activationResponse = new AccountActivationResponse();
            activationResponse.IsSuccess = false;
            try
            {
                var _contactID = Convert.ToInt64(contactId);
                //Get Contact invitation data
                ContactInvitationInfo contactInvitationInfo = _contactInvitationInfoRepository.GetContactInvitationInfoByToken(tokenID, _contactID);
                if (contactInvitationInfo != null && contactInvitationInfo.ContactInvitationInfoID > 0)
                {
                    //  if (contactInvitationInfo.IsComplete || (contactInvitationInfo.CreatedDateTime - DateTime.Now).TotalHours > 240)
                    if (contactInvitationInfo.IsComplete)
                    {
                        activationResponse.Message = "Activation link is expired.";
                    }
                    else
                    {
                        activationResponse.IsSuccess = true;
                        activationResponse.UserId = contactInvitationInfo.Contact?.Users?.UserID == null ? 0 : contactInvitationInfo.Contact.Users.UserID;
                        activationResponse.ContactId = contactInvitationInfo.Contact?.ContactID == null ? 0 : contactInvitationInfo.Contact.ContactID;
                        activationResponse.TokenID = tokenID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> GetActivateAccountInfo " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetActivateAccountInfo", null);

            }
            return activationResponse;
        }
        public AccountActivationResponse ActivateAccount(AccountActivationParam accountActivationParam)
        {
            AccountActivationResponse activationResponse = new AccountActivationResponse();
            activationResponse.IsSuccess = false;
            try
            {
                //Get Contact invitation data
                Contact contact = _contactRepository.GetContactsByID(accountActivationParam.ContactID);

                if (contact != null && contact.ContactID > 0 && contact.Users != null && contact.Users.UserID == accountActivationParam.UserID)
                {
                    //Update contact info
                    contact.AccountStatusID = (long)AccountStatusEnumeration.Active;
                    contact.LastModifiedByUserID = accountActivationParam.UserID;
                    contact.LastModifiedDateTime = System.DateTime.Now;

                    //Update User info
                    contact.Users.PasswordHash = StringCipher.Encrypt(accountActivationParam.Password, _appSettings.PasswordSalt);
                    contact.Users.PasswordSalt = _appSettings.PasswordSalt;
                    contact.Users.IsAccountActivated = true;
                    contact.Users.LastPasswordChangedDateTime = System.DateTime.Now;
                    contact.Users.LastModifiedByUserID = accountActivationParam.UserID;
                    contact.Users.AccountActivationDate = System.DateTime.Now;
                    _contactRepository.SaveOrUpdateContact(contact, accountActivationParam.UserID);


                    //Update ContactInvitation Status   
                    ContactInvitationInfo contactInvitationInfo = _contactInvitationInfoRepository.GetContactInvitationInfoByToken(accountActivationParam.TokenID, accountActivationParam.ContactID);
                    if (contactInvitationInfo != null && contactInvitationInfo.ContactInvitationInfoID > 0)
                    {
                        contactInvitationInfo.IsComplete = true;
                        contactInvitationInfo.ContactInvitationStatusID = (long)ContactInvitationStatus.ACCEPT;
                        contactInvitationInfo.LastModifiedDateTime = System.DateTime.Now;
                        contactInvitationInfo.LastModifiedByUserID = accountActivationParam.UserID;
                        _contactInvitationInfoRepository.SaveOrUpdateContactInvitation(contactInvitationInfo, accountActivationParam.UserID);
                    }


                    activationResponse.IsSuccess = true;
                    activationResponse.Message = "Account activated successfully.";
                    activationResponse.ContactId = contact.ContactID;
                    activationResponse.ContactId = contact.Users.UserID;

                    sendPostAccountActivationNotification(contact);
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> ActivateAccount " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ActivateAccount", null);

            }
            return activationResponse;

        }


        public ContactListResponse GetBusinessContacts(long businessID)
        {
            ContactListResponse contactListResponse = new ContactListResponse();
            contactListResponse.BusinessContactResultEntity = new PageResultEntity<BusinessContactListingEntity>();
            int totalRecordCount = 0;
            try
            {
                var result = this._businessContactRepository.GetBusinessContacts(businessID).Where(x => x.Contact.IsActive == true).ToList().OrderBy(o => o.Contact.FirstName);
                if (result != null && result.Count() > 0)
                {
                    List<BusinessContactListingEntity> listOfContactListingViewEntity = result
                            .Select(x => new BusinessContactListingEntity
                            {
                                BusinessUserID = x.BusinessUserID,
                                BusinessRoleID = x.BusinessRoleID,
                                ContactID = x.ContactID,
                                BusinessID = businessID,
                                FirstName = x.Contact != null ? x.Contact.FirstName : string.Empty,
                                MiddleName = x.Contact != null ? x.Contact.MiddleName : string.Empty,
                                LastName = x.Contact != null ? x.Contact.LastName : string.Empty,
                                PhoneNo = x.Contact != null ? x.Contact.PhoneNo : string.Empty,
                                EmailAddress = x.Contact != null ? x.Contact.EmailAddress : string.Empty,
                                SalutationID = x.Contact != null ? x.Contact.SalutationID : 0,
                                IsActive = x.IsActive,
                                BusinessRoleName = x.Contact != null ? x.BusinessRole.BusinessRoleName : string.Empty,
                                AccountStatus = x.Contact.AccountStatus != null ? x.Contact.AccountStatus.Description : string.Empty,
                                ShowActivationLink = false,
                                AccountStatusID = x.Contact.AccountStatusID
                            })
                            .ToList();

                    int prgInvitationCount = _programInvitationRepository.GetAll().Where(x => x.BusinessID == businessID && x.IsActive == true).Count();
                    totalRecordCount = listOfContactListingViewEntity.Count();
                    if (prgInvitationCount > 0 && totalRecordCount > 0)
                    {
                        foreach (var user in listOfContactListingViewEntity)
                        {
                            if (user.AccountStatusID == 1 || user.AccountStatusID == 2)
                                user.ShowActivationLink = true;
                        }
                    }

                    contactListResponse.BusinessContactResultEntity.DataList = listOfContactListingViewEntity;
                    contactListResponse.BusinessContactResultEntity.TotalRecordCount = totalRecordCount;
                    contactListResponse.IsSuccess = true;
                }
                else
                {
                    contactListResponse.IsSuccess = false;
                    contactListResponse.Message = "There are no users assigned to this Business Entity.";
                }

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetAllBusinessContact", null);
                contactListResponse.IsSuccess = false;
                contactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetAllBusinessContact", null);
                contactListResponse.IsSuccess = false;
                contactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return contactListResponse;

        }

        public bool sendPostAccountActivationNotification(Contact contact)
        {
            try
            {

                var callBack = _appSettings.BaseUrl;
                var generalEmail = _appSettings.GeneralEmail;

                //sending invitation 
                bool response = _notificationService.SendPostAccountActivationEmail(contact, generalEmail, (long)EmailTemplateNameID.POSTACCOUNTACTIVATION);

                return response;

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> sendInvitation " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> sendInvitation " + ex.InnerException == null
           ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                return false;
            }
        }

        public CommonResponse ResendActivationLink(string userName, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (userName == string.Empty)
            {
                Logger.LogError("Input Parameter userName is Empty at ResendActivationLink method");
                validationMessages.Add("Input Parameter UserName is Empty.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSession is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            try
            {

                User user = _userRepository.GetActiveByUserName(userName);
                if (user == null || user.UserID == 0)
                {
                    Logger.LogError("User name doesn't exists or user is inactive.");
                    validationMessages.Add("User name doesn't exists or user is inactive.");
                    commonResponse = new CommonResponse(
                        ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonResponse;

                }
                ContactRequest ContactEntityParam = new ContactRequest();
                ContactEntityParam.ContactID = user.ContactID;
                ContactEntityParam.EmailAddress = user?.Contact?.EmailAddress;
                ContactEntityParam.EmailAddress = ContactEntityParam.EmailAddress != string.Empty ? ContactEntityParam.EmailAddress : user.UserName;

                sendContactInvitation(ContactEntityParam, userSessionEntity);

                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> AddContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AddContact ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> AddContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AddContact", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public ContactListResponse GetAllInternalContacts()
        {
            ContactListResponse contactListResponse = new ContactListResponse();
            contactListResponse.ContactPageResultEntity = new PageResultEntity<ContactListingViewEntity>();
            int totalRecordCount = 0;
            try
            {
                List<ContactListingViewEntity> listOfContactListingViewEntity = null;

                listOfContactListingViewEntity = this._contactRepository.GetAll().Where(c => c.IsActive == true && c.Users != null && c.Users.UserRoles.Any(y => y.IsActive == true && y.RoleID != (long)RoleIDEnumerations.Borrower && y.RoleID != (long)RoleIDEnumerations.SiteAdmin))
                        .Select(x => new ContactListingViewEntity
                        {
                            ContactID = x.ContactID,
                            FirstName = CommonUtility.FirstCharToUpper(x.FirstName),
                            LastName = CommonUtility.FirstCharToUpper(x.LastName),
                            PhoneNumber = x.PhoneNo,
                            EmailAddress = x.EmailAddress,
                            AccountStatus = x.AccountStatus.Description,
                            UserID = x.Users == null ? 0 : x.Users.UserID,
                            Role = x.Users == null ? "" : x.Users.UserRoles == null ? "" : x.Users.UserRoles.FirstOrDefault(a => a.IsActive == true).Role.RoleDescription,
                            Active = x.IsActive == true ? "Active" : "Inactive"
                        })
                        .ToList();
                totalRecordCount = listOfContactListingViewEntity.Count();
                contactListResponse.ContactPageResultEntity.DataList = listOfContactListingViewEntity.OrderBy(x => x.FirstName).ToList();
                contactListResponse.ContactPageResultEntity.TotalRecordCount = totalRecordCount;
                contactListResponse.IsSuccess = true;

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetAllInternalContacts", null);
                contactListResponse.IsSuccess = false;
                contactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetAllInternalContacts", null);
                contactListResponse.IsSuccess = false;
                contactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return contactListResponse;

        }
        public ContactListResponse GetAllBusinessContacts(long? businessID)
        {
            ContactListResponse contactListResponse = new ContactListResponse();
            contactListResponse.ContactPageResultEntity = new PageResultEntity<ContactListingViewEntity>();
            int totalRecordCount = 0;
            businessID = businessID == null ? 0 : businessID;
            try
            {
                List<ContactListingViewEntity> listOfContactListingViewEntity = null;
                listOfContactListingViewEntity = this._contactRepository.GetAll().Where(c => c.IsActive == true && c.Users.UserRoles.Any(y => y.RoleID == (long)RoleIDEnumerations.Borrower))
                        .Select(x => new ContactListingViewEntity
                        {
                            ContactID = x.ContactID,
                            FirstName = CommonUtility.FirstCharToUpper(x.FirstName),
                            LastName = CommonUtility.FirstCharToUpper(x.LastName),
                            PhoneNumber = x.PhoneNo,
                            EmailAddress = x.EmailAddress,
                            AccountStatus = x.AccountStatus.Description,
                            UserID = x.Users == null ? 0 : x.Users.UserID,
                            Role = x.Users == null ? "" : x.Users.UserRoles == null ? "" : x.Users.UserRoles.FirstOrDefault(a => a.IsActive == true).Role.RoleDescription,
                            Active = x.IsActive == true ? "Active" : "Inactive"
                        })
                        .ToList();
                
                var allBusinessContacts = this._businessContactRepository.GetAll().Select(x => new
                {
                    ContactID = x.ContactID,
                    BusinessID = x.BusinessID,
                    BusinessName = x.BusinessEntity.BusinessName,
                }).ToList();
                var businessContacts = allBusinessContacts.Where(x => x.BusinessID == businessID).ToList();               

                if (businessContacts != null && businessContacts.Count() > 0)
                {
                    foreach (var businessContact in businessContacts)
                    {
                        var removeItem = listOfContactListingViewEntity.Where(x => x.ContactID == businessContact.ContactID).FirstOrDefault();
                        var businessName=

                        listOfContactListingViewEntity.Remove(removeItem);
                    }
                }
                var result = from contact in listOfContactListingViewEntity
                             join bu in allBusinessContacts on contact.ContactID equals bu.ContactID                             
                             select new ContactListingViewEntity
                             {
                                 ContactID = contact.ContactID,
                                 FirstName = contact.FirstName,
                                 LastName = contact.LastName,
                                 PhoneNumber = contact.PhoneNumber,
                                 EmailAddress = contact.EmailAddress,
                                 AccountStatus = contact.AccountStatus,
                                 UserID = contact.UserID,
                                 Role = contact.Role,
                                 Active = contact.Active,
                                 BusinessName = bu.BusinessName,
                             };
                if (result != null)
                {
                    var consolidatedInvitationDetails = result
                                               .GroupBy(c => new
                                               {
                                                   c.ContactID,
                                                   c.FirstName,
                                                   c.LastName,
                                                   c.PhoneNumber,
                                                   c.EmailAddress,
                                                   c.AccountStatus,
                                                   c.UserID,
                                                   c.Role,
                                                   c.Active,
                                               })
                                                .Select(g => g.FirstOrDefault());
                    listOfContactListingViewEntity = consolidatedInvitationDetails?.OrderBy(o => o.FirstName).ToList();
                }
                totalRecordCount = listOfContactListingViewEntity.Count();
                contactListResponse.ContactPageResultEntity.DataList = listOfContactListingViewEntity.OrderBy(x => x.FirstName).ToList();
                contactListResponse.ContactPageResultEntity.TotalRecordCount = totalRecordCount;
                contactListResponse.IsSuccess = true;

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetAllBusinessContacts", null);
                contactListResponse.IsSuccess = false;
                contactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetAllBusinessContacts", null);
                contactListResponse.IsSuccess = false;
                contactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return contactListResponse;

        }

        //this method is used to save or update business contact
        public CommonResponse SaveorUpdateContact(ContactRequest ContactEntityParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (ContactEntityParam == null)
            {
                Logger.LogError("Input Parameter is null.");
                validationMessages.Add("Input Parameter is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSession is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            try
            {
                var validator = new ContactInvitationValidation();
                var modelValidationResults = validator.Validate(ContactEntityParam, options =>
                {
                    options.IncludeRuleSets("mandatoryFields", "invalidInput");
                });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                    return commonResponse;
                }
                if (ContactEntityParam.ContactID > 0)
                {
                    Contact contact_ = this._contactRepository.FirstOrDefault(a => a.ContactID == ContactEntityParam.ContactID && a.IsActive);
                    if (contact_ != null)
                    {
                        var contactEditEmailCheck = this._contactRepository.GetAll().Where(x => x.EmailAddress == ContactEntityParam.EmailAddress && x.ContactID != contact_.ContactID && x.IsActive).ToList();
                        if (contactEditEmailCheck != null && contactEditEmailCheck.Count > 0)
                        {
                            validationMessages.Add("Email address is already used for other Contact.");
                            commonResponse = new CommonResponse(
                            ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                            return commonResponse;
                        }

                        contact_.ContactID = ContactEntityParam.ContactID;
                        contact_.FirstName = ContactEntityParam.FirstName;
                        contact_.MiddleName = ContactEntityParam.MiddleName;
                        contact_.LastName = ContactEntityParam.LastName;
                        contact_.SalutationID = ContactEntityParam.SalutationID;
                        contact_.PhoneNo = ContactEntityParam.PhoneNo;
                        contact_.EmailAddress = ContactEntityParam.EmailAddress;
                        contact_.LastModifiedByUserID = userSessionEntity.UserID;
                        contact_.LastModifiedDateTime = DateTime.Now;

                        if (contact_.Users != null && contact_.Users.UserID != 0)
                        {
                            contact_.Users.UserName = ContactEntityParam.EmailAddress;
                            contact_.Users.LastModifiedByUserID = userSessionEntity.UserID;
                            contact_.Users.LastModifiedDateTime = DateTime.Now;
                            //contact_.Users.UserName = ContactEntityParam.EmailAddress;                       

                        }

                        this._contactRepository.SaveOrUpdateContact(contact_, userSessionEntity.UserID);

                        validationMessages.Add("Contact updated successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        return commonResponse;
                    }
                }

                var _contactEmailCheck = this._contactRepository.GetByEmailAddress(ContactEntityParam.EmailAddress);
                if (ContactEntityParam.ContactID == 0 && _contactEmailCheck != null && _contactEmailCheck.ContactID > 0)
                {
                    validationMessages.Add("Email address already exists.");
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonResponse;
                }

                Contact contact = this._mapper.Map<Contact>(ContactEntityParam);
                contact.AccountStatusID = (long)AccountStatusEnumeration.NotInvited;
                contact.CreatedByUserID = userSessionEntity.UserID;
                contact.CreatedDateTime = System.DateTime.Now;
                contact.LastModifiedByUserID = userSessionEntity.UserID;
                contact.LastModifiedDateTime = System.DateTime.Now;

                if (ContactEntityParam.IsActive == false)
                    contact.AccountStatusID = (long)AccountStatusEnumeration.Deactivated;

                User user = new User();
                user.ContactID = contact.ContactID;
                user.IdentityID = Guid.NewGuid();
                user.UserName = contact.EmailAddress;
                user.IsActive = contact.IsActive;
                user.CreatedByUserID = contact.CreatedByUserID;
                user.CreatedDateTime = DateTime.Now;
                user.LastModifiedByUserID = contact.LastModifiedByUserID;
                user.LastModifiedDateTime = DateTime.Now;
                user.IsAccountActivated = false;

                List<UserRole> userRoles = new List<UserRole>();
                foreach (var role in ContactEntityParam.UserRoles)
                {
                    UserRole _userRole = new UserRole();
                    _userRole.UserID = userSessionEntity.UserID;
                    _userRole.RoleID = role;
                    _userRole.IsActive = contact.IsActive;
                    _userRole.CreatedByUserID = userSessionEntity.UserID;
                    _userRole.CreatedDateTime = DateTime.Now;
                    _userRole.LastModifiedByUserID = userSessionEntity.UserID;
                    _userRole.LastModifiedDateTime = DateTime.Now;
                    userRoles.Add(_userRole);
                }
                user.UserRoles = userRoles;
                contact.Users = user;             

                this._contactRepository.SaveOrUpdateContact(contact, userSessionEntity.UserID);
                ContactEntityParam.ContactID = contact.ContactID;
                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> SaveorUpdateContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> SaveorUpdateContact ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> SaveorUpdateContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> SaveorUpdateContact", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public CommonResponse AddRoleForContact(BusinessContactRequest ContactEntityParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (ContactEntityParam == null)
            {
                Logger.LogError("Input Parameter is null.");
                validationMessages.Add("Input Parameter is null.");
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSession is null");
                validationMessages.Add("User session entity is null");
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            try
            {
                BusinessUser businessUser = new BusinessUser();

                businessUser.BusinessRoleID = ContactEntityParam.BusinessRoleID;
                businessUser.ContactID = ContactEntityParam.ContactID;
                businessUser.BusinessID = ContactEntityParam.BusinessID;
                businessUser.CreatedByUserID = userSessionEntity.UserID;
                businessUser.CreatedDateTime = DateTime.Now;
                businessUser.LastModifiedByUserID = userSessionEntity.UserID;
                businessUser.LastModifiedDateTime = DateTime.Now;
                businessUser.IsActive = true;
                this._contactRepository.SaveOrUpdateBusinessContact(businessUser, userSessionEntity.UserID);

                validationMessages.Add("Role added successfully.");
                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                return commonResponse;

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> AddRoleForContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AddRoleForContact ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> AddRoleForContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AddRoleForContact", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public CommonResponse ChangePassword(PasswordChangeRequest model)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                User user = _userRepository.GetUserByContactID(model.ContactID);
                model.ExistingPassword = StringCipher.Encrypt(model.ExistingPassword, _appSettings.PasswordSalt);
                model.NewPassword = StringCipher.Encrypt(model.NewPassword, _appSettings.PasswordSalt);

                if (user.PasswordHash == model.ExistingPassword)
                {

                    if (user.PasswordHash == model.NewPassword)
                    {
                        validationMessages.Add("New password entered should not be the same as old password.");
                        commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                        commonResponse.StatusMessage = "New password entered should not be the same as old password.";
                    }
                    else
                    {
                        user.PasswordHash = model.NewPassword;
                        user.LastPasswordChangedDateTime = System.DateTime.Now;
                        _userRepository.UpdateUser(user);
                        validationMessages.Add("Password changed successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.StatusMessage = "Password changed successfully.";
                    }
                }
                else
                {
                    validationMessages.Add("Existing Password is not valid password.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Existing Password is not valid password.";

                }

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> ChangePassword " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ChangePassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> ChangePassword " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ChangePassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            return commonResponse;
        }

        public CommonResponse ForgotPassword(ForgotPasswordRequest model)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                string tokenID = "";
                string callBack = "";
                string callBackURL = "";
                User user = _userRepository.GetActiveByUserName(model.EmailId);
                UserSessionEntity userSessionEntity = new UserSessionEntity();
                if (user != null && model.EmailId == user.UserName)
                {
                    if (user.IsLockedOut)
                    {
                        try
                        {
                            tokenID = CommonUtility.CreateUniqueID(user.ContactID.ToString());
                            callBack = "";
                            userSessionEntity.UserID = user.UserID;
                            callBackURL = "";
                            var email = _notificationService.SendForgetPasswordEmail("Account Locked", callBackURL, (long)EmailTemplateNameID.ACCOUNTLOCKED, user.ContactID, userSessionEntity);
                            validationMessages.Add("Account Locked mail sent successfully.");
                            commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                            commonResponse.StatusMessage = "Account Locked mail sent successfully.";
                        }
                        catch (Exception ex)
                        {
                            Logger.LogDebug("Error at Contact Service -> AccountLocked " + ex.InnerException == null
                             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                            LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> AccountLocked ", null);
                            commonResponse = new CommonResponse(
                            ResponseStatus.Fail, MessageConstants.EditFailure, null);
                        }

                    }
                    else
                    {
                        tokenID = CommonUtility.CreateUniqueID(user.ContactID.ToString());
                        callBack = _appSettings.BaseUrl + "/ForgotPasswordActivation/" + tokenID + "/" + user.ContactID.ToString();
                        userSessionEntity.UserID = user.UserID;
                        callBackURL = "<tr> <td style='padding: 20px 0 20px 180px;'> <table class='buttonwrapper' border='0' cellspacing='0' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>Reset Password</a> </td> </tr> </table> </td> </tr> ";
                        var email = _notificationService.SendForgetPasswordEmail("Password reset request", callBackURL, (long)EmailTemplateNameID.FORGOTPASSWORD, user.ContactID, userSessionEntity);
                        UserPasswordResetInfo UserPasswordResetInfo = new UserPasswordResetInfo();
                        UserPasswordResetInfo.UserID = user.UserID;
                        UserPasswordResetInfo.PasswordResetDate = DateTime.Now;
                        UserPasswordResetInfo.TokenID = tokenID;
                        UserPasswordResetInfo.IsExpiry = false;
                        UserPasswordResetInfo.IsComplete = false;
                        UserPasswordResetInfo.IsAdminOrSelf = "Self";
                        UserPasswordResetInfo.CreatedDateTime = DateTime.Now;
                        UserPasswordResetInfo.CreatedByUserID = user.UserID;
                        UserPasswordResetInfo.LastModifiedDateTime = DateTime.Now;
                        UserPasswordResetInfo.LastModifiedByUserID = user.UserID;
                        UserPasswordResetInfo.IsActive = true;
                        _userPasswordResetInfoRepository.SaveOrUpdateForgetPassword(UserPasswordResetInfo, user.UserID);
                        validationMessages.Add("Password reset mail sent successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.StatusMessage = "Password reset mail sent successfully.";
                    }
                }
                else
                {
                    validationMessages.Add("Couldn't find the user.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Couldn't find the user.";
                }

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> ForgetPassword " + ex.InnerException == null
         ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ForgetPassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> ForgetPassword " + ex.InnerException == null
        ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ForgetPassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            return commonResponse;
        }
        public ForgotPasswordResponse GetForgotPasswordInfo(string TokenID, string ContactId)
        {
            ForgotPasswordResponse forgotpasswordResponse = new ForgotPasswordResponse();
            forgotpasswordResponse.IsSuccess = false;
            try
            {
                var userPasswordResetInfo = _userPasswordResetInfoRepository.GetForgetPasswordInfoByToken(TokenID);
                if (userPasswordResetInfo != null)
                {
                    if (userPasswordResetInfo.IsComplete)
                    {
                        forgotpasswordResponse.Message = "Password reset link is expired.";
                    }
                    else
                    {
                        forgotpasswordResponse.IsSuccess = true;
                        forgotpasswordResponse.UserId = userPasswordResetInfo.UserID;
                        forgotpasswordResponse.TokenID = TokenID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> GetForgetPasswordInfo " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetForgetPasswordInfo", null);

            }
            return forgotpasswordResponse;
        }


        public CommonResponse UpdatePassword(NewPasswordChangeRequest model)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                var userPasswordResetInfo = _userPasswordResetInfoRepository.GetForgetPasswordInfoByToken(model.TokenID);
                if (userPasswordResetInfo != null)
                {
                    User user = _userRepository.GetByUserID(userPasswordResetInfo.UserID);
                    model.NewPassword = StringCipher.Encrypt(model.NewPassword, _appSettings.PasswordSalt);

                    if (user.PasswordHash == model.NewPassword)
                    {
                        validationMessages.Add("New password entered should not be the same as old password.");
                        commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                        commonResponse.StatusMessage = "New password entered should not be the same as old password.";
                        return commonResponse;
                    }
                    else
                    {
                        user.PasswordHash = model.NewPassword;
                        user.LastPasswordChangedDateTime = System.DateTime.Now;
                        _userRepository.UpdateUser(user);
                        validationMessages.Add("Password changed successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.StatusMessage = "Password changed successfully.";

                    }

                    if (userPasswordResetInfo.UserPasswordResetInfoID > 0)
                    {
                        UserPasswordResetInfo UserPasswordResetInformation = new UserPasswordResetInfo();
                        userPasswordResetInfo.IsComplete = true;
                        userPasswordResetInfo.LastModifiedDateTime = System.DateTime.Now;
                        userPasswordResetInfo.LastModifiedByUserID = userPasswordResetInfo.UserID;
                        _userPasswordResetInfoRepository.SaveOrUpdateForgetPassword(userPasswordResetInfo, userPasswordResetInfo.UserID);

                    }

                }

                else
                {
                    validationMessages.Add("Invalid request.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Invalid request.";

                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> UpdatePassword " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> UpdatePassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> UpdatePassword " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> UpdatePassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            return commonResponse;
        }

        public CommonResponse DeactivateBusinessContact(long ContactID, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (ContactID == 0)
            {
                Logger.LogError("Input Parameter UserID is null");
                validationMessages.Add("Contact ID is 0.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                commonResponse.StatusMessage = "Contact ID is 0.";
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                commonResponse.StatusMessage = "User session entity is null.";
                return commonResponse;
            }
            try
            {
                Contact contact = this._contactRepository.FirstOrDefault(a => a.ContactID == ContactID);

                if (contact != null && contact.IsActive == true)
                {
                    contact.AccountStatusID = (long)AccountStatusEnumeration.Deactivated;
                    contact.LastModifiedByUserID = userSessionEntity.UserID;
                    contact.LastModifiedDateTime = DateTime.Now;
                    this._contactRepository.SaveOrUpdateContact(contact, userSessionEntity.UserID);
                    validationMessages.Add("Account deactivated successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "Account deactivated successfully.";
                    commonResponse.ID = ContactID;

                }

                else
                {
                    validationMessages.Add("Contact doesn't exist.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Contact doesn't exist.";
                    commonResponse.ID = ContactID;
                }

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> DeactivateBusiness " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> DeactivateBusiness " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonResponse;
        }
        public CommonResponse ActivateBusinessContact(long ContactID, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (ContactID == 0)
            {
                Logger.LogError("Input Parameter UserID is null");
                validationMessages.Add("Contact ID is 0.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                commonResponse.StatusMessage = "Contact ID is 0.";
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSessionEntity is null.");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                commonResponse.StatusMessage = "User session entity is null.";
                return commonResponse;
            }
            try
            {
                Contact contact = this._contactRepository.FirstOrDefault(a => a.ContactID == ContactID);

                if (contact != null && contact.IsActive == true)
                {
                    contact.AccountStatusID = (long)AccountStatusEnumeration.Active;
                    contact.LastModifiedByUserID = userSessionEntity.UserID;
                    contact.LastModifiedDateTime = DateTime.Now;
                    this._contactRepository.SaveOrUpdateContact(contact, userSessionEntity.UserID);
                    validationMessages.Add("Account activated successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "Account activated successfully.";
                    commonResponse.ID = ContactID;

                }

                else
                {
                    validationMessages.Add("Contact doesn't exist.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Contact doesn't exist.";
                    commonResponse.ID = ContactID;
                }

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> ActivateBusinessContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> ActivateBusinessContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonResponse;
        }


        public CommonResponse ResetBusinessContact(ForgotPasswordRequest model)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                string tokenID = "";
                string callBack = "";
                string callBackURL = "";
                User user = _userRepository.GetActiveByUserName(model.EmailId);
                UserSessionEntity userSessionEntity = new UserSessionEntity();
                if (user != null && model.EmailId == user.UserName)
                {
                    tokenID = CommonUtility.CreateUniqueID(user.ContactID.ToString());
                    callBack = _appSettings.BaseUrl + "/ResetContactPasswordActivation/" + tokenID + "/" + user.ContactID.ToString();
                    userSessionEntity.UserID = user.UserID;
                    callBackURL = "<tr> <td style='padding: 20px 0 20px 100px;'> <table class='buttonwrapper' border='0' cellspacing='3' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>Reset Access</a> </td> </tr> </table> </td> </tr> ";
                    var email = _notificationService.SendResetBusinessContactEmail("Password Reset Access", callBackURL, (long)EmailTemplateNameID.RESETBUSINESSCONTACT, user.ContactID, userSessionEntity);
                    UserPasswordResetInfo UserPasswordResetInfo = new UserPasswordResetInfo();
                    UserPasswordResetInfo.UserID = user.UserID;
                    UserPasswordResetInfo.PasswordResetDate = DateTime.Now;
                    UserPasswordResetInfo.TokenID = tokenID;
                    UserPasswordResetInfo.IsExpiry = false;
                    UserPasswordResetInfo.IsComplete = false;
                    UserPasswordResetInfo.IsAdminOrSelf = "Admin";
                    UserPasswordResetInfo.CreatedDateTime = DateTime.Now;
                    UserPasswordResetInfo.CreatedByUserID = user.UserID;
                    UserPasswordResetInfo.LastModifiedDateTime = DateTime.Now;
                    UserPasswordResetInfo.LastModifiedByUserID = user.UserID;
                    UserPasswordResetInfo.IsActive = true;
                    _userPasswordResetInfoRepository.SaveOrUpdateForgetPassword(UserPasswordResetInfo, user.UserID);
                    validationMessages.Add("Reset mail sent successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "Reset mail sent successfully.";
                    commonResponse.ID = user.ContactID;
                }
                else
                {
                    validationMessages.Add("Couldn't find the user.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Couldn't find the user.";
                }

            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> ResetBusinessContact " + ex.InnerException == null
         ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ForgetPassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> ResetBusinessContact " + ex.InnerException == null
        ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ForgetPassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            return commonResponse;
        }

        public ForgotPasswordResponse GetResetContactPasswordInfo(string TokenID, string ContactId)
        {
            ForgotPasswordResponse forgotpasswordResponse = new ForgotPasswordResponse();
            forgotpasswordResponse.IsSuccess = false;
            try
            {
                var userPasswordResetInfo = _userPasswordResetInfoRepository.GetForgetPasswordInfoByToken(TokenID);
                if (userPasswordResetInfo != null)
                {
                    if (System.DateTime.Now > userPasswordResetInfo.LastModifiedDateTime.AddDays(1))
                    {
                        forgotpasswordResponse.Message = "Password reset link is expired.";
                        if (userPasswordResetInfo.UserPasswordResetInfoID > 0)
                        {
                            UserPasswordResetInfo UserPasswordResetInformation = new UserPasswordResetInfo();
                            userPasswordResetInfo.IsExpiry = true;
                            userPasswordResetInfo.LastModifiedDateTime = System.DateTime.Now;
                            _userPasswordResetInfoRepository.SaveOrUpdateForgetPassword(userPasswordResetInfo, userPasswordResetInfo.UserID);

                        }
                    }
                    else
                    {
                        forgotpasswordResponse.IsSuccess = true;
                        forgotpasswordResponse.UserId = userPasswordResetInfo.UserID;
                        forgotpasswordResponse.TokenID = TokenID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> GetResetContactPasswordInfo " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> GetResetContactPasswordInfo", null);

            }
            return forgotpasswordResponse;
        }

        public CommonResponse UpdatePasswordForResetContact(NewPasswordChangeRequest model)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                var userPasswordResetInfo = _userPasswordResetInfoRepository.GetForgetPasswordInfoByToken(model.TokenID);
                if (userPasswordResetInfo != null)
                {
                    User user = _userRepository.GetByUserID(userPasswordResetInfo.UserID);
                    model.NewPassword = StringCipher.Encrypt(model.NewPassword, _appSettings.PasswordSalt);

                    if (user.PasswordHash == model.NewPassword)
                    {
                        validationMessages.Add("New password entered should not be the same as old password.");
                        commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                        commonResponse.StatusMessage = "New password entered should not be the same as old password.";
                        return commonResponse;
                    }
                    else
                    {
                        user.PasswordHash = model.NewPassword;
                        user.LastPasswordChangedDateTime = System.DateTime.Now;
                        _userRepository.UpdateUser(user);
                        validationMessages.Add("Reset business contact successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.StatusMessage = "Reset business contact successfully.";

                    }

                    if (userPasswordResetInfo.UserPasswordResetInfoID > 0)
                    {
                        UserPasswordResetInfo UserPasswordResetInformation = new UserPasswordResetInfo();
                        userPasswordResetInfo.IsComplete = true;
                        userPasswordResetInfo.LastModifiedDateTime = System.DateTime.Now;
                        userPasswordResetInfo.LastModifiedByUserID = userPasswordResetInfo.UserID;
                        _userPasswordResetInfoRepository.SaveOrUpdateForgetPassword(userPasswordResetInfo, userPasswordResetInfo.UserID);

                    }

                }

                else
                {
                    validationMessages.Add("Invalid request.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Invalid request.";

                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> UpdatePasswordForResetContact " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> UpdatePassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> UpdatePasswordForResetContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> UpdatePassword ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            return commonResponse;
        }

        public CommonResponse UnblockBusinessContact(ForgotPasswordRequest model)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (model.ContactID == 0)
            {
                Logger.LogError("Input Parameter ContactID is null");
                validationMessages.Add("Contact ID is 0.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                commonResponse.StatusMessage = "Contact ID is 0.";
                return commonResponse;
            }
            try
            {
                Contact contact = this._contactRepository.FirstOrDefault(a => a.ContactID == model.ContactID && a.IsActive == true);
                if (contact != null)
                {
                    contact.LastModifiedByUserID = contact.Users.UserID;
                    contact.LastModifiedDateTime = System.DateTime.Now;
                    contact.AccountStatusID = (long)AccountStatusEnumeration.Active;
                    if (contact.Users != null && contact.Users.UserID != 0)
                    {
                        contact.Users.LastModifiedByUserID = contact.Users.UserID;
                        contact.Users.LastModifiedDateTime = DateTime.Now;
                        contact.Users.IsLockedOut = false;
                        contact.Users.FailedPasswordAttemptCount = 0;
                    }
                    this._contactRepository.SaveOrUpdateContact(contact, contact.Users.UserID);
                    validationMessages.Add("User account has been activated successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "User account has been activated successfully.";
                    commonResponse.ID = contact.ContactID;
                }

                else
                {
                    validationMessages.Add("Invalid request.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    commonResponse.StatusMessage = "Invalid request.";

                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> UnblockBusinessContact " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> UnblockBusinessContact ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> UnblockBusinessContact " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> UnblockBusinessContact ", null);
                commonResponse = new CommonResponse(
                ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            return commonResponse;
        }
        public CommonResponse SaveNotificationMode(NotificationTypeRequest NotificationTypeRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;

            if (NotificationTypeRequest == null)
            {
                Logger.LogError("Input Parameter is null");
                validationMessages.Add("Input Parameter is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);

            }
            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter UserSession is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);

            }
            try
            {
                if (NotificationTypeRequest.NotificationModeTypeID > 0)
                {
                    var notificationModesType = this._notificationModeRepository.GetNotificationModeByUser(NotificationTypeRequest.UserID);

                    notificationModesType.NotificationModesID = NotificationTypeRequest.NotificationMode;
                    notificationModesType.LastModifiedByUserID = NotificationTypeRequest.UserID;
                    notificationModesType.LastModifiedDateTime = DateTime.Now;
                    this._notificationModeRepository.SaveOrUpdateNotificationType(notificationModesType, NotificationTypeRequest.UserID);
                    validationMessages.Add("Notification type updated successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.ID = notificationModesType.ID;
                    commonResponse.StatusMessage = "Notification type updated successfully.";
                    return commonResponse;

                }
                else
                {
                    NotificationModesType notificationMode = new NotificationModesType();
                    notificationMode.NotificationModesID = NotificationTypeRequest.NotificationMode;
                    notificationMode.UserID = NotificationTypeRequest.UserID;
                    notificationMode.CreatedByUserID = NotificationTypeRequest.UserID;
                    notificationMode.CreatedDateTime = DateTime.Now;

                    this._notificationModeRepository.SaveOrUpdateNotificationType(notificationMode, NotificationTypeRequest.UserID);
                    validationMessages.Add("Notification type added successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.ID = notificationMode.ID;
                    commonResponse.StatusMessage = "Notification type added successfully.";
                    return commonResponse;
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> SaveNotificationMode " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveNotificationMode ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> SaveNotificationMode " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveNotificationMode", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public NotificationTypeResponse GetNotificationModeByUser(long UserId)
        {
            NotificationTypeResponse notificationModeByUser = new NotificationTypeResponse();
            int totalRecordCount = 0;
            try
            {

                var notificationModesType = this._notificationModeRepository.GetNotificationModeByUser(UserId);
                if (notificationModesType != null)
                {

                    notificationModeByUser.NotificationModeTypeID = notificationModesType.ID;
                    notificationModeByUser.ID = notificationModesType.ID;
                    notificationModeByUser.IsSuccess = true;
                    notificationModeByUser.IsNotificationModelType = true;
                    notificationModeByUser.NotificationModeTypeText = notificationModesType.NotificationMode.ModeType;
                    notificationModeByUser.NotificationID = notificationModesType.NotificationModesID;

                }
                else
                {
                    notificationModeByUser.IsSuccess = true;
                    notificationModeByUser.IsNotificationModelType = true;
                    notificationModeByUser.NotificationModeTypeText = "Email";
                    notificationModeByUser.NotificationID = 1;
                }

                List<NotificationTypeEntity> listOfNotificationType = null;

                listOfNotificationType = this._notificationModeImplRepository.GetAll().Where(x => x.IsActive == true)
                 .Select(x => new NotificationTypeEntity
                 {
                     NotificationID = x.ID,
                     NotificationTypeName = string.IsNullOrEmpty(x.ModeType) ? "" : x.ModeType,
                     IsActive = x.IsActive
                 })
                 .ToList();

                totalRecordCount = listOfNotificationType.Count();

                if (totalRecordCount < 1)
                {
                    notificationModeByUser.IsSuccess = false;
                }
                else
                {
                    notificationModeByUser.NotificationTypeEntity = listOfNotificationType; 
                    notificationModeByUser.IsSuccess = true;
                }
                notificationModeByUser.IsSuccess = true;

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in Contact-> GetNotificationModeByUser", null);
                notificationModeByUser.IsSuccess = false;
                notificationModeByUser.Message = "Unable to retrieve notification mode.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in Contact-> GetNotificationModeByUser", null);
                notificationModeByUser.IsSuccess = false;
                notificationModeByUser.Message = "Unable to retrieve Unable to retrieve notification mode.";
            }
            return notificationModeByUser;
        }
        //public List<ProgramInvitationContactRole> GetProgramInvitationContactRoles(long contactID, long roleID)
        //{
        //    var res = 
        //}

        public ContactListResponse ExportAllBusinessContacts(ExportBusinessContactRequest request)
        { 
            var response = this.GetAllBusinessContacts(0);
            if (response == null)
            {
                return response;
            }
            if (request != null && request.FilterParameters != null && request.FilterParameters.Count() > 0)
            {
                string searchBy = request.FilterParameters.FirstOrDefault().Key;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    searchBy = searchBy.Trim().ToLower();
                    var searchValue = request.FilterParameters.FirstOrDefault().Value;
                    if (searchBy == "businessname")
                    {
                        response.ContactPageResultEntity.DataList = response.ContactPageResultEntity.DataList.Where(x => x.BusinessName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "firstname")
                    {
                        response.ContactPageResultEntity.DataList = response.ContactPageResultEntity.DataList.Where(x => x.FirstName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "lastname")
                    {
                        response.ContactPageResultEntity.DataList = response.ContactPageResultEntity.DataList.Where(x => x.LastName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "phonenumber")
                    {
                        response.ContactPageResultEntity.DataList = response.ContactPageResultEntity.DataList.Where(x => x.PhoneNumber.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "accountstatus")
                    {
                        response.ContactPageResultEntity.DataList = response.ContactPageResultEntity.DataList.Where(x => x.AccountStatus.Contains(searchValue)).ToList();
                    }                    
                    else if (searchBy == "all")
                    {
                        response.ContactPageResultEntity.DataList = response.ContactPageResultEntity.DataList.Where(x => x.BusinessName.Contains(searchValue)
                        || x.FirstName.Contains(searchValue) || x.LastName.Contains(searchValue) || x.PhoneNumber.Contains(searchValue)
                        || x.AccountStatus.Contains(searchValue)).ToList();
                    }
                }

            }

            return response;
        }
            #endregion Methods

    }
}