using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Constants;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Service.Interfaces;
using FluentValidation;
using ThoughtFocus.Common.Exceptions;
using System.Linq;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Validations.InputParameterValidation.Admin;
using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.DataAccess.Models;
using Microsoft.Extensions.Options;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Repository.Interfaces.ContactManagement;
using ThoughtFocus.Domain.Enumeration;
using System.Globalization;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.Common.WorkFlowRepository.Interface;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Domain.AffiliateContact;
using ThoughtFocus.DataAccess.Models.Master;
using System.IO;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Workflow;
using ThoughtFocus.Common.Workflow.Core.PersistanceModel;
using ThoughtFocus.Domain.Request;
using System.Diagnostics;

namespace ThoughtFocus.Service.Impl
{
    public class AdminService : IAdminService
    {
        #region Fields
        private readonly ILogger<AdminService> Logger;
        private readonly IMapper _mapper;
        private IBusinessEntityRepository _businessEntityRepository;
        private INotificationService _notificationService;
        public IProgramInvitationRepository _programInvitationRepository { get; }
        private readonly AppSettings _appSettings;
        private IFundingSourceRepository _fundingSourceRepository;
        private IContactRepository _contactRepository;
        public IBusinessContactRepository _businessContactRepository { get; }
        private IContactInvitationInfoRepository _contactInvitationInfoRepository;
        public IProgramInviteeRepository _programInviteeRepository { get; }
        public IWorkflowProcessTransitionHistoryRepository _workflowProcessTransitionHistoryRepository { get; }
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        private IUrbanLeagueAffiliateRepository _affiliateContactsRepository;
        public readonly IFundUtilizationRepository _fundUtilizationRepository;
        private readonly IQuestionsRepository _questionRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IAgreementRepository _agreementRepository;
        private readonly ICiviCRMDataExportLogRepository _civiCRMDataExportLogRepository;
        private readonly IBusinessOwnerMasterRepository _businessOwnerMasterRepository;
        private readonly ILoanBusinessDetailMasterRepository _loanBusinessDetailMasterRepository;
        private readonly IBusinessOwnerRepository _businessOwnerRepository;
        private readonly ILoanBusinessDetailRepository _loanBusinessDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMasterService _masterService;
        private readonly IFundingEntityRepository _fundingEntityRepository;
        #endregion Fields

        #region Constructors

        public AdminService(
         ILogger<AdminService> logger,
         IMapper mapper, IBusinessEntityRepository businessEntityRepository, INotificationService notificationService,
         IProgramInvitationRepository programInvitationRepository, IOptions<AppSettings> appSettings,
         IFundingSourceRepository fundingSourceRepository, IContactRepository contactRepository, IBusinessContactRepository businessContactRepository,
         IContactInvitationInfoRepository contactInvitationInfoRepository, IProgramInviteeRepository programInviteeRepository,
         IWorkflowProcessTransitionHistoryRepository workflowProcessTransitionHistoryRepository,
         ILoanApplicationRepository loanApplicationRepository,
         IUrbanLeagueAffiliateRepository affiliateContactsRepository,
         IFundUtilizationRepository fundUtilizationRepository,
         IQuestionsRepository questionRepository,
         IDocumentTypeRepository documentTypeRepository,
         IAgreementRepository agreementRepository,
         ICiviCRMDataExportLogRepository civiCRMDataExportLogRepository,
         IBusinessOwnerMasterRepository BusinessOwnerMasterRepository,
         ILoanBusinessDetailMasterRepository loanBusinessDetailMasterRepository,
         IBusinessOwnerRepository businessOwnerRepository,
         ILoanBusinessDetailRepository loanBusinessDetailRepository,
         IUserRepository userRepository,
         IMasterService masterService,
         IFundingEntityRepository fundingEntityRepository
         )
        {
            this.Logger = logger;
            this._mapper = mapper;
            this._businessEntityRepository = businessEntityRepository;
            this._notificationService = notificationService;
            this._programInvitationRepository = programInvitationRepository;
            this._appSettings = appSettings.Value;
            this._fundingSourceRepository = fundingSourceRepository;
            this._contactRepository = contactRepository;
            this._businessContactRepository = businessContactRepository;
            this._contactInvitationInfoRepository = contactInvitationInfoRepository;
            this._programInviteeRepository = programInviteeRepository;
            this._workflowProcessTransitionHistoryRepository = workflowProcessTransitionHistoryRepository;
            this._LoanApplicationRepository = loanApplicationRepository;
            this._affiliateContactsRepository = affiliateContactsRepository;
            this._fundUtilizationRepository = fundUtilizationRepository;
            this._questionRepository = questionRepository;
            this._documentTypeRepository = documentTypeRepository;
            this._agreementRepository = agreementRepository;
            this._civiCRMDataExportLogRepository = civiCRMDataExportLogRepository;
            this._businessOwnerMasterRepository = BusinessOwnerMasterRepository;
            this._loanBusinessDetailMasterRepository = loanBusinessDetailMasterRepository;
            this._businessOwnerRepository = businessOwnerRepository;
            this._loanBusinessDetailRepository = loanBusinessDetailRepository;
            this._userRepository = userRepository;
            this._masterService = masterService;
            this._fundingEntityRepository = fundingEntityRepository;
        }

        #endregion Constructors

        #region Methods


        public BusinessViewEntityResponse AddBusinessEntity(BusinessEntityRequest saveBusinessEntity, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            BusinessViewEntityResponse businessViewEntityResponse = new BusinessViewEntityResponse();

            try
            {
                if (saveBusinessEntity == null)
                {
                    Logger.LogError("Business entity is null");
                    businessViewEntityResponse.IsSuccess = false;
                    businessViewEntityResponse.Message = "Input Parameter AddBusinessEntity is null";
                    return businessViewEntityResponse;
                }
                if (userSessionEntity == null)
                {
                    Logger.LogError("User session entity is null");
                    businessViewEntityResponse.IsSuccess = false;
                    businessViewEntityResponse.Message = "Input Parameter UserSessionEntity is null";
                    return businessViewEntityResponse;
                }
                else
                {
                    var business = this._businessEntityRepository.GetByEIN(saveBusinessEntity.EIN);
                    if (saveBusinessEntity.ID == 0 && business != null && business.ID > 0)
                    {
                        validationMessages.Add("This EIN already exists.");
                        businessViewEntityResponse.IsSuccess = false;
                        businessViewEntityResponse.ValidationErrors = validationMessages;
                        return businessViewEntityResponse;
                    }
                }
                var validator = new BusinessEntityRequestValidation();
                var modelValidationResults = validator.Validate(saveBusinessEntity, options => { options.IncludeRuleSets("mandatoryFields", "invalidInput"); });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    businessViewEntityResponse.ValidationErrors = validationMessages;
                    return businessViewEntityResponse;
                }

                BusinessEntity businessEntity = new BusinessEntity();
                if (saveBusinessEntity.ID > 0)
                {
                    var businessNameCheck = this._businessEntityRepository.GetAll().Where(x => x.EIN == saveBusinessEntity.EIN && x.ID != saveBusinessEntity.ID && x.IsActive == true).ToList();
                    if (businessNameCheck != null && businessNameCheck.Count > 0)
                    {
                        validationMessages.Add("This EIN already exists.");
                        businessViewEntityResponse.IsSuccess = false;
                        businessViewEntityResponse.ValidationErrors = validationMessages;
                        return businessViewEntityResponse;
                    }
                    businessEntity = this._businessEntityRepository.GetAll().Where(x => x.ID == saveBusinessEntity.ID).FirstOrDefault();
                    businessEntity.EIN = saveBusinessEntity.EIN;
                    businessEntity.AffiliateID = saveBusinessEntity.AffiliateID;
                    businessEntity.BusinessName = saveBusinessEntity.BusinessName;
                    businessEntity.LastModifiedByUserID = userSessionEntity.UserID;
                    businessEntity.LastModifiedDateTime = DateTime.Now;

                }
                else
                {
                    businessEntity = this._mapper.Map<BusinessEntity>(saveBusinessEntity);
                    businessEntity.CreatedByUserID = userSessionEntity.UserID;
                    businessEntity.CreatedDateTime = DateTime.Now;
                    businessEntity.LastModifiedByUserID = userSessionEntity.UserID;
                    businessEntity.LastModifiedDateTime = DateTime.Now;
                    businessEntity.IsActive = true;
                    businessEntity.BusinessTypeID = null;
                }

                this._businessEntityRepository.SaveOrUpdateBusinessEntity(businessEntity, userSessionEntity.UserID);
                businessViewEntityResponse.ID = businessEntity.ID;
                businessViewEntityResponse.IsSuccess = true;
                validationMessages.Add("Business Entity added successfully.");
                businessViewEntityResponse.ValidationErrors = validationMessages;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> AddBusinessEntity", null);
                businessViewEntityResponse.IsSuccess = false;
                Logger.LogDebug("Error at BusinessEntity Service -> AddBusinessEntity " + ex.InnerException == null
                ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> AddBusinessEntity", null);
                businessViewEntityResponse.IsSuccess = false;
                Logger.LogDebug("Error at BusinessEntity Service -> AddBusinessEntity " + ex.InnerException == null
              ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
            }
            return businessViewEntityResponse;
        }

        public BusinessViewEntityResponse GetBusinessEntity(long businessEntityID)
        {
            BusinessViewEntityResponse businessViewEntityResponse = new BusinessViewEntityResponse();
            businessViewEntityResponse.businessViewEntity = new BusinessViewEntity();


            if (businessEntityID == 0)
            {
                Logger.LogError("contactID is 0");
                businessViewEntityResponse.IsSuccess = false;
                businessViewEntityResponse.Message = "Contact doesn't exist.";
            }
            try
            {
                BusinessEntity businessEntity = this._businessEntityRepository.GetBusinessEntity(businessEntityID);
                if (businessEntity == null)
                {
                    businessViewEntityResponse.IsSuccess = false;
                    businessViewEntityResponse.Message = "Source doesnot exist.";
                    return businessViewEntityResponse;
                }
                BusinessViewEntity businessViewEntity = this._mapper.Map<BusinessViewEntity>(businessEntity);

                businessViewEntityResponse.businessViewEntity = businessViewEntity;
                businessViewEntityResponse.IsSuccess = true;

                if (businessViewEntityResponse != null && businessViewEntityResponse.IsSuccess)
                {
                    businessViewEntityResponse.IsSuccess = true;
                }
                else
                {
                    businessViewEntityResponse.IsSuccess = false;
                    businessViewEntityResponse.Message = "Business Entity ID doesnot exist.";
                }

                businessViewEntityResponse.CanBeDeleted = true;
                long[] _programInvitationsID = this._programInvitationRepository.GetAll().Where(x => x.BusinessID == businessEntity.ID).Select(y => y.ProgramInvitationID).ToArray();
                if (_programInvitationsID != null && _programInvitationsID.Count() > 0)
                {
                    int applicationCount = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && _programInvitationsID.Contains(c.ProgramInvitationID)).Count();
                    if (applicationCount > 0)
                        businessViewEntityResponse.CanBeDeleted = false;
                }

                businessViewEntityResponse.IsPaymentSchedule = this._fundingSourceRepository.IsPaymentScheduleExist(businessEntityID);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetBusinessEntity", null);
                businessViewEntityResponse.IsSuccess = false;
                businessViewEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetBusinessEntity", null);
                businessViewEntityResponse.IsSuccess = false;
                businessViewEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return businessViewEntityResponse;
        }

        public BusinessEntityListResponse GetAllBusinessEntityInformation()
        {
            BusinessEntityListResponse businessEntityListResponse = new BusinessEntityListResponse();
            try
            {
                List<string> validationMessages = new List<string>();
                List<BusinessEntityListingView> listOfBusinessEntityViewEntity = null;

                listOfBusinessEntityViewEntity = this._businessEntityRepository.GetBusinessEntity().Where(c => c.IsActive == true).OrderBy(o => o.BusinessName)
                .Select(x => new BusinessEntityListingView
                {
                    ID = x.ID,
                    EIN = x.EIN,
                    BusinessName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.BusinessName),
                    AffiliateID = x.AffiliateID,
                    BusinessTypeID = x.BusinessTypeID != null ? x.BusinessTypeID.Value : 0,
                    AffiliateName = x.Affiliate != null ? x.Affiliate.AffiliateName : string.Empty,
                    BusinessTypeName = x.BusinessType != null ? x.BusinessType.Type : string.Empty,
                })
                .ToList();

                businessEntityListResponse.businessEntityListResponse = listOfBusinessEntityViewEntity;

                if (businessEntityListResponse.businessEntityListResponse != null)
                {
                    businessEntityListResponse.IsSuccess = true;
                    businessEntityListResponse.Message = "Business Entity returned  successfully.";
                }
                else
                {
                    businessEntityListResponse.IsSuccess = false;
                    businessEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in BusinessEntityService-> GetAllBusinessEntityInformation", null);
                businessEntityListResponse.IsSuccess = false;
                businessEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in BusinessEntityService-> GetAllBusinessEntityInformation", null);
                businessEntityListResponse.IsSuccess = false;
                businessEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return businessEntityListResponse;
        }

        public CommonResponse SaveProgramInvitation(ProgramInvitationRequest programInvitationRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (programInvitationRequest == null)
            {
                Logger.LogError("Input Parameter is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
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
                var validator = new ProgramInvitationValidation();
                var modelValidationResults = validator.Validate(programInvitationRequest, options =>
                {
                    options.IncludeRuleSets("mandatoryFields");
                });
                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonResponse;
                }

                ProgramInvitation programInvitation = null;

                //check if program invitation is available with pgmID 1
                programInvitation = _programInvitationRepository.GetAll().Where(x => x.BusinessID == programInvitationRequest.BusinessID && x.ProgramID == programInvitationRequest.ProgramID && x.IsActive == true && x.ProgramStatusID != 3).OrderByDescending(o => o.ProgramInvitationID).FirstOrDefault();
                if (programInvitation == null || programInvitation.ProgramInvitationID == 0)
                {
                    programInvitation = new ProgramInvitation();

                    programInvitation.ProgramStatusID = programInvitationRequest.ProgramStatusID;
                    programInvitation.BusinessID = programInvitationRequest.BusinessID;
                    programInvitation.ProgramID = programInvitationRequest.ProgramID;
                    programInvitation.CreatedByUserID = userSessionEntity.UserID;
                    programInvitation.CreatedDateTime = DateTime.Now;
                    programInvitation.LastModifiedByUserID = userSessionEntity.UserID;
                    programInvitation.LastModifiedDateTime = DateTime.Now;
                    programInvitation.IsActive = true;
                }

                ProgramInvitee programInvitee = new ProgramInvitee();
                programInvitee.ContactID = programInvitationRequest.ContactID;
                programInvitee.ProgramInvitationID = programInvitation.ProgramInvitationID;
                programInvitee.CreatedByUserID = userSessionEntity.UserID;
                programInvitee.CreatedDateTime = DateTime.Now;
                programInvitee.LastModifiedByUserID = userSessionEntity.UserID;
                programInvitee.LastModifiedDateTime = DateTime.Now;
                programInvitee.IsActive = true;
                programInvitation.ProgramInvitee.Add(programInvitee);

                _programInvitationRepository.SaveOrUpdateProgramInvitation(programInvitation, userSessionEntity.UserID);

                validationMessages.Add("Program invitation added successfully.");
                sendProgramInvitation(programInvitation.ProgramInvitationID, programInvitation.ProgramID, programInvitationRequest, userSessionEntity);
                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at AdminService -> SendProgramInvitation " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactService-> SendProgramInvitation ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at AdminService -> SendProgramInvitation " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactService-> SendProgramInvitation", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonResponse;
        }

        public ProgramInvitationResponse GetProgramInvitation(UserSessionEntity userSessionEntity)
        {
            ProgramInvitationResponse programInvitationResponse = new ProgramInvitationResponse();

            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter user session is null");
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve Contact as user session entity is null.";
            }

            try
            {

                var ContactId = userSessionEntity.ContactID;
                var programInvitationListingViews = new List<ProgramInvitationListingView>();
                ThoughtFocus.DataAccess.Models.Contact.Contact contact = this._contactRepository.GetContactsByID(ContactId);

                //For admin role 
                if (contact != null && contact.Users != null && contact.Users.UserRoles != null && contact.Users.UserRoles.Where(x => (x.RoleID == 1 || x.RoleID == 3 || x.RoleID == 5 || x.RoleID == 7 || x.RoleID == 8) && x.IsActive == true).Count() > 0)
                {
                   
                        var programInvitations = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true)
                           .Select(x => new ProgramInvitationListingView
                           {
                               ID = x.ProgramInvitationID,
                               ProgramID = x.ProgramID,
                               ProgramName = x.FundingSource != null ? x.FundingSource.ProgramName : string.Empty,
                               BusinessID = x.BusinessID,
                               BusinessName = x.BusinessEntity != null ? x.BusinessEntity.BusinessName : string.Empty,
                               ProgramStatus = x.ProgramStatus != null ? x.ProgramStatus.ProgramStatusName : string.Empty,
                               FundingEntityName = x.FundingSource != null ? x.FundingSource.FundingEntity != null ? x.FundingSource.FundingEntity.FundingEntityName : string.Empty : string.Empty,
                               DateInvited = string.Format("{0:MM/dd/yyyy}", x.CreatedDateTime),
                               AffiliateName = x.BusinessEntity != null ? x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty : string.Empty
                           }).OrderByDescending(x => x.ID)
                           .ToList();
                        programInvitationResponse.ProgramInvitations = programInvitations;
                        programInvitationResponse.IsSuccess = true;                   
                }
                else
                {
                    long[] businessIDs = this._businessContactRepository.GetBusinessUserByContactID(ContactId).Where(x => x.BusinessRoleID != 4)
                                    .Select(x => x.BusinessID)
                                    .ToArray();

                    if (businessIDs != null && businessIDs.Count() > 0)
                    {
                        var programInvitations = this._programInvitationRepository.GetProgramInvitationByBusinessID(businessIDs).Where(x => x.ProgramStatusID == 1 && x.IsActive == true)
                        .Select(x => new ProgramInvitationListingView
                        {
                            ID = x.ProgramInvitationID,
                            ProgramID = x.ProgramID,
                            ProgramName = x.FundingSource != null ? x.FundingSource.ProgramName : string.Empty,
                            BusinessID = x.BusinessID,
                            BusinessName = x.BusinessEntity != null ? x.BusinessEntity.BusinessName : string.Empty,
                            ProgramStatus = x.ProgramStatus != null ? x.ProgramStatus.ProgramStatusName : string.Empty,
                            FundingEntityName = x.FundingSource != null ? x.FundingSource.FundingEntity != null ? x.FundingSource.FundingEntity.FundingEntityName : string.Empty : string.Empty,
                            DateInvited = string.Format("{0:MM/dd/yyyy}", x.CreatedDateTime),
                            AffiliateName = x.BusinessEntity != null ? x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty : string.Empty
                        }).OrderByDescending(x => x.ID)
                        .ToList();
                        programInvitationResponse.ProgramInvitations = programInvitations;
                        programInvitationResponse.IsSuccess = true;
                    }
                }
                if (programInvitationResponse.ProgramInvitations != null && programInvitationResponse.ProgramInvitations.Count() > 0)
                {
                    if (contact.ProgramInvitationContactRoles != null && contact.ProgramInvitationContactRoles.Count() > 0)
                    {
                        var programInvitationContactRoles = contact.ProgramInvitationContactRoles.Where(pc => pc.IsActive == true).ToList();
                        if (programInvitationContactRoles != null && programInvitationContactRoles.Count() > 0)
                        {
                            if (programInvitationContactRoles.FirstOrDefault().ProgramID > 0)
                            {
                                foreach (var pr in programInvitationContactRoles)
                                {
                                    try
                                    {
                                        var programInvitationContactRole = programInvitationResponse.ProgramInvitations.FindAll(p => p.ProgramID == pr.ProgramID);

                                        if (programInvitationContactRole != null && programInvitationContactRole.Count > 0)
                                        {
                                            programInvitationListingViews.AddRange(programInvitationContactRole);
                                        }
                                    }
                                    catch
                                    {

                                    }

                                }
                                if (programInvitationListingViews != null && programInvitationListingViews.Count > 0)
                                {
                                    var piv = from pil in programInvitationListingViews
                                              orderby pil.ID descending
                                              select pil;
                                    programInvitationResponse.ProgramInvitations = piv.ToList();
                                }
                                else
                                {
                                    programInvitationResponse.ProgramInvitations = new List<ProgramInvitationListingView>();
                                }

                            }
                        }

                    }
                }

                if (programInvitationResponse.ProgramInvitations != null && programInvitationResponse.ProgramInvitations.Count() < 1)
                {
                    programInvitationResponse.IsSuccess = false;
                    programInvitationResponse.Message = "Unable to retrieve data.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramInvitation", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramInvitation", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programInvitationResponse;
        }

        public bool sendProgramInvitation(long ProgramInvitationID, long programID, ProgramInvitationRequest programInvitationRequest, UserSessionEntity userSessionEntity)
        {
            bool response = false;
            try
            {
                var callBackURL = "";
                var generalEmail = _appSettings.GeneralEmail;

                ThoughtFocus.DataAccess.Models.Contact.Contact contact = this._contactRepository.GetContactsByID(programInvitationRequest.ContactID);

                if (contact.Users.IsAccountActivated)
                {
                    callBackURL = "You may begin a NEW application at the following link: " + _appSettings.BaseUrl;

                    //sending invitation 
                    response = _notificationService.SendProgramInvitationEmail("Program Invitation Email", ProgramInvitationID, programID, callBackURL, programInvitationRequest.ContactID, userSessionEntity, generalEmail);
                }
                else
                {
                    var tokenID = CommonUtility.CreateUniqueID(contact.ContactID.ToString());
                    var activationUrl = _appSettings.BaseUrl + "/ActivateAccount/" + tokenID + "/" + contact.ContactID.ToString();

                    //callBackURL = "Please click on 'Activate Account' to activate the account. You will be directed to a page where you will be asked to set your password to gain access to the platform. <br /><br /> <tr> <td style='padding: 20px 0 20px 180px;'> <table class='buttonwrapper' border='0' cellspacing='0' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + activationUrl + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>Activate Account</a> </td> </tr> </table> </td> </tr> ";
                    callBackURL = "Please click on 'Activate Account' to activate the account. You will be directed to a page where you will be asked to set your password to gain access to the platform. <br /><br /> <p style='font-family: sans-serif; font-size: 14px; vertical-align: top;  border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + activationUrl + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>Activate Account</a> </p>";

                    //sending invitation 
                    response = _notificationService.SendProgramInvitationEmail("Program Invitation Email", ProgramInvitationID, programID, callBackURL, programInvitationRequest.ContactID, userSessionEntity, generalEmail);

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
                }

                return response;
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Contact Service -> sendInvitation " + ex.InnerException == null
            ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                return response;
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Contact Service -> sendInvitation " + ex.InnerException == null
           ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                return response;
            }
        }

        public ProgramInvitationPreRequiredDataResponse GetProgramInvitationPerRequiredData(UserSessionEntity userSessionEntity)
        {
            ProgramInvitationPreRequiredDataResponse preRequiredData = new ProgramInvitationPreRequiredDataResponse();
            try
            {
                preRequiredData.BusinessEntities = this._businessEntityRepository.GetBusinessEntity().Where(c => c.IsActive == true)
                    .Select(x => new BusinessEntities
                    {
                        ID = x.ID,
                        BusinessName = x.BusinessName,
                    }).ToList();

                Int64[] borrowerroles = new Int64[] { 2 };

                preRequiredData.Contacts = this._contactRepository.GetAll().
                    Where(c => c.IsActive == true && c.Users.UserRoles.Any(x => borrowerroles.Contains(x.RoleID)))
                    .Select(x => new ContactResponse
                    {
                        ContactID = x.ContactID,
                        FullName = string.Concat(x.FirstName != null ? x.FirstName + " " : "", x.MiddleName != null ? x.MiddleName + " " : "", x.LastName != null ? x.LastName : "").Replace("  ", " "),
                    }).ToList();

                preRequiredData.Programs = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true)
                    .Select(x => new ProgramResponse
                    {
                        ProgramId = x.FundingSourceID,
                        ProgramName = x.ProgramName
                    }).ToList();
                preRequiredData.IsAllSelect = true;
                //User user = this._userRepository.FirstOrDefault(a => a.IsActive == true && preRequiredData.Contacts.Contains(c => c.D));

                //var userrole = this._contactRepository.GetAll().
                //            Where(cr => cr.IsActive == true && cr.Users.UserRoles.Any(x=>x.UserID == userSessionEntity.UserID));
                var contacts = preRequiredData.Contacts.Select(c => c.ContactID).ToList();
                var programInvitationContactRoles = this._contactRepository.GetProgramInvitationContactRoles(userSessionEntity.ContactID);
                if (programInvitationContactRoles != null && programInvitationContactRoles.Count() > 0)
                {
                    if (programInvitationContactRoles.FirstOrDefault().ProgramID > 0)
                    {
                        var programList = from s in preRequiredData.Programs
                                          join st in programInvitationContactRoles
                                          on s.ProgramId equals st.ProgramID // key selector 
                                          select new ProgramResponse
                                          {
                                              ProgramId = s.ProgramId,
                                              ProgramName = s.ProgramName
                                          };
                        preRequiredData.Programs = programList.ToList();
                        preRequiredData.IsAllSelect = false;
                    }
                }
                preRequiredData.FundingEntities = this._fundingEntityRepository.GetAll().Where(c => c.IsActive == true)
                   .Select(x => new FundingEntitiesResponse
                   {
                       FundingEntityId = x.FundingEntityID,
                       FundingEntityName = x.FundingEntityName
                   }).OrderBy(x => x.FundingEntityName).ToList();
                preRequiredData.IsSuccess = true;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in BusinessEntityServiceImpl-> GetProgramInvitationPerRequiredData", null);
                preRequiredData.IsSuccess = false;
                preRequiredData.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in BusinessEntityServiceImpl-> GetProgramInvitationPerRequiredData", null);
                preRequiredData.IsSuccess = false;
                preRequiredData.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return preRequiredData;
        }

        public BusinessUserResponse GetBusinessUsers(long businessID)
        {
            BusinessUserResponse businessUserResponse = new BusinessUserResponse();

            try
            {
                List<BusinessUser> businessusers = this._businessContactRepository.GetBusinessContactsByID(businessID);
                if (businessusers != null && businessusers.Count > 0)
                {
                    businessUserResponse.Contacts = businessusers.Where(a => a.Contact.IsActive == true && a.BusinessRoleID != 4).Select(x => new ContactResponse
                    {
                        ContactID = x.ContactID,
                        FullName = string.Concat(x.Contact?.FirstName != null ? x.Contact?.FirstName + " " : "", x.Contact?.MiddleName != null ? x.Contact?.MiddleName + " " : "", x.Contact?.LastName != null ? x.Contact?.LastName : "").Replace("  ", " "),
                    }).ToList();
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in BusinessEntityServiceImpl-> GetBusinessUser", null);
                businessUserResponse.IsSuccess = false;
                businessUserResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in BusinessEntityServiceImpl-> GetBusinessUser", null);
                businessUserResponse.IsSuccess = false;
                businessUserResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return businessUserResponse;
        }

        public BusinessProgramInvitationResponse GetProgramInvitationByBusinessID(long businessID, UserSessionEntity userSessionEntity)
        {
            BusinessProgramInvitationResponse programInvitationResponse = new BusinessProgramInvitationResponse();

            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter user session is null");
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve Contact as user session entity is null.";
            }

            try
            {
                var programInvitations = this._programInviteeRepository.GetProgramInvitees().Where(x => x.ProgramInvitation.BusinessID == businessID && x.ProgramInvitation.IsActive == true)
                .Select(x => new BusinessProgramInvitationListingView
                {
                    ID = x.ProgramInvitationID,
                    ProgramID = x.ProgramInvitation != null ? x.ProgramInvitation.ProgramID : 0,
                    ProgramName = x.ProgramInvitation.FundingSource != null ? x.ProgramInvitation.FundingSource.ProgramName : string.Empty,
                    BusinessID = x.ProgramInvitation != null ? x.ProgramInvitation.BusinessID : 0,
                    BusinessName = x.ProgramInvitation.BusinessEntity != null ? x.ProgramInvitation.BusinessEntity.BusinessName : string.Empty,
                    ProgramStatus = x.ProgramInvitation.ProgramStatus != null ? x.ProgramInvitation.ProgramStatus.ProgramStatusName : string.Empty,
                    ContactID = x.ContactID,
                    FirstName = x.Contact != null ? x.Contact.FirstName : string.Empty,
                    LastName = x.Contact != null ? x.Contact.LastName : string.Empty,
                    NotifiedOn = x.CreatedDateTime.ToShortDateString()
                }).OrderByDescending(x => x.ID)
                .ToList();

                programInvitationResponse.BusinessProgramInvitations = programInvitations;
                programInvitationResponse.IsSuccess = true;

                if (programInvitationResponse.BusinessProgramInvitations != null && programInvitationResponse.BusinessProgramInvitations.Count() < 1)
                {
                    programInvitationResponse.IsSuccess = false;
                    programInvitationResponse.Message = "Unable to retrieve data.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramInvitation", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramInvitation", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programInvitationResponse;
        }

        public ConsolidatedReportDataResponse GetCumulativeReportData(CumulativeReportRequest cumulativeReportRequest)
        {
            ConsolidatedReportDataResponse programInvitationResponse = new ConsolidatedReportDataResponse();

            try
            {
                //if date request is null/empty the from date will be current month start and to date will be current date  
                DateTime _fromDate = DateTime.Now;
                DateTime _toDate = DateTime.Now;
                string fromDate = cumulativeReportRequest.FromDate;
                string toDate = cumulativeReportRequest.ToDate;

                if (fromDate == null || fromDate == string.Empty)
                    _fromDate = new DateTime(DateTime.Now.Year, 1, 1);

                else
                    _fromDate = DateTime.Parse(fromDate);

                if (toDate == null || toDate == string.Empty)
                    _toDate = DateTime.Now;
                else
                    _toDate = DateTime.Parse(toDate);

                _toDate = _toDate.AddDays(1);

                Dictionary<string, string> data = new Dictionary<string, string>();
                programInvitationResponse.CumulativeReportData = data;
                programInvitationResponse.IsSuccess = true;

                // // Entity: all, Program: all
                if ((cumulativeReportRequest.FundingEntityID == null || cumulativeReportRequest.FundingEntityID.Count() == 0 || cumulativeReportRequest.FundingEntityID.FirstOrDefault()==0) && (cumulativeReportRequest.ProgramID.FirstOrDefault() == 0))
                {
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        cumulativeReportRequest.ProgramID = programIds;
                    }
                }
                else if ((cumulativeReportRequest.ProgramID == null || cumulativeReportRequest.ProgramID.Count() == 0 || cumulativeReportRequest.ProgramID.FirstOrDefault() == 0) && cumulativeReportRequest.FundingEntityID != null && (cumulativeReportRequest.FundingEntityID.Count() > 0 || cumulativeReportRequest.FundingEntityID.FirstOrDefault()> 0))
                {
                    // Entity: selected, Program: all
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true && cumulativeReportRequest.FundingEntityID.Contains(c.FundingEntityID)).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        cumulativeReportRequest.ProgramID = programIds;
                    }
                }

                //Invitation Sent
                //long[] invitedContacts = null;
                var contactInvitationInfo = new List<ContactInvitationInfo>();
                if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                {
                    //long[] invitedProgram = this._programInviteeRepository.GetAll().Where(c => c.CreatedDateTime >= _fromDate
                    //          && c.CreatedDateTime <= _toDate && c.IsActive == true && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(x => x.ContactID).ToArray();

                    //invitedContacts = this._contactInvitationInfoRepository.GetAll().Where(c => c.ContactInvitedDateTime >= _fromDate
                    //            && c.ContactInvitedDateTime <= _toDate && c.Contact.IsActive == true
                    //            && c.Contact.Users.UserRoles.Any(x => x.RoleID == 2)
                    //            && invitedProgram.Contains(c.ContactID)).Select(x => x.ContactID).Distinct().ToArray();
                    var invitedProgram = this._programInviteeRepository.GetAll().Where(c => c.CreatedDateTime >= _fromDate
                          && c.CreatedDateTime <= _toDate && c.IsActive == true
                          && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).ToList();

                    contactInvitationInfo = this._contactInvitationInfoRepository.GetAll().Where(c => c.ContactInvitedDateTime >= _fromDate
                               && c.ContactInvitedDateTime <= _toDate && c.Contact.IsActive == true
                               && c.Contact.Users.UserRoles.Any(x => x.RoleID == 2)
                               && c.ContactID == c.Contact.ContactID).ToList();

                    var basicDetail = this._programInvitationRepository.GetAll().Where(c => c.IsActive == true && cumulativeReportRequest.ProgramID.Contains(c.ProgramID)).OrderBy(o => o.BusinessEntity.BusinessName)
                             .Select(x => new 
                             {
                                 BusinessID = x.BusinessEntity.ID,
                                 ProgramInvitationID = x.ProgramInvitationID,
                                 ProgramID = x.FundingSource == null ? 0 : x.FundingSource.FundingSourceID,
                                 FundingEntityID = x.FundingSource == null ? 0 : x.FundingSource.FundingEntityID,
                                 FundingEntityName = x.FundingSource == null ? string.Empty : x.FundingSource.FundingEntity.FundingEntityName,
                                 ProgramName = x.FundingSource == null ? string.Empty : x.FundingSource.ProgramName,
                                 FundingEIN = x.FundingSource == null ? string.Empty : x.FundingSource.FundingEntity.EIN,
                                 FundingTIN = x.FundingSource == null ? string.Empty : x.FundingSource.FundingEntity.TIN,
                                 BusinessName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.BusinessEntity.BusinessName),
                                 AffiliateName = x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty,

                             }).ToList();
                    // }
                    //if (invitedContacts != null && invitedContacts.Count() > 0)
                    //{

                    //    long[] InvitationSentCount = this._businessContactRepository.GetAll().Where(c => invitedContacts.Contains(c.ContactID)
                    //    && c.Contact.IsActive == true
                    //     && c.IsActive == true).Select(y => y.ContactID).Distinct().ToArray();

                    //    data.Add("Invitation Sent", InvitationSentCount.Count().ToString());
                    //}

                    var result = from invited in invitedProgram
                                 join Info in contactInvitationInfo
                                      on invited.ContactID equals Info.ContactID
                                 join basic in basicDetail on invited.ProgramInvitationID equals basic.ProgramInvitationID
                                 select new
                                 {
                                     invited.ContactID,
                                     basic.FundingEntityName,
                                     basic.ProgramName,
                                     basic.BusinessName,
                                     basic.AffiliateName,
                                     basic.BusinessID,
                                     basic.ProgramInvitationID,
                                     Info.InvitationEmailAddreess,
                                     FullName = String.Concat(Info.Contact.FirstName, " ", Info.Contact.MiddleName, " ", Info.Contact.LastName),
                                     PhoneNo = Info.Contact.PhoneNo,
                                     InvitationSentDateTime = Info.ContactInvitedDateTime.ToString("dd/MM/yyyy"),
                                 };
                    if (result != null && result?.Count() > 0)
                    {
                        var consolidatedInvitationDetails = result
                                                   .GroupBy(c => new
                                                   {
                                                       c.FundingEntityName,
                                                       c.ProgramName,
                                                       c.BusinessName,
                                                       c.AffiliateName,
                                                       c.InvitationEmailAddreess,
                                                       c.FullName,
                                                       c.PhoneNo,
                                                       c.ProgramInvitationID,
                                                   })
                                                    .Select(g => g.FirstOrDefault());

                        if (consolidatedInvitationDetails != null && consolidatedInvitationDetails.Count() > 0)
                        {
                            data.Add("Invitation Sent", consolidatedInvitationDetails.Count().ToString());
                        }
                        else
                        {
                            data.Add("Invitation Sent", "0");
                        }
                    }
                    
                    
                }
                else
                {
                    data.Add("Invitation Sent", "0");
                }


                //Active Account.    
                long[] businessContacts = new long[] { };

                if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                {
                    long[] programBusiness = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true && cumulativeReportRequest.ProgramID.Contains(x.ProgramID)).Select(x => x.BusinessID).ToArray();

                    businessContacts = this._businessContactRepository.GetAll().Where(c => c.Contact.IsActive == true && c.IsActive == true
                                   && programBusiness.Contains(c.BusinessID)).Select(x => x.ContactID).ToArray();
                }
                
                int ActiveAccountCount = this._contactRepository.GetAll().Where(c => c.Users.AccountActivationDate >= _fromDate
                                   && c.Users.AccountActivationDate <= _toDate && c.IsActive == true
                                               && c.Users.UserRoles.Any(x => x.RoleID == 2)
                                               && businessContacts.Contains(c.ContactID)).Count();


                if (ActiveAccountCount > 0)
                {
                    data.Add("Active Account", ActiveAccountCount.ToString());
                }
                else
                {
                    data.Add("Active Account", "0");
                }
                /**************Application Started******************************/

                int startedApplicationsCount = 0;

                long[] startedApplications = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && c.ToStateName == "Drafted").Select(x => x.ProcessInstanceID).ToArray();

                if (startedApplications != null && startedApplications.Count() > 0)
                {
                    if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                    {
                        startedApplicationsCount = this._LoanApplicationRepository.GetAll().Where(c => startedApplications.Contains(c.LoanApplicationID) && c.IsActive == true && c.ApplicationStatusID == 3 && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Count();
                    }

                    data.Add("Application Started", startedApplicationsCount.ToString());
                }
                else
                {
                    data.Add("Application Started", "0");
                }

                //Application Submitted

                int submittedApplicationsCount = 0;

                long[] submittedApplications = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && c.ToStateName == "Submitted").Select(x => x.ProcessInstanceID).ToArray();

                if (submittedApplications != null && submittedApplications.Count() > 0)
                {
                    if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                    {
                        submittedApplicationsCount = this._LoanApplicationRepository.GetAll().Where(c => submittedApplications.Contains(c.LoanApplicationID) && c.IsActive == true && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Count();
                    }
                    

                    data.Add("Application Submitted", submittedApplicationsCount.ToString());
                }
                else
                {
                    data.Add("Application Submitted", "0");
                }


                //////////Application Funded

                int fundedApplicationsCount = 0;

                long[] fundedApplications = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && (c.ToStateName == "AccountDisbursed" || c.ToStateName == "FinalDisbursed" || c.ToStateName.EndsWith(" Disbursement"))).Select(x => x.ProcessInstanceID).ToArray();

                if (fundedApplications != null && fundedApplications.Count() > 0)
                {
                    if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                    {
                        fundedApplicationsCount = this._LoanApplicationRepository.GetAll().Where(c => fundedApplications.Contains(c.LoanApplicationID) && c.IsActive == true && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Count();
                    }
                    

                    data.Add("Application Funded", fundedApplicationsCount.ToString());
                }
                else
                {
                    data.Add("Application Funded", "0");
                }

                //Fund Released
                decimal[] utilizations = new decimal[] { };
                var fundTransactions = this._fundUtilizationRepository.GetAll().ToList();

                if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                {
                    utilizations = fundTransactions.Where(t => t.TransactionDate >= _fromDate && t.TransactionDate < _toDate &&
                    t.IsActive == true && t.TransactionTypeID == 3 && cumulativeReportRequest.ProgramID.Contains(t.FundingSourceID)).Select(x => x.TransactionAmount).ToArray();
                }
                
                if (utilizations.Count() > 0)
                {
                    data.Add("Fund Released", "$ " + Decimal.Truncate(utilizations.Sum()).ToString("#,##0"));
                }
                else
                {
                    data.Add("Fund Released", "$ 0");
                }

                programInvitationResponse.Fromdate = _fromDate.ToString("MM-dd-yyyy");
                programInvitationResponse.Todate = _toDate.AddDays(-1).ToString("MM-dd-yyyy");
                programInvitationResponse.ProgramID = cumulativeReportRequest.ProgramID;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> ConsolidatedReportDataResponse", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> ConsolidatedReportDataResponse", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programInvitationResponse;
        }

        public AffiliateContactListResponse GetAllAffiliates()
        {
            AffiliateContactListResponse affiliatecontactListResponse = new AffiliateContactListResponse();
            affiliatecontactListResponse.AffiliateContactPageResultEntity = new PageResultEntity<AffiliateContactEntity>();
            int totalRecordCount = 0;

            try
            {
                List<AffiliateContactEntity> listOfAffiliateEntity = null;
                listOfAffiliateEntity = _affiliateContactsRepository.GetAll().Where(c => c.IsActive == true)
                    .Select(x => new AffiliateContactEntity
                    {
                        AffiliateName = x.AffiliateName == null ? "" : x.AffiliateName,
                        FirstName = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).FirstName,
                        LastName = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).LastName,
                        PhoneNumber = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).PhoneNumber,
                        EmailAddress = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).EmailAddress,
                        Active = x.IsActive == true ? "Active" : "Inactive",
                        AffiliateAddress = x.AffiliateAddress == null ? "" : x.AffiliateAddress,
                        AffiliateID = x.AffiliateID
                    }).ToList();

                totalRecordCount = listOfAffiliateEntity.Count();
                if (totalRecordCount < 1)
                {
                    affiliatecontactListResponse.IsSuccess = false;
                }
                else
                {
                    affiliatecontactListResponse.AffiliateContactPageResultEntity.DataList = listOfAffiliateEntity.OrderBy(x => x.AffiliateName).ToList();
                    affiliatecontactListResponse.AffiliateContactPageResultEntity.TotalRecordCount = totalRecordCount;
                    affiliatecontactListResponse.IsSuccess = true;
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllAffiliateContacts", null);
                affiliatecontactListResponse.IsSuccess = false;
                affiliatecontactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllAffiliateContacts", null);
                affiliatecontactListResponse.IsSuccess = false;
                affiliatecontactListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return affiliatecontactListResponse;
        }

        public AffiliateContactListResponse GetAffiliate(long? AffiliateID)
        {
            AffiliateContactListResponse affiliatecontactResponse = new AffiliateContactListResponse();
            affiliatecontactResponse.AffiliateContactPageResultEntity = new PageResultEntity<AffiliateContactEntity>();
            int totalRecordCount = 0;
            AffiliateID = AffiliateID == null ? 0 : AffiliateID;
            try
            {
                List<AffiliateContactEntity> listOfAffiliateEntity = null;

                listOfAffiliateEntity = _affiliateContactsRepository.GetAll().Where(c => c.IsActive == true && c.AffiliateID == AffiliateID)

                    .Select(x => new AffiliateContactEntity
                    {
                        AffiliateID = x.AffiliateID,
                        AffiliateName = x.AffiliateName == null ? "" : x.AffiliateName,
                        FirstName = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).FirstName,
                        LastName = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).LastName,
                        PhoneNumber = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).PhoneNumber,
                        EmailAddress = x.UrbanLeagueAffiliateContacts.Count() == 0 ? "" : x.UrbanLeagueAffiliateContacts.FirstOrDefault(a => a.IsActive == true).EmailAddress,
                        Active = x.IsActive == true ? "Active" : "Inactive",
                        AffiliateAddress = x.AffiliateAddress == null ? "" : x.AffiliateAddress,
                    }).ToList();

                totalRecordCount = listOfAffiliateEntity.Count();

                if (totalRecordCount < 1)
                {
                    affiliatecontactResponse.IsSuccess = false;
                }
                else
                {
                    affiliatecontactResponse.AffiliateContactPageResultEntity.DataList = listOfAffiliateEntity.OrderBy(x => x.AffiliateName).ToList();
                    affiliatecontactResponse.AffiliateContactPageResultEntity.TotalRecordCount = totalRecordCount;
                    affiliatecontactResponse.IsSuccess = true;
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllAffiliateContactById", null);
                affiliatecontactResponse.IsSuccess = false;
                affiliatecontactResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllAffiliateContactById", null);
                affiliatecontactResponse.IsSuccess = false;
                affiliatecontactResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return affiliatecontactResponse;
        }

        public CommonResponse SaveOrUpdateAffiliate(AffiliateContactRequest affiliateRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;

            if (affiliateRequest == null)
            {
                Logger.LogError("Input Parameter is null.");
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

                if (affiliateRequest.AffiliateID > 0)
                {
                    UrbanLeagueAffiliate UrbanLeagueAffiliate = this._affiliateContactsRepository.FirstOrDefault(a => a.AffiliateID == affiliateRequest.AffiliateID);
                    if (UrbanLeagueAffiliate != null)
                    {
                        var affiliateNameCheck = this._affiliateContactsRepository.GetAll().Where(x => x.AffiliateName == affiliateRequest.AffiliateName && x.AffiliateID != UrbanLeagueAffiliate.AffiliateID).ToList();
                        if (affiliateNameCheck != null && affiliateNameCheck.Count > 0)
                        {
                            validationMessages.Add("Affiliate name already exists.");
                            commonResponse = new CommonResponse(
                            ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                            commonResponse.StatusMessage = "Affiliate name already exists.";
                            commonResponse.ID = UrbanLeagueAffiliate.AffiliateID;
                            return commonResponse;
                        }
                        UrbanLeagueAffiliate.AffiliateID = affiliateRequest.AffiliateID;
                        UrbanLeagueAffiliate.AffiliateAddress = affiliateRequest.AffiliateAddress;
                        UrbanLeagueAffiliate.AffiliateName = affiliateRequest.AffiliateName;

                        if (UrbanLeagueAffiliate.UrbanLeagueAffiliateContacts.Count() == 0)
                        {
                            UrbanLeagueAffiliate.UrbanLeagueAffiliateContacts = new List<UrbanLeagueAffiliateContact>();
                            UrbanLeagueAffiliateContact affiliateContact = new UrbanLeagueAffiliateContact();
                            affiliateContact.AffiliateID = UrbanLeagueAffiliate.AffiliateID;
                            affiliateContact.FirstName = affiliateRequest.FirstName;
                            affiliateContact.LastName = affiliateRequest.LastName;
                            affiliateContact.PhoneNumber = affiliateRequest.PhoneNumber;
                            affiliateContact.EmailAddress = affiliateRequest.EmailAddress;
                            affiliateContact.CreatedByUserID = userSessionEntity.UserID;
                            affiliateContact.CreatedDateTime = System.DateTime.Now;
                            affiliateContact.LastModifiedByUserID = userSessionEntity.UserID;
                            affiliateContact.LastModifiedDateTime = System.DateTime.Now;
                            affiliateContact.IsActive = true;
                            UrbanLeagueAffiliate.UrbanLeagueAffiliateContacts.Add(affiliateContact);
                        }
                        else
                        {
                            foreach (var affiliateContact in UrbanLeagueAffiliate.UrbanLeagueAffiliateContacts)
                            {
                                affiliateContact.FirstName = affiliateRequest.FirstName;
                                affiliateContact.LastName = affiliateRequest.LastName;
                                affiliateContact.PhoneNumber = affiliateRequest.PhoneNumber;
                                affiliateContact.EmailAddress = affiliateRequest.EmailAddress;
                                affiliateContact.LastModifiedByUserID = userSessionEntity.UserID;
                                affiliateContact.LastModifiedDateTime = System.DateTime.Now;
                                affiliateContact.IsActive = true;
                            }
                        }

                        this._affiliateContactsRepository.SaveOrUpdateAffiliate(UrbanLeagueAffiliate, userSessionEntity.UserID);
                        validationMessages.Add("Affiliate updated successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.ID = UrbanLeagueAffiliate.AffiliateID;
                        commonResponse.StatusMessage = "Affiliate updated successfully.";
                    }
                }
                else
                {
                    var _affiliateNameCheck = this._affiliateContactsRepository.GetAffiliateName(affiliateRequest.AffiliateName);

                    if (affiliateRequest.AffiliateID == 0 && _affiliateNameCheck != null && _affiliateNameCheck.AffiliateID > 0)
                    {
                        validationMessages.Add("Affiliate name already exists.");
                        commonResponse = new CommonResponse(
                        ResponseStatus.Fail, MessageConstants.SaveFailure, validationMessages);
                        commonResponse.StatusMessage = "Affiliate name already exists.";
                        commonResponse.ID = _affiliateNameCheck.AffiliateID;
                        return commonResponse;
                    }

                    UrbanLeagueAffiliate UrbanLeagueAffiliate = new UrbanLeagueAffiliate();
                    UrbanLeagueAffiliate.AffiliateAddress = affiliateRequest.AffiliateAddress;
                    UrbanLeagueAffiliate.AffiliateName = affiliateRequest.AffiliateName;
                    UrbanLeagueAffiliate.IsActive = true;
                    UrbanLeagueAffiliate.DisplayOrder = 1;

                    UrbanLeagueAffiliateContact UrbanLeagueAffiliateContact = new UrbanLeagueAffiliateContact();
                    UrbanLeagueAffiliateContact.FirstName = affiliateRequest.FirstName;
                    UrbanLeagueAffiliateContact.LastName = affiliateRequest.LastName;
                    UrbanLeagueAffiliateContact.PhoneNumber = affiliateRequest.PhoneNumber;
                    UrbanLeagueAffiliateContact.EmailAddress = affiliateRequest.EmailAddress;
                    UrbanLeagueAffiliateContact.AffiliateID = UrbanLeagueAffiliate.AffiliateID;
                    UrbanLeagueAffiliateContact.IsActive = true;
                    UrbanLeagueAffiliateContact.CreatedDateTime = System.DateTime.Now;
                    UrbanLeagueAffiliateContact.CreatedByUserID = userSessionEntity.UserID;

                    UrbanLeagueAffiliate.UrbanLeagueAffiliateContacts = new List<UrbanLeagueAffiliateContact>();

                    UrbanLeagueAffiliate.UrbanLeagueAffiliateContacts.Add(UrbanLeagueAffiliateContact);

                    this._affiliateContactsRepository.SaveOrUpdateAffiliate(UrbanLeagueAffiliate, userSessionEntity.UserID);

                    validationMessages.Add("Affiliate added successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.ID = UrbanLeagueAffiliate.AffiliateID;
                    commonResponse.StatusMessage = "Affiliate added successfully.";
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateAffiliate " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateAffiliate ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateAffiliate " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateAffiliate", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public List<ConsolidatedReportExportDataResponse> GetCumulativeReportExcelData(CumulativeReportRequest cumulativeReportRequest)
        {
            List<ConsolidatedReportExportDataResponse> response = new List<ConsolidatedReportExportDataResponse>();

            try
            {
                DateTime _fromDate = DateTime.Now;
                DateTime _toDate = DateTime.Now;
                string fromDate = cumulativeReportRequest.FromDate;
                string toDate = cumulativeReportRequest.ToDate;
                if (fromDate == null || fromDate == string.Empty)
                    _fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                else
                    _fromDate = DateTime.Parse(fromDate);

                if (toDate == null || toDate == string.Empty)
                    _toDate = DateTime.Now;
                else
                    _toDate = DateTime.Parse(toDate);

                _toDate = _toDate.AddDays(1);
                // // Entity: all, Program: all
                if ((cumulativeReportRequest.FundingEntityID == null || cumulativeReportRequest.FundingEntityID.Count() == 0 || cumulativeReportRequest.FundingEntityID.FirstOrDefault() == 0) && (cumulativeReportRequest.ProgramID.FirstOrDefault() == 0))
                {
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        cumulativeReportRequest.ProgramID = programIds;
                    }
                }
                else if ((cumulativeReportRequest.ProgramID == null || cumulativeReportRequest.ProgramID.Count() == 0 || cumulativeReportRequest.ProgramID.FirstOrDefault() == 0) && cumulativeReportRequest.FundingEntityID != null && (cumulativeReportRequest.FundingEntityID.Count() > 0 || cumulativeReportRequest.FundingEntityID.FirstOrDefault() > 0))
                {
                    // Entity: selected, Program: all
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true && cumulativeReportRequest.FundingEntityID.Contains(c.FundingEntityID)).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        cumulativeReportRequest.ProgramID = programIds;
                    }
                }
                //get all the businesses 
                if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                {
                    response = this._programInvitationRepository.GetAll().Where(c => c.IsActive == true && cumulativeReportRequest.ProgramID.Contains(c.ProgramID)).OrderBy(o => o.BusinessEntity.BusinessName)
                             .Select(x => new ConsolidatedReportExportDataResponse
                             {
                                 BusinessID = x.BusinessEntity.ID,
                                 ProgramInvitationID = x.ProgramInvitationID,
                                 ProgramName = x.FundingSource == null ? "" : x.FundingSource.ProgramName,
                                 BusinessName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.BusinessEntity.BusinessName),
                                 AffiliateName = x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty,
                             }).Distinct().ToList();
                }
                

                long[] submittedApplications = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && c.ToStateName == "Submitted").Select(x => x.ProcessInstanceID).ToArray();
                long[] fundedApplications = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && (c.ToStateName == "AccountDisbursed" || c.ToStateName == "FinalDisbursed" || c.ToStateName.EndsWith(" Disbursement"))).Select(x => x.ProcessInstanceID).ToArray();
                long[] startedApplications = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && c.ToStateName == "Drafted").Select(x => x.ProcessInstanceID).ToArray();

                foreach (var data in response)
                {
                    if (submittedApplications != null && submittedApplications.Count() > 0)
                    {
                        if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                        {
                            data.ApplicationSubmitted = this._LoanApplicationRepository.GetAll().Where(c => submittedApplications.Contains(c.LoanApplicationID) && c.IsActive == true
                                      && c.ProgramInvitationID == data.ProgramInvitationID && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Count();
                        }
                        
                    }
                    if (fundedApplications != null && fundedApplications.Count() > 0)
                    {
                        if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                        {
                            data.ApplicationFunded = this._LoanApplicationRepository.GetAll().Where(c => fundedApplications.Contains(c.LoanApplicationID)
                             && c.IsActive == true && c.ProgramInvitationID == data.ProgramInvitationID && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Count();

                            long[] applicationID = this._LoanApplicationRepository.GetAll().Where(c => fundedApplications.Contains(c.LoanApplicationID) && c.IsActive == true &&
                             c.ProgramInvitationID == data.ProgramInvitationID && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(s => s.LoanApplicationID).ToArray();

                            foreach (var id in applicationID)
                            {
                                data.Fundreleased = data.Fundreleased + (this._fundUtilizationRepository.GetAll().Where(x => x.ApplicationID == id && cumulativeReportRequest.ProgramID.Contains(x.FundingSourceID)).Select(y => y.TransactionAmount)).Sum();
                            }
                        }
                        

                    }
                    //started Applications
                    if (startedApplications != null && startedApplications.Count() > 0)
                    {
                        if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                        {
                            data.ApplicationStarted = this._LoanApplicationRepository.GetAll().Where(c => startedApplications.Contains(c.LoanApplicationID) && c.IsActive == true
                                     && c.ProgramInvitationID == data.ProgramInvitationID && c.ApplicationStatusID ==3 && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Count();
                        }
                        
                    }
                    //Active Account.    
                    //long[] businessContacts = null;

                    //if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                    //{
                    //    long[] programBusiness = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == data.ProgramInvitationID
                    //    && cumulativeReportRequest.ProgramID.Contains(x.ProgramID)).Select(x => x.BusinessID).ToArray();

                    //    businessContacts = this._businessContactRepository.GetAll().Where(c => c.Contact.IsActive == true && c.IsActive == true
                    //                   && programBusiness.Contains(c.BusinessID)).Select(x => x.ContactID).ToArray();
                    //}


                    //int activeAccountCount = this._contactRepository.GetAll().Where(c => c.Users.AccountActivationDate >= _fromDate
                    //                   && c.Users.AccountActivationDate <= _toDate && c.IsActive == true
                    //                               && c.Users.UserRoles.Any(x => x.RoleID == 2)
                    //                               && businessContacts.Contains(c.ContactID)).Count();
                    //data.ActivatedAccounts = activeAccountCount;
                    data.ActivatedAccounts = 0;
                    //Invitation Sent
                    long[] invitedContacts = null;
                    if (cumulativeReportRequest.ProgramID != null && cumulativeReportRequest.ProgramID.Count > 0)
                    {
                        long[] invitedProgram = this._programInviteeRepository.GetAll().Where(c => c.CreatedDateTime >= _fromDate
                                  && c.CreatedDateTime <= _toDate && c.IsActive == true
                                  && c.ProgramInvitationID == data.ProgramInvitationID && cumulativeReportRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(x => x.ContactID).ToArray();

                        invitedContacts = this._contactInvitationInfoRepository.GetAll().Where(c => c.ContactInvitedDateTime >= _fromDate
                                    && c.ContactInvitedDateTime <= _toDate && c.Contact.IsActive == true
                                    && c.Contact.Users.UserRoles.Any(x => x.RoleID == 2)
                                    && invitedProgram.Contains(c.ContactID)).Select(x => x.ContactID).Distinct().ToArray();
                    }
                    

                    if (invitedContacts != null && invitedContacts.Count() > 0)
                    {

                        int invitationSentCount = this._businessContactRepository.GetAll().Where(c => invitedContacts.Contains(c.ContactID)
                        && c.Contact.IsActive == true
                         && c.IsActive == true).Select(y => y.ContactID).Distinct().Count();

                        data.ApplicationInvited = invitationSentCount;
                    }
                }

                response.RemoveAll(data => data.ApplicationSubmitted == 0 && data.ApplicationFunded == 0 && data.Fundreleased == 0 && data.ApplicationStarted == 0 && data.ApplicationInvited == 0 && data.ActivatedAccounts == 0);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetCumulativeReportExcelData", null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetCumulativeReportExcelData", null);
            }
            return response;
        }

        public CommonResponse DeleteBusinessEntity(long businessEntityID, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;

            if (businessEntityID == 0)
            {
                Logger.LogError("business id is 0");
                validationMessages.Add("Unable to delete Business due to invalid request.");
                commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }

            try
            {
                long[] _programInvitationsID = this._programInvitationRepository.GetAll().Where(x => x.BusinessID == businessEntityID).Select(y => y.ProgramInvitationID).ToArray();
                if (_programInvitationsID != null && _programInvitationsID.Count() > 0)
                {
                    int applicationCount = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && _programInvitationsID.Contains(c.ProgramInvitationID)).Count();
                    if (applicationCount > 0)
                    {
                        validationMessages.Add("Business entity cannot be deleted as this business is associated with NUL Application.");
                        commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                        return commonResponse;
                    }
                }

                BusinessEntity businessEntity = this._businessEntityRepository.GetBusinessEntity(businessEntityID);
                if (businessEntity == null)
                {
                    validationMessages.Add("Unable to delete business as Business Entity is unavailable.");
                    commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonResponse;
                }

                //In-Activate program invitations 
                List<ProgramInvitation> ProgramInvitations = this._programInvitationRepository.GetAll().Where(x => x.BusinessID == businessEntityID).ToList();
                foreach (var item in ProgramInvitations)
                {
                    item.IsActive = false;
                    item.LastModifiedByUserID = userSessionEntity.UserID;
                    item.LastModifiedDateTime = DateTime.Now;
                    this._programInvitationRepository.SaveOrUpdateProgramInvitation(item, userSessionEntity.UserID);
                }

                //In-Activate businessusers
                List<BusinessUser> businessusers = this._businessContactRepository.GetBusinessContactsByID(businessEntityID);
                foreach (var item in businessusers)
                {
                    item.IsActive = false;
                    item.LastModifiedByUserID = userSessionEntity.UserID;
                    item.LastModifiedDateTime = DateTime.Now;
                    this._businessContactRepository.SaveOrUpdateBusinessUser(item, userSessionEntity.UserID);
                }

                businessEntity.IsActive = false;
                businessEntity.LastModifiedByUserID = userSessionEntity.UserID;
                businessEntity.LastModifiedDateTime = DateTime.Now;
                this._businessEntityRepository.SaveOrUpdateBusinessEntity(businessEntity, userSessionEntity.UserID);

                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Deleted, validationMessages);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> DeleteBusinessEntity", null);

                validationMessages.Add("Unable to delete Business Entity.");
                commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> DeleteBusinessEntity", null);
                validationMessages.Add("Unable to delete Business Entity.");
                commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
            }
            return commonResponse;
        }

        public QuestionsResponse GetAllQuestions()
        {
            QuestionsResponse questionsListResponse = new QuestionsResponse();
            questionsListResponse.QuestionsPageResultEntity = new PageResultEntity<QuestionsViewEntity>();
            List<QuestionsViewEntity> listOfQuestions = null;
            int totalRecordCount = 0;

            try
            {
                listOfQuestions = this._questionRepository.GetAll().Where(x => x.IsActive == true && x.ResponseTypeID == 1)
                 .Select(x => new QuestionsViewEntity
                 {
                     QuestionText = string.IsNullOrEmpty(x.QuestionText) ? "" : x.QuestionText,
                     ResponseTypeID = x.ResponseTypeID,
                     IsActive = x.IsActive,
                     QuestionID = x.QuestionID
                 }).ToList();

                totalRecordCount = listOfQuestions.Count();

                if (totalRecordCount < 1)
                {
                    questionsListResponse.IsSuccess = false;
                }
                else
                {
                    questionsListResponse.QuestionsPageResultEntity.DataList = listOfQuestions.ToList();
                    questionsListResponse.QuestionsPageResultEntity.TotalRecordCount = totalRecordCount;
                    questionsListResponse.IsSuccess = true;
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllQuestions", null);
                questionsListResponse.IsSuccess = false;
                questionsListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllQuestions", null);
                questionsListResponse.IsSuccess = false;
                questionsListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return questionsListResponse;
        }

        public QuestionsResponse GetQuestion(long? questionID)
        {
            QuestionsResponse questionsListResponse = new QuestionsResponse();
            questionsListResponse.QuestionsPageResultEntity = new PageResultEntity<QuestionsViewEntity>();
            int totalRecordCount = 0;
            questionID = questionID == null ? 0 : questionID;
            try
            {
                List<QuestionsViewEntity> listOfQuestions = null;

                listOfQuestions = this._questionRepository.GetAll().Where(x => x.IsActive == true && x.QuestionID == questionID)
                 .Select(x => new QuestionsViewEntity
                 {
                     QuestionText = string.IsNullOrEmpty(x.QuestionText) ? "" : x.QuestionText,
                     ResponseTypeID = x.ResponseTypeID,
                     IsActive = x.IsActive,
                     QuestionID = x.QuestionID
                 })
                 .ToList();

                totalRecordCount = listOfQuestions.Count();

                if (totalRecordCount < 1)
                {
                    questionsListResponse.IsSuccess = false;
                }
                else
                {
                    questionsListResponse.QuestionsPageResultEntity.DataList = listOfQuestions.ToList();
                    questionsListResponse.QuestionsPageResultEntity.TotalRecordCount = totalRecordCount;
                    questionsListResponse.IsSuccess = true;
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetQuestion", null);
                questionsListResponse.IsSuccess = false;
                questionsListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetQuestion", null);
                questionsListResponse.IsSuccess = false;
                questionsListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return questionsListResponse;
        }

        public CommonResponse SaveOrUpdateQuestions(QuestionsRequest questionsRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;

            if (questionsRequest == null)
            {
                Logger.LogError("Input Parameter is null.");
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

                if (questionsRequest.QuestionID > 0)
                {
                    Question Questions = this._questionRepository.FirstOrDefault(a => a.QuestionID == questionsRequest.QuestionID && a.IsActive == true);
                    if (Questions != null)
                    {
                        Questions.QuestionID = questionsRequest.QuestionID;
                        Questions.QuestionText = questionsRequest.QuestionText;
                        Questions.ResponseTypeID = 1;
                        Questions.IsActive = true;

                        this._questionRepository.SaveOrUpdateQuestion(Questions, userSessionEntity.UserID);
                        validationMessages.Add("Question updated successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.ID = Questions.QuestionID;
                        commonResponse.StatusMessage = "Question updated successfully.";
                        return commonResponse;
                    }
                }
                else
                {
                    Question Questions = new Question();
                    Questions.QuestionText = questionsRequest.QuestionText;
                    Questions.Version = 1;
                    Questions.ResponseTypeID = 1;
                    Questions.IsActive = true;
                    this._questionRepository.SaveOrUpdateQuestion(Questions, userSessionEntity.UserID);
                    validationMessages.Add("Question added successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.ID = Questions.QuestionID;
                    commonResponse.StatusMessage = "Question added successfully.";
                    return commonResponse;
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateQuestions " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateQuestions ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateQuestions " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateQuestions", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public DocumentsResponse GetAllDocumentTypes()
        {
            DocumentsResponse documentListResponse = new DocumentsResponse();
            documentListResponse.DocumentsPageResultEntity = new PageResultEntity<DocumentViewEntity>();
            int totalRecordCount = 0;

            try
            {
                List<DocumentViewEntity> listOfDocuments = null;
                listOfDocuments = this._documentTypeRepository.GetAll().Where(x => x.IsActive == true && x.DocumentCategoryID == 1 && x.DocumentTypeID != 6)
                 .Select(x => new DocumentViewEntity
                 {
                     DocumentTypeID = x.DocumentTypeID,
                     DocumentName = string.IsNullOrEmpty(x.Name) ? "" : x.Name,
                     IsActive = x.IsActive
                 })
                 .ToList();

                totalRecordCount = listOfDocuments.Count();

                if (totalRecordCount < 1)
                {
                    documentListResponse.IsSuccess = false;
                }
                else
                {
                    documentListResponse.DocumentsPageResultEntity.DataList = listOfDocuments.ToList();
                    documentListResponse.DocumentsPageResultEntity.TotalRecordCount = totalRecordCount;
                    documentListResponse.IsSuccess = true;
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllDocumentTypes", null);
                documentListResponse.IsSuccess = false;
                documentListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllDocumentTypes", null);
                documentListResponse.IsSuccess = false;
                documentListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return documentListResponse;
        }

        public DocumentsResponse GetDocument(long? documentTypeID)
        {
            DocumentsResponse documentListResponse = new DocumentsResponse();
            documentListResponse.DocumentsPageResultEntity = new PageResultEntity<DocumentViewEntity>();
            int totalRecordCount = 0;
            try
            {
                List<DocumentViewEntity> listOfQuestions = null;

                listOfQuestions = this._documentTypeRepository.GetAll().Where(x => x.IsActive == true && x.DocumentTypeID == documentTypeID)
                 .Select(x => new DocumentViewEntity
                 {
                     DocumentTypeID = x.DocumentTypeID,
                     DocumentName = string.IsNullOrEmpty(x.Name) ? "" : x.Name,
                     IsActive = x.IsActive
                 })
                 .ToList();

                totalRecordCount = listOfQuestions.Count();

                if (totalRecordCount < 1)
                {
                    documentListResponse.IsSuccess = false;
                }
                else
                {
                    documentListResponse.DocumentsPageResultEntity.DataList = listOfQuestions.ToList();
                    documentListResponse.DocumentsPageResultEntity.TotalRecordCount = totalRecordCount;
                    documentListResponse.IsSuccess = true;
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetDocument", null);
                documentListResponse.IsSuccess = false;
                documentListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetDocument", null);
                documentListResponse.IsSuccess = false;
                documentListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return documentListResponse;
        }

        public CommonResponse SaveOrUpdateDocuments(DocumentsRequest documentsRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;

            if (documentsRequest == null)
            {
                Logger.LogError("Input Parameter is null.");
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

                if (documentsRequest.DocumentTypeID > 0)
                {
                    DocumentType documentType = this._documentTypeRepository.FirstOrDefault(a => a.DocumentTypeID == documentsRequest.DocumentTypeID && a.IsActive == true);
                    if (documentType != null)
                    {
                        var documentNameCheck = this._documentTypeRepository.GetAll().Where(x => x.Name == documentsRequest.DocumentName && x.DocumentTypeID != documentType.DocumentTypeID && x.IsActive == true).ToList();
                        if (documentNameCheck != null && documentNameCheck.Count > 0)
                        {
                            validationMessages.Add("Document name already exists.");
                            commonResponse = new CommonResponse(
                            ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                            commonResponse.StatusMessage = "Document name already exists.";
                            commonResponse.ID = documentType.DocumentTypeID;
                            return commonResponse;
                        }
                        documentType.Name = documentsRequest.DocumentName;
                        documentType.Description = documentsRequest.DocumentName;
                        documentType.IsActive = true;

                        this._documentTypeRepository.SaveOrUpdateDocumentTypes(documentType, userSessionEntity.UserID);
                        validationMessages.Add("Document updated successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.ID = documentType.DocumentTypeID;
                        commonResponse.StatusMessage = "Document updated successfully.";
                        return commonResponse;
                    }
                }
                else
                {
                    var documentNameCheck = this._documentTypeRepository.GetDocumentName(documentsRequest.DocumentName);

                    if (documentNameCheck != null && documentsRequest.DocumentTypeID == 0 && documentNameCheck.DocumentTypeID > 0)
                    {
                        validationMessages.Add("Document name already exists.");
                        commonResponse = new CommonResponse(
                        ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                        commonResponse.StatusMessage = "Document name already exists.";
                        commonResponse.ID = documentNameCheck.DocumentTypeID;
                        return commonResponse;
                    }
                    DocumentType documentType = new DocumentType();
                    documentType.Name = documentsRequest.DocumentName;
                    documentType.Description = documentsRequest.DocumentName;
                    documentType.DocumentCategoryID = 1;
                    documentType.IsActive = true;
                    this._documentTypeRepository.SaveOrUpdateDocumentTypes(documentType, userSessionEntity.UserID);
                    validationMessages.Add("Document added successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.ID = documentType.DocumentTypeID;
                    commonResponse.StatusMessage = "Document added successfully.";
                    return commonResponse;
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateDocuments " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateDocuments ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateDocuments " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateDocuments", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public ProgramAgreementResponse ProgramWiseAgreement(long programID)
        {
            ProgramAgreementResponse programAgreementResponse = new ProgramAgreementResponse();

            try
            {
                if (programID != 0)
                {
                    var fundingSource = this._fundingSourceRepository.GetFundingSourceByID(programID);
                    if (fundingSource != null && fundingSource.AgreementID != null && fundingSource.AgreementID > 0)
                    {
                        programAgreementResponse.IsSuccess = true;
                        programAgreementResponse.AgreementName = string.IsNullOrEmpty(fundingSource.Agreement.AgreementName) ? "" : fundingSource.Agreement.AgreementName;
                        programAgreementResponse.AgreementID = fundingSource.AgreementID;
                        programAgreementResponse.ProgramIdID = fundingSource.FundingSourceID;
                        programAgreementResponse.AgreementBody = string.IsNullOrEmpty(fundingSource.Agreement.Body) ? "" : fundingSource.Agreement.Body;
                    }
                }
                else
                {
                    programAgreementResponse.IsSuccess = false;
                    programAgreementResponse.Message = "Couldn't find the Agreement for this Program.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> ProgramWiseAgreement", null);
                programAgreementResponse.IsSuccess = false;
                programAgreementResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> ProgramWiseAgreement", null);
                programAgreementResponse.IsSuccess = false;
                programAgreementResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programAgreementResponse;
        }

        public CommonResponse SaveOrUpdateAgreementName(AgreementRequest agreementRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;

            if (agreementRequest == null)
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
                if (agreementRequest.AgreementID > 0 && agreementRequest.ProgramID > 0)
                {
                    var fundingSource = this._fundingSourceRepository.GetFundingSourceByID(agreementRequest.ProgramID);
                    if (fundingSource == null)
                    {
                        Logger.LogError("Funding source/Program is not available");
                        validationMessages.Add("Funding source/Program is not available.");
                        commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    }

                    Agreement agreement = this._agreementRepository.FirstOrDefault(a => a.AgreementID == fundingSource.AgreementID);
                    if (agreement != null)
                    {
                        agreement.AgreementName = agreementRequest.AgreementName;
                        agreement.IsActive = true;
                        agreement.Body = agreementRequest.AgreementBody;

                        this._agreementRepository.SaveOrUpdateAgreement(agreement, userSessionEntity.UserID);
                        validationMessages.Add("Agreement updated successfully.");
                        commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonResponse.ID = agreement.AgreementID;
                        commonResponse.StatusMessage = "Agreement updated successfully.";
                        return commonResponse;
                    }
                    else
                    {
                        if (agreementRequest.AgreementID > 0)
                        {
                            agreement = this._agreementRepository.FirstOrDefault(a => a.AgreementID == agreementRequest.AgreementID);
                        }

                        if (agreement != null)
                        {
                            agreement.AgreementName = agreementRequest.AgreementName;
                            agreement.IsActive = true;
                            agreement.Body = agreementRequest.AgreementBody;
                            fundingSource.AgreementID = agreement.AgreementID;
                            fundingSource.LastModifiedByUserID = userSessionEntity.UserID;
                            fundingSource.LastModifiedDateTime = DateTime.Now;
                            fundingSource.LastModifiedByUserID = userSessionEntity.UserID;
                            fundingSource.Agreement = agreement;
                            this._fundingSourceRepository.SaveOrUpdateFundingSource(fundingSource, userSessionEntity.UserID);
                            validationMessages.Add("Agreement updated successfully.");
                            commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                            commonResponse.ID = agreement.AgreementID;
                            commonResponse.StatusMessage = "Agreement updated successfully.";
                            return commonResponse;
                        }
                        else
                        {
                            agreement = new Agreement();
                            agreement.AgreementName = agreementRequest.AgreementName;
                            agreement.Body = agreementRequest.AgreementBody;
                            agreement.IsActive = true;

                            if (fundingSource != null)
                            {
                                fundingSource.AgreementID = agreement.AgreementID;
                                fundingSource.LastModifiedByUserID = userSessionEntity.UserID;
                                fundingSource.LastModifiedDateTime = DateTime.Now;
                                fundingSource.LastModifiedByUserID = userSessionEntity.UserID;
                                fundingSource.Agreement = agreement;
                                this._fundingSourceRepository.SaveOrUpdateFundingSource(fundingSource, userSessionEntity.UserID);

                                validationMessages.Add("Agreement saved successfully.");
                                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                                commonResponse.ID = agreement.AgreementID;
                                commonResponse.StatusMessage = "Agreement saved successfully.";
                                return commonResponse;
                            }
                        }
                    }
                }
                else
                {
                    Agreement agreement = new Agreement();
                    agreement.AgreementName = agreementRequest.AgreementName;
                    agreement.Body = agreementRequest.AgreementBody;
                    agreement.IsActive = true;

                    var fundingSource = this._fundingSourceRepository.GetFundingSourceByID(agreementRequest.ProgramID);
                    if (fundingSource != null)
                    {
                        fundingSource.AgreementID = agreement.AgreementID;
                        fundingSource.LastModifiedByUserID = userSessionEntity.UserID;
                        fundingSource.LastModifiedDateTime = DateTime.Now;
                        fundingSource.LastModifiedByUserID = userSessionEntity.UserID;
                    }

                    if (agreement != null)
                    {
                        fundingSource.Agreement = agreement;
                    }

                    this._fundingSourceRepository.SaveOrUpdateFundingSource(fundingSource, userSessionEntity.UserID);

                    validationMessages.Add("Agreement saved successfully.");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.ID = agreement.AgreementID;
                    commonResponse.StatusMessage = "Agreement saved successfully.";
                    return commonResponse;
                }
            }
            catch (BusinessException ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateAgreementName " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateAgreementName ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("Error at Admin Service -> SaveOrUpdateAgreementName " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> SaveOrUpdateAgreementName", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.SaveFailure, null);
            }
            return commonResponse;
        }

        public List<CiviCRMOrganizationDataResponse> GetCiviCRMOrganizationExportData(UserSessionEntity userSessionEntity)
        {
            List<CiviCRMOrganizationDataResponse> civiCRMOrganizationDataResponse = new List<CiviCRMOrganizationDataResponse>();
            CiviCRMDataExportLogResponse civiCRMDataExportLogResponse = new CiviCRMDataExportLogResponse();
            int totalRecordCount = 0;

            try
            {
                civiCRMOrganizationDataResponse = this._businessEntityRepository.GetAll().Where(c => c.IsActive == true)
                .Select(x => new CiviCRMOrganizationDataResponse
                {
                    External_ID_OrdID = x.ID,
                    Organization_Name = x.BusinessName,
                }).ToList();

                if (civiCRMOrganizationDataResponse != null && civiCRMOrganizationDataResponse.Count > 0)
                {
                    foreach (var item in civiCRMOrganizationDataResponse)
                    {
                        var programInvitation = this._programInvitationRepository.GetAll().Where(x => x.BusinessID == item.External_ID_OrdID && x.ProgramStatusID == 2 && x.IsActive == true).OrderByDescending(x => x.ProgramInvitationID).ToList();
                        long recentProgramInvitationID = 0;
                        //Check if more than one program invitation is there.
                        if (programInvitation != null && programInvitation.Count > 1)
                        {
                            var programInvitationList = new Dictionary<long, DateTime>();
                            foreach (var invitation in programInvitation)
                            {
                                var loanApplication = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == invitation.ProgramInvitationID && x.ApplicationStatusID > 3).ToList();
                                if (loanApplication != null && loanApplication.Count > 0)
                                {
                                    programInvitationList.Add(invitation.ProgramInvitationID, loanApplication.FirstOrDefault().LastModifiedDateTime);
                                }
                            }

                            if (programInvitationList.Count > 0)
                            {
                                recentProgramInvitationID = programInvitationList.OrderByDescending(p => p.Value).Select(p => p.Key).FirstOrDefault();
                            }
                        }
                        else
                        {
                            recentProgramInvitationID = programInvitation.Select(y => y.ProgramInvitationID).FirstOrDefault();

                        }


                        if (item != null && recentProgramInvitationID > 0)
                        {
                            var loanApplication = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == recentProgramInvitationID && x.ApplicationStatusID > 3).FirstOrDefault();
                            var loanBusinessDetailMaster = _loanBusinessDetailMasterRepository.GetAll().Where(l => l.BusinessID == item.External_ID_OrdID).FirstOrDefault();
                            var businessEntityData = _businessEntityRepository.GetAll().Where(b => b.ID == item.External_ID_OrdID && b.IsActive == true).FirstOrDefault();

                            if (loanApplication != null && loanApplication.LoanApplicationID > 0)
                            {
                                item.Street_Address = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.Address : loanApplication.LoanBusinessDetail.Address;
                                item.City = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.City : loanApplication.LoanBusinessDetail.City;
                                item.State = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.State.StateName : loanApplication.LoanBusinessDetail.State.StateName;
                                item.Zip = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.Zip : loanApplication.LoanBusinessDetail.Zip;
                                item.Phone_Number = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.PhoneNumber : loanApplication.LoanBusinessDetail.PhoneNumber;
                                item.Email = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.EmailAddress : loanApplication.LoanBusinessDetail.EmailAddress;
                                //item.Affiliate = loanApplication.LoanBusinessDetail.Affiliate.AffiliateName;
                                //item.EIN_Number = loanApplication.LoanBusinessDetail.EIN;
                                item.Affiliate = businessEntityData?.Affiliate?.AffiliateName;
                                item.EIN_Number = businessEntityData?.EIN;
                                item.SIC_Code = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.SIC.Division.ToString() + loanBusinessDetailMaster?.SIC.Code.ToString() + loanBusinessDetailMaster?.SIC.IndustryTitle.ToString()
                                                : loanApplication.LoanBusinessDetail.SIC.Division.ToString() + loanApplication.LoanBusinessDetail?.SIC.Code.ToString() + loanApplication.LoanBusinessDetail?.SIC.IndustryTitle.ToString();
                                item.NAICS = loanBusinessDetailMaster != null ? loanBusinessDetailMaster?.NaicsCode : loanApplication.LoanBusinessDetail?.NaicsCode;
                                item.Business_Type = loanBusinessDetailMaster != null ? loanBusinessDetailMaster.BusinessType?.Description : loanApplication.LoanBusinessDetail.BusinessType?.Description;
                                item.Industry = loanBusinessDetailMaster != null ? loanBusinessDetailMaster?.IndustryType.Description : loanApplication.LoanBusinessDetail?.IndustryType.Description;

                                if (loanBusinessDetailMaster != null)
                                {
                                    item.Url = !string.IsNullOrEmpty(loanBusinessDetailMaster?.Url) ? "https://" + loanBusinessDetailMaster?.Url : string.Empty;
                                }
                                else
                                {
                                    item.Url = !string.IsNullOrEmpty(loanApplication.LoanBusinessDetail?.Url) ? "https://" + loanApplication.LoanBusinessDetail?.Url : string.Empty;
                                }
                                item.Contact_Type = "Grant Recipient";
                            }
                        }

                        var programInvitations = this._programInvitationRepository.GetAll().Where(x => x.BusinessID == item.External_ID_OrdID && x.IsActive == true && x.ProgramStatusID == 2).OrderBy(x => x.ProgramInvitationID).ToList();

                        if (programInvitations != null && programInvitations.Count() > 0)
                        {
                            int i = 1;
                            var fundTransactions = this._fundUtilizationRepository.GetAll().Where(c => c.IsActive == true).ToList();

                            foreach (var invitation in programInvitations)
                            {

                                var loanApplication = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && c.ProgramInvitationID == invitation.ProgramInvitationID && (c.ApplicationStatusID == 13 || c.ApplicationStatusID == 40)).FirstOrDefault();

                                if (loanApplication != null)
                                {
                                    decimal transactionAmount = fundTransactions.Where(t => t.IsActive == true && t.ApplicationID == loanApplication.LoanApplicationID && t.TransactionTypeID == 3).Select(x => x.TransactionAmount).FirstOrDefault();

                                    DateTime transactionDate = fundTransactions.Where(t => t.IsActive == true && t.ApplicationID == loanApplication.LoanApplicationID && t.TransactionTypeID == 3).Select(x => x.TransactionDate).FirstOrDefault();

                                    if (i == 1)
                                    {
                                        item.Grant1_Program = invitation.FundingSource.ProgramName;
                                        item.Grant1_ReceivedDate = transactionDate != null ? String.Format("{0:MM/dd/yyyy}", transactionDate) : string.Empty;
                                        item.Grant1_AmountFunded = transactionAmount > 0 ? Decimal.Truncate(transactionAmount) : 0;
                                    }

                                    if (i == 2)
                                    {
                                        item.Grant2_Program = invitation.FundingSource.ProgramName;
                                        item.Grant2_ReceivedDate = transactionDate != null ? String.Format("{0:MM/dd/yyyy}", transactionDate) : string.Empty;
                                        item.Grant2_AmountFunded = transactionAmount > 0 ? Decimal.Truncate(transactionAmount) : 0;
                                    }

                                    if (i == 3)
                                    {
                                        item.Grant3_Program = invitation.FundingSource.ProgramName;
                                        item.Grant3_ReceivedDate = transactionDate != null ? String.Format("{0:MM/dd/yyyy}", transactionDate) : string.Empty;
                                        item.Grant3_AmountFunded = transactionAmount > 0 ? Decimal.Truncate(transactionAmount) : 0;
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                }

                if (civiCRMOrganizationDataResponse != null && civiCRMOrganizationDataResponse.Count() > 0)
                {
                    List<CiviCRMOrganizationDataResponse> tobeRemovedList = civiCRMOrganizationDataResponse.Where(x => (x.EIN_Number == string.Empty || x.EIN_Number == null)).ToList();
                    if (tobeRemovedList != null && tobeRemovedList.Count > 0)
                    {
                        foreach (var item in tobeRemovedList)
                        {
                            civiCRMOrganizationDataResponse.Remove(item);
                        }
                    }
                }

                totalRecordCount = civiCRMOrganizationDataResponse.Count();
                ThoughtFocus.DataAccess.Models.Admin.CiviCRMDataExportLog civiCRMDataLog = new DataAccess.Models.Admin.CiviCRMDataExportLog();

                // Save the organization log details in CiviCRMOrganizationExportDetails
                CiviCRMOrganizationExportDetail civiCRMOrganizationExportLog = null;

                if (totalRecordCount < 0)
                {
                    civiCRMDataExportLogResponse.IsSuccess = false;
                }
                else
                {
                    civiCRMDataLog.IsActive = true;
                    civiCRMDataLog.ExportType = 1;
                    civiCRMDataLog.Recordscount = totalRecordCount;
                    civiCRMDataLog.ExportedBy = userSessionEntity.UserID;
                    civiCRMDataLog.ExportedOn = DateTime.Now;
                    civiCRMDataLog.ExportedOrganizationDetails = new List<CiviCRMOrganizationExportDetail>();
                    // Save CiViCRMLog Data into CiviCRMOrganizationExportDetails table
                    foreach (var org in civiCRMOrganizationDataResponse)
                    {
                        civiCRMOrganizationExportLog = new CiviCRMOrganizationExportDetail();
                        civiCRMOrganizationExportLog.LogID = civiCRMDataLog.ID;
                        civiCRMOrganizationExportLog.BusinessEntityID = org.External_ID_OrdID;
                        civiCRMDataLog.ExportedOrganizationDetails.Add(civiCRMOrganizationExportLog);
                    }

                    this._civiCRMDataExportLogRepository.SaveCiviCRMLog(civiCRMDataLog, userSessionEntity.UserID);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService -> GetCiviCRMOrganizationExportData", null);
                civiCRMDataExportLogResponse.IsSuccess = false;
                civiCRMDataExportLogResponse.Message = "Unable to retrieve the data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService -> GetCiviCRMOrganizationExportData", null);
                civiCRMDataExportLogResponse.IsSuccess = false;
                civiCRMDataExportLogResponse.Message = "Unable to retrieve the data at this moment. Please try after sometime.";
            }


            return civiCRMOrganizationDataResponse;
        }

        public List<CiviCRMContactsDataResponse> GetCiviCRMContactExportData(UserSessionEntity userSessionEntity)
        {
            List<CiviCRMContactsDataResponse> civiCRMContactsDataResponse = new List<CiviCRMContactsDataResponse>();
            CiviCRMDataExportLogResponse civiCRMDataExportLogResponse = new CiviCRMDataExportLogResponse();
            int totalRecordCount = 0;

            try
            {
                civiCRMContactsDataResponse = this._businessEntityRepository.GetAll().Where(c => c.IsActive == true)
                .Select(x => new CiviCRMContactsDataResponse
                {
                    OrganizationID = x.ID,
                }).ToList();

                if (civiCRMContactsDataResponse != null && civiCRMContactsDataResponse.Count > 0)
                {
                    foreach (var item in civiCRMContactsDataResponse)
                    {


                        //var recentProgramInvitation = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramStatusID == 2 && x.BusinessID == item.OrganizationID).OrderByDescending(x => x.ProgramInvitationID).FirstOrDefault();
                        var programInvitation = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramStatusID == 2 && x.BusinessID == item.OrganizationID).OrderByDescending(x => x.ProgramInvitationID).ToList();
                        long recentProgramInvitationID = 0;

                        //Check if more than one program invitation is there.
                        if (programInvitation != null && programInvitation.Count > 1)
                        {
                            var programInvitationList = new Dictionary<long, DateTime>();
                            foreach (var invitation in programInvitation)
                            {
                                var loanApplication = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == invitation.ProgramInvitationID && x.ApplicationStatusID > 3).ToList();
                                if (loanApplication != null && loanApplication.Count > 0)
                                {
                                    programInvitationList.Add(invitation.ProgramInvitationID, loanApplication.FirstOrDefault().LastModifiedDateTime);
                                }
                            }

                            if (programInvitationList.Count > 0)
                            {
                                recentProgramInvitationID = programInvitationList.OrderByDescending(o => o.Value).Select(o => o.Key).FirstOrDefault();
                            }
                        }
                        else
                        {
                            recentProgramInvitationID = programInvitation.Select(y => y.ProgramInvitationID).FirstOrDefault();

                        }

                        if (item != null && recentProgramInvitationID > 0)
                        {
                            var loanApplication = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == recentProgramInvitationID && x.ApplicationStatusID > 3).FirstOrDefault();
                            var loanBusinessDetailMaster = _loanBusinessDetailMasterRepository.GetAll().Where(b => b.BusinessID == item.OrganizationID).FirstOrDefault();

                            if (loanApplication != null && loanApplication.LoanApplicationID > 0)
                            {
                                var businessUser = this._businessContactRepository.GetAll().Where(c => c.BusinessID == item.OrganizationID && (c.BusinessRoleID == 1 || c.BusinessRoleID == 2 || c.BusinessRoleID == 5 || c.BusinessRoleID == 6 || c.BusinessRoleID == 7 || c.BusinessRoleID == 8) && c.IsActive == true).ToList();
                                var businessEntity = _businessEntityRepository.GetAll().Where(b => b.ID == item.OrganizationID && b.IsActive == true).FirstOrDefault();

                                if (businessUser != null && businessUser.Count > 0)
                                {
                                    foreach (var user in businessUser)
                                    {
                                        var contact = this._contactRepository.GetAll().Where(x => x.IsActive == true && x.ContactID == user.ContactID).FirstOrDefault();

                                        if (contact != null && contact.ContactID > 0)
                                        {
                                            item.Prefix = !string.IsNullOrEmpty(contact.Salutation.SalutationName) ? contact.Salutation.SalutationName : string.Empty;
                                            item.First_Name = contact.FirstName;
                                            item.Middle_Name = contact.MiddleName;
                                            item.Last_Name = contact.LastName;
                                            item.Affiliate = businessEntity?.Affiliate?.AffiliateName;
                                            item.Organization_Name = businessEntity?.BusinessName;

                                            item.Role = user.BusinessRole.BusinessRoleName;
                                            item.Street_Address = loanBusinessDetailMaster != null ? loanBusinessDetailMaster?.Address : loanApplication.LoanBusinessDetail?.Address;
                                            item.City = loanBusinessDetailMaster != null ? loanBusinessDetailMaster?.City : loanApplication.LoanBusinessDetail?.City;
                                            item.State = loanBusinessDetailMaster != null ? loanBusinessDetailMaster?.State?.StateName : loanApplication.LoanBusinessDetail?.State?.StateName;
                                            item.Zip = loanBusinessDetailMaster != null ? loanBusinessDetailMaster?.Zip : loanApplication.LoanBusinessDetail?.Zip;
                                            item.Phone_Number = contact.PhoneNo;
                                            item.Phone_Type = "Mobile";
                                            item.Email = contact.EmailAddress;
                                            item.ExternalID = user.BusinessID;
                                            item.ContactID = 10000 + user.BusinessUserID;
                                            item.UniqueID = contact.ContactID;


                                            if (!String.IsNullOrEmpty(item.Email))
                                            {
                                                var businessOwnersMaster = this._businessOwnerMasterRepository.GetAll().Where(x => x.IsActive == true && x.EmailAddress == item.Email && x.BusinessID == item.OrganizationID).ToList();
                                                //dynamic businessOwners = null;

                                                if (businessOwnersMaster != null && businessOwnersMaster.Count > 0)
                                                {

                                                    BusinessOwnerMaster businessOwnerMaster = null;

                                                    //businessOwner = businessOwners.Where(x => x.EmailAddress == item.Email && x.IsActive == true).FirstOrDefault(); }
                                                    businessOwnerMaster = businessOwnersMaster.Where(x => x.EmailAddress == item.Email && x.IsActive == true).FirstOrDefault();


                                                    //   if (businessOwner != null && businessOwner.IsActive == true && businessOwner.LoanApplicationID > 0)
                                                    if (businessOwnerMaster != null && businessOwnerMaster.IsActive == true)
                                                    {
                                                        // Veteran type demographic values change 
                                                        if (businessOwnerMaster.Veteran != null)
                                                        {
                                                            if (businessOwnerMaster.Veteran.IsActive == true && businessOwnerMaster.Veteran.VeteranID == 1)
                                                            {
                                                                item.Veteran = "No";
                                                            }
                                                            else if (businessOwnerMaster.Veteran.IsActive == true && businessOwnerMaster.Veteran.VeteranID == 2)
                                                            {
                                                                item.Veteran = "Yes";
                                                            }
                                                        }

                                                        if (businessOwnerMaster.Gender != null)
                                                        {
                                                            // Gender type demographic values change 
                                                            if (businessOwnerMaster.Gender.IsActive == true && businessOwnerMaster.Gender.GenderID == 3)
                                                            {
                                                                item.Gender = "Transgender";
                                                            }
                                                            else
                                                            {
                                                                item.Gender = businessOwnerMaster.Gender.GenderName;
                                                            }
                                                        }

                                                        if (businessOwnerMaster.Race != null)
                                                        {
                                                            // Race type demographic values change 
                                                            if (businessOwnerMaster.Race.IsActive == true && businessOwnerMaster.Race.RaceID == 1)
                                                            {
                                                                item.Race = "American Indian/Alaska Native";
                                                            }
                                                            else if (businessOwnerMaster.Race.IsActive == true && businessOwnerMaster.Race.RaceID == 3)
                                                            {
                                                                item.Race = "Black/African American";
                                                            }
                                                            else if (businessOwnerMaster.Race.IsActive == true && businessOwnerMaster.Race.RaceID == 4)
                                                            {
                                                                item.Race = "Hawaiian Native/Other Pacific Islander";
                                                            }
                                                            else if (businessOwnerMaster.Race.IsActive == true && businessOwnerMaster.Race.RaceID == 6)
                                                            {
                                                                item.Race = "More than one Race";
                                                            }
                                                            else
                                                            {
                                                                item.Race = businessOwnerMaster.Race.RaceName;
                                                            }
                                                        }

                                                        if (businessOwnerMaster.Ethnicity != null)
                                                        {
                                                            // Ethnicity type demographic values change 
                                                            if (businessOwnerMaster.Ethnicity.IsActive == true && businessOwnerMaster.Ethnicity.EthnicityID == 4)
                                                            {
                                                                item.Ethnicity = "Non-Hispanic";
                                                            }
                                                            else if (businessOwnerMaster.Ethnicity.IsActive == true && businessOwnerMaster.Ethnicity.EthnicityID == 5)
                                                            {
                                                                item.Ethnicity = "Hispanic/Latino";
                                                            }
                                                            else if (businessOwnerMaster.Ethnicity.IsActive == true && businessOwnerMaster.Ethnicity.EthnicityID == 8)
                                                            {
                                                                item.Ethnicity = "More than one Race";
                                                            }
                                                            else if (businessOwnerMaster.Ethnicity.IsActive == true && businessOwnerMaster.Ethnicity.EthnicityID == 6)
                                                            {
                                                                item.Ethnicity = "Hawaiian Native/Other Pacific Islander";
                                                            }
                                                            else
                                                            {
                                                                item.Ethnicity = businessOwnerMaster.Ethnicity.EthnicityName;
                                                            }
                                                            item.Ethnicity = item.Ethnicity.Replace(" or ", "/");
                                                        }


                                                    }
                                                }
                                                else
                                                {
                                                    var businessOwners = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.BusinessOwners.Any(y => y.EmailAddress == item.Email) && x.ApplicationStatusID > 3).Select(b => b.BusinessOwners).FirstOrDefault();
                                                    BusinessOwner businessOwner = null;
                                                    if (businessOwners != null)
                                                    {
                                                        businessOwner = businessOwners.Where(x => x.EmailAddress == item.Email && x.IsActive == true).FirstOrDefault();
                                                        //businessOwner = businessOwners.Where(x => x.EmailAddress == item.Email && x.IsActive == true).FirstOrDefault();
                                                    }

                                                    if (businessOwner != null && businessOwner.IsActive == true && businessOwner.LoanApplicationID > 0)
                                                    //if (businessOwner != null && businessOwner.IsActive == true)
                                                    {
                                                        // Veteran type demographic values change 
                                                        if (businessOwner.Veteran != null)
                                                        {
                                                            if (businessOwner.Veteran.IsActive == true && businessOwner.Veteran.VeteranID == 1)
                                                            {
                                                                item.Veteran = "No";
                                                            }
                                                            else if (businessOwner.Veteran.IsActive == true && businessOwner.Veteran.VeteranID == 2)
                                                            {
                                                                item.Veteran = "Yes";
                                                            }
                                                        }

                                                        if (businessOwner.Gender != null)
                                                        {
                                                            // Gender type demographic values change 
                                                            if (businessOwner.Gender.IsActive == true && businessOwner.Gender.GenderID == 3)
                                                            {
                                                                item.Gender = "Transgender";
                                                            }
                                                            else
                                                            {
                                                                item.Gender = businessOwner.Gender.GenderName;
                                                            }
                                                        }

                                                        if (businessOwner.Race != null)
                                                        {
                                                            // Race type demographic values change 
                                                            if (businessOwner.Race.IsActive == true && businessOwner.Race.RaceID == 1)
                                                            {
                                                                item.Race = "American Indian/Alaska Native";
                                                            }
                                                            else if (businessOwner.Race.IsActive == true && businessOwner.Race.RaceID == 3)
                                                            {
                                                                item.Race = "Black/African American";
                                                            }
                                                            else if (businessOwner.Race.IsActive == true && businessOwner.Race.RaceID == 4)
                                                            {
                                                                item.Race = "Hawaiian Native/Other Pacific Islander";
                                                            }
                                                            else if (businessOwner.Race.IsActive == true && businessOwner.Race.RaceID == 6)
                                                            {
                                                                item.Race = "More than one Race";
                                                            }
                                                            else
                                                            {
                                                                item.Race = businessOwner.Race.RaceName;
                                                            }
                                                        }

                                                        if (businessOwner.Ethnicity != null)
                                                        {
                                                            // Ethnicity type demographic values change 
                                                            if (businessOwner.Ethnicity.IsActive == true && businessOwner.Ethnicity.EthnicityID == 4)
                                                            {
                                                                item.Ethnicity = "Non-Hispanic";
                                                            }
                                                            else if (businessOwner.Ethnicity.IsActive == true && businessOwner.Ethnicity.EthnicityID == 5)
                                                            {
                                                                item.Ethnicity = "Hispanic/Latino";
                                                            }
                                                            else if (businessOwner.Ethnicity.IsActive == true && businessOwner.Ethnicity.EthnicityID == 8)
                                                            {
                                                                item.Ethnicity = "More than one Race";
                                                            }
                                                            else if (businessOwner.Ethnicity.IsActive == true && businessOwner.Ethnicity.EthnicityID == 6)
                                                            {
                                                                item.Ethnicity = "Hawaiian Native/Other Pacific Islander";
                                                            }
                                                            else
                                                            {
                                                                item.Ethnicity = businessOwner.Ethnicity.EthnicityName;
                                                            }
                                                            item.Ethnicity = item.Ethnicity.Replace(" or ", "/");
                                                        }


                                                    }
                                                }


                                            }
                                            else
                                            {
                                                item.Veteran = string.Empty;
                                                item.Race = string.Empty;
                                                item.Gender = string.Empty;
                                                item.Ethnicity = string.Empty;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                List<CiviCRMContactsDataResponse> tobeRemovedList = civiCRMContactsDataResponse.Where(x => x.ContactID < 1).ToList();
                if (tobeRemovedList != null && tobeRemovedList.Count > 0)
                {
                    foreach (var item in tobeRemovedList)
                    {
                        civiCRMContactsDataResponse.Remove(item);
                    }
                }

                totalRecordCount = civiCRMContactsDataResponse.Count();
                ThoughtFocus.DataAccess.Models.Admin.CiviCRMDataExportLog civiCRMDataLog = new DataAccess.Models.Admin.CiviCRMDataExportLog();

                // Save the contact log details in CiviCRMContactExportDetails
                ThoughtFocus.DataAccess.Models.Admin.CiviCRMContactExportDetail civiCRMContactExportLog = null;

                if (totalRecordCount < 0)
                {
                    civiCRMDataExportLogResponse.IsSuccess = false;
                }
                else
                {
                    civiCRMDataLog.IsActive = true;
                    civiCRMDataLog.ExportType = 2;
                    civiCRMDataLog.Recordscount = totalRecordCount;
                    civiCRMDataLog.ExportedBy = userSessionEntity.UserID;
                    civiCRMDataLog.ExportedOn = DateTime.Now;
                    civiCRMDataLog.ExportedContactDetails = new List<DataAccess.Models.Admin.CiviCRMContactExportDetail>();
                    // Save CiViCRMLog Data into SaveCiviCRMContactExportLog table
                    foreach (var contact in civiCRMContactsDataResponse)
                    {
                        civiCRMContactExportLog = new DataAccess.Models.Admin.CiviCRMContactExportDetail();
                        civiCRMContactExportLog.LogID = civiCRMDataLog.ID;
                        civiCRMContactExportLog.ContactID = contact.UniqueID;
                        civiCRMDataLog.ExportedContactDetails.Add(civiCRMContactExportLog);
                    }
                    this._civiCRMDataExportLogRepository.SaveCiviCRMLog(civiCRMDataLog, userSessionEntity.UserID);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService -> GetCiviCRMContactExportData", null);
                civiCRMDataExportLogResponse.IsSuccess = false;
                civiCRMDataExportLogResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService -> GetCiviCRMContactExportData", null);
                civiCRMDataExportLogResponse.IsSuccess = false;
                civiCRMDataExportLogResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }

            return civiCRMContactsDataResponse;
        }

        public CiviCRMDataExportLogResponse GetAllCiviCRMDataExportLog()
        {
            CiviCRMDataExportLogResponse civiCRMLogListResponse = new CiviCRMDataExportLogResponse();
            int totalRecordCount = 0;

            try
            {
                List<CiviCRMDataExportLogDetails> listOfCiviCRMDataExportLog = null;
                var contact = this._contactRepository.GetAll().ToList();

                listOfCiviCRMDataExportLog = this._civiCRMDataExportLogRepository.GetAll().Where(c => c.IsActive == true).OrderByDescending(o => o.ExportedOn)
                    .Select(x => new CiviCRMDataExportLogDetails
                    {
                        ExportedUserID = x.ExportedBy,
                        ExportedType = x.ExportType == 1 ? "Organization" : "Contact",
                        ExportedDate = string.Format("{0:MM/dd/yyyy}", x.ExportedOn),
                        RecordCount = x.Recordscount,
                    }).ToList();
                foreach (var item in listOfCiviCRMDataExportLog)
                {
                    var exportedBy = contact.Where(c => c.Users.UserID == item.ExportedUserID).FirstOrDefault();
                    if (exportedBy != null)
                    {
                        item.ExportedBy = string.Concat(exportedBy.FirstName != null ? exportedBy.FirstName + " " : "", exportedBy.MiddleName != null ? exportedBy.MiddleName + " " : "", exportedBy.LastName != null ? exportedBy.LastName : "").Replace("  ", " ");
                    }
                }

                civiCRMLogListResponse.ExportLogs = listOfCiviCRMDataExportLog;
                totalRecordCount = listOfCiviCRMDataExportLog.Count();

                if (totalRecordCount < 1)
                {
                    civiCRMLogListResponse.IsSuccess = false;
                    civiCRMLogListResponse.Message = "Unable to retrieve the data at this moment. Please try after sometime.";
                }
                else
                {
                    civiCRMLogListResponse.IsSuccess = true;
                    civiCRMLogListResponse.Message = "Successfully returned CiviCRM Data Export Log.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllCiviCRMDataExportLog", null);
                civiCRMLogListResponse.IsSuccess = false;
                civiCRMLogListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetAllCiviCRMDataExportLog", null);
                civiCRMLogListResponse.IsSuccess = false;
                civiCRMLogListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return civiCRMLogListResponse;
        }

        public BusinessProfileMasterDataResponse GetBusinessProfileMasterData(long businessID)
        {
            BusinessProfileMasterDataResponse businessProfileData = new BusinessProfileMasterDataResponse();
            List<BusinessOwnerMaster> businessOwnerMasters = new List<BusinessOwnerMaster>();

            try
            {
                //get master data for business profile
                businessProfileData.BusinessProfileMasterDataEntity = _masterService.GetMasterEntityForBusinessProfile();
                businessOwnerMasters = _businessOwnerMasterRepository.GetAll().Where(b => b.BusinessID == businessID && b.IsActive == true).ToList();
                var loanBusinessDetailMaster = _loanBusinessDetailMasterRepository.GetAll().Where(l => l.BusinessID == businessID)?.FirstOrDefault();

                List<LoanApplication> loanApplication = new List<LoanApplication>();
                var sortedLoanApp = new LoanApplication();
                long loanApplicationId = 0;
                if (loanBusinessDetailMaster == null || businessOwnerMasters.Count == 0)
                {
                    var programInvitation = _programInvitationRepository.GetAll().Where(b => b.IsActive == true && b.BusinessID == businessID).ToList();

                    foreach (var programInvite in programInvitation)
                    {
                        var loanApp = _LoanApplicationRepository.GetAll().Where(b => b.IsActive == true && b.ProgramInvitationID == programInvite.ProgramInvitationID).FirstOrDefault();
                        if (loanApp != null)
                        {
                            loanApplication.Add(loanApp);
                        }

                    }
                    sortedLoanApp = loanApplication.OrderByDescending(o => o.LastModifiedDateTime).FirstOrDefault();
                    loanApplicationId = sortedLoanApp != null ? sortedLoanApp.LoanApplicationID : 0;
                }

                //var programInvitationId = programInvitation != null ? programInvitation.ProgramInvitationID : 0;
                var businessEntities = _businessEntityRepository.GetBusinessEntity(businessID);

                if (businessOwnerMasters.Count == 0)
                {

                    var businessOwners = _businessOwnerRepository.GetAll().Where(b => b.IsActive == true && b.LoanApplicationID == loanApplicationId);

                    if (sortedLoanApp != null && businessOwners != null)
                    {
                        foreach (var businessOwner in businessOwners)
                        {
                            BusinessOwnerMasterParam businessOwnerMasterParam = new BusinessOwnerMasterParam();
                            businessOwnerMasterParam = _mapper.Map<BusinessOwnerMasterParam>(businessOwner);
                            businessOwnerMasterParam.GenderName = businessOwner?.Gender?.GenderName != null ? businessOwner?.Gender?.GenderName : "";
                            businessOwnerMasterParam.VeteranName = businessOwner?.Veteran?.VeteranType != null ? businessOwner?.Veteran?.VeteranType : "";
                            businessOwnerMasterParam.RaceName = businessOwner?.Race?.RaceName != null ? businessOwner?.Race?.RaceName : "";
                            businessOwnerMasterParam.EthnicityName = businessOwner?.Ethnicity?.EthnicityName != null ? businessOwner?.Ethnicity?.EthnicityName : "";
                            businessOwnerMasterParam.BusinessID = businessID;
                            businessOwnerMasterParam.BusinessOwnerName = businessOwner.BusinessOwnerName != null ? businessOwner.BusinessOwnerName : "";
                            businessOwnerMasterParam.Demographic = businessOwner.Demographic != null ? businessOwner.Demographic : "";
                            businessOwnerMasterParam.EmailAddress = businessOwner.EmailAddress != null ? businessOwner.EmailAddress : "";
                            // businessOwnerMasterParam.OwnedPercentage = businessOwner.OwnedPercentage != null ? businessOwner.OwnedPercentage : "";
                            businessOwnerMasterParam.PhoneNumber = businessOwner.PhoneNumber != null ? businessOwner.PhoneNumber : "";

                            businessProfileData.BusinessOwnerMasterParam.Add(businessOwnerMasterParam);
                        }
                        businessProfileData.IsSuccess = true;
                        businessProfileData.Message = "Successfully retrieve Business Profile Master data.";

                    }
                    else
                    {
                        businessProfileData = SetEmptyStringToNullFields(businessProfileData);

                        if (businessEntities != null)
                        {
                            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessID = businessEntities.ID;
                            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessName = businessEntities.BusinessName;
                            businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateName = businessEntities.Affiliate?.AffiliateName;
                            businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateID = businessEntities.Affiliate?.AffiliateID;
                            businessProfileData.LoanBusinessDetailMasterPreParam.EIN = businessEntities?.EIN;
                            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessTypeID = businessEntities?.BusinessTypeID;
                        }
                        businessProfileData.IsSuccess = true;
                        businessProfileData.Message = "No Data Found";
                        return businessProfileData;

                    }

                }
                else
                {
                    foreach (var businessOwnerMaster in businessOwnerMasters)
                    {
                        BusinessOwnerMasterParam businessOwnerMasterParam = new BusinessOwnerMasterParam();
                        businessOwnerMasterParam = _mapper.Map<BusinessOwnerMasterParam>(businessOwnerMaster);
                        businessOwnerMasterParam.GenderName = businessOwnerMaster?.Gender?.GenderName != null ? businessOwnerMaster?.Gender?.GenderName : "";
                        businessOwnerMasterParam.VeteranName = businessOwnerMaster?.Veteran?.VeteranType != null ? businessOwnerMaster?.Veteran?.VeteranType : "";
                        businessOwnerMasterParam.RaceName = businessOwnerMaster?.Race?.RaceName != null ? businessOwnerMaster?.Race?.RaceName : "";
                        businessOwnerMasterParam.EthnicityName = businessOwnerMaster?.Ethnicity?.EthnicityName != null ? businessOwnerMaster?.Ethnicity?.EthnicityName : "";

                        businessOwnerMasterParam.BusinessOwnerName = businessOwnerMaster.BusinessOwnerName != null ? businessOwnerMaster.BusinessOwnerName : "";
                        businessOwnerMasterParam.Demographic = businessOwnerMaster.Demographic != null ? businessOwnerMaster.Demographic : "";
                        businessOwnerMasterParam.EmailAddress = businessOwnerMaster.EmailAddress != null ? businessOwnerMaster.EmailAddress : "";
                        //businessOwnerMasterParam.OwnedPercentage = businessOwnerMaster.OwnedPercentage != null ? businessOwnerMaster.OwnedPercentage : "";
                        businessOwnerMasterParam.PhoneNumber = businessOwnerMaster.PhoneNumber != null ? businessOwnerMaster.PhoneNumber : "";

                        businessProfileData.BusinessOwnerMasterParam.Add(businessOwnerMasterParam);

                    }
                    businessProfileData.IsSuccess = true;
                    businessProfileData.Message = "Successfully retrieve Business Profile Master data.";

                }


                if (loanBusinessDetailMaster == null)
                {

                    var loanBusinessDetails = _loanBusinessDetailRepository.GetAll().Where(b => b.LoanApplicationID == loanApplicationId).FirstOrDefault();

                    if (sortedLoanApp != null && loanBusinessDetails != null)
                    {
                        businessProfileData.LoanBusinessDetailMasterPreParam = _mapper.Map<LoanBusinessDetailMasterPreParam>(loanBusinessDetails);
                        businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateName = businessEntities?.Affiliate?.AffiliateName ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.IndustryTypeName = loanBusinessDetails?.IndustryType?.Type ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.BusinessTypeName = loanBusinessDetails?.BusinessType?.Type ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.NaicsCode = loanBusinessDetails?.NaicsCode ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.SIC_Name = loanBusinessDetails?.SIC?.IndustryTitle ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.StateName = loanBusinessDetails?.State?.StateName ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.StartDate = loanBusinessDetails?.StartDate != null ? loanBusinessDetails?.StartDate.ToString("MM/dd/yyyy") : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.BusinessID = businessID;
                        businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateID = loanBusinessDetails?.AffiliateID;

                        businessProfileData.LoanBusinessDetailMasterPreParam.Address = loanBusinessDetails.Address ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.BankAccountNumber = loanBusinessDetails.BankAccountNumber != null ? loanBusinessDetails.BankAccountNumber : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.BankRoutingNumber = loanBusinessDetails.BankRoutingNumber != null ? loanBusinessDetails.BankRoutingNumber : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.BusinessName = businessEntities?.BusinessName ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.City = loanBusinessDetails.City != null ? loanBusinessDetails.City : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.Comment = loanBusinessDetails.Comment != null ? loanBusinessDetails.Comment : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.DBA = loanBusinessDetails.DBA != null ? loanBusinessDetails.DBA : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.DUNS = loanBusinessDetails.DUNS != null ? loanBusinessDetails.DUNS : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.EIN = businessEntities?.EIN ?? "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.Url = loanBusinessDetails.Url != null ? loanBusinessDetails.Url : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.EmailAddress = loanBusinessDetails.EmailAddress != null ? loanBusinessDetails.EmailAddress : "";
                        businessProfileData.LoanBusinessDetailMasterPreParam.Zip = loanBusinessDetails.Zip != null ? loanBusinessDetails.Zip : "";

                        businessProfileData.LoanBusinessDetailMasterPreParam.NumberOfYearsInBusiness = loanBusinessDetails.NumberOfYearsInBusiness.ToString();
                        businessProfileData.LoanBusinessDetailMasterPreParam.EmployeeStrength = loanBusinessDetails.EmployeeStrength.ToString();


                        businessProfileData.IsSuccess = true;
                        businessProfileData.Message = "Successfully retrieve Business Profile Master data.";

                    }
                    else
                    {
                        businessProfileData = SetEmptyStringToNullFields(businessProfileData);

                        if (businessEntities != null)
                        {
                            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessID = businessEntities.ID;
                            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessName = businessEntities.BusinessName;
                            businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateName = businessEntities.Affiliate?.AffiliateName;
                            businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateID = businessEntities.Affiliate?.AffiliateID;
                            businessProfileData.LoanBusinessDetailMasterPreParam.EIN = businessEntities?.EIN;
                            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessTypeID = businessEntities?.BusinessTypeID;
                        }
                        businessProfileData.IsSuccess = true;
                        businessProfileData.Message = "No Data Found.";

                        return businessProfileData;

                    }
                }
                else
                {
                    businessProfileData.LoanBusinessDetailMasterPreParam = _mapper.Map<LoanBusinessDetailMasterPreParam>(loanBusinessDetailMaster);
                    businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateName = businessEntities?.Affiliate?.AffiliateName;
                    businessProfileData.LoanBusinessDetailMasterPreParam.IndustryTypeName = loanBusinessDetailMaster?.IndustryType?.Type != null ? loanBusinessDetailMaster?.IndustryType?.Type : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.BusinessTypeName = loanBusinessDetailMaster?.BusinessType?.Type != null ? loanBusinessDetailMaster?.BusinessType?.Type : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.NaicsCode = loanBusinessDetailMaster?.NaicsCode != null ? loanBusinessDetailMaster?.NaicsCode : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.SIC_Name = loanBusinessDetailMaster?.SIC?.IndustryTitle != null ? loanBusinessDetailMaster?.SIC?.IndustryTitle : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.StateName = loanBusinessDetailMaster?.State?.StateName != null ? loanBusinessDetailMaster?.State?.StateName : "";
                    //businessProfileData.LoanBusinessDetailMasterPreParam.StartDate = loanBusinessDetailMaster?.StartDate != null ? loanBusinessDetailMaster?.StartDate.ToString("MM/dd/yyyy") : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateID = businessEntities.AffiliateID;

                    businessProfileData.LoanBusinessDetailMasterPreParam.Address = loanBusinessDetailMaster.Address != null ? loanBusinessDetailMaster.Address : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.BankAccountNumber = loanBusinessDetailMaster.BankAccountNumber != null ? loanBusinessDetailMaster.BankAccountNumber : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.BankRoutingNumber = loanBusinessDetailMaster.BankRoutingNumber != null ? loanBusinessDetailMaster.BankRoutingNumber : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.BusinessName = businessEntities?.BusinessName ?? "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.City = loanBusinessDetailMaster.City != null ? loanBusinessDetailMaster.City : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.Comment = loanBusinessDetailMaster.Comment != null ? loanBusinessDetailMaster.Comment : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.DBA = loanBusinessDetailMaster.DBA != null ? loanBusinessDetailMaster.DBA : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.DUNS = loanBusinessDetailMaster.DUNS != null ? loanBusinessDetailMaster.DUNS : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.EIN = businessEntities.EIN ?? "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.Url = loanBusinessDetailMaster.Url != null ? loanBusinessDetailMaster.Url : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.EmailAddress = loanBusinessDetailMaster.EmailAddress != null ? loanBusinessDetailMaster.EmailAddress : "";
                    businessProfileData.LoanBusinessDetailMasterPreParam.Zip = loanBusinessDetailMaster.Zip != null ? loanBusinessDetailMaster.Zip : "";

                    var noOfYearInBusiness = loanBusinessDetailMaster.NumberOfYearsInBusiness;
                    businessProfileData.LoanBusinessDetailMasterPreParam.NumberOfYearsInBusiness = noOfYearInBusiness == 123456789
                                                ? string.Empty : noOfYearInBusiness.ToString();

                    var employeeStrength = loanBusinessDetailMaster.EmployeeStrength;
                    businessProfileData.LoanBusinessDetailMasterPreParam.EmployeeStrength = employeeStrength == 123456789
                                               ? string.Empty : employeeStrength.ToString();
                    var startDate = String.Format("{0:MM/dd/yyyy}", loanBusinessDetailMaster.StartDate);

                    businessProfileData.LoanBusinessDetailMasterPreParam.StartDate = startDate.Contains("01/01/0001")
                                              ? string.Empty : startDate;

                    businessProfileData.IsSuccess = true;
                    businessProfileData.Message = "Successfully retrieve Business Profile Master data.";
                }
            }
            catch (Exception ex)
            {
                businessProfileData = SetEmptyStringToNullFields(businessProfileData);
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetBusinessProfileMasterData()", null);
                businessProfileData.IsSuccess = false;
                businessProfileData.Message = "Unable to retrieve business profile data at this moment. Please try after sometime.";

            }
            return businessProfileData;
        }

        private static BusinessProfileMasterDataResponse SetEmptyStringToNullFields(BusinessProfileMasterDataResponse businessProfileData)
        {
            businessProfileData.BusinessOwnerMasterParam = new List<BusinessOwnerMasterParam>();
            businessProfileData.BusinessOwnerMasterParam.Add(new BusinessOwnerMasterParam
            {
                BusinessOwnerName = "",
                Demographic = "",
                EmailAddress = "",
                EthnicityName = "",
                GenderName = "",
                PhoneNumber = "",
                RaceName = "",
                VeteranName = ""
            }
            );
            businessProfileData.LoanBusinessDetailMasterPreParam = new LoanBusinessDetailMasterPreParam();
            businessProfileData.LoanBusinessDetailMasterPreParam.Address = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.AffiliateName = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.BankAccountNumber = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.BankRoutingNumber = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessName = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.BusinessTypeName = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.City = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.Comment = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.DBA = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.DUNS = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.EIN = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.EmailAddress = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.IndustryTypeName = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.NaicsCode = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.PhoneNumber = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.SIC_Name = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.IndustryTypeName = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.StartDate = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.Url = "";
            businessProfileData.LoanBusinessDetailMasterPreParam.Zip = "";

            return businessProfileData;
        }

        public BusinessEntityListByUserResponse GetAllBusinessEntityByUser(string userName)
        {
            BusinessEntityListByUserResponse businessEntityListByUserResponse = new BusinessEntityListByUserResponse();


            try
            {
                var contactId = _userRepository.GetByUserName(userName)?.ContactID;
                var businessUsers = _businessContactRepository.GetBusinessUserByContactID((long)contactId).ToList();

                foreach (var businessUser in businessUsers)
                {

                    var businessEntities = _businessEntityRepository.GetAll().Where(b => b.ID == businessUser.BusinessID).Distinct();
                    foreach (var businessEntitie in businessEntities)
                    {
                        var programInvitationList = _programInvitationRepository.GetAll().Where(p => p.BusinessID == businessEntitie.ID && p.IsActive == true).ToList();
                        var programInvitationId = programInvitationList.Select(p => p.ProgramInvitationID);
                        businessEntityListByUserResponse.BusinessEntity.Add(
                            new BusinesEntityByUser
                            {
                                BusinessId = businessEntitie.ID,
                                BusinessName = businessEntitie.BusinessName,
                                ProgramInvitationId = programInvitationId.ToList<long>()
                            }
                        );
                    }
                }
                businessEntityListByUserResponse.IsSuccess = true;
                businessEntityListByUserResponse.Message = "Fetched Business Entity for User successfully";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetBusinessEntityByUser()", null);
                businessEntityListByUserResponse.IsSuccess = false;
                businessEntityListByUserResponse.Message = "Unable to retrieve business Entity for user at this moment. Please try after sometime.";

            }
            return businessEntityListByUserResponse;
        }
        public CommonResponse SendUpdateFundDetailEmailNotifiaction(long loanApplicationID, UserSessionEntity userSessionEntity)
        {
            CommonResponse commonResponse = null;
            LoanApplication loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(loanApplicationID);
            if (loanApplication != null)
            {
                List<string> validationMessages = new List<string>();

                try
                {
                    ProgramInvitation programInvitation = this._programInvitationRepository.GetProgramInvitation(loanApplication.ProgramInvitationID);
                    string callBack = _appSettings.BaseUrl + "/programinvitation/form/" + programInvitation.BusinessID;

                    userSessionEntity.UserID = userSessionEntity.UserID;
                    string callBackURL = "<tr> <td style='padding: 20px 0 20px 100px;'> <table class='buttonwrapper' border='0' cellspacing='3' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Business</a> </td> </tr> </table> </td> </tr> ";
                    //callBackURL = "<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> <br /><br /> </td>";
                    var email = _notificationService.SendSPAEmail("Payment Schedule not updated", callBackURL, (long)EmailTemplateNameID.PAYMENTSCHEDULENOTIFICATION, loanApplication.ProgramInvitationID, userSessionEntity.ContactID, loanApplication.LoanApplicationID, "NULTreasury", userSessionEntity);
                    validationMessages.Add("NOTIFY has been sent");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "NOTIFY mail has been sent successfully.";
                    commonResponse.ID = userSessionEntity.ContactID;
                    if (!email)
                    {
                        validationMessages.Add("NOTIFY fail to send");
                        commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                        commonResponse.StatusMessage = "NOTIFY mail fail to send successfully.";
                        commonResponse.ID = userSessionEntity.ContactID;
                    }

                    //}
                }
                catch (BusinessException ex)
                {
                    //Logger.LogDebug("Error at Contact Service -> ResetBusinessContact " + ex.InnerException == null
                    //? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                    // LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ForgetPassword ", null);
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, null);
                }
                catch (Exception ex)
                {
                    // Logger.LogDebug("Error at Contact Service -> ResetBusinessContact " + ex.InnerException == null
                    // ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                    // LoggerExtensions.LogInformation(Logger, null, ex, "Exception in ContactServiceImpl-> ForgetPassword ", null);
                    commonResponse = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, null);
                }

                return commonResponse;
            }
            return commonResponse;
        }

        public ProgramsResponse GetProgramDetailsByFundEntityID(long fundEntityID)
        {
            var response = new ProgramsResponse();

            try
            {
                response.Programs = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true)
                    .Select(x => new ProgramResponse
                    {
                        ProgramId = x.FundingSourceID,
                        ProgramName = x.ProgramName
                    }).ToList();
                response.IsSuccess = false;
                response.Message = "Programs Data are retrieved successfully.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramDetails", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        public ReportDetailResponse FetchReportDetailByReportType(ReportDetailRequest reportDetailRequest)
        {
            var response = new ReportDetailResponse();
            try
            {
                DateTime _fromDate = DateTime.Now;
                DateTime _toDate = DateTime.Now;
                string fromDate = reportDetailRequest.FromDate;
                string toDate = reportDetailRequest.ToDate;
                if (fromDate == null || fromDate == string.Empty)
                    _fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                else
                    _fromDate = DateTime.Parse(fromDate);

                if (toDate == null || toDate == string.Empty)
                    _toDate = DateTime.Now;
                else
                    _toDate = DateTime.Parse(toDate);

                _toDate = _toDate.AddDays(1);
                // // Entity: all, Program: all
                if ((reportDetailRequest.FundingEntityID == null || reportDetailRequest.FundingEntityID.Count() == 0 || reportDetailRequest.FundingEntityID.FirstOrDefault() == 0) && (reportDetailRequest.ProgramID.FirstOrDefault() == 0))
                {
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        reportDetailRequest.ProgramID = programIds;
                    }
                }
                else if ((reportDetailRequest.ProgramID == null || reportDetailRequest.ProgramID.Count() == 0 || reportDetailRequest.ProgramID.FirstOrDefault() == 0) && reportDetailRequest.FundingEntityID != null && (reportDetailRequest.FundingEntityID.Count() > 0 || reportDetailRequest.FundingEntityID.FirstOrDefault() > 0))
                {
                    // Entity: selected, Program: all
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true && reportDetailRequest.FundingEntityID.Contains(c.FundingEntityID)).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        reportDetailRequest.ProgramID = programIds;
                    }
                }

                var basicDetail = new List<BasicDetail>();
                if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
                {
                    basicDetail = this._programInvitationRepository.GetAll().Where(c => c.IsActive == true && reportDetailRequest.ProgramID.Contains(c.ProgramID)).OrderBy(o => o.BusinessEntity.BusinessName)
                             .Select(x => new BasicDetail
                             {
                                 BusinessID = x.BusinessEntity.ID,
                                 ProgramInvitationID = x.ProgramInvitationID,
                                 ProgramID = x.FundingSource == null ? 0 : x.FundingSource.FundingSourceID,
                                 FundingEntityID = x.FundingSource == null ? 0 : x.FundingSource.FundingEntityID,
                                 FundingEntityName = x.FundingSource == null ? string.Empty : x.FundingSource.FundingEntity.FundingEntityName,
                                 ProgramName = x.FundingSource == null ? string.Empty : x.FundingSource.ProgramName,
                                 FundingEIN = x.FundingSource == null ? string.Empty : x.FundingSource.FundingEntity.EIN,
                                 FundingTIN = x.FundingSource == null ? string.Empty : x.FundingSource.FundingEntity.TIN,
                                 BusinessName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.BusinessEntity.BusinessName),
                                 AffiliateName = x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty,

                             }).ToList();
                }

                if (reportDetailRequest.ReportType == CommonConstants.InvitedReportFlag)
                {
                    response.InvitationDetail = this.GetInvitationDetail(_fromDate, _toDate, basicDetail, reportDetailRequest);
                }
                else if (reportDetailRequest.ReportType == CommonConstants.ActiveAccountReportFlag)
                {
                    response.ActiveAccountDetail = this.GetActiveAccountDetail(_fromDate, _toDate, basicDetail, reportDetailRequest);
                }
                else if (reportDetailRequest.ReportType == CommonConstants.SubmittedReportFlag)
                {
                    response.ApplicationSubmittedDetail = this.GetApplicationSubmittedDetail(_fromDate, _toDate, basicDetail, reportDetailRequest);
                }
                else if (reportDetailRequest.ReportType == CommonConstants.StartedReportFlag)
                {
                    response.ApplicationStartedDetail = this.GetApplicationStartedDetail(_fromDate, _toDate, basicDetail, reportDetailRequest);
                }
                else if (reportDetailRequest.ReportType == CommonConstants.FundedReportFlag)
                {
                    response.ApplicationFundedDetail = this.GetApplicationFundedDetail(_fromDate, _toDate, basicDetail, reportDetailRequest);
                }
                else if (reportDetailRequest.ReportType == CommonConstants.FundReleasedReportFlag)
                {
                    response.FundReleasedDetail = this.GetFundReleasedDetail(_fromDate, _toDate, basicDetail, reportDetailRequest);
                }
                response.IsSuccess = true;
                response.Message = "Programs Data are retrieved successfully.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramDetails", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        private List<InvitationDetail> GetInvitationDetail(DateTime _fromDate, DateTime _toDate, List<BasicDetail> basicDetail, ReportDetailRequest reportDetailRequest)
        {
            var details = new List<InvitationDetail>();
            var contactInvitationInfo = new List<ContactInvitationInfo>();

            if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
            {
                var invitedProgram = this._programInviteeRepository.GetAll().Where(c => c.CreatedDateTime >= _fromDate
                          && c.CreatedDateTime <= _toDate && c.IsActive == true
                          && reportDetailRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).ToList();

                contactInvitationInfo = this._contactInvitationInfoRepository.GetAll().Where(c => c.ContactInvitedDateTime >= _fromDate
                               && c.ContactInvitedDateTime <= _toDate && c.Contact.IsActive == true
                               && c.Contact.Users.UserRoles.Any(x => x.RoleID == 2)
                               && c.ContactID == c.Contact.ContactID).ToList();
                var result = from invited in invitedProgram
                             join basic in basicDetail
                                  on invited.ProgramInvitationID equals basic.ProgramInvitationID
                             select new
                             {
                                 invited.ContactID,
                                 basic.FundingEntityName,
                                 basic.ProgramName,
                                 basic.BusinessName,
                                 basic.AffiliateName,
                                 basic.BusinessID,
                                 basic.ProgramInvitationID,
                             } into InvitedContact
                             join info in contactInvitationInfo
                             on InvitedContact.ContactID equals info.ContactID
                             select new InvitationDetail
                             {
                                 ProgramInvitationID= InvitedContact.ProgramInvitationID,
                                 FundingEntityName = InvitedContact.FundingEntityName,
                                 ProgramName = InvitedContact.ProgramName,
                                 BusinessName = InvitedContact.BusinessName,
                                 AffiliateName = InvitedContact.AffiliateName,
                                 InvitationEmailAddreess = info.InvitationEmailAddreess,
                                 FullName = String.Concat(info.Contact.FirstName, " ", info.Contact.MiddleName, " ", info.Contact.LastName),
                                 PhoneNo = info.Contact.PhoneNo,
                                 InvitationSentDateTime = info.ContactInvitedDateTime.ToString("dd/MM/yyyy"),
                             };
                details = result?.ToList();
            }
            if (details != null && details.Count > 0)
            {
                var consolidatedInvitationDetails = details
                                           .GroupBy(c => new
                                           {
                                               c.FundingEntityName,
                                               c.ProgramName,
                                               c.BusinessName,
                                               c.AffiliateName,
                                               c.InvitationEmailAddreess,
                                               c.FullName,
                                               c.PhoneNo,
                                               c.ProgramInvitationID
                                           })
                                            .Select(g => g.FirstOrDefault());
                details = consolidatedInvitationDetails?.OrderBy(o => o.FundingEntityName).ThenBy(o => o.BusinessName).ThenBy(o => o.AffiliateName).ThenBy(o => o.ProgramName).ThenBy(o => o.InvitationEmailAddreess).ToList();
            }
            return details;
        }
        private List<ActiveAccountDetail> GetActiveAccountDetail(DateTime _fromDate, DateTime _toDate, List<BasicDetail> basicDetail, ReportDetailRequest reportDetailRequest)
        {
            var details = new List<ActiveAccountDetail>();
            if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
            {
                var programBusiness = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true && reportDetailRequest.ProgramID.Contains(x.ProgramID)).Select(s => new ProgramInvitation
                {
                    ProgramInvitationID = s.ProgramInvitationID,
                }).ToList();

                var businessContacts = this._businessContactRepository.GetAll().Where(c => c.Contact.IsActive == true && c.IsActive == true &&
                                        c.Contact.Users.AccountActivationDate >= _fromDate && c.Contact.Users.AccountActivationDate <= _toDate && c.IsActive == true
                                        && c.Contact.Users.UserRoles.Any(x => x.RoleID == 2)
                                        && c.ContactID == c.Contact.ContactID).ToList();

                var result = from pb in programBusiness
                             join basic in basicDetail
                                  on pb.ProgramInvitationID equals basic.ProgramInvitationID
                             select new
                             {
                                 basic.FundingEntityName,
                                 basic.ProgramName,
                                 basic.BusinessName,
                                 basic.AffiliateName,
                                 basic.BusinessID,
                                 basic.ProgramInvitationID,
                             } into progBusiness
                             join bsc in businessContacts
                             on progBusiness.BusinessID equals bsc.BusinessID
                             select new ActiveAccountDetail
                             {
                                 //FundingEntityName = progBusiness.FundingEntityName,
                                 //ProgramName = progBusiness.ProgramName,
                                 //BusinessName = progBusiness.BusinessName,
                                 //AffiliateName = progBusiness.AffiliateName,
                                 EmailAddress = bsc.Contact.Users.UserName,
                                 FullName = String.Concat(bsc.Contact.FirstName, " ", bsc.Contact.MiddleName, " ", bsc.Contact.LastName),
                                 PhoneNo = bsc.Contact.PhoneNo,
                                 FirstLoginDateTime = bsc.Contact.Users.FirstLoginDateTime?.ToString("dd/MM/yyyy"),
                                 LastLoginDateTime = bsc.Contact.Users.LastLoginDateTime?.ToString("dd/MM/yyyy"),
                                 AccountActivationDate = bsc.Contact.Users.AccountActivationDate?.ToString("dd/MM/yyyy"),
                                 IsAccountActivated = bsc.Contact?.Users?.IsAccountActivated == true ? "Yes" : "No",
                                 IsLockedOut = bsc.Contact?.Users?.IsLockedOut == true ? "Yes" : "No",
                             };
                details = result?.ToList();
            }
            if (details != null && details.Count > 0)
            {
                var consolidatedActiveAccountDetail = details
                                           .GroupBy(c => new
                                           {
                                               //c.FundingEntityName,
                                               //c.ProgramName,
                                               //c.BusinessName,
                                               //c.AffiliateName,
                                               c.EmailAddress,
                                               c.FullName,
                                               c.PhoneNo,
                                               c.FirstLoginDateTime,
                                               c.LastLoginDateTime,
                                               c.AccountActivationDate,
                                           })
                                            .Select(g => g.FirstOrDefault());
                //details = consolidatedActiveAccountDetail?.OrderBy(o => o.FundingEntityName).ThenBy(o => o.BusinessName).ThenBy(o => o.AffiliateName).ThenBy(o => o.ProgramName).ThenBy(o => o.EmailAddress).ToList();
                details = consolidatedActiveAccountDetail?.OrderBy(o => o.EmailAddress).ToList();
            }
            return details;
        }
        private List<ApplicationSubmittedDetail> GetApplicationSubmittedDetail(DateTime _fromDate, DateTime _toDate, List<BasicDetail> basicDetail, ReportDetailRequest reportDetailRequest)
        {
            var details = new List<ApplicationSubmittedDetail>();
            if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
            {
                var submittedHistry = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && c.ToStateName == "Submitted").Select(s => new WorkflowProcessTransitionHistory
                {
                    ProcessInstanceID = s.ProcessInstanceID,
                }).ToList();
                var applicationSubmitted = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && reportDetailRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(s => new LoanApplication
                {
                    LoanNumber = s.LoanNumber,
                    LoanApplicationID = s.LoanApplicationID,
                    ProgramInvitationID = s.ProgramInvitationID,
                    IsConcentAccepted = s.IsConcentAccepted,
                    ConcentAcceptedDate = s.ConcentAcceptedDate,
                }).ToList();
                var result = from apllication in applicationSubmitted
                             join basic in basicDetail
                                  on apllication.ProgramInvitationID equals basic.ProgramInvitationID
                             select new
                             {
                                 apllication.LoanNumber,
                                 apllication.LoanApplicationID,
                                 apllication.IsConcentAccepted,
                                 apllication.ConcentAcceptedDate,
                                 basic.FundingEntityName,
                                 basic.ProgramName,
                                 basic.BusinessName,
                                 basic.AffiliateName,
                                 basic.BusinessID,
                                 basic.ProgramInvitationID,
                             } into submittted

                             join subHist in submittedHistry
                             on submittted.LoanApplicationID equals subHist.ProcessInstanceID
                             select new ApplicationSubmittedDetail
                             {
                                 FundingEntityName = submittted?.FundingEntityName,
                                 ProgramName = submittted?.ProgramName,
                                 BusinessName = submittted?.BusinessName,
                                 AffiliateName = submittted?.AffiliateName,
                                 LoanNumber = submittted?.LoanNumber,
                                 ConcentAcceptedDate = submittted?.ConcentAcceptedDate.ToString("dd/MM/yyyy"),
                                 IsConcentAccepted = submittted?.IsConcentAccepted == true ? "Yes" : "No",
                             };
                details = result?.ToList();
                if (details != null && details.Count > 0)
                {
                    var consolidatedInvitationDetails = details
                                               .GroupBy(c => new
                                               {
                                                   c.ProgramName,
                                                   c.BusinessName,
                                                   c.AffiliateName,
                                                   c.LoanNumber,
                                                   c.IsConcentAccepted,
                                                   c.ConcentAcceptedDate,
                                                   c.FundingEntityName,
                                                   
                                               })
                                                .Select(g => g.FirstOrDefault());
                    details = consolidatedInvitationDetails?.OrderBy(o => o.FundingEntityName).ThenBy(o => o.BusinessName).ThenBy(o => o.AffiliateName).ThenBy(o => o.ProgramName).ThenBy(o => o.LoanNumber).ToList();
                }
            }
            return details;
        }
        private List<ApplicationStartedDetail> GetApplicationStartedDetail(DateTime _fromDate, DateTime _toDate, List<BasicDetail> basicDetail, ReportDetailRequest reportDetailRequest)
        {
            var details = new List<ApplicationStartedDetail>();
            if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
            {
                var startedHistry = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && c.ToStateName == "Drafted").Select(s => new WorkflowProcessTransitionHistory
                {
                    ProcessInstanceID = s.ProcessInstanceID,
                }).ToList();
                var applicationStarted = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && c.ApplicationStatusID == 3 && reportDetailRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(s => new LoanApplication
                {
                    LoanNumber = s.LoanNumber,
                    LoanApplicationID = s.LoanApplicationID,
                    ProgramInvitationID = s.ProgramInvitationID,
                    IsConcentAccepted = s.IsConcentAccepted,
                    ConcentAcceptedDate = s.ConcentAcceptedDate,
                    LastModifiedDateTime = s.LastModifiedDateTime,
                }).ToList();
                var result = from apllication in applicationStarted
                             join basic in basicDetail
                                  on apllication.ProgramInvitationID equals basic.ProgramInvitationID
                             select new
                             {
                                 apllication.LoanNumber,
                                 apllication.LoanApplicationID,
                                 apllication.IsConcentAccepted,
                                 apllication.ConcentAcceptedDate,
                                 apllication.LastModifiedDateTime,
                                 basic.ProgramName,
                                 basic.BusinessName,
                                 basic.AffiliateName,
                                 basic.BusinessID,
                                 basic.ProgramInvitationID,
                                 basic.FundingEntityName,
                             } into started

                             join subHist in startedHistry
                             on started.LoanApplicationID equals subHist.ProcessInstanceID
                             select new ApplicationStartedDetail
                             {
                                 FundingEntityName = started?.FundingEntityName,
                                 ProgramName = started?.ProgramName,
                                 BusinessName = started?.BusinessName,
                                 AffiliateName = started?.AffiliateName,
                                 LoanNumber = started?.LoanNumber,
                                 ConcentAcceptedDate = started?.ConcentAcceptedDate.ToString("dd/MM/yyyy"),
                                 IsConcentAccepted = started?.IsConcentAccepted == true ? "Yes" : "No",
                                 ApplicationStartedDate= started?.LastModifiedDateTime.ToString("dd/MM/yyyy"),
                             };
                details = result?.ToList();
                if (details != null && details.Count > 0)
                {
                    var consolidatedInvitationDetails = details
                                               .GroupBy(c => new
                                               {
                                                   c.FundingEntityName,
                                                   c.ProgramName,
                                                   c.BusinessName,
                                                   c.AffiliateName,
                                                   c.LoanNumber,
                                                   c.IsConcentAccepted,
                                                   c.ConcentAcceptedDate,
                                                   c.ApplicationStartedDate,
                                               })
                                                .Select(g => g.FirstOrDefault());
                    details = consolidatedInvitationDetails?.OrderBy(o => o.FundingEntityName).ThenBy(o => o.BusinessName).ThenBy(o => o.AffiliateName).ThenBy(o => o.ProgramName).ThenBy(o => o.LoanNumber).ToList();
                }
            }
            return details;
        }
        private List<ApplicationFundedDetail> GetApplicationFundedDetail(DateTime _fromDate, DateTime _toDate, List<BasicDetail> basicDetail, ReportDetailRequest reportDetailRequest)
        {
            var details = new List<ApplicationFundedDetail>();
            if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
            {
                var fundedHistry = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && (c.ToStateName == "AccountDisbursed" || c.ToStateName == "FinalDisbursed" || c.ToStateName.EndsWith(" Disbursement"))).Select(s => new WorkflowProcessTransitionHistory
                {
                    ProcessInstanceID = s.ProcessInstanceID,
                }).ToList();
                var loanApplication = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && reportDetailRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(s => new
                {
                    LoanNumber = s.LoanNumber,
                    LoanApplicationID = s.LoanApplicationID,
                    ProgramInvitationID = s.ProgramInvitationID,
                    ProgramID = s.ProgramInvitation.ProgramID,
                }).ToList();
                var fundUtilization = this._fundUtilizationRepository.GetAll().Where(x => reportDetailRequest.ProgramID.Contains(x.FundingSourceID)).ToList();
                //var result = from apllication in loanApplication
                //             join basic in basicDetail on apllication.ProgramInvitationID equals basic.ProgramInvitationID
                //             join tras in fundUtilization on apllication.LoanApplicationID equals tras.ApplicationID
                //             select new
                //             {
                //                 apllication.LoanNumber,
                //                 apllication.LoanApplicationID,
                //                 basic.ProgramName,
                //                 basic.BusinessName,
                //                 basic.AffiliateName,
                //                 basic.BusinessID,
                //                 basic.ProgramInvitationID,
                //                 basic.FundingEntityName,
                //                 basic.FundingTIN,
                //                 basic.FundingEntityID,
                //                 basic.FundingEIN,
                //                 tras.TransactionAmount,
                //                 tras.TransactionDate,
                //                 basic.ProgramID,
                //             } into funded

                //             join fundedHist in fundedHistry
                //             on funded.LoanApplicationID equals fundedHist.ProcessInstanceID
                //             select new
                //             {
                //                 ProgramName = funded?.ProgramName,
                //                 BusinessName = funded?.BusinessName,
                //                 AffiliateName = funded?.AffiliateName,
                //                 LoanNumber = funded?.LoanNumber,
                //                 LoanApplicationID = funded.LoanApplicationID,
                //                 FundingEntityName = funded?.FundingEntityName,
                //                 FundingEIN = funded?.FundingEIN,
                //                 FundingTIN = funded?.FundingTIN,
                //                 TransactionAmount = funded?.TransactionAmount,
                //                 TransactionDate = funded?.TransactionDate,
                //                 ProgramID = funded?.ProgramID,
                //             };
                //details = result?.ToList();
                var result = from fundedHist in fundedHistry
                             join loan in loanApplication on fundedHist.ProcessInstanceID equals loan.LoanApplicationID
                             join basic in basicDetail on new { X1 = loan.ProgramInvitationID, X2 = loan.ProgramID } equals new { X1 = basic.ProgramInvitationID, X2 = basic.ProgramID }
                             select new
                             {
                                 ProgramName = basic.ProgramName,
                                 BusinessName = basic.BusinessName,
                                 AffiliateName = basic.AffiliateName,
                                 LoanNumber = loan.LoanNumber,
                                 FundingEntityName = basic.FundingEntityName,
                                 LoanApplicationID = loan.LoanApplicationID,
                                 ProgramId = basic.ProgramID,
                             };
                result = result.GroupBy(c => new
                {
                    c.ProgramName,
                    c.BusinessName,
                    c.AffiliateName,
                    c.LoanNumber,
                    c.FundingEntityName,
                    c.LoanApplicationID,
                    c.ProgramId,
                })
                   .Select(g => g.FirstOrDefault());
                if (result != null && result.Count() > 0)
                {
                    

                    var transactionResult = from r in result
                                            join tras in fundUtilization
                                            on r.LoanApplicationID equals tras.ApplicationID
                                            select new
                                            {
                                                ProgramName = r.ProgramName,
                                                BusinessName = r.BusinessName,
                                                AffiliateName = r.AffiliateName,
                                                LoanNumber = r.LoanNumber,
                                                FundingEntityName = r.FundingEntityName,
                                                LoanApplicationID = r.LoanApplicationID,
                                                ProgramID = r.ProgramId,
                                                TransactionAmount = tras.TransactionAmount,
                                                TransactionDate = tras.TransactionDate,
                                            };

                    // sum based on loan if part payment exist
                    var consolidatedInvitationDetails = transactionResult
                                               .GroupBy(c => new
                                               {
                                                   c.ProgramName,
                                                   c.BusinessName,
                                                   c.AffiliateName,
                                                   c.LoanNumber,
                                                   c.LoanApplicationID,
                                                   c.FundingEntityName,
                                                   c.ProgramID,
                                               })
                                                .Select(s => new ApplicationFundedDetail
                                                {
                                                    ProgramName = s.FirstOrDefault().ProgramName,
                                                    BusinessName = s.FirstOrDefault().BusinessName,
                                                    AffiliateName = s.FirstOrDefault().AffiliateName,
                                                    LoanNumber = s.FirstOrDefault().LoanNumber,
                                                    FundingEntityName = s.FirstOrDefault().FundingEntityName,
                                                    LoanApplicationID = s.FirstOrDefault().LoanApplicationID,
                                                    DateOfDisbursement = s.FirstOrDefault().TransactionDate.ToString("dd/MM/yyyy"),
                                                    FundedAmount = s.Sum(l => l.TransactionAmount) > 0 ? "$ " + String.Format("{0:#,#}", s.Sum(l => l.TransactionAmount)) : "$ 0",

                                                }).ToList();
                    // to show all part payment
                    //var consolidatedInvitationDetails = from t in result
                    //                                    group t by new
                    //                         {
                    //                             t.LoanNumber,
                    //                             t.LoanApplicationID,
                    //                             t.ProgramName,
                    //                             t.BusinessName,
                    //                             t.AffiliateName,
                    //                             t.FundingEntityName,
                    //                             t.ProgramID,
                    //                            // t.TransactionDate,

                    //                                    } into g
                    //                         select new ApplicationFundedDetail
                    //                         {
                    //                             ProgramName = g.Key.ProgramName,
                    //                             BusinessName = g.Key.BusinessName,
                    //                             AffiliateName = g.Key.AffiliateName,
                    //                             LoanNumber = g.Key.LoanNumber,
                    //                             FundingEntityName = g.Key.FundingEntityName,
                    //                             LoanApplicationID = g.Key.LoanApplicationID,
                    //                             DateOfDisbursement = g.Key.TransactionDate?.ToString("dd/MM/yyyy"),
                    //                             FundedAmount = g.Sum(s => s.TransactionAmount) > 0 ? "$ " + String.Format("{0:#,#}", g.Sum(s => s.TransactionAmount)) : "$ 0",

                    //                         };
                    details = consolidatedInvitationDetails?.OrderBy(o => o.FundingEntityName).ThenBy(o => o.BusinessName).ThenBy(o => o.AffiliateName).ThenBy(o => o.ProgramName).ThenBy(o => o.LoanNumber).ToList();
                }
            }
            return details;
        }
        private List<FundReleasedDetail> GetFundReleasedDetail(DateTime _fromDate, DateTime _toDate, List<BasicDetail> basicDetail, ReportDetailRequest reportDetailRequest)
        {
            var details = new List<FundReleasedDetail>();
            if (reportDetailRequest.ProgramID != null && reportDetailRequest.ProgramID.Count > 0)
            {
                var fundedHistry = this._workflowProcessTransitionHistoryRepository.GetAll().Where(c => c.TransitionTime >= _fromDate && c.TransitionTime <= _toDate && (c.ToStateName == "AccountDisbursed" || c.ToStateName == "FinalDisbursed" || c.ToStateName.EndsWith(" Disbursement"))).Select(s => new WorkflowProcessTransitionHistory
                {
                    ProcessInstanceID = s.ProcessInstanceID,
                }).ToList();
                var loanApplication = this._LoanApplicationRepository.GetAll().Where(c => c.IsActive == true && reportDetailRequest.ProgramID.Contains(c.ProgramInvitation.ProgramID)).Select(s => new
                {
                    LoanNumber = s.LoanNumber,
                    LoanApplicationID = s.LoanApplicationID,
                    ProgramInvitationID = s.ProgramInvitationID,
                    ProgramID = s.ProgramInvitation.ProgramID,
                }).ToList();
                var fundUtilization = this._fundUtilizationRepository.GetAll().Where(x => reportDetailRequest.ProgramID.Contains(x.FundingSourceID)).ToList();
                var result = from fundedHist in fundedHistry
                             join loan in loanApplication on fundedHist.ProcessInstanceID equals loan.LoanApplicationID
                             join basic in basicDetail on new { X1 = loan.ProgramInvitationID, X2 = loan.ProgramID } equals new { X1 = basic.ProgramInvitationID, X2 = basic.ProgramID }
                             select new
                             {
                                 ProgramName = basic.ProgramName,
                                 BusinessName = basic.BusinessName,
                                 AffiliateName = basic.AffiliateName,
                                 LoanNumber = loan.LoanNumber,
                                 FundingEntityName = basic.FundingEntityName,
                                 LoanApplicationID = loan.LoanApplicationID,
                                 ProgramId = basic.ProgramID,
                             };
                result = result.GroupBy(c => new
                {
                    c.ProgramName,
                    c.BusinessName,
                    c.AffiliateName,
                    c.LoanNumber,
                    c.FundingEntityName,
                    c.LoanApplicationID,
                    c.ProgramId,
                })
                             .Select(g => g.FirstOrDefault());
                var transactionResult = from r in result
                                        join tras in fundUtilization
                                        on r.LoanApplicationID equals tras.ApplicationID
                                        select new
                                        {
                                            ProgramName = r.ProgramName,
                                            BusinessName = r.BusinessName,
                                            AffiliateName = r.AffiliateName,
                                            LoanNumber = r.LoanNumber,
                                            FundingEntityName = r.FundingEntityName,
                                            LoanApplicationID = r.LoanApplicationID,
                                            ProgramID = r.ProgramId,
                                            TransactionAmount = tras.TransactionAmount,
                                            DateOfDisbursement = tras.TransactionDate,
                                        };

                var fundReleasedDetail = from t in transactionResult
                                         group t by new
                                         {
                                             t.LoanNumber,
                                             t.LoanApplicationID,
                                             t.ProgramName,
                                             t.BusinessName,
                                             t.AffiliateName,
                                             t.FundingEntityName,
                                             t.ProgramID,
                                             t.DateOfDisbursement,
                                         } into g
                                         select new FundReleasedDetail
                                         {
                                             ProgramName = g.Key.ProgramName,
                                             BusinessName = g.Key.BusinessName,
                                             AffiliateName = g.Key.AffiliateName,
                                             LoanNumber = g.Key.LoanNumber,
                                             FundingEntityName = g.Key.FundingEntityName,
                                             LoanApplicationID = g.Key.LoanApplicationID,
                                             DateOfDisbursement = g.Key.DateOfDisbursement.ToString("dd/MM/yyyy"),
                                             ReleasedAmount = g.Sum(s => s.TransactionAmount) > 0 ? "$ " + String.Format("{0:#,#}", g.Sum(s => s.TransactionAmount)) : "$ 0",
                                             
                                         };
                details = fundReleasedDetail?.OrderBy(o => o.FundingEntityName).ThenBy(o => o.BusinessName).ThenBy(o => o.AffiliateName).ThenBy(o => o.ProgramName).ThenBy(o => o.LoanNumber).ToList();
            }
            return details;
        }
        public ProgramInvitationResponse GetExportProgramInvitation(ExportProgramInvitationsRequest request, UserSessionEntity userSessionEntity)
        {  
            var response = this.GetProgramInvitationByProgram(request, userSessionEntity);
            if (response == null)
            {
                return response;
            }
            if(request!=null && request.FilterParameters!=null && request.FilterParameters.Count() > 0)
            {
                string searchBy = request.FilterParameters.FirstOrDefault().Key;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    searchBy = searchBy.Trim().ToLower();
                    var searchValue = request.FilterParameters.FirstOrDefault().Value;
                    if (searchBy == "businessname")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x =>x.BusinessName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "fundingentityname")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x => x.FundingEntityName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "programname")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x => x.ProgramName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "programstatus")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x => x.ProgramStatus.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "affiliatename")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x => x.AffiliateName.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "dateinvited")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x => x.DateInvited.Contains(searchValue)).ToList();
                    }
                    else if (searchBy == "all")
                    {
                        response.ProgramInvitations = response.ProgramInvitations.Where(x => x.BusinessName.Contains(searchValue)
                        || x.FundingEntityName.Contains(searchValue) || x.ProgramName.Contains(searchValue) || x.ProgramStatus.Contains(searchValue)
                        || x.AffiliateName.Contains(searchValue) || x.DateInvited.Contains(searchValue)).ToList();
                    }
                }
                
            }

            return response;


        }
        public ProgramInvitationResponse GetProgramInvitationByProgram(ProgramInvitationsRequest request, UserSessionEntity userSessionEntity)
        {
            ProgramInvitationResponse programInvitationResponse = new ProgramInvitationResponse();

            if (userSessionEntity == null)
            {
                Logger.LogError("Input Parameter user session is null");
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve Contact as user session entity is null.";
            }

            try
            {
              

                var ContactId = userSessionEntity.ContactID;
                var programInvitationListingViews = new List<ProgramInvitationListingView>();
                ThoughtFocus.DataAccess.Models.Contact.Contact contact = this._contactRepository.GetContactsByID(ContactId);

                //For admin role 
                if (contact != null && contact.Users != null && contact.Users.UserRoles != null && contact.Users.UserRoles.Where(x => (x.RoleID == 1 || x.RoleID == 3 || x.RoleID == 5 || x.RoleID == 7 || x.RoleID == 8) && x.IsActive == true).Count() > 0)
                {
                    if (request.ProgramID != null && request.ProgramID.Count() > 0 && request.ProgramID.FirstOrDefault() > 0)
                    {
                        var programInvitations = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true && request.ProgramID.Contains(x.ProgramID))
                           .Select(x => new ProgramInvitationListingView
                           {
                               ID = x.ProgramInvitationID,
                               ProgramID = x.ProgramID,
                               ProgramName = x.FundingSource != null ? x.FundingSource.ProgramName : string.Empty,
                               BusinessID = x.BusinessID,
                               BusinessName = x.BusinessEntity != null ? x.BusinessEntity.BusinessName : string.Empty,
                               ProgramStatus = x.ProgramStatus != null ? x.ProgramStatus.ProgramStatusName : string.Empty,
                               FundingEntityName = x.FundingSource != null ? x.FundingSource.FundingEntity != null ? x.FundingSource.FundingEntity.FundingEntityName : string.Empty : string.Empty,
                               DateInvited = string.Format("{0:MM/dd/yyyy}", x.CreatedDateTime),
                               AffiliateName = x.BusinessEntity != null ? x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty : string.Empty
                           }).OrderByDescending(x => x.ID)
                           .ToList();
                        programInvitationResponse.ProgramInvitations = programInvitations;

                        programInvitationResponse.Programs = programInvitations
                            .Select(x => new ProgramResponse
                            {
                                ProgramId = x.ProgramID,
                                ProgramName = x.ProgramName
                            }).Distinct().OrderBy(o => o.ProgramName).ToList();
                        programInvitationResponse.IsSuccess = true;
                    }
                    else
                    {
                        var programInvitations = this._programInvitationRepository.GetAll().Where(x => x.IsActive == true)
                       .Select(x => new ProgramInvitationListingView
                       {
                           ID = x.ProgramInvitationID,
                           ProgramID = x.ProgramID,
                           ProgramName = x.FundingSource != null ? x.FundingSource.ProgramName : string.Empty,
                           BusinessID = x.BusinessID,
                           BusinessName = x.BusinessEntity != null ? x.BusinessEntity.BusinessName : string.Empty,
                           ProgramStatus = x.ProgramStatus != null ? x.ProgramStatus.ProgramStatusName : string.Empty,
                           FundingEntityName = x.FundingSource != null ? x.FundingSource.FundingEntity != null ? x.FundingSource.FundingEntity.FundingEntityName : string.Empty : string.Empty,
                           DateInvited = string.Format("{0:MM/dd/yyyy}", x.CreatedDateTime),
                           AffiliateName = x.BusinessEntity != null ? x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty : string.Empty
                       }).OrderByDescending(x => x.ID)
                       .ToList();
                        programInvitationResponse.ProgramInvitations = programInvitations;

                        programInvitationResponse.Programs = programInvitations
                            .Select(x => new ProgramResponse
                            {
                                ProgramId = x.ProgramID,
                                ProgramName = x.ProgramName
                            }).Distinct().OrderBy(o => o.ProgramName).ToList();

                        programInvitationResponse.IsSuccess = true;
                    }
                }
                else
                {
                    long[] businessIDs = this._businessContactRepository.GetBusinessUserByContactID(ContactId).Where(x => x.BusinessRoleID != 4)
                                    .Select(x => x.BusinessID)
                                    .ToArray();

                    if (businessIDs != null && businessIDs.Count() > 0)
                    {
                        var programInvitations = this._programInvitationRepository.GetProgramInvitationByBusinessID(businessIDs).Where(x => x.ProgramStatusID == 1 && x.IsActive == true)
                        .Select(x => new ProgramInvitationListingView
                        {
                            ID = x.ProgramInvitationID,
                            ProgramID = x.ProgramID,
                            ProgramName = x.FundingSource != null ? x.FundingSource.ProgramName : string.Empty,
                            BusinessID = x.BusinessID,
                            BusinessName = x.BusinessEntity != null ? x.BusinessEntity.BusinessName : string.Empty,
                            ProgramStatus = x.ProgramStatus != null ? x.ProgramStatus.ProgramStatusName : string.Empty,
                            FundingEntityName = x.FundingSource != null ? x.FundingSource.FundingEntity != null ? x.FundingSource.FundingEntity.FundingEntityName : string.Empty : string.Empty,
                            DateInvited = string.Format("{0:MM/dd/yyyy}", x.CreatedDateTime),
                            AffiliateName = x.BusinessEntity != null ? x.BusinessEntity.Affiliate != null ? x.BusinessEntity.Affiliate.AffiliateName : string.Empty : string.Empty
                        }).OrderByDescending(x => x.ID)
                        .ToList();

                        if (request.ProgramID != null && request.ProgramID.Count() > 0 && request.ProgramID.FirstOrDefault() > 0)
                        {
                            programInvitations = programInvitations.Where(x => request.ProgramID.Contains(x.ProgramID)).ToList();
                        }
                        programInvitationResponse.ProgramInvitations = programInvitations;

                        programInvitationResponse.Programs = programInvitations
                            .Select(x => new ProgramResponse
                            {
                                ProgramId = x.ProgramID,
                                ProgramName = x.ProgramName
                            }).Distinct().OrderBy(o => o.ProgramName).ToList();
                        programInvitationResponse.IsSuccess = true;
                    }
                }
                if (programInvitationResponse.ProgramInvitations != null && programInvitationResponse.ProgramInvitations.Count() > 0)
                {
                    if (contact.ProgramInvitationContactRoles != null && contact.ProgramInvitationContactRoles.Count() > 0)
                    {
                        var programInvitationContactRoles = contact.ProgramInvitationContactRoles.Where(pc => pc.IsActive == true).ToList();
                        if (programInvitationContactRoles != null && programInvitationContactRoles.Count() > 0)
                        {
                            if (programInvitationContactRoles.FirstOrDefault().ProgramID > 0)
                            {
                                foreach (var pr in programInvitationContactRoles)
                                {
                                    try
                                    {
                                        var programInvitationContactRole = programInvitationResponse.ProgramInvitations.FindAll(p => p.ProgramID == pr.ProgramID);

                                        if (programInvitationContactRole != null && programInvitationContactRole.Count > 0)
                                        {
                                            programInvitationListingViews.AddRange(programInvitationContactRole);
                                        }
                                    }
                                    catch
                                    {

                                    }

                                }
                                if (programInvitationListingViews != null && programInvitationListingViews.Count > 0)
                                {
                                    var piv = from pil in programInvitationListingViews
                                              orderby pil.ID descending
                                              select pil;
                                    programInvitationResponse.ProgramInvitations = piv.ToList();
                                }
                                else
                                {
                                    programInvitationResponse.ProgramInvitations = new List<ProgramInvitationListingView>();
                                }

                            }
                        }

                    }
                }
                
                if (programInvitationResponse.ProgramInvitations != null && programInvitationResponse.ProgramInvitations.Count() < 1)
                {
                    programInvitationResponse.IsSuccess = true;
                    programInvitationResponse.Message = "No record found!.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramInvitation", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetProgramInvitation", null);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programInvitationResponse;
        }
       
        public ConsolidatedFundReportDataResponse GetConsolidatedFundReportData(FundReportRequest request, UserSessionEntity userSessionEntity)
        {
            var response = new ConsolidatedFundReportDataResponse();

            try
            {
                var ConsolidatedFundDetail = new List<ConsolidatedFundDetail>();
                var basicDetail = new List<BasicDetail>();
                response.BusinessEntities = this._businessEntityRepository.GetBusinessEntity().Where(c => c.IsActive == true)
                  .Select(x => new BusinessEntities
                  {
                      ID = x.ID,
                      BusinessName = x.BusinessName,
                  }).Distinct().OrderBy(o => o.BusinessName).ToList();

                response.Programs = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true)
                   .Select(x => new ProgramResponse
                   {
                       ProgramId = x.FundingSourceID,
                       ProgramName = x.ProgramName
                   }).Distinct().OrderBy(o => o.ProgramName).ToList();

                // // Entity: all, Program: all
                if ((request.BusinessEntityID == null || request.BusinessEntityID.Count() == 0 || request.BusinessEntityID.FirstOrDefault() == 0) && (request.ProgramID.FirstOrDefault() == 0))
                {
                    var fundingSourceID = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).Select(c => c.FundingSourceID).ToList();
                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        request.ProgramID = programIds;
                    }
                    
                }
                else if ((request.ProgramID == null || request.ProgramID.Count() == 0 || request.ProgramID.FirstOrDefault() == 0) && request.BusinessEntityID != null && (request.BusinessEntityID.Count() > 0 || request.BusinessEntityID.FirstOrDefault() > 0))
                {
                    // Entity: selected, Program: all

                    var fundingSourceID = this._programInvitationRepository.GetAll().Where(c => c.IsActive == true && request.BusinessEntityID.Contains(c.BusinessID)).Select(c => c.ProgramID).Distinct().ToList();

                    if (fundingSourceID != null)
                    {
                        List<long?> programIds = new List<long?>();
                        foreach (var id in fundingSourceID)
                        {
                            programIds.Add(id);
                        }
                        request.ProgramID = programIds;
                    }
                    
                }
                

                if (request.ProgramID != null && request.ProgramID.Count > 0)
                {
                    if(request.BusinessEntityID.FirstOrDefault() > 0)
                    {
                        basicDetail = this._programInvitationRepository.GetAll().Where(c => c.IsActive == true && request.BusinessEntityID.Contains(c.BusinessID) && request.ProgramID.Contains(c.ProgramID)).OrderBy(o => o.BusinessEntity.BusinessName)
                           .Select(x => new BasicDetail
                           {
                               BusinessID = x.BusinessEntity.ID,
                               ProgramID = x.FundingSource == null ? 0 : x.FundingSource.FundingSourceID,
                               ProgramName = x.FundingSource == null ? "" : x.FundingSource.ProgramName,
                               BusinessName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.BusinessEntity.BusinessName),
                           }).ToList();
                    }
                    else
                    {
                        basicDetail = this._programInvitationRepository.GetAll().Where(c => c.IsActive == true && request.ProgramID.Contains(c.ProgramID)).OrderBy(o => o.BusinessEntity.BusinessName)
                          .Select(x => new BasicDetail
                          {
                              BusinessID = x.BusinessEntity.ID,
                              ProgramID = x.FundingSource == null ? 0 : x.FundingSource.FundingSourceID,
                              ProgramName = x.FundingSource == null ? "" : x.FundingSource.ProgramName,
                              BusinessName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.BusinessEntity.BusinessName),
                          }).ToList();
                    }

                    if (basicDetail != null && basicDetail.Count > 0)
                    {
                        var uniqueBasicDetail = basicDetail
                                                   .GroupBy(c => new
                                                   {
                                                       c.BusinessID,
                                                       c.ProgramID,
                                                       c.BusinessName,
                                                       c.ProgramName,                                                       
                                                   })
                                                    .Select(g => g.FirstOrDefault());
                        basicDetail = uniqueBasicDetail?.OrderBy(o => o.BusinessName).ToList();
                    }

                    var FundTransaction = this._fundUtilizationRepository.GetAllFundTransaction().Where(t=> request.ProgramID.Contains(t.FundingSourceID) && t.IsActive == true).Select(y => new
                    {
                        TransactionTypeId = y.TransactionTypeID,
                        TransactionAmount = y.TransactionAmount,
                        ProgramID = y.FundingSourceID,
                    }).ToList();

                    var PaymentScheduleSummary = this._fundingSourceRepository.GetAllPaymentScheduleSummary().Where(x => request.ProgramID.Contains(x.ProgramID) && x.IsActive == true)
                                         .Select(y => new
                                         {
                                             FundAllocatedAmount = y.FundAllocatedAmount,
                                             FundDisbursedAmount = y.FundDisbursedAmount,
                                             ProgramID = y.ProgramID,
                                         });



                    var AddTransaction = FundTransaction.Where(x => x.TransactionTypeId == 1).Select(y => new
                    {
                        TransactionAmount = y.TransactionAmount,
                        ProgramID = y.ProgramID,
                    });
                    var RemovedTransaction = FundTransaction.Where(x => x.TransactionTypeId == 2).Select(y => new
                    {
                        TransactionAmount = y.TransactionAmount,
                        ProgramID = y.ProgramID,
                    });

                    

                    var PaymentSchedule = from p in PaymentScheduleSummary
                                        group p by new
                                        {
                                            p.ProgramID,
                                        } into pay
                                        select new
                                        {
                                            ProgramID = pay.Key.ProgramID,
                                            FundAllocatedAmount = pay.Sum(s => s.FundAllocatedAmount),
                                            FundDisbursedAmount = pay.Sum(s => s.FundDisbursedAmount),
                                        };


                    var FundAdded = from t in AddTransaction
                                          group t by new
                                          {
                                              t.ProgramID,
                                          } into g
                                          select new
                                          {
                                              ProgramID = g.Key.ProgramID,
                                              TotalAddedAmount = g.Sum(s => s.TransactionAmount),
                                          };
                    var FundRemove = from r in RemovedTransaction
                                      group r by new
                                      {
                                          r.ProgramID,
                                      } into rm
                                      select new
                                      {
                                          ProgramID = rm.Key.ProgramID,
                                          TotalRemovedAmount = rm.Sum(s => s.TransactionAmount),
                                      };

                    if (FundAdded != null)
                    {
                       
                        foreach (var fund in basicDetail)
                        {
                         var fundDetail = new ConsolidatedFundDetail();
                         fundDetail.ProgramName= fund.ProgramName;
                         fundDetail.BusinessName = fund.BusinessName;
                         if (PaymentSchedule != null)
                            {
                              var payment = PaymentSchedule.Where(r => r.ProgramID == fund.ProgramID).FirstOrDefault();
                               if (payment != null)
                                {
                                  fundDetail.FundsAllocated = payment.FundAllocatedAmount >0 ? "$ " + String.Format("{0:#,#}", payment.FundAllocatedAmount) : "$ 0";
                                  fundDetail.FundedDisbursed = payment.FundDisbursedAmount > 0 ? "$ " + String.Format("{0:#,#}", payment.FundDisbursedAmount) : "$ 0";
                                }
                                else
                                {
                                    fundDetail.FundsAllocated = "$ 0";
                                    fundDetail.FundedDisbursed = "$ 0";
                                }

                            }
                         if (FundRemove != null)
                           {
                            var remove = FundRemove.Where(r => r.ProgramID == fund.ProgramID).FirstOrDefault();
                            var added = FundAdded.Where(r => r.ProgramID == fund.ProgramID).FirstOrDefault();
                              if (remove != null)
                                 {
                                    decimal netAmount= (added.TotalAddedAmount - remove.TotalRemovedAmount);
                                    fundDetail.FundedAmount = netAmount > 0 ? "$ " + String.Format("{0:#,#}", netAmount) : "$ 0";
                                }
                               else if(added!=null)
                                  {
                                     fundDetail.FundedAmount = added.TotalAddedAmount > 0 ? "$ " + String.Format("{0:#,#}", added.TotalAddedAmount) : "$ 0";
                                }
                                else
                                {
                                    fundDetail.FundedAmount = "$ 0";
                                }

                           }
                          ConsolidatedFundDetail.Add(fundDetail);
                        }
                        response.ConsolidatedFundDetails= ConsolidatedFundDetail;
                    }
                    
                }
                
                return response;


            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetConsolidatedFundReportData", null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(Logger, null, ex, "Exception in AdminService-> GetConsolidatedFundReportData", null);
            }
            return response;
        }
        #endregion Methods
    }
}