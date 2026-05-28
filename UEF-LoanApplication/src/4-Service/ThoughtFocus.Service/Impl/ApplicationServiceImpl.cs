using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Domain;
using ThoughtFocus.Constants;
using ThoughtFocus.Domain.User;
using FluentValidation;
using ThoughtFocus.Business.Interfaces.Application;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Workflow;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Domain.Enumeration;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.Validations.InputParameterValidation.Application;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Repository.Interfaces.Notification;
using Microsoft.Extensions.Options;
using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Common.Workflow.Core.Model;
using ThoughtFocus.Common.Workflow.Core.PersistanceModel;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models.Admin;

namespace ThoughtFocus.Service.Impl
{
    public class ApplicationServiceImpl : IApplicationService
    {

        #region Fields
        private readonly ILogger<ApplicationServiceImpl> _Logger;
        private readonly IMapper _Mapper;
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        private readonly IProgramInvitationRepository _ProgramInvitationRepository;
        private readonly ILoanApplication _loanApplication;
        private long WorkFlowID = 2;//New Change to 2
        private string _StateName = "";
        private IUserRepository _userRepository;
        private readonly IApplicationDocumentRepository _applicationDocumentRepository;
        private readonly IDocumentInformationProvider _documentInformationProvider;
        private IMasterService _masterService;
        private readonly IBusinessOwnerRepository _BusinessOwnerRepository;
        private readonly IBusinessOwnerMasterRepository _businessOwnerMasterRepository;
        private readonly IMapper _automapper;
        private readonly ILoanBusinessDetailRepository _loanBusinessDetailRepository;
        private readonly ILoanBusinessDetailMasterRepository _loanBusinessDetailMasterRepository;
        private readonly IFundingApplicationRepository _fundingApplicationRepository;
        private readonly IQuestionResponseRepository _questionResponseRepository;
        private readonly IUrbanLeagueAffiliateRepository _affiliateContactsRepository;
        public readonly IFundUtilizationRepository _fundUtilizationRepository;
        public readonly IBusinessContactRepository _businessContactRepository;
        private readonly ILoanApplicationAgreementDetailRepository _LoanApplicationAgreementDetailRepository;
        private readonly IAdminService _adminService;
        private readonly INotificationModeRepository _notificationModeRepository;
        private readonly IProgramInviteeRepository _programInviteeRepository;
        private readonly IFundingSourceRepository _fundingSourceRepository;
        private readonly IFundingTypeRepository _fundingTypeRepository;
        private readonly IGenaralOptionRepository _genaralOptionRepository;
        private readonly AppSettings _appSettings;
        private INotificationService _notificationService;
      
        #endregion Fields

        #region Constructors
        public ApplicationServiceImpl(
            ILogger<ApplicationServiceImpl> logger,
            IMapper mapper,
            ILoanApplicationRepository loanApplicationRepository,
            ILoanApplication loanApplication,
            IUserRepository userRepository,
            IProgramInvitationRepository programInvitationRepository,
            IApplicationDocumentRepository applicationDocumentRepository,
            IDocumentInformationProvider documentInformationProvider,
            IMasterService masterService,
            IBusinessOwnerRepository businessOwnerRepository,
            ILoanBusinessDetailRepository loanBusinessDetailRepository,
            IMapper autoMapper,
            IFundingApplicationRepository fundingApplicationRepository,
            IQuestionResponseRepository questionResponseRepository,
            IUrbanLeagueAffiliateRepository affiliateContactsRepository,
            IFundUtilizationRepository fundUtilizationRepository,
            IBusinessContactRepository businessContactRepository,
            ILoanApplicationAgreementDetailRepository LoanApplicationAgreementDetailRepository,
            IBusinessOwnerMasterRepository BusinessOwnerMasterRepository,
             ILoanBusinessDetailMasterRepository LoanBusinessDetailMasterRepository,
            IAdminService adminService,
            INotificationModeRepository notificationModeRepository, IOptions<AppSettings> appSettings, INotificationService notificationService,
            IProgramInviteeRepository programInviteeRepository, IFundingSourceRepository fundingSourceRepository, IFundingTypeRepository fundingTypeRepository, IGenaralOptionRepository genaralOptionRepository
            )
        {
            this._Logger = logger;
            this._Mapper = mapper;
            this._LoanApplicationRepository = loanApplicationRepository;
            this._loanApplication = loanApplication;
            this._userRepository = userRepository;
            this._ProgramInvitationRepository = programInvitationRepository;
            this._applicationDocumentRepository = applicationDocumentRepository;
            this._documentInformationProvider = documentInformationProvider;
            this._masterService = masterService;
            this._BusinessOwnerRepository = businessOwnerRepository;
            this._loanBusinessDetailRepository = loanBusinessDetailRepository;
            this._automapper = mapper;
            this._fundingApplicationRepository = fundingApplicationRepository;
            this._questionResponseRepository = questionResponseRepository;
            this._affiliateContactsRepository = affiliateContactsRepository;
            this._fundUtilizationRepository = fundUtilizationRepository;
            _businessContactRepository = businessContactRepository;
            _LoanApplicationAgreementDetailRepository = LoanApplicationAgreementDetailRepository;
            this._businessOwnerMasterRepository = BusinessOwnerMasterRepository;

            this._loanBusinessDetailMasterRepository = LoanBusinessDetailMasterRepository;
            this._adminService = adminService;
            
            _notificationModeRepository = notificationModeRepository;
            _programInviteeRepository = programInviteeRepository;
            this._fundingSourceRepository = fundingSourceRepository;
            this._fundingTypeRepository = fundingTypeRepository;
            this._genaralOptionRepository = genaralOptionRepository;
            this._appSettings = appSettings.Value;
            this._notificationService = notificationService;
            
        }

        #endregion Constructors

        #region Methods

        public CommonResponse SaveLoanApplication(LoanApplicationRequest loanApplicationRequest, UserSessionEntity userSessionEntity)
        {
            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;
            List<string> validationMessages = new List<string>();
            if (loanApplicationRequest == null)
            {
                _Logger.LogError("Input Parameter loanApplicationRequest is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }

            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }


            try
            {
                if (loanApplicationRequest.ApplicationStatusID < (long)ApplicationStatusEnumeration.Submitted)
                {
                    //Save business profile master data
                    var programInvite = _ProgramInvitationRepository.GetAll().Where(p => p.ProgramInvitationID == loanApplicationRequest.ProgramInvitationID).FirstOrDefault();

                    var businessOwnerMasters = _businessOwnerMasterRepository.GetAll().Where(b => b.BusinessID == programInvite.BusinessID).ToList();

                    foreach (var owner in businessOwnerMasters)
                    {
                        owner.IsActive = false;
                    }

                    foreach (var owner in loanApplicationRequest.BusinessOwners)
                    {
                        var businessOwnerMaster = _Mapper.Map<BusinessOwnerMaster>(owner);
                        businessOwnerMaster.IsActive = true;
                        businessOwnerMaster.ID = 0;
                        businessOwnerMaster.BusinessID = programInvite.BusinessID;
                        businessOwnerMasters.Add(businessOwnerMaster);
                    }

                    var loanBusinessDetailsMaster = _Mapper.Map<LoanBusinessDetailMaster>(loanApplicationRequest.LoanBusinessDetails);
                    _businessOwnerMasterRepository.SaveOrUpdateBusinessOwnerMaster(businessOwnerMasters, userSessionEntity.UserID);
                    var loadBusinessID = _loanBusinessDetailMasterRepository.GetAll().Where(l => l.BusinessID == programInvite.BusinessID).FirstOrDefault();
                    loanBusinessDetailsMaster.ID = loadBusinessID != null ? loadBusinessID.ID : 0;
                    loanBusinessDetailsMaster.BusinessID = programInvite.BusinessID;
                    _loanBusinessDetailMasterRepository.SaveOrUpdateLoanBusinessDetailsMaster(loanBusinessDetailsMaster, userSessionEntity.UserID);

                }
                /////

                this.SaveLoanBusinessDetailAndOwner(loanApplicationRequest, userSessionEntity);

                this.SaveFundingApplicationData(loanApplicationRequest, userSessionEntity);
                this.SaveLoanApplicationDocuments(loanApplicationRequest, userSessionEntity);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                commonCreationParam.ID = loanApplicationRequest.LoanApplicationID;
            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }

            return commonCreationParam;
        }

        public CommonResponse SaveBusinessProfileData(BusinessProfileRequest businessProfileRequest, UserSessionEntity userSessionEntity)
        {

            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;
            List<string> validationMessages = new List<string>();
            if (businessProfileRequest == null)
            {
                _Logger.LogError("Input Parameter businessProfileRequest is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }

            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }


            try
            {
               
                var businessOwnerMasters = _businessOwnerMasterRepository.GetAll().Where(a => a.BusinessID == businessProfileRequest.LoanBusinessDetailMasterParam.BusinessID).ToList();

                foreach (var owner in businessOwnerMasters)
                {
                    owner.IsActive = false;
                }

                foreach (var owner in businessProfileRequest.BusinessOwnerMasterParam)
                {
                    var businessOwnerMaster = _Mapper.Map<BusinessOwnerMaster>(owner);
                    businessOwnerMaster.IsActive = true;
                    businessOwnerMaster.ID = 0;
                    businessOwnerMasters.Add(businessOwnerMaster);
                }

                var noOfYearsInBusiness = businessProfileRequest.LoanBusinessDetailMasterParam.NumberOfYearsInBusiness;
                var employeeStrength = businessProfileRequest.LoanBusinessDetailMasterParam.EmployeeStrength;

                var loanBusinessDetailsMaster = _Mapper.Map<LoanBusinessDetailMaster>(businessProfileRequest.LoanBusinessDetailMasterParam);
                loanBusinessDetailsMaster.NumberOfYearsInBusiness = noOfYearsInBusiness ?? 123456789;
                loanBusinessDetailsMaster.EmployeeStrength = employeeStrength ?? 123456789;
                _businessOwnerMasterRepository.SaveOrUpdateBusinessOwnerMaster(businessOwnerMasters, userSessionEntity.UserID);
                var loadBusinessID = _loanBusinessDetailMasterRepository.GetAll().Where(l => l.ID == businessProfileRequest.LoanBusinessDetailMasterParam.ID).FirstOrDefault();
                loanBusinessDetailsMaster.ID = loadBusinessID != null ? loadBusinessID.ID : 0;
                _loanBusinessDetailMasterRepository.SaveOrUpdateLoanBusinessDetailsMaster(loanBusinessDetailsMaster, userSessionEntity.UserID);

                var programInvitations = _ProgramInvitationRepository.GetAll().Where(p => p.BusinessID == loanBusinessDetailsMaster.BusinessID).ToList();
                //This is for new program
                foreach (var programInvitation in programInvitations)
                {
                    var loanApplication = _LoanApplicationRepository.GetAll().Where(l => l.ProgramInvitationID == programInvitation.ProgramInvitationID && l.IsActive == true).FirstOrDefault();
                    if (loanApplication != null && loanApplication.ApplicationStatusID < 4)
                    {
                        var businessOwners = loanApplication.BusinessOwners != null ?
                                                loanApplication.BusinessOwners.ToList() : new List<BusinessOwner>();
                        loanApplication.LoanBusinessDetail = loanApplication.LoanBusinessDetail ?? new LoanBusinessDetail();

                        if (loanApplication.LoanApplicationID > 0)
                        {
                            foreach (var owner in businessOwners)
                            {
                                owner.IsActive = false;
                            }

                            foreach (var businessOwnerMaster in businessProfileRequest.BusinessOwnerMasterParam)
                            {
                                BusinessOwner businessOwner = new BusinessOwner();
                                businessOwner = this._Mapper.Map<BusinessOwner>(businessOwnerMaster);
                                businessOwner.IsActive = true;
                                businessOwner.ID = 0;
                                businessOwner.LoanApplicationID = loanApplication.LoanApplicationID;
                                businessOwners.Add(businessOwner);

                            }
                        }
                        else
                        {
                            foreach (var owner in businessProfileRequest.BusinessOwnerMasterParam)
                            {
                                BusinessOwner BusinessOwner = this._Mapper.Map<BusinessOwner>(owner);
                                BusinessOwner.IsActive = true;
                                businessOwners.Add(BusinessOwner);
                            }

                        }

                        LoanBusinessDetail loanBusinessDetailsMapped = new LoanBusinessDetail();

                        loanBusinessDetailsMapped = this._Mapper.Map<LoanBusinessDetail>(loanBusinessDetailsMaster);
                        loanBusinessDetailsMapped.LoanApplicationID = loanApplication.LoanApplicationID;
                        loanBusinessDetailsMapped.ID = loanApplication.LoanBusinessDetail.ID;

                        _BusinessOwnerRepository.SaveOrUpdateBusinessOwner(businessOwners, userSessionEntity.UserID);
                        _loanBusinessDetailRepository.SaveOrUpdateLoanBusinessDetails(loanBusinessDetailsMapped, userSessionEntity.UserID);

                    }

                }

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);

            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }

            return commonCreationParam;
        }

        private CommonResponse SaveLoanBusinessDetailAndOwner(LoanApplicationRequest loanApplicationRequest, UserSessionEntity userSessionEntity)
        {
            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;
            List<string> validationMessages = new List<string>();
            if (loanApplicationRequest == null)
            {
                _Logger.LogError("Input Parameter loanApplicationRequest is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }

            try
            {
                LoanApplication LoanApplication = null;

                //Map business profile param into loan application's business profile
                if (loanApplicationRequest.LoanApplicationID > 0)
                {
                    LoanApplication = this._LoanApplicationRepository.GetAll().Where(x => x.LoanApplicationID == loanApplicationRequest.LoanApplicationID).FirstOrDefault();
                    loanApplicationRequest.LoanBusinessDetails.LoanApplicationID = LoanApplication.LoanBusinessDetail.LoanApplicationID;
                    loanApplicationRequest.LoanBusinessDetails.ID = LoanApplication.LoanBusinessDetail.ID;
                    LoanApplication.LoanBusinessDetail = this._Mapper.Map<LoanBusinessDetail>(loanApplicationRequest.LoanBusinessDetails);
                    LoanApplication.LastModifiedByUserID = userSessionEntity.UserID;
                    LoanApplication.LastModifiedDateTime = currentDate;
                    LoanApplication.IsActive = true;
                    List<BusinessOwner> removeBusinessOwners = new List<BusinessOwner>();
                    foreach (var owner in LoanApplication.BusinessOwners)
                    {
                        owner.IsActive = false;
                    }
                    foreach (var businessowner in loanApplicationRequest.BusinessOwners)
                    {
                        businessowner.ID = 0;
                        businessowner.LoanApplicationID = loanApplicationRequest.LoanApplicationID;
                        BusinessOwner BusinessOwner = this._Mapper.Map<BusinessOwner>(businessowner);
                        BusinessOwner.IsActive = true;
                        LoanApplication.BusinessOwners.Add(BusinessOwner);
                    }
                }
                else
                {
                    LoanApplication = this._Mapper.Map<LoanApplication>(loanApplicationRequest);
                    LoanApplication.IsConcentAccepted = true;
                    LoanApplication.ConcentAcceptedDate = currentDate;
                    LoanApplication.CreatedByUserID = userSessionEntity.UserID;
                    LoanApplication.CreatedDateTime = currentDate;
                    LoanApplication.LastModifiedByUserID = userSessionEntity.UserID;
                    LoanApplication.LastModifiedDateTime = currentDate;
                    LoanApplication.IsActive = true;
                    LoanApplication.DateApplied = currentDate;
                    LoanApplication.ApplicationStatusID = 1;
                    LoanApplication.ApplicationTypeID = 1;
                    LoanApplication.LoanBusinessDetail = this._Mapper.Map<LoanBusinessDetail>(loanApplicationRequest.LoanBusinessDetails);
                    LoanApplication.BusinessOwners = new List<BusinessOwner>();
                    foreach (var owner in loanApplicationRequest.BusinessOwners)
                    {

                        BusinessOwner BusinessOwner = this._Mapper.Map<BusinessOwner>(owner);
                        BusinessOwner.IsActive = true;
                        BusinessOwner.ID = 0;
                        LoanApplication.BusinessOwners.Add(BusinessOwner);
                    }
                }
                //Assign loan number for newly generating loan application
                if (loanApplicationRequest.LoanApplicationID == 0)
                {
                    long count = this._loanBusinessDetailRepository.GetAll().Count();
                    LoanApplication.LoanNumber = _loanApplication.GenerateLoanNumber(loanApplicationRequest, count + 1);
                }
                else
                {
                    LoanApplication.LoanNumber = this._LoanApplicationRepository.GetAll().Where(x => x.LoanApplicationID == loanApplicationRequest.LoanApplicationID).FirstOrDefault().LoanNumber;
                }

                //call repository method to save loan application
                this._LoanApplicationRepository.SaveLoanApplicationDetails(LoanApplication, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                commonCreationParam.ID = LoanApplication.LoanApplicationID;
                loanApplicationRequest.LoanApplicationID = LoanApplication.LoanApplicationID;
            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }

            return commonCreationParam;
        }

        private CommonResponse SaveFundingApplicationData(LoanApplicationRequest loanApplicationRequest, UserSessionEntity userSessionEntity)
        {
            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;
            List<string> validationMessages = new List<string>();
            if (loanApplicationRequest == null)
            {
                _Logger.LogError("Input Parameter loanApplicationRequest is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }

            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }

            try
            {
                LoanApplication LoanApplication = this._Mapper.Map<LoanApplication>(loanApplicationRequest);

                if (loanApplicationRequest.LoanApplicationID > 0)
                {
                    LoanApplication.FundingApplication = this._fundingApplicationRepository.GetAll().AsNoTracking().FirstOrDefault(a => a.LoanApplicationID == loanApplicationRequest.LoanApplicationID);
                    loanApplicationRequest.FundingApplication.LoanApplicationID = LoanApplication.FundingApplication != null ? LoanApplication.FundingApplication.LoanApplicationID : loanApplicationRequest.LoanApplicationID;
                    loanApplicationRequest.FundingApplication.ID = LoanApplication.FundingApplication != null ? LoanApplication.FundingApplication.ID : 0;
                    LoanApplication.FundingApplication = this._Mapper.Map<FundingApplication>(loanApplicationRequest.FundingApplication);
                    LoanApplication.FundingApplication.ProgramID = _ProgramInvitationRepository.GetProgramInvitation(loanApplicationRequest.ProgramInvitationID) != null ? _ProgramInvitationRepository.GetProgramInvitation(loanApplicationRequest.ProgramInvitationID).ProgramID : 0;

                    List<QuestionResponse> questionResponses = new List<QuestionResponse>();

                    foreach (var questionResponse in loanApplicationRequest.FundingApplication.QuestionResponse)
                    {
                        QuestionResponse questionRes = this._questionResponseRepository.GetAll().AsNoTracking().FirstOrDefault(a => a.LoanApplicationID == loanApplicationRequest.LoanApplicationID && a.QuestionID == questionResponse.QuestionID);
                        questionResponse.ID = questionRes != null ? questionRes.ID : 0;
                        questionResponse.LoanApplicationID = questionRes != null ? questionRes.LoanApplicationID : loanApplicationRequest.LoanApplicationID;
                        QuestionResponse questionResponsemapped = this._Mapper.Map<QuestionResponse>(questionResponse);
                        questionResponsemapped.LoanApplicationID = loanApplicationRequest.LoanApplicationID;
                        LoanApplication.QuestionResponse.Add(questionResponsemapped);
                    }
                }
                else
                {
                    LoanApplication.FundingApplication = this._Mapper.Map<FundingApplication>(loanApplicationRequest.FundingApplication);
                    LoanApplication.QuestionResponse = this._Mapper.Map<List<QuestionResponse>>(loanApplicationRequest.FundingApplication.QuestionResponse);
                }

                this._fundingApplicationRepository.SaveOrUpdateLoanFundingApplication(LoanApplication.FundingApplication, userSessionEntity.UserID);
                this._questionResponseRepository.SaveOrUpdateQuestionResponses(LoanApplication.QuestionResponse, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                commonCreationParam.ID = LoanApplication.LoanApplicationID;
            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }
            return commonCreationParam;
        }

        private CommonResponse SaveLoanApplicationDocuments(LoanApplicationRequest loanApplicationRequest, UserSessionEntity userSessionEntity)
        {
            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;
            List<string> validationMessages = new List<string>();
            if (loanApplicationRequest == null)
            {
                _Logger.LogError("Input Parameter loanApplicationRequest is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                validationMessages.Add("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                LoanApplication LoanApplication = this._Mapper.Map<LoanApplication>(loanApplicationRequest);

                if (loanApplicationRequest.LoanApplicationID > 0)
                {
                    List<ApplicationDocument> documents = new List<ApplicationDocument>();

                    foreach (var document in loanApplicationRequest.ApplicationDocuments)
                    {
                        ApplicationDocument applicationDocument = this._applicationDocumentRepository.GetAll().AsNoTracking().FirstOrDefault(a => a.LoanApplicationID == loanApplicationRequest.LoanApplicationID​​​​​​​​ && a.ApplicationDocumentID == document.ApplicationDocumentID);
                        document.LoanApplicationID = applicationDocument == null ? loanApplicationRequest.LoanApplicationID : applicationDocument.LoanApplicationID;
                        document.ApplicationDocumentID = applicationDocument == null ? 0 : applicationDocument.ApplicationDocumentID;

                        applicationDocument = this._Mapper.Map<ApplicationDocument>(document);
                        if (applicationDocument.ApplicationDocumentID > 0)
                        {
                            applicationDocument.CreatedDateTime = currentDate;
                            applicationDocument.CreatedByUserID = userSessionEntity.UserID;
                            applicationDocument.LastModifiedDateTime = currentDate;
                            applicationDocument.LastModifiedByUserID = userSessionEntity.UserID;
                            applicationDocument.DocumentName = !string.IsNullOrEmpty(document.DocumentName) ? document.DocumentName : "";
                            this._applicationDocumentRepository.SaveApplicationDocument(applicationDocument);
                        }
                        else
                        {
                            applicationDocument.CreatedDateTime = currentDate;
                            applicationDocument.CreatedByUserID = userSessionEntity.UserID;
                            applicationDocument.LastModifiedDateTime = currentDate;
                            applicationDocument.LastModifiedByUserID = userSessionEntity.UserID;
                        }
                        var isDocumentExist = this._applicationDocumentRepository.GetApplicationDocumentByApplicationIDAndDocumentTypeIDAndName(applicationDocument.LoanApplicationID, applicationDocument.DocumentTypeID, applicationDocument.FileName);
                        if (isDocumentExist == null)
                        {
                            this._applicationDocumentRepository.SaveApplicationDocument(applicationDocument);
                        }
                    }
                }
                else
                {
                    LoanApplication.ApplicationDocuments = this._Mapper.Map<List<ApplicationDocument>>(loanApplicationRequest.ApplicationDocuments);

                    foreach (var document in LoanApplication.ApplicationDocuments)
                    {
                        document.CreatedDateTime = currentDate;
                        document.CreatedByUserID = userSessionEntity.UserID;
                        document.LastModifiedDateTime = currentDate;
                        document.LastModifiedByUserID = userSessionEntity.UserID;
                        this._applicationDocumentRepository.SaveApplicationDocument(document);
                    }
                }

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                commonCreationParam.ID = LoanApplication.LoanApplicationID;
            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }

            return commonCreationParam;
        }

        public ApplicationListResponse GetAllLoanApplicationInformation(PageFilterEntity pageFilter, UserSessionEntity userSessionEntity)
        {
            ApplicationListResponse applicationListResponse = new ApplicationListResponse();

            if (pageFilter == null)
            {
                _Logger.LogError("Input Parameter PageFilter is null");
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";

                return applicationListResponse;
            }

            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";

                return applicationListResponse;
            }

            if (userSessionEntity.UserID == 0)
            {
                _Logger.LogError("Input Parameter UserSessionEntity.UserID is null");
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return applicationListResponse;
            }

            try
            {
                applicationListResponse = this._loanApplication.GetAllLoanApplicationInformation(pageFilter, userSessionEntity.UserID);

                if (applicationListResponse.ApplicationPageResultEntity.DataList == null && applicationListResponse.IsSuccess == false)
                {
                    applicationListResponse.IsSuccess = false;
                    applicationListResponse.ID = applicationListResponse.ID;
                    applicationListResponse.Message = "Couldn't find the Loan Application for this program.";
                    return applicationListResponse;
                }

                List<FundUtilization> fundUtilizations = _fundUtilizationRepository.GetAll().Where(x => x.TransactionTypeID == 3 && x.IsActive == true).ToList();

                foreach (var response in applicationListResponse.ApplicationPageResultEntity.DataList)
                {
                    if (response.ApplicationStatusID == (long)ApplicationStatusEnumeration.AccountDisbursed)
                    {
                        FundUtilization fundUtilization = fundUtilizations.Where(x => x.ApplicationID == response.LoanApplicationID && x.IsActive == true).FirstOrDefault();

                        if (fundUtilization != null && fundUtilization.TransactionAmount > 0)
                        {
                            response.LoanAmount = String.Format("{0:#,#}", fundUtilization.TransactionAmount);
                        }
                        else
                        {
                            response.LoanAmount = "0";
                        }
                    }
                    else
                    {
                        var fundUtilizationList = fundUtilizations.Where(x => x.ApplicationID == response.LoanApplicationID && x.IsActive == true).ToList();

                        if (fundUtilizationList != null && fundUtilizationList.Count > 0)
                        {
                            response.LoanAmount = String.Format("{0:#,#}", fundUtilizationList.Sum(fu=>fu.TransactionAmount));
                        }
                        else
                        {
                            response.LoanAmount = "0";
                        }
                    }
                }

                if (applicationListResponse.ApplicationPageResultEntity != null && applicationListResponse.IsSuccess)
                {
                    applicationListResponse.IsSuccess = true;
                }
                else
                {
                    applicationListResponse.IsSuccess = false;
                    applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetAllLoanApplicationInformation", null);
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetAllLoanApplicationInformation", null);
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return applicationListResponse;
        }


        public ApplicationDocumentResponse GetApplicationDocuments(long applicationID)
        {
            ApplicationDocumentResponse applicationDocumentResponse = new ApplicationDocumentResponse();
            List<ApplicationDocumentViewModel> applicationDocumentViewModels = new List<ApplicationDocumentViewModel>();
            ApplicationDocumentViewModel applicationDocumentViewModel = new ApplicationDocumentViewModel();
            DocumentRepository.Domain.DocumentEntity document = null;
            try
            {
                List<ApplicationDocument> applicationDocuments = this._applicationDocumentRepository.GetApplicationDocuments(applicationID);

                if (applicationDocuments != null && applicationDocuments.Count > 0)
                {
                    foreach (var applicationDocument in applicationDocuments)
                    {
                        document = this._documentInformationProvider.GetDocumentByID(applicationDocument.DocumentGUID);

                        if (document != null)
                        {
                            applicationDocumentViewModels.Add(new ApplicationDocumentViewModel
                            {
                                FileName = document.Name,
                                FilePath = document.Path,
                                UploadDate = document.LastUploadedDate == null ? document.LastModifiedDateTime.ToString("MM/dd/yyyy") : document.LastUploadedDate.Value.ToString("MM/dd/yyyy"),
                                ApplicationDocumentID = applicationDocument.ApplicationDocumentID,
                                DocumentKey = document.Key,
                                StorageKey = document.StorageKey,
                                DocumentID = document.DocumentID,
                                DocumentName = applicationDocument.DocumentName,
                            });
                        }
                        applicationDocumentViewModel.Uploading = false;
                    }
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetApplicationDocuments", null);
                applicationDocumentResponse.IsSuccess = false;
                applicationDocumentResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetApplicationDocuments", null);
                applicationDocumentResponse.IsSuccess = false;
                applicationDocumentResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            applicationDocumentResponse.IsSuccess = true;
            applicationDocumentResponse.ApplicationDocumentViewModels = applicationDocumentViewModels;
            return applicationDocumentResponse;
        }

        public ApplicationCreationPreRequiredData GetPrePopulatedApplicationData(long ProgramInvitationID)
        {
            ApplicationCreationPreRequiredData AppCreationPreRequiredData = new ApplicationCreationPreRequiredData();
            try
            {
                /* Get ALl master data entity */
            
                MasterDataEntity MasterData = _masterService.GetAllMasterEntity();

                /* Map required master data for application creation */
                AppCreationPreRequiredData = this._Mapper.Map<ApplicationCreationPreRequiredData>(MasterData);

                /* Assigned value business entity and funding source related required value */
                var programInvitation = _ProgramInvitationRepository.GetProgramInvitation(ProgramInvitationID);
                
                if (programInvitation != null)
                {
                    AppCreationPreRequiredData.BusinessName = programInvitation.BusinessEntity?.BusinessName;
                    AppCreationPreRequiredData.Affiliate = programInvitation.BusinessEntity.Affiliate.AffiliateName;
                    AppCreationPreRequiredData.AffiliateID = programInvitation.BusinessEntity.Affiliate.AffiliateID;
                    AppCreationPreRequiredData.EIN = programInvitation.BusinessEntity?.EIN;
                    AppCreationPreRequiredData.BusinessStartdate = programInvitation.BusinessEntity.StartDate != null ? String.Format("{0:MM/dd/yyyy}", programInvitation.BusinessEntity.StartDate) : string.Empty;
                    AppCreationPreRequiredData.ProgramName = programInvitation.FundingSource?.ProgramName;
                    AppCreationPreRequiredData.FundingSource = programInvitation.FundingSource?.FundingEntity.FundingEntityName;
                    AppCreationPreRequiredData.FundingSource = programInvitation.FundingSource?.FundingEntity.FundingEntityName;
                    AppCreationPreRequiredData.IsSuccess = true;
                    AppCreationPreRequiredData.HasDefaultFundAmount = programInvitation.FundingSource.HasDefaultFundingAmount;
                    AppCreationPreRequiredData.RequestedFundAmount = programInvitation.FundingSource.HasDefaultFundingAmount == true ? programInvitation.FundingSource.MinimumLoanAmount : 0;
                    AppCreationPreRequiredData.ProgramLogoSource = programInvitation.FundingSource?.Logo?.Source;
                    AppCreationPreRequiredData.showProgramLogo = !string.IsNullOrEmpty(AppCreationPreRequiredData.ProgramLogoSource) ? true : false;
                    AppCreationPreRequiredData.FundingEntityLogoSource = programInvitation.FundingSource.FundingEntity.Logo?.Source;
                    AppCreationPreRequiredData.showFundingEntityLogo = !string.IsNullOrEmpty(AppCreationPreRequiredData.FundingEntityLogoSource) ? true : false;
                    AppCreationPreRequiredData.MinimumFundAmount = Decimal.Truncate(programInvitation.FundingSource.MinimumLoanAmount);
                    AppCreationPreRequiredData.MaximumFundAmount = Decimal.Truncate(programInvitation.FundingSource.MaximumLoanAmount);
                    AppCreationPreRequiredData.ProgramQuestions = programInvitation.FundingSource.ProgramQuestions.Select(x => new ThoughtFocus.Domain.Response.ProgramQuestion
                    {
                        QuestionID = x.QuestionID,
                        QuestionText = x.Question.QuestionText,
                        IsRequired = x.IsMadatory,
                        ResponseType = x.Question.ResponseTypeID,
                        Response = string.Empty
                    }).ToList();

                    //Override if application already created
                    if (programInvitation.ProgramStatusID == 2)
                    {
                        LoanApplication loanApplication = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == ProgramInvitationID).FirstOrDefault();
                        if (loanApplication != null && loanApplication.LoanApplicationID > 0 && loanApplication.FundingApplication != null && loanApplication.FundingApplication.ID > 0)
                        {
                            AppCreationPreRequiredData.RequestedFundAmount = Decimal.Truncate(loanApplication.FundingApplication.RequestedFundAmount);
                        }
                        if (loanApplication.QuestionResponse != null && loanApplication.QuestionResponse.Count() > 0)
                        {
                            foreach (var question in AppCreationPreRequiredData.ProgramQuestions)
                            {
                                question.Response = loanApplication.QuestionResponse.Where(x => x.QuestionID == question.QuestionID).Select(s => s.Response).FirstOrDefault();
                            }
                        }
                    }
                    AppCreationPreRequiredData.BussinessProfileHelpfulGuideTemplate = programInvitation.FundingSource?.ProgramHelpfulGuides.Where(x => x.IsActive == true && x.HelpfulGuideTemplate.TypeID == 1).Select(y => y.HelpfulGuideTemplate.Body).FirstOrDefault();
                    AppCreationPreRequiredData.FundingApplicationHelpfulGuideTemplate = programInvitation.FundingSource?.ProgramHelpfulGuides.Where(x => x.IsActive == true && x.HelpfulGuideTemplate.TypeID == 2).Select(y => y.HelpfulGuideTemplate.Body).FirstOrDefault();
                    AppCreationPreRequiredData.DocumentTabHelpfulGuideTemplate = programInvitation.FundingSource?.ProgramHelpfulGuides.Where(x => x.IsActive == true && x.HelpfulGuideTemplate.TypeID == 3).Select(y => y.HelpfulGuideTemplate.Body).FirstOrDefault();
                    AppCreationPreRequiredData.ReviewTabHelpfulGuideTemplate = programInvitation.FundingSource?.ProgramHelpfulGuides.Where(x => x.IsActive == true && x.HelpfulGuideTemplate.TypeID == 4).Select(y => y.HelpfulGuideTemplate.Body).FirstOrDefault();

                    AppCreationPreRequiredData.ProgramDocuments = programInvitation.FundingSource.ProgramDocuments.Where(x => x.DocumentTypeID != 6).Select(x => new ProgramDocuments
                    {
                        ProgramDocumentID = x.ID,
                        IsRequired = x.IsMandatory,
                        DisplayOrder = x.DisplayOrder,
                        DocumentName = x.DocumentType?.Name,
                        DocumentCategoryID = x.DocumentType?.DocumentCategoryID,
                        DocumentID = x.DocumentTypeID,
                        DocumentTypeID = x.DocumentTypeID,
                        IsActive = x.IsActive,
                        ProgramID = x.ProgramID,
                        Response = string.Empty
                    }).ToList();

                    //get master business profile data if new application created
                    if (programInvitation.ProgramStatusID == 1)
                    {
                        var businessProfileMasterData = _adminService.GetBusinessProfileMasterData(programInvitation.BusinessID);
                        AppCreationPreRequiredData.BusinessOwnerMasterParam = businessProfileMasterData.BusinessOwnerMasterParam;
                        AppCreationPreRequiredData.LoanBusinessDetailMasterPreParam = businessProfileMasterData.LoanBusinessDetailMasterPreParam;
                    }
                    //

                    AppCreationPreRequiredData.Message = "Successfully fetched the data";
                }
                else
                {
                    AppCreationPreRequiredData.IsSuccess = false;
                }
                AppCreationPreRequiredData.ProgressReportId = _masterService.GetProgressReportId();
                //Get paymnet mode
                LoanApplication application = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitationID == ProgramInvitationID).FirstOrDefault();
                if (application != null && application.LoanApplicationID > 0 && application.FundingApplication != null && application.FundingApplication.ID > 0)
                {
                    AppCreationPreRequiredData.IsPaymentSchedule = application.FundingApplication.IsPaymentSchedule;
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetPrePopulatedApplicationData ", null);
                AppCreationPreRequiredData.IsSuccess = false;
            }
            return AppCreationPreRequiredData;
        }

        
        public ApplicationViewEntityResponse GetLoanApplicationData(long applicationID, UserSessionEntity userSessionEntity)
        {
            ApplicationViewEntityResponse applicationViewEntityResponse = new ApplicationViewEntityResponse();
            applicationViewEntityResponse.applicationViewEntity = new ApplicationViewEntity();

            applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails = new LoanBusinessDetailParam();
            applicationViewEntityResponse.applicationViewEntity.BusinessOwners = new List<BusinessOwnerParam>();
            applicationViewEntityResponse.applicationViewEntity.FundingApplication = new FundingApplicationParam();
            applicationViewEntityResponse.applicationViewEntity.ApplicationDocuments = new List<ProgramDocuments>();            
            if (applicationID == 0)
            {
                _Logger.LogError("Application ID is 0");
                applicationViewEntityResponse.IsSuccess = false;
                applicationViewEntityResponse.Message = "Unable to retrieve application data as application ID is 0";
            }
            try
            {
                LoanApplication loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(applicationID);               

                //CommonConstants.ThresholdRequestAmount = 0;
                //var masterOptionResponse = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                //if (masterOptionResponse != null && masterOptionResponse.Count > 0)
                //{
                //    CommonConstants.ThresholdRequestAmount = long.Parse(masterOptionResponse.FirstOrDefault().OptionValue);
                //}
                if (loanApplication == null)
                {
                    applicationViewEntityResponse.applicationViewEntity = new ApplicationViewEntity();
                    applicationViewEntityResponse.IsSuccess = false;
                    applicationViewEntityResponse.Message = "Application doesn't exist.";
                    return applicationViewEntityResponse;
                }
                if (loanApplication.FundingApplication != null)
                {
                    if(loanApplication.FundingApplication.IsPaymentSchedule == null)
                    {
                        loanApplication.FundingApplication.IsPaymentSchedule = false;
                    }
                }
             
              

                var businessEntity = this._adminService.GetAllBusinessEntityInformation();               
                applicationViewEntityResponse.applicationViewEntity.ProgramId = loanApplication?.ProgramInvitation?.FundingSource?.FundingSourceID;
                //applicationViewEntityResponse.applicationViewEntity.BusinessId = businessEntity?.businessEntityListResponse?.Where(b => b.BusinessName == loanApplication?.LoanBusinessDetail?.BusinessName).FirstOrDefault().ID;
                applicationViewEntityResponse.applicationViewEntity.ProgramName = loanApplication?.ProgramInvitation?.FundingSource?.ProgramName;
                applicationViewEntityResponse.applicationViewEntity.FundingEntityName = loanApplication?.ProgramInvitation?.FundingSource?.FundingEntity?.FundingEntityName;
                applicationViewEntityResponse.applicationViewEntity.ApplicationID = loanApplication.LoanApplicationID;
                applicationViewEntityResponse.applicationViewEntity.LoanNumber = loanApplication.LoanNumber;
                applicationViewEntityResponse.applicationViewEntity.ApplicationStatusID = loanApplication.ApplicationStatusID;
                applicationViewEntityResponse.applicationViewEntity.ApplicationStatus = loanApplication.ApplicationStatus?.Description;
                
                applicationViewEntityResponse.applicationViewEntity.ApplicationTypeName = loanApplication.ApplicationType?.ApplicationTypeName;
                applicationViewEntityResponse.applicationViewEntity.DateApplied = loanApplication.DateApplied != null ? String.Format("{0:MM/dd/yyyy}", loanApplication.DateApplied) : string.Empty;
                applicationViewEntityResponse.applicationViewEntity.ProgramInvitationID = loanApplication.ProgramInvitationID;
                applicationViewEntityResponse.applicationViewEntity.FundingApplication = this._Mapper.Map<FundingApplicationParam>(loanApplication.FundingApplication);

                applicationViewEntityResponse.applicationViewEntity.BusinessOwners = this._Mapper.Map<List<BusinessOwnerParam>>(loanApplication.BusinessOwners.Where(x => x.IsActive == true).ToList());
                applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails = this._Mapper.Map<LoanBusinessDetailParam>(loanApplication.LoanBusinessDetail);
                

                applicationViewEntityResponse.applicationViewEntity.showAccountDisbursedInfo = false;
                if(applicationViewEntityResponse.applicationViewEntity.FundingApplication != null)
                {
                    applicationViewEntityResponse.applicationViewEntity.FundingApplication.IsSPAExist = false;
                }
                if(applicationViewEntityResponse.applicationViewEntity != null)
                {
                    applicationViewEntityResponse.applicationViewEntity.ShowAccountDisbursedInfoIfSPA = false;
                }  
                
                
                if (applicationViewEntityResponse.applicationViewEntity.ApplicationStatusID == (long)ApplicationStatusEnumeration.AccountDisbursed || applicationViewEntityResponse.applicationViewEntity.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAAccountDisbursed)
                {
                    applicationViewEntityResponse.applicationViewEntity.showAccountDisbursedInfo = true;
                    FundUtilization fundUtilization = _fundUtilizationRepository.GetAll().Where(x => x.ApplicationID == loanApplication.LoanApplicationID && x.IsActive == true).FirstOrDefault();
                    applicationViewEntityResponse.applicationViewEntity.FundedAmount = "0";
                    applicationViewEntityResponse.applicationViewEntity.FundedDate = string.Empty;
                    if (fundUtilization != null)
                    {
                        applicationViewEntityResponse.applicationViewEntity.FundedAmount = fundUtilization.TransactionAmount > 0 ? String.Format("{0:#,#}", fundUtilization.TransactionAmount) : string.Empty;
                        applicationViewEntityResponse.applicationViewEntity.FundedDate = fundUtilization.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");
                    }
                }
                //Prod issue fix-drafted state issue--30-09-2021
                if (applicationViewEntityResponse.applicationViewEntity.FundingApplication == null)
                {
                    applicationViewEntityResponse.applicationViewEntity.FundingApplication = new FundingApplicationParam();
                }
                if (loanApplication.ProgramInvitation.FundingSource != null)
                {
                    applicationViewEntityResponse.applicationViewEntity.FundingApplication.HasDefaultFundAmount = loanApplication.ProgramInvitation.FundingSource.HasDefaultFundingAmount;
                }

                if (applicationViewEntityResponse.applicationViewEntity.FundingApplication == null)
                {
                    applicationViewEntityResponse.applicationViewEntity.FundingApplication = new FundingApplicationParam();
                }

                if (loanApplication.ProgramInvitation.FundingSource != null)
                {
                    applicationViewEntityResponse.applicationViewEntity.FundingApplication.HasDefaultFundAmount = loanApplication.ProgramInvitation.FundingSource.HasDefaultFundingAmount;
                }

                List<DocumentRequest> _applicationDocuments = new List<DocumentRequest>();

                if (loanApplication.ApplicationDocuments != null & loanApplication.ApplicationDocuments.Count > 0)
                    _applicationDocuments = this._Mapper.Map<List<DocumentRequest>>(loanApplication.ApplicationDocuments);

                var programInvitation = _ProgramInvitationRepository.GetProgramInvitation(loanApplication.ProgramInvitationID);
                applicationViewEntityResponse.applicationViewEntity.BusinessId = programInvitation.BusinessID;



                // if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.RequestedFundAmount > 250000)
                //if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount)
                if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.IsPaymentSchedule==true)
                {
                    

                    var paymentScheduleSummary = GetPaymentScheduleSummaryDetail(programInvitation.BusinessID, programInvitation.ProgramID, applicationID);
                    if(paymentScheduleSummary != null)
                    {
                        var paymentScheduleTransactions = _fundingSourceRepository.GetPaymentScheduleTransaction(programInvitation.BusinessID, loanApplication.LoanApplicationID);
                        if (paymentScheduleSummary != null)
                        {
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary = new PaymentScheduleSummaryRequest();
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.ID = paymentScheduleSummary.FundPaymentScheduleID;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.BusinessID = paymentScheduleSummary.BusinessID;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.ProgramID = paymentScheduleSummary.ProgramID;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.LoanApplicationID = loanApplication.LoanApplicationID;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.FundAllocatedAmount = paymentScheduleSummary.FundAllocatedSummaryAmount;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.FundDisbursedAmount = paymentScheduleSummary.FundDisbursedSummaryAmount;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.FundPendingAmount = paymentScheduleSummary.FundPendingSummaryAmount;
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleSummary.FundRequestedAmount = paymentScheduleSummary.FundRequestedSummaryAmount;

                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PendingDisbursement =  string.Format("{0:#,#}", Decimal.Truncate(paymentScheduleSummary.FundPendingSummaryAmount>0 ? paymentScheduleSummary.FundPendingSummaryAmount : 0)); 
                        }
                        if (applicationViewEntityResponse.applicationViewEntity.FundingApplication.PendingDisbursement.Trim() == "")
                        {
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PendingDisbursement = "$ 0";
                        }
                        else
                        {
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PendingDisbursement = "$ " + applicationViewEntityResponse.applicationViewEntity.FundingApplication.PendingDisbursement;
                        }
                        if (paymentScheduleTransactions != null && paymentScheduleTransactions.Count > 0)
                        {
                            applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleTransaction = new List<PaymentScheduleTransactionRequest>();
                            foreach (var ps in paymentScheduleTransactions)
                            {
                                if (ps.TransactionStatusID == 1)
                                {
                                    var paymentScheduleTransaction = new PaymentScheduleTransactionRequest();
                                    //shouldn’t be allowed to complete the funding process for payment (transaction date) greater than system date
                                    if ((ps.TransactionDate.Date - DateTime.Now.Date).TotalDays <= CommonConstants.DisbursedEnabledDays)
                                    {
                                        paymentScheduleTransaction.IsEnabled = true;
                                    }

                                    paymentScheduleTransaction.BusinessID = ps.BusinessID;
                                    paymentScheduleTransaction.FundedAmount = ps.FundedAmount;
                                    paymentScheduleTransaction.FundedAmountFormat = "$ " + string.Format("{0:#,#}", Decimal.Truncate(ps.FundedAmount));
                                    paymentScheduleTransaction.LoanApplicationID = ps.LoanApplicationID;
                                    paymentScheduleTransaction.PaymentScheduleID = ps.PaymentScheduleID;
                                    paymentScheduleTransaction.ProgramID = ps.ProgramID;
                                    paymentScheduleTransaction.TransactionStatusID = ps.TransactionStatusID;
                                    paymentScheduleTransaction.TransactionDate = ps.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/"); 
                                    paymentScheduleTransaction.DisbursedDate = ps.DisbursedDate?.ToString("MM/dd/yyyy").Replace("-", "/"); 
                                    applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleTransaction.Add(paymentScheduleTransaction);
                                }
                            }
                            if (applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleTransaction != null && applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleTransaction.Count > 0)
                            {
                                applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleTransaction = applicationViewEntityResponse.applicationViewEntity.FundingApplication.PaymentScheduleTransaction.OrderBy(o => o.TransactionDate).ToList();
                            }

                        }
                        var spa = _fundingSourceRepository.GetPaymentAgreementDocumentByApplicationId(loanApplication.LoanApplicationID);
                        if (spa != null && spa.FileUploadedSourceUrl != "" && spa.FileUploadedSourceUrl != null)
                        {
                            if (applicationViewEntityResponse.applicationViewEntity.FundingApplication != null)
                            {
                                applicationViewEntityResponse.applicationViewEntity.FundingApplication.IsSPAExist = true;
                            }
                        }
                        var paymentScheduleStatus = this._LoanApplicationRepository.GetPaymentScheduleStatusById(loanApplication.LoanApplicationID);
                        if (paymentScheduleStatus != null && ((loanApplication.ApplicationStatusID == 40) && (paymentScheduleStatus.ApplicationStatusID == 40 || paymentScheduleStatus.ApplicationStatusID == 0)))
                        {
                            applicationViewEntityResponse.applicationViewEntity.ApplicationStatus = paymentScheduleStatus.Status;

                        }
                        if (paymentScheduleStatus != null && userSessionEntity!=null)
                        {

                            User user = this._userRepository.GetByUserID(userSessionEntity.UserID);
                            var isBorrower = false;
                            if (user != null)
                            {
                                var activeRoles = user.UserRoles.Where(a => a.IsActive == true && (a.RoleID == 2)).FirstOrDefault();
                                isBorrower =  activeRoles?.RoleID == 2 ? true : false;
                            }

                            if(isBorrower)
                            {
                                if (loanApplication.ApplicationStatusID == 40)
                                {
                                    if (paymentScheduleStatus.Status.Equals("Final Disbursement", StringComparison.OrdinalIgnoreCase) || paymentScheduleStatus.Status.Equals("Account Disbursed", StringComparison.OrdinalIgnoreCase) || paymentScheduleStatus.Status.Equals("Fund Disbursed", StringComparison.OrdinalIgnoreCase))
                                    {
                                        applicationViewEntityResponse.applicationViewEntity.IsPaymentRequested = false;
                                    }
                                    else
                                    {
                                        applicationViewEntityResponse.applicationViewEntity.IsPaymentRequested = true;
                                    }
                                }
                            }
                            
                            //if(loanApplication.ApplicationStatusID == 8 || ((!paymentScheduleStatus.Status.Equals("Final Disbursement", StringComparison.OrdinalIgnoreCase) || !paymentScheduleStatus.Status.Equals("Account Disbursed", StringComparison.OrdinalIgnoreCase)) && loanApplication.ApplicationStatusID == 40))
                            //{
                            //    applicationViewEntityResponse.applicationViewEntity.IsPaymentRequested = true;
                            //}
                            
                        }
                        if (paymentScheduleStatus != null && (( paymentScheduleStatus.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAAccountDisbursed ) ||
                           (paymentScheduleStatus.DisbursementCount ==1 && (paymentScheduleStatus.Status== CommonConstants.AccountDisbursedFLag || paymentScheduleStatus.Status == CommonConstants.FinalDisbursementFLag))))
                        {
                            applicationViewEntityResponse.applicationViewEntity.showAccountDisbursedInfo = true;                            
                            FundUtilization fundUtilization = _fundUtilizationRepository.GetAll().Where(x => x.ApplicationID == loanApplication.LoanApplicationID && x.IsActive == true).FirstOrDefault();
                            applicationViewEntityResponse.applicationViewEntity.FundedAmount = "0";
                            applicationViewEntityResponse.applicationViewEntity.FundedDate = string.Empty;
                            if (fundUtilization != null)
                            {
                                applicationViewEntityResponse.applicationViewEntity.FundedAmount = fundUtilization.TransactionAmount > 0 ? String.Format("{0:#,#}", fundUtilization.TransactionAmount) : string.Empty;
                                applicationViewEntityResponse.applicationViewEntity.FundedDate = fundUtilization.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");
                            }
                            //For SPA

                            //CommonConstants.ThresholdRequestAmount = 0;
                            //var masterOptionResponse = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                            //if (masterOptionResponse != null && masterOptionResponse.Count > 0)
                            //{
                            //    CommonConstants.ThresholdRequestAmount = long.Parse(masterOptionResponse.FirstOrDefault().OptionValue);
                            //}
                            applicationViewEntityResponse.applicationViewEntity.ShowAccountDisbursedInfoIfSPA = false;
                            //if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount)
                            if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.IsPaymentSchedule==true)
                            {

                                applicationViewEntityResponse.applicationViewEntity.ShowAccountDisbursedInfoIfSPA = true;
                                var lstTansactions = _fundingSourceRepository.GetPaymentScheduleTransactionByLoanID(loanApplication.LoanApplicationID).Where(x => x.IsActive == true);
                             
                                if (lstTansactions != null && lstTansactions.Count() > 0)
                                {
                                    applicationViewEntityResponse.applicationViewEntity.DisbursedTransactionDetails = new List<TransactionAccountDisbursed>();
                                    string ordinal = "";
                                    int maxCount = 1;
                                    //var lstTansactionsDisbursed = lstTansactions.Where(x => x.TransactionStatusID == 2).OrderBy(x => x.TransactionDate);
                                    var lstAllTansactions = lstTansactions.OrderBy(x => x.TransactionDate);
                                    foreach (var trans in lstAllTansactions)
                                    {
                                        if (trans.TransactionStatusID==2)
                                        {
                                            var transactionAccountDisbursed = new TransactionAccountDisbursed();
                                            if (maxCount == lstTansactions.Count())
                                            {
                                                transactionAccountDisbursed.DisbursementName = "Final Disbursement";
                                            }
                                            else
                                            {

                                                // ordinal = AddOrdinal(maxCount);
                                                ordinal = NumberOrdinal.Ordinal(maxCount);
                                                transactionAccountDisbursed.DisbursementName = ordinal + " Disbursement";
                                            }
                                            maxCount = maxCount + 1;
                                            if (paymentScheduleStatus.DisbursementCount == 1 && paymentScheduleStatus.Status == CommonConstants.AccountDisbursedFLag)
                                            {
                                                transactionAccountDisbursed.DisbursementName = "Disbursed Amount";
                                            }
                                            transactionAccountDisbursed.Amount = trans.FundedAmount > 0 ? "$ " + String.Format("{0:#,#}", trans.FundedAmount) : "$ 0";
                                            transactionAccountDisbursed.TransactionDate = trans.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");
                                            transactionAccountDisbursed.DisbursedDate = trans.DisbursedDate?.ToString("MM/dd/yyyy").Replace("-", "/");
                                            applicationViewEntityResponse.applicationViewEntity.DisbursedTransactionDetails.Add(transactionAccountDisbursed);
                                        }
                                        else if (trans.TransactionStatusID == 1)
                                        {
                                            var transactionAccountPending = new TransactionAccountDisbursed();
                                            transactionAccountPending.DisbursementName = "Amount";
                                            transactionAccountPending.Amount = trans.FundedAmount > 0 ? "$ " + String.Format("{0:#,#}", trans.FundedAmount) : "$ 0";
                                            transactionAccountPending.TransactionDate = trans.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");                                            
                                            applicationViewEntityResponse.applicationViewEntity.PendingTransactionDetails.Add(transactionAccountPending);
                                        }
                                        
                                    }

                                }

                            }
                        }
                        else
                        {
                            //If Disbursed is not done but need to display the pending payment                        

                            
                                var lstTansactions = _fundingSourceRepository.GetPaymentScheduleTransactionByLoanID(loanApplication.LoanApplicationID).Where(x => x.IsActive == true);

                                if (lstTansactions != null && lstTansactions.Count() > 0)
                                {  
                                    var lstAllTansactions = lstTansactions.OrderBy(x => x.TransactionDate);
                                    foreach (var trans in lstAllTansactions)
                                    {                                        
                                         if (trans.TransactionStatusID == 1)
                                        {
                                            var transactionAccountPending = new TransactionAccountDisbursed();
                                            transactionAccountPending.DisbursementName = "Amount";
                                            transactionAccountPending.Amount = trans.FundedAmount > 0 ? "$ " + String.Format("{0:#,#}", trans.FundedAmount) : "$ 0";
                                            transactionAccountPending.TransactionDate = trans.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");
                                            applicationViewEntityResponse.applicationViewEntity.PendingTransactionDetails.Add(transactionAccountPending);
                                        }

                                    }
                            }
                            applicationViewEntityResponse.applicationViewEntity.FundedAmount = "$ 0";
                            applicationViewEntityResponse.applicationViewEntity.FundedDate = string.Empty;
                        }
                    }
                }
                applicationViewEntityResponse.applicationViewEntity.ApplicationDocuments = programInvitation.FundingSource.ProgramDocuments.Where(x => x.DocumentTypeID != 6).Select(x => new ProgramDocuments
                {
                    ProgramDocumentID = x.ID,
                    IsRequired = x.IsMandatory,
                    DisplayOrder = x.DisplayOrder,
                    DocumentName = x.DocumentType?.Name,
                    DocumentCategoryID = x.DocumentType?.DocumentCategoryID,
                    DocumentID = x.DocumentTypeID,
                    DocumentTypeID = x.DocumentTypeID,
                    IsActive = x.IsActive,
                    ProgramID = x.ProgramID,
                    Response = string.Empty
                }).ToList();
                int documentTypeId= _masterService.GetProgressReportId();
                foreach (var doc in _applicationDocuments)
                {
                    if (doc.DocumentTypeID == 6 || doc.DocumentTypeID == documentTypeId)
                    {
                        ProgramDocuments progDoc = new ProgramDocuments();

                        progDoc.ApplicationDocumentID = doc.ApplicationDocumentID;
                        progDoc.DocumentGUID = doc.DocumentGUID;
                        progDoc.ProgramDocumentID = doc.ApplicationDocumentID;
                        progDoc.DocumentID = doc.DocumentTypeID;
                        progDoc.DocumentCategoryID = 1;
                        progDoc.ProgramID = programInvitation.ProgramID;
                        progDoc.IsRequired = false;
                        progDoc.DocumentName = doc.DocumentName;
                        progDoc.IsActive = doc.IsActive;
                        progDoc.DocumentTypeID = doc.DocumentTypeID;
                        progDoc.FileName = doc.FileName;
                        progDoc.PhysicalFileStorageKey = doc.PhysicalFileStorageKey;
                        progDoc.FileSize = doc.FileSize;
                        progDoc.LoanApplicationID​​​​​​​​ = doc.LoanApplicationID;
                        progDoc.FileSource = doc.FileSource;
                        applicationViewEntityResponse.applicationViewEntity.ApplicationDocuments.Add(progDoc);
                    }
                    else
                    {
                        var programData = applicationViewEntityResponse.applicationViewEntity.ApplicationDocuments.Where(x => x.DocumentID == doc.DocumentTypeID)
                        .Select(programData =>
                        {
                            programData.ApplicationDocumentID = doc.ApplicationDocumentID;
                            programData.DocumentGUID = doc.DocumentGUID;
                            programData.ProgramDocumentID = doc.ApplicationDocumentID;
                            programData.DocumentID = doc.DocumentTypeID;
                            programData.DocumentCategoryID = 1;
                            programData.ProgramID = programInvitation.ProgramID;
                            programData.DocumentName = doc.DocumentName;
                            programData.IsActive = doc.IsActive;
                            programData.DocumentTypeID = doc.DocumentTypeID;
                            programData.FileName = doc.FileName;
                            programData.PhysicalFileStorageKey = doc.PhysicalFileStorageKey;
                            programData.FileSize = doc.FileSize;
                            programData.LoanApplicationID​​​​​​​​ = doc.LoanApplicationID;
                            programData.FileSource = doc.FileSource; return programData;
                        })
                       .FirstOrDefault();
                    }


                }

                if (applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails != null)
                {
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.AffiliateName = loanApplication?.LoanBusinessDetail?.Affiliate?.AffiliateName;
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.BusinessTypeName = loanApplication?.LoanBusinessDetail?.BusinessType?.Type;
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.IndustryTypeName = loanApplication?.LoanBusinessDetail?.IndustryType?.Type;
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.SIC_Name = loanApplication?.LoanBusinessDetail?.SIC?.IndustryTitle;
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.StateName = loanApplication?.LoanBusinessDetail?.State?.StateName;
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.StartDate = loanApplication?.LoanBusinessDetail?.StartDate.ToString("MM/dd/yyyy");
                    applicationViewEntityResponse.applicationViewEntity.LoanBusinessDetails.NaicsCode = loanApplication?.LoanBusinessDetail?.NaicsCode;
                }

                if (applicationViewEntityResponse.applicationViewEntity.BusinessOwners != null && applicationViewEntityResponse.applicationViewEntity.BusinessOwners.Count > 0)
                {
                    foreach (var owners in applicationViewEntityResponse.applicationViewEntity.BusinessOwners)
                    {
                        owners.GenderName = loanApplication.BusinessOwners.Where(x => x.ID == owners.ID).Select(y => y.Gender?.GenderName).FirstOrDefault();
                        owners.VeteranName = loanApplication.BusinessOwners.Where(x => x.ID == owners.ID).Select(y => y.Veteran?.VeteranType).FirstOrDefault();
                        owners.RaceName = loanApplication.BusinessOwners.Where(x => x.ID == owners.ID).Select(y => y.Race?.RaceName).FirstOrDefault();
                        owners.EthnicityName = loanApplication.BusinessOwners.Where(x => x.ID == owners.ID).Select(y => y.Ethnicity?.EthnicityName).FirstOrDefault();
                    }
                }
                if (applicationViewEntityResponse.applicationViewEntity.FundingApplication != null)
                {
                    applicationViewEntityResponse.applicationViewEntity.FundingApplication.ProgramQuestions = loanApplication?.ProgramInvitation?.FundingSource.ProgramQuestions.Where(p=>p.IsActive == true).Select(x => new ThoughtFocus.Domain.Response.ProgramQuestion
                    {
                        QuestionID = x.QuestionID,
                        QuestionText = x.Question.QuestionText,
                        IsRequired = x.IsMadatory,
                        ResponseType = x.Question.ResponseTypeID,
                        Response = string.Empty
                    }).ToList();

                    if (applicationViewEntityResponse.applicationViewEntity.FundingApplication.ProgramQuestions != null && applicationViewEntityResponse.applicationViewEntity.FundingApplication.ProgramQuestions.Count() > 0)
                    {
                        foreach (var question in applicationViewEntityResponse.applicationViewEntity.FundingApplication.ProgramQuestions)
                        {
                            question.Response = loanApplication.QuestionResponse.Where(x => x.QuestionID == question.QuestionID).Select(s => s.Response).FirstOrDefault();
                        }
                    }
                }

                applicationViewEntityResponse.applicationViewEntity.ProgramLogoSource = (loanApplication?.ProgramInvitation?.FundingSource?.Logo?.Source);

                if (!string.IsNullOrEmpty(applicationViewEntityResponse.applicationViewEntity.ProgramLogoSource))
                {
                    applicationViewEntityResponse.applicationViewEntity.showProgramLogo = true;
                }

                applicationViewEntityResponse.applicationViewEntity.FundingEntityLogoSource = (loanApplication?.ProgramInvitation?.FundingSource?.FundingEntity?.Logo?.Source);

                if (!string.IsNullOrEmpty(applicationViewEntityResponse.applicationViewEntity.FundingEntityLogoSource))
                {
                    applicationViewEntityResponse.applicationViewEntity.showFundingEntityLogo = true;
                }

                applicationViewEntityResponse.applicationViewEntity.ShowUserAgreementdetailsInfo = false;
                if ((applicationViewEntityResponse.applicationViewEntity.ApplicationStatusID >= (long)ApplicationStatusEnumeration.CFOApproved
                    && applicationViewEntityResponse.applicationViewEntity.ApplicationStatusID != (long)ApplicationStatusEnumeration.AgreementRejected
                    && applicationViewEntityResponse.applicationViewEntity.ApplicationStatusID != (long)ApplicationStatusEnumeration.RequestedMoreInformationToSave
                    )

                   )
                {
                    applicationViewEntityResponse.applicationViewEntity.ShowUserAgreementdetailsInfo = true;
                    applicationViewEntityResponse.applicationViewEntity.AgreementAcceptedBy = "";
                    applicationViewEntityResponse.applicationViewEntity.AgreementAcceptanceDateTime = "";

                    LoanApplicationAgreementDetail LoanApplicationAgreementDetail = this._LoanApplicationAgreementDetailRepository.GetAll().Where(x => x.IsActive == true && x.ApplicationID == applicationID).FirstOrDefault();
                    if (LoanApplicationAgreementDetail != null)
                    {
                        applicationViewEntityResponse.applicationViewEntity.AgreementAcceptanceDateTime = LoanApplicationAgreementDetail.CreatedDateTime.ToString("MM/dd/yyyy").Replace("-", "/");
                        User User = this._userRepository.GetAll().Where(x => x.IsActive == true && x.UserID == LoanApplicationAgreementDetail.CreatedByUserID).FirstOrDefault();

                        if (User != null)
                        {

                            applicationViewEntityResponse.applicationViewEntity.GrantAgreementName = "Grant Agreement";
                            applicationViewEntityResponse.applicationViewEntity.AgreementAcceptedBy = User?.Contact?.FirstName + " " + User?.Contact?.MiddleName + " " + User?.Contact?.LastName;
                        }
                    }

                    //For SPA if exist
                    //if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.RequestedFundAmount > 250000)
                    //if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount)
                    if (loanApplication.FundingApplication != null && loanApplication.FundingApplication.IsPaymentSchedule == true)
                    {
                        
                        applicationViewEntityResponse.applicationViewEntity.SPAName = "Schedule of Payment Agreement";
                        applicationViewEntityResponse.applicationViewEntity.ShowUserSPAdetailsInfo = true;
                        applicationViewEntityResponse.applicationViewEntity.SPAAcceptedBy = "";
                        applicationViewEntityResponse.applicationViewEntity.SPAAcceptanceDateTime = "";
                        var spa = this._LoanApplicationAgreementDetailRepository.GetLoanApplicationSchedulePaymentAreementDetail( applicationID);
                        if (spa != null)
                        {
                            applicationViewEntityResponse.applicationViewEntity.SPAAcceptanceDateTime = spa.CreatedDateTime.ToString("MM/dd/yyyy").Replace("-", "/");
                            User User = this._userRepository.GetAll().Where(x => x.IsActive == true && x.UserID == spa.CreatedByUserID).FirstOrDefault();

                            if (User != null)
                            {
                                applicationViewEntityResponse.applicationViewEntity.SPAAcceptedBy = User?.Contact?.FirstName + " " + User?.Contact?.MiddleName + " " + User?.Contact?.LastName;
                            }
                        }
                    }
                    

                }
                applicationViewEntityResponse.applicationViewEntity.ProgressReportId = _masterService.GetProgressReportId();
                applicationViewEntityResponse.IsSuccess = true;

                if (applicationViewEntityResponse != null && applicationViewEntityResponse.IsSuccess)
                {
                    applicationViewEntityResponse.IsSuccess = true;
                }
                else
                {
                    applicationViewEntityResponse.IsSuccess = false;
                    applicationViewEntityResponse.Message = "Contact ID doesn't exist.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetLoanApplicationData ", null);
                applicationViewEntityResponse.IsSuccess = false;
                applicationViewEntityResponse.Message = "Unable to retrieve application information.";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetLoanApplicationData ", null);
                applicationViewEntityResponse.IsSuccess = false;
                applicationViewEntityResponse.Message = "Unable to retrieve application information.";
            }
            return applicationViewEntityResponse;
        }

        public BaseResponse ApplicationCommandHandler(LoanApplicationRequest loanApplicationParam, UserSessionEntity userSessionEntity)
        {
            BaseResponse createResponse = new BaseResponse();
            CommonResponse commonResponse = null;
            List<string> validationMessages = new List<string>();
            if (String.IsNullOrEmpty(loanApplicationParam.CommandName))
            {
                createResponse.IsSuccess = false;
                createResponse.Message = "Unable to process command. Please try after sometime.";
                return createResponse;
            }
            if (userSessionEntity == null)
            {
                createResponse.IsSuccess = false;
                createResponse.Message = "Unable to process command. Please try after sometime.";
                return createResponse;
            }
            if (userSessionEntity.UserID == 0)
            {
                createResponse.IsSuccess = false;
                createResponse.Message = "Unable to process command. Please try after sometime.";
                return createResponse;
            }

            try
            {
                //Temp comment
                /* var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(loanApplicationParam.LoanApplicationID);
                 if (loanApplication != null && loanApplication.LoanApplicationWorFlowDetails != null)
                 {
                     if (loanApplication.LoanApplicationWorFlowDetails.FirstOrDefault().LoanApplicantID == loanApplicationParam.LoanApplicationID)
                     {
                         WorkFlowID = loanApplication.LoanApplicationWorFlowDetails.FirstOrDefault().WorkFlowID;
                     }
                 }*/
                var processInstance = WorkflowInit.GetProcessInstanceByProcessID(loanApplicationParam.LoanApplicationID);
                if (processInstance != null && processInstance.WorkFlowID == 1)
                {
                    //New comment below code
                    this.CreateProcessInstanceForSPA(loanApplicationParam.LoanApplicationID, WorkFlowID, 2);
                    processInstance = WorkflowInit.GetProcessInstanceByProcessID(loanApplicationParam.LoanApplicationID);
                    if (processInstance != null)
                    {
                        WorkFlowID = processInstance.WorkFlowID;
                    }
                }
                //New Comment below code
                /*var processInstance = WorkflowInit.GetProcessInstanceByProcessID(loanApplicationParam.LoanApplicationID);
                {
                    if(processInstance != null)
                    {
                        WorkFlowID = processInstance.WorkFlowID;
                    }
                }*/
                if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Submit.ToString() || loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Save.ToString())
                {
                    //validation loanapplication fields
                    if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Submit.ToString())
                    {
                        var validator = new LoanApplicationParamValidation();
                        var modelValidationResults = validator.Validate(loanApplicationParam, options => { options.IncludeRuleSets("requiredInputValidation", "invalidInput"); });

                        if (!modelValidationResults.IsValid)
                        {
                            foreach (var error in modelValidationResults.Errors)
                            {
                                validationMessages.Add(error.ErrorMessage);
                            }
                        }

                        // validation for ProgramDocument
                        var programInvitation = _ProgramInvitationRepository.GetProgramInvitation(loanApplicationParam.ProgramInvitationID);

                        //validation for ProgramQuestions
                        var QuestionsList = programInvitation.FundingSource.ProgramQuestions.Where(x => x.IsMadatory == true && x.IsActive == true).ToList();
                        if (QuestionsList.Count > 0)
                        {
                            if (loanApplicationParam.FundingApplication.QuestionResponse.Count < 1)
                            {
                                validationMessages.Add("Please select the mandatory questions under funding application tab section.");
                            }
                            else
                            {
                                foreach (var question in QuestionsList)
                                {
                                    var Questions = loanApplicationParam.FundingApplication.QuestionResponse.Where(x => x.QuestionID == question.QuestionID).FirstOrDefault();

                                    if (Questions == null)
                                    {
                                        validationMessages.Add("Please select the mandatory questions under funding application tab section.");
                                        break;
                                    }

                                }
                            }
                        }

                        if (validationMessages != null && validationMessages.Count > 0)
                        {
                            createResponse = new BaseResponse(
                            ResponseStatus.Fail, MessageConstants.invalidInputs);
                            createResponse.Message = "Please enter all required fields.";
                            createResponse.ID = loanApplicationParam.LoanApplicationID;
                            createResponse.ValidationErrors = validationMessages;
                            createResponse.StackTrace = MessageConstants.invalidInputs;
                            return createResponse;
                        }

                    }

                    _Logger.LogDebug("Submit is initiated by User {0}", userSessionEntity.UserID);
                    if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Submit.ToString())
                        commonResponse = this.SaveLoanApplication(loanApplicationParam, userSessionEntity);
                    if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Save.ToString() && loanApplicationParam.IsBusinessProfile)
                    {
                        commonResponse = this.SaveLoanBusinessDetailAndOwner(loanApplicationParam, userSessionEntity);

                        if (loanApplicationParam.ApplicationStatusID < (long)ApplicationStatusEnumeration.Submitted)
                        {

                            //Save business profile master data
                            var programInvite = _ProgramInvitationRepository.GetAll().Where(p => p.ProgramInvitationID == loanApplicationParam.ProgramInvitationID).FirstOrDefault();
                            var businessOwnerMasters = _businessOwnerMasterRepository.GetAll().Where(b => b.BusinessID == programInvite.BusinessID).ToList();

                            foreach (var owner in businessOwnerMasters)
                            {
                                owner.IsActive = false;
                            }

                            foreach (var owner in loanApplicationParam.BusinessOwners)
                            {
                                var businessOwnerMaster = _Mapper.Map<BusinessOwnerMaster>(owner);
                                businessOwnerMaster.IsActive = true;
                                businessOwnerMaster.ID = 0;
                                businessOwnerMaster.BusinessID = programInvite.BusinessID;
                                businessOwnerMasters.Add(businessOwnerMaster);
                            }

                            var loanBusinessDetailsMaster = _Mapper.Map<LoanBusinessDetailMaster>(loanApplicationParam.LoanBusinessDetails);
                            _businessOwnerMasterRepository.SaveOrUpdateBusinessOwnerMaster(businessOwnerMasters, userSessionEntity.UserID);
                            var loadBusinessID = _loanBusinessDetailMasterRepository.GetAll().Where(l => l.BusinessID == programInvite.BusinessID).FirstOrDefault();
                            loanBusinessDetailsMaster.ID = loadBusinessID != null ? loadBusinessID.ID : 0;
                            loanBusinessDetailsMaster.BusinessID = programInvite.BusinessID;
                            _loanBusinessDetailMasterRepository.SaveOrUpdateLoanBusinessDetailsMaster(loanBusinessDetailsMaster, userSessionEntity.UserID);

                        }
                        ////

                    }
                    if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Save.ToString() && loanApplicationParam.IsFundingApp)
                        commonResponse = this.SaveFundingApplicationData(loanApplicationParam, userSessionEntity);
                    if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Save.ToString() && loanApplicationParam.IsApplicationDocuments)
                        commonResponse = this.SaveLoanApplicationDocuments(loanApplicationParam, userSessionEntity);



                    if (commonResponse.StatusMessage == "Success")
                    {
                        if (commonResponse.ID > 0)
                        {
                            _Logger.LogDebug(String.Format("Save is successfull  by User {0} with loanApplicationID {1}", userSessionEntity.UserID, createResponse.ID));
                            _StateName = "SetState";
                            loanApplicationParam.LoanApplicationID = commonResponse.ID;
                            this.CreateProcessInstance(commonResponse.ID, WorkFlowID);
                            //Change Work Flow Temp comment
                            //CommonConstants.ThresholdRequestAmount = 0;
                            //var masterOptionResponse = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                            //if (masterOptionResponse != null && masterOptionResponse.Count > 0)
                            //{
                            //    CommonConstants.ThresholdRequestAmount = long.Parse(masterOptionResponse.FirstOrDefault().OptionValue);
                            //}
                            //if (loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.RequestedFundAmount > 250000)
                            //if (loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount)
                            
                            createResponse = this.ExecuteWorkflowCommand(commonResponse.ID, loanApplicationParam, WorkFlowID, _StateName, userSessionEntity);
                            if (!createResponse.IsSuccess)
                            {
                                createResponse.Message = "Unable to process command. Please try after sometime.";
                                return createResponse;
                            }
                            createResponse.IsSuccess = true;
                            createResponse.Message = GetWorkFlowMessage(loanApplicationParam.CommandName);
                            createResponse.ID = loanApplicationParam.LoanApplicationID;
                        }
                    }
                    else
                    {
                        createResponse.IsSuccess = false;
                        createResponse.ValidationErrors = commonResponse.ValidationErrors;
                        createResponse.Message = "Loan Application creation failed.";
                        return createResponse;
                    }
                }
                else
                {
                    //saving application details when RMI from UW only for (cfo/controller roles)
                    bool IsCfoORControllerAcceptRMI = false;
                    User user = this._userRepository.GetByUserID(userSessionEntity.UserID);
                    if (user != null)
                    {
                        var activeRoles = user.UserRoles.Where(a => a.IsActive == true && (a.RoleID == 4 || a.RoleID == 6)).FirstOrDefault();
                        if (activeRoles != null)
                        {
                            IsCfoORControllerAcceptRMI = true;

                        }
                    }

                    if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Accept.ToString() && IsCfoORControllerAcceptRMI && loanApplicationParam.IsCfoORControllerAcceptByRMI)
                    {
                        commonResponse = this.SaveLoanApplication(loanApplicationParam, userSessionEntity);
                    }
                    var loanApp = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(loanApplicationParam.LoanApplicationID);
                    //New comment below code
                    /*if ((loanApp.ApplicationStatusID == 4 || loanApp.ApplicationStatusID == 7) && (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Accept.ToString() || loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Approve.ToString()))
                    {                        
                        if (processInstance.WorkFlowID == 1)
                        {
                            //if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Approve.ToString() && processInstance == null)
                            //{
                            //    WorkFlowID = 2;
                            //}
                            //var fundingApplication = this._fundingApplicationRepository.GetAll().AsNoTracking().FirstOrDefault(a => a.LoanApplicationID == commonResponse.ID);
                            //if(fundingApplication != null)
                            //{
                            //    if(loanApplicationParam.FundingApplication.IsPaymentSchedule == false)
                            //    {
                            //        fundingApplication.IsPaymentSchedule = false;
                            //        this._fundingApplicationRepository.SaveOrUpdateLoanFundingApplication(fundingApplication, userSessionEntity.UserID);
                            //    }
                            //}
                            //New comment below code
                            this.CreateProcessInstanceForSPA(loanApp.LoanApplicationID, WorkFlowID, 2);
                            processInstance = WorkflowInit.GetProcessInstanceByProcessID(loanApplicationParam.LoanApplicationID);
                            if (processInstance != null)
                            {
                                WorkFlowID = processInstance.WorkFlowID;
                            }

                            //this._LoanApplicationRepository.SaveLoanApplicationWorkFlowDetails(commonResponse.ID, WorkFlowID, userSessionEntity.UserID);
                        }
                    }*/
                    var programInvitation = _ProgramInvitationRepository.GetProgramInvitation(loanApplicationParam.ProgramInvitationID);
                    decimal availableLimit = programInvitation != null ? GetAvailableLimit(programInvitation.ProgramID) : 0;
                    
                    //CommonConstants.ThresholdRequestAmount = 0;
                    //var masterOptionResponse = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                    //if (masterOptionResponse != null && masterOptionResponse.Count > 0)
                    //{
                    //    CommonConstants.ThresholdRequestAmount = long.Parse(masterOptionResponse.FirstOrDefault().OptionValue);
                    //}

                    //if (loanApplicationParam.CommandName == "Complete Funding" && loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.RequestedFundAmount > 250000)
                    //if (loanApplicationParam.CommandName == "Complete Funding" && loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount)
                    if (loanApplicationParam.CommandName == "Complete Funding" && loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.IsPaymentSchedule == true)
                    {
                        if (programInvitation != null)
                        {
                            if (availableLimit < 0 || loanApplicationParam.FundUtilization.TransactionAmount > availableLimit)
                            {
                                createResponse.Message = "Please add fund to the Program to proceed with fund disbursement.";
                                createResponse.ID = loanApplicationParam.LoanApplicationID;
                                createResponse.IsSuccess = false;
                                createResponse.IsAvailableLimitExceeds = true;
                                return createResponse;
                            }
                            var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(loanApplicationParam.LoanApplicationID);

                            if (loanApplicationParam.FundingApplication.PaymentScheduleSummary.ID > 0 && loanApplicationParam.FundingApplication.PaymentScheduleTransaction.FirstOrDefault().PaymentScheduleID > 0)//update existing
                            {
                                var paymentScheduleSummary = _fundingSourceRepository.GetPaymentScheduleSummaryByID(loanApplicationParam.FundingApplication.PaymentScheduleSummary.ID);
                                if (paymentScheduleSummary != null)
                                {
                                    paymentScheduleSummary.FundRequestedAmount = loanApplicationParam.FundingApplication.PaymentScheduleSummary.FundRequestedAmount;
                                    paymentScheduleSummary.FundAllocatedAmount = loanApplicationParam.FundingApplication.PaymentScheduleSummary.FundAllocatedAmount;
                                    paymentScheduleSummary.FundPendingAmount = paymentScheduleSummary.FundPendingAmount - loanApplicationParam.FundUtilization.TransactionAmount.Value;
                                    paymentScheduleSummary.FundDisbursedAmount = loanApplicationParam.FundingApplication.PaymentScheduleSummary.FundDisbursedAmount + loanApplicationParam.FundUtilization.TransactionAmount.Value;
                                    
                                    paymentScheduleSummary.LastModifiedByUserID = userSessionEntity.UserID;
                                    paymentScheduleSummary.LastModifiedDateTime = DateTime.Now;
                                    this._fundingSourceRepository.SaveOrUpdatePaymentScheduleSummary(paymentScheduleSummary, userSessionEntity.UserID);
                                }


                                if (loanApplicationParam.FundingApplication.PaymentScheduleTransaction != null && loanApplicationParam.FundingApplication.PaymentScheduleTransaction.FirstOrDefault().PaymentScheduleID > 0)//update existing
                                {
                                    if (loanApplicationParam.FundingApplication.PaymentScheduleTransaction != null && loanApplicationParam.FundingApplication.PaymentScheduleTransaction.Count > 0)
                                    {
                                        var paymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransactionById(loanApplicationParam.FundingApplication.PaymentScheduleTransaction.FirstOrDefault().PaymentScheduleID);
                                        if (paymentSchedule != null)
                                        {
                                            paymentSchedule.TransactionStatusID = 2;
                                            paymentSchedule.CreatedByUserID = userSessionEntity.UserID;
                                            paymentSchedule.CreatedDateTime = DateTime.Now;
                                            paymentSchedule.LastModifiedByUserID = userSessionEntity.UserID;
                                            paymentSchedule.LastModifiedDateTime = DateTime.Now;
                                            paymentSchedule.DisbursedDate = DateTime.Now;
                                            this._fundingSourceRepository.SaveOrUpdatePaymentScheduleTransaction(paymentSchedule, userSessionEntity.UserID);
                                        }
                                    }
                                }


                                var paymentScheduleTransactions = _fundingSourceRepository.GetPaymentScheduleTransaction(programInvitation.BusinessID, loanApplicationParam.LoanApplicationID);
                                if (paymentScheduleTransactions != null && paymentScheduleTransactions.Count > 0)
                                {
                                    paymentScheduleTransactions = paymentScheduleTransactions.FindAll(ps => ps.TransactionStatusID == 1);
                                }

                                if (paymentScheduleTransactions != null && paymentScheduleTransactions.Count > 0)
                                {

                                    if (paymentScheduleTransactions != null && paymentScheduleTransactions.Count > 0)
                                    {
                                        SavePaymentScheduleStatus(loanApplication.LoanApplicationID, paymentScheduleTransactions.Count, userSessionEntity);
                                        this._LoanApplicationRepository.UpdateLoanApplicationApplicationStatus(loanApplication, userSessionEntity.UserID, (int)ApplicationStatusEnumeration.SPAAccountDisbursed);

                                        FundUtilization fundUtilization = new FundUtilization();
                                        fundUtilization.ApplicationID = loanApplication.LoanApplicationID;
                                        fundUtilization.Comment = loanApplicationParam.FundUtilization.Comment;
                                        fundUtilization.DateofDisbursement = loanApplicationParam.FundUtilization.DateofDisbursement.Value;
                                        fundUtilization.OriginatingBankAccount = loanApplicationParam.FundUtilization.OriginatingBankAccount;
                                        fundUtilization.DestinationBankAccount = loanApplicationParam.FundUtilization.DestinationBankAccount;
                                        fundUtilization.BankRoutingNumber = loanApplicationParam.FundUtilization.BankRoutingNumber;
                                        fundUtilization.TransactionAmount = loanApplicationParam.FundUtilization.TransactionAmount.Value;
                                        fundUtilization.TransactionDate = System.DateTime.Now;
                                        fundUtilization.TransactionTypeID = (int)TransactionTypeEnumeration.Allocated;
                                        fundUtilization.CreatedDateTime = System.DateTime.Now;
                                        fundUtilization.CreatedByUserID = userSessionEntity.UserID;
                                        fundUtilization.IsActive = true;
                                        fundUtilization.LastModifiedDateTime = System.DateTime.Now;
                                        fundUtilization.LastModifiedByUserID = userSessionEntity.UserID;
                                        fundUtilization.FundingSourceID = loanApplication?.FundingApplication?.ProgramID;
                                        //Add document 

                                        this._fundUtilizationRepository.SaveOrUpdateFundUtilization(fundUtilization, userSessionEntity.UserID);

                                        this.AddPersistenceStateHistory(processInstance);
                                        

                                        var emailSent = this.SendFundDisbursedEmailNotifiaction(loanApplication, userSessionEntity);
                                    }
                                }
                                //var updatedPaymentScheduleSummary = _fundingSourceRepository.GetPaymentScheduleSummaryByID(loanApplicationParam.FundingApplication.PaymentScheduleSummary.ID);
                                if (paymentScheduleTransactions != null && paymentScheduleTransactions.Count == 0)
                                {
                                    SavePaymentScheduleStatus(loanApplication.LoanApplicationID, 0, userSessionEntity);
                                    createResponse = this.ExecuteWorkflowCommand(loanApplicationParam.LoanApplicationID, loanApplicationParam, WorkFlowID, _StateName, userSessionEntity);
                                    if (!createResponse.IsSuccess)
                                    {
                                        createResponse.Message = "Unable to process command. Please try after sometime.";
                                        return createResponse;
                                    }
                                    createResponse.Message = GetWorkFlowMessage(loanApplicationParam.CommandName);//"Transaction completed successfully.";
                                    createResponse.ID = loanApplicationParam.LoanApplicationID;
                                    createResponse.IsSuccess = true;
                                    //createResponse.Message = "Application saved successfully.";
                                    createResponse.ID = loanApplicationParam.LoanApplicationID;
                                    return createResponse;
                                }
                                else
                                {
                                    var paymentScheduleStatus = this._LoanApplicationRepository.GetPaymentScheduleStatusById(loanApplication.LoanApplicationID);
                                    createResponse.Message = GetWorkFlowMessage("SPAMessage").Replace("{n}", NumberOrdinal.Ordinal(paymentScheduleStatus.DisbursementCount)); //"Transaction completed successfully.";
                                    createResponse.ID = loanApplicationParam.LoanApplicationID;
                                    createResponse.IsSuccess = true;
                                   // createResponse.Message = "Application saved successfully.";
                                    createResponse.ID = loanApplicationParam.LoanApplicationID;
                                    return createResponse;
                                }
                            }
                            else
                            {
                                createResponse.Message = "Please update the Schedule of Payment details to approve this application.";
                                createResponse.ID = loanApplicationParam.LoanApplicationID;
                                createResponse.IsSuccess = false;
                                createResponse.ID = loanApplicationParam.LoanApplicationID;
                                return createResponse;

                            }
                        }
                    }

                    //if (loanApplicationParam.CommandName == "Complete Funding" && loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.RequestedFundAmount <= CommonConstants.ThresholdRequestAmount)
                    //if (loanApplicationParam.CommandName == "Complete Funding" && loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.RequestedFundAmount <= 250000)
                    //New comment below code
                    /*if (loanApplicationParam.CommandName == "Complete Funding" && loanApplicationParam.FundingApplication != null && loanApplicationParam.FundingApplication.IsPaymentSchedule == false)
                    {

                        if (availableLimit <= 0 || loanApplicationParam.FundUtilization.TransactionAmount > availableLimit)
                        {
                            createResponse.Message = "Please add fund to the Program to proceed with fund disbursement.";
                            createResponse.ID = loanApplicationParam.LoanApplicationID;
                            createResponse.IsSuccess = false;
                            createResponse.IsAvailableLimitExceeds = true;
                            return createResponse;
                        }
                    }*/
                    
                    createResponse = this.ExecuteWorkflowCommand(loanApplicationParam.LoanApplicationID, loanApplicationParam, WorkFlowID, _StateName, userSessionEntity);
                    if (!createResponse.IsSuccess)
                    {
                        createResponse.Message = "Unable to process command. Please try after sometime.";
                        return createResponse;
                    }
                    createResponse.Message = GetWorkFlowMessage(loanApplicationParam.CommandName);//"Transaction completed successfully.";
                    createResponse.ID = loanApplicationParam.LoanApplicationID;
                    createResponse.IsSuccess = true;
                    //createResponse.Message = "Application saved successfully.";
                    createResponse.ID = loanApplicationParam.LoanApplicationID;
                    return createResponse;
                }
                              

            }
            catch (ConditionFailedException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationCommandHandler-> loanApplicationParam, userSessionEntity ", null);
                createResponse.IsSuccess = false;
                createResponse.Message = ex.Message;
                return createResponse;
            }
            catch (WorkFlowException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationCommandHandler-> loanApplicationParam, userSessionEntity ", null);
                createResponse.IsSuccess = false;
                createResponse.Message = "Unable to process command. Please try after sometime.";
                return createResponse;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationCommandHandler-> loanApplicationParam, userSessionEntity ", null);
                createResponse.IsSuccess = false;
                createResponse.Message = "Unable to process command. Please try after sometime.";
                return createResponse;
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationCommandHandler-> loanApplicationParam, userSessionEntity ", null);
                createResponse.IsSuccess = false;
                createResponse.Message = "Unable to process command. Please try after sometime.";
                return createResponse;
            }
            createResponse.IsSuccess = true;
            createResponse.Message = GetWorkFlowMessage(loanApplicationParam.CommandName);//"Application saved successfully.";
            createResponse.ID = loanApplicationParam.LoanApplicationID;
            return createResponse;
        }
        private void CreateProcessInstance(long processID, long workFlowID)
        {
            try
            {
                _Logger.LogDebug(String.Format("Process instance creation is initiated for applicationID {0}", processID));
                if (WorkflowInit.Runtime.IsProcessExists(processID, workFlowID))
                {
                    _Logger.LogDebug(String.Format("Process instance is already created for applicationID {0}", processID));
                    return;
                }
                WorkflowInit.CreateProcessInstance(processID, workFlowID);
                _Logger.LogDebug(String.Format("Process instance is successfully created for applicationID {0}", processID));
                return;
            }
            catch (WorkFlowException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateProcessInstanceForSPA(long processID, long workFlowID, long workFlowSpaId)
        {
            try
            {
                _Logger.LogDebug(String.Format("Process instance creation is initiated for applicationID {0}", processID));
                if (WorkflowInit.Runtime.IsProcessExists(processID, workFlowID))
                {
                    _Logger.LogDebug(String.Format("Process instance is already created for applicationID {0}", processID));
                    WorkflowInit.Runtime.UpdateProcessInstance(processID, workFlowID, workFlowSpaId);
                    return;
                }
                //WorkflowInit.CreateProcessInstance(processID, workFlowID);
                _Logger.LogDebug(String.Format("Process instance is successfully created for applicationID {0}", processID));
                return;
            }
            catch (WorkFlowException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateProcessInstanceState(long processID, long workFlowID)
        {
            try
            {
                _Logger.LogDebug(String.Format("Process instance creation is initiated for applicationID {0}", processID));
                if (WorkflowInit.Runtime.IsProcessExists(processID, workFlowID))
                {
                    _Logger.LogDebug(String.Format("Process instance is already created for applicationID {0}", processID));
                    //WorkflowInit.Runtime.UpdateProcessInstanceState(processID, workFlowID, "UWReviewRequestedMoreDetailsCompletedByBorrower");
                    WorkflowInit.Runtime.UpdateProcessInstanceState(processID, workFlowID, "UWReview");
                    return;
                }
                _Logger.LogDebug(String.Format("Process instance is successfully created for applicationID {0}", processID));
                return;
            }
            catch (WorkFlowException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BaseResponse ExecuteWorkflowCommand(long processID, LoanApplicationRequest loanApplicationParam, long workFlowID, string stateName, UserSessionEntity userSessionEntity)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                User user = this._userRepository.GetByUserID(userSessionEntity.UserID);

                if (user == null)
                {
                    _Logger.LogError(String.Format("user associated with UserID {0} is null in ExecuteWorkflowCommand.", userSessionEntity.UserID));
                    baseResponse.IsSuccess = false;
                    return baseResponse;
                }
                String ProcessStateName = "";
                Dictionary<string, object> workFlowParameters = new Dictionary<string, object>();

                if (ThoughtFocus.Workflow.WorkflowInit.Runtime.IsProcessExists(processID, workFlowID))
                {
                    workFlowParameters.Add("UserSessionModel", userSessionEntity);
                    
                    workFlowParameters.Add("Command", loanApplicationParam.CommandName);
                    workFlowParameters.Add("Comment", loanApplicationParam.TransitionComments);

                    if (stateName == "SetState")
                    {
                        workFlowParameters.Add("FundUtilization", loanApplicationParam.FundUtilization);
                        ProcessStateName = WorkflowInit.Runtime.GetCurrentActivityName(processID, workFlowID);

                        if (String.IsNullOrEmpty(ProcessStateName))
                        {
                            _Logger.LogError(String.Format("ProcessStateName is Empty for ProcessID {0}  in ExecuteWorkflowCommand.", processID));
                            baseResponse.IsSuccess = false;
                            return baseResponse;
                        }

                        if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Save.ToString())
                        {
                            if (ProcessStateName == ApplicationStatusEnumeration.RequestedMoreDetails.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.RequestedMoreDetails.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                            else if (ProcessStateName == ApplicationStatusEnumeration.UWReviewToRequestedMoreDetails.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.UWReviewToRequestedMoreDetails.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                            else if (ProcessStateName == ApplicationStatusEnumeration.RequestedMoreInfo.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.RequestedMoreInfo.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                            else
                            {

                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.Drafted.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                        }

                        if (loanApplicationParam.CommandName == WorkFlowCommandEnumeration.Submit.ToString())
                        {
                            if (ProcessStateName == ApplicationStatusEnumeration.Initialized.ToString() || ProcessStateName == ApplicationStatusEnumeration.Drafted.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.Submitted.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                            else if (ProcessStateName == ApplicationStatusEnumeration.RequestedMoreInfo.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.RequestCompleted.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                            else if (ProcessStateName == ApplicationStatusEnumeration.RequestedMoreDetails.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.RequestMoreDeatailsCompletedByBorrower.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                            else if (ProcessStateName == ApplicationStatusEnumeration.UWReviewToRequestedMoreDetails.ToString())
                            {
                                baseResponse = WorkflowInit.SetState(processID, workFlowID, ApplicationStatusEnumeration.UWReviewRequestedMoreDetailsCompletedByBorrower.ToString(), workFlowParameters);
                                if (!baseResponse.IsSuccess)
                                {
                                    _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                                    baseResponse.IsSuccess = false;
                                    return baseResponse;
                                }
                            }
                        }
                    }
                    else
                    {
                        bool isTransactionDocument = loanApplicationParam.ApplicationDocuments.Any(a => a.DocumentTypeID == (long)DocumentTypeEnumeration.FundingDetailDocument);
                        if (isTransactionDocument)
                        {
                            loanApplicationParam.FundUtilization.ApplicationDocument = new DocumentRequest();
                            foreach (var document in loanApplicationParam.ApplicationDocuments)
                            {
                                loanApplicationParam.FundUtilization.ApplicationDocument.DocumentGUID = document.DocumentGUID;
                                loanApplicationParam.FundUtilization.ApplicationDocument.DocumentTypeID = document.DocumentTypeID;
                                loanApplicationParam.FundUtilization.ApplicationDocument.FileName = document.FileName;
                                loanApplicationParam.FundUtilization.ApplicationDocument.FileSize = document.FileSize;
                                loanApplicationParam.FundUtilization.ApplicationDocument.DocumentName = document.DocumentName;
                                loanApplicationParam.FundUtilization.ApplicationDocument.LoanApplicationID = loanApplicationParam.LoanApplicationID;
                                loanApplicationParam.FundUtilization.ApplicationDocument.PhysicalFileStorageKey = document.PhysicalFileStorageKey;
                            }
                        }

                        workFlowParameters.Add("FundUtilization", loanApplicationParam.FundUtilization);
                        _Logger.LogDebug(String.Format("ExecuteCommand  is initiated for applicationID {0} with State {1}", processID, loanApplicationParam.CommandName));
                        baseResponse = WorkflowInit.ExecuteCommand(processID, loanApplicationParam.CommandName, WorkFlowID, workFlowParameters);
                        if (!baseResponse.IsSuccess)
                        {
                            _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.ExecuteCommand call for Process ID {0}", processID));
                            baseResponse.IsSuccess = false;
                            return baseResponse;
                        }
                    }
                }
                else
                {
                    _Logger.LogError(String.Format("ExecuteCommad was called before Process is created  for ProcessID {0} with StateName {1}", processID, stateName));
                }
            }
            catch (WorkFlowException ex)
            {
                throw ex;
            }
            catch (ConditionFailedException ex)
            {
                throw ex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            baseResponse.IsSuccess = true;
            baseResponse.ID = processID;
            return baseResponse;
        }
        public BaseResponse ExecuteRequestPaymentWorkflowCommand(long processID, LoanApplicationRequest loanApplicationParam, long workFlowID, string stateName, UserSessionEntity userSessionEntity)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                User user = this._userRepository.GetByUserID(userSessionEntity.UserID);

                if (user == null)
                {
                    _Logger.LogError(String.Format("user associated with UserID {0} is null in ExecuteWorkflowCommand.", userSessionEntity.UserID));
                    baseResponse.IsSuccess = false;
                    return baseResponse;
                }
                String ProcessStateName = "";
                Dictionary<string, object> workFlowParameters = new Dictionary<string, object>();

                if (ThoughtFocus.Workflow.WorkflowInit.Runtime.IsProcessExists(processID, workFlowID))
                {
                    workFlowParameters.Add("UserSessionModel", userSessionEntity);

                    workFlowParameters.Add("Command", loanApplicationParam.CommandName);
                    workFlowParameters.Add("Comment", loanApplicationParam.TransitionComments);

                    if (stateName == "SetState")
                    {
                        workFlowParameters.Add("FundUtilization", loanApplicationParam.FundUtilization);
                        ProcessStateName = WorkflowInit.Runtime.GetCurrentActivityName(processID, workFlowID);

                        if (String.IsNullOrEmpty(ProcessStateName))
                        {
                            _Logger.LogError(String.Format("ProcessStateName is Empty for ProcessID {0}  in ExecuteWorkflowCommand.", processID));
                            baseResponse.IsSuccess = false;
                            return baseResponse;
                        }

                        baseResponse = WorkflowInit.SetStateSPA(processID, workFlowID, "UWReview", workFlowParameters);
                        if (!baseResponse.IsSuccess)
                        {
                            _Logger.LogError(String.Format("baseResponse  returned false in WorkflowInit.SetState call for Process ID {0}", processID));
                            baseResponse.IsSuccess = false;
                            return baseResponse;
                        }
                    }
                }
                else
                {
                    _Logger.LogError(String.Format("ExecuteCommad was called before Process is created  for ProcessID {0} with StateName {1}", processID, stateName));
                }
            }
            catch (WorkFlowException ex)
            {
                throw ex;
            }
            catch (ConditionFailedException ex)
            {
                throw ex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            baseResponse.IsSuccess = true;
            baseResponse.ID = processID;
            return baseResponse;
        }
        public WorkFlowCommandResponse GetWorkFlowCommands(long applicationID, UserSessionEntity userSessionEntity)
        {
            WorkFlowCommandResponse workFlowCommandResponse = new Domain.CustomView.WorkFlowCommandResponse();
            #region Validation

            if (userSessionEntity == null)
            {
                workFlowCommandResponse.IsSuccess = false;
                workFlowCommandResponse.Message = "Unable to fetch commands. Please try after sometime.";
                return workFlowCommandResponse;
            }
            if (userSessionEntity.UserID == 0)
            {
                workFlowCommandResponse.IsSuccess = false;
                workFlowCommandResponse.Message = "Unable to fetch commands. Please try after sometime.";
                return workFlowCommandResponse;
            }

            #endregion

            List<WorkflowCommandViewEntity> WorkflowCommandViewEntities = new List<WorkflowCommandViewEntity>();
            try
            {
                Guid UserID = Guid.Empty;
                User user = this._userRepository.GetByUserID(userSessionEntity.UserID);

                if (user != null)
                {
                    UserID = user.IdentityID;
                }
                else
                {
                    _Logger.LogError(String.Format("user is null for userID {0}", userSessionEntity.UserID));
                    workFlowCommandResponse.IsSuccess = false;
                    workFlowCommandResponse.WorkFlowCommands = null;
                    workFlowCommandResponse.Message = "Unable to fetch commands. Please try after sometime.";
                }

                
                var processInstance = WorkflowInit.GetProcessInstanceByProcessID(applicationID);
                if(processInstance != null)
                {
                    WorkFlowID = processInstance.WorkFlowID;
                }

                WorkflowCommandViewEntities = WorkflowInit.GetWorkFlowCommands(applicationID, UserID, WorkFlowID);
                var fundingApplication = this._fundingApplicationRepository.GetAll().AsNoTracking().FirstOrDefault(a => a.LoanApplicationID == applicationID && a.IsPaymentSchedule == true);
                if(fundingApplication != null)
                {
                    var re = WorkflowCommandViewEntities.Find(w => w.WorkflowCommandID == 7);
                    if(re != null)
                    {
                        WorkflowCommandViewEntities.Remove(re);
                    }
                    
                }
                workFlowCommandResponse.IsSuccess = true;
                workFlowCommandResponse.WorkFlowCommands = WorkflowCommandViewEntities;
            }
            catch (Exception ex)
            {
                //LoggerExtensions.LogMessage(Logger, ex);
                workFlowCommandResponse.IsSuccess = false;
                workFlowCommandResponse.WorkFlowCommands = null;
                workFlowCommandResponse.Message = "Unable to fetch commands. Please try after sometime." + ex;
            }
            //catch (Exception ex 1)
            //{
            //    //LoggerExtensions.LogMessage(Logger, ex);
            //    workFlowCommandResponse.IsSuccess = false;
            //    workFlowCommandResponse.WorkFlowCommands = null;
            //    workFlowCommandResponse.Message = "Unable to fetch commands. Please try after sometime.";
            //}
            //catch (Exception)
            //{
            //    // LoggerExtensions.LogMessage(Logger, ex);
            //    workFlowCommandResponse.IsSuccess = false;
            //    workFlowCommandResponse.WorkFlowCommands = null;
            //    workFlowCommandResponse.Message = "Unable to fetch commands. Please try after sometime.";
            //}
            return workFlowCommandResponse;
        }

        public ApplicationListResponse GetLoanApplications(ExportExcel filterRequest, UserSessionEntity userSessionEntity)
        {
            ApplicationListResponse applicationListResponse = new ApplicationListResponse();
            applicationListResponse.ApplicationPageResultEntityExportToExcel = new PageResultEntity<ApplicationListingViewEntityExportToExcel>();
            List<ApplicationListingViewEntityExportToExcel> listOfApplicationListingViewEntity = null;

            try
            {
                List<long> ProgramIDs = filterRequest.FilterParameters.Where(a => a.Key == "ProgramID").Select(a => Convert.ToInt64(a.Value)).ToList();

                IQueryable<ThoughtFocus.DataAccess.Models.Application.LoanApplication> query = this._LoanApplicationRepository.GetLoanApplications(userSessionEntity.UserID, ProgramIDs);

                listOfApplicationListingViewEntity = query.OrderByDescending(o=>o.DateApplied)
                .Select(x => new ApplicationListingViewEntityExportToExcel
                {
                    LoanNumber = x.LoanNumber,
                    DateApplied = x.DateApplied.ToString("MM/dd/yyyy").Replace("-", "/"),
                    BusinessName = x.LoanBusinessDetail != null ? x.LoanBusinessDetail.BusinessName : string.Empty,
                    AffiliateName = x.LoanBusinessDetail.Affiliate != null ? x.LoanBusinessDetail.Affiliate.AffiliateName : string.Empty,
                    LoanProgramName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.FundingApplication == null ? string.Empty : x.FundingApplication.FundingSource != null ? x.FundingApplication.FundingSource.ProgramName : string.Empty),
                    LoanType = "Fund",//x.ApplicationType.ApplicationTypeName != null ? x.ApplicationType.ApplicationTypeName : string.Empty,
                    LoanAmount = x.FundingApplication == null ? "0" : string.Format("{0:#,#}", x.FundingApplication.RequestedFundAmount),
                    ApplicationStatus = x.PaymentScheduleStatus != null ? (x.ApplicationStatusID == 40 ? x.PaymentScheduleStatus.Status : x.ApplicationStatus.Description) : x.ApplicationStatus.Description,//x.ApplicationStatus.Description,
                    ContactInfo = x.ProgramInvitation.ProgramInvitee.Where(pi => pi.ProgramInvitationID == x.ProgramInvitationID && pi.ProgramInvitation.ProgramID == x.ProgramInvitation.ProgramID).FirstOrDefault().Contact.EmailAddress,

                }).ToList();
                var paymentScheduleSummarys = this._fundingSourceRepository.GetAllPaymentScheduleSummary();
                foreach (var pss in paymentScheduleSummarys)
                {
                    var listOfApplication = listOfApplicationListingViewEntity.Find(l => l.LoanApplicationID == pss.LoanApplicationID);
                    if (listOfApplication != null)
                    {
                        listOfApplication.FundAllocatedAmount = pss.FundAllocatedAmount > 0 ? string.Format("{0:#,#}", pss.FundAllocatedAmount) : "0";
                    }
                }
                var loanBusinessDetails = this._loanBusinessDetailRepository.GetAll().ToList();
                //if (loanBusinessDetails.Count > 0)
                //{
                //    foreach (var businessDetail in loanBusinessDetails)
                //    {
                //        var listOfApplication = listOfApplicationListingViewEntity.Find(l => l.LoanApplicationID == businessDetail.LoanApplicationID);
                //        if (listOfApplication != null)
                //        {
                //            listOfApplication.ContactInfo = businessDetail.EmailAddress;
                //        }
                //    }

                //}
                applicationListResponse.ApplicationPageResultEntityExportToExcel.DataList = listOfApplicationListingViewEntity;

                if (applicationListResponse.ApplicationPageResultEntityExportToExcel != null)
                {
                    applicationListResponse.IsSuccess = true;
                }
                else
                {
                    applicationListResponse.IsSuccess = false;
                    applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetAllLoanApplicationInformation", null);
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in ApplicationServiceImpl-> GetAllLoanApplicationInformation", null);
                applicationListResponse.IsSuccess = false;
                applicationListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return applicationListResponse;
        }
        public IQueryable<LoanApplication> GetAllLoanApplicationsForUser(long UserID)
        {
            var programIDs = new List<long>();
            programIDs.Add(0);
            return _LoanApplicationRepository.GetLoanApplications(UserID, programIDs);
        }
        public long GetProgramStatusId(long programInvitationId)
        {
            var loanApplicationStatusId = this._loanBusinessDetailRepository.GetProgramStatusId(programInvitationId);

            return loanApplicationStatusId;
        }
        public List<BusinessUser> GetBussinessUsers(long programInvitationId)
        {
            List<BusinessUser> businessUser = new List<BusinessUser>();
            var programInvitation = _ProgramInvitationRepository.GetProgramInvitation(programInvitationId);

            if (programInvitation != null)
            {
                businessUser = _businessContactRepository.GetBusinessContactsByID(programInvitation.BusinessID);
            }

            return businessUser;
        }


        public PaymentScheduleSummaryResponse GetThresholdApplicationSummary(long businessID, long programID, long applicationID)
        {
            var response = new PaymentScheduleSummaryResponse();
            var summary = new PaymentScheduleSummaryDetails();

            try
            {
                // parameter validation

                if (businessID <= 0 || programID <= 0 || applicationID <= 0)
                {
                    response.IsSuccess = false;
                    response.IsValidationError = true;
                    response.ValidationError = new ValidationErrorResponse();
                    response.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "businessID/programID/applicationID";
                    validationError.ErrorMessage = "Please provide valid business ID , program ID & application ID";
                    response.ValidationError.ValidationError = new List<ValidationError>();
                    response.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                    /** Get transaction Response  list **/


                    summary = GetPaymentScheduleSummaryDetail(businessID, programID, applicationID);

                    response.paymentScheduleSummary = summary;
                    response.IsSuccess = true;
                    response.IsValidationError = false;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetThresholdApplicationSummary ", null);
                response.IsSuccess = false;
                response.IsValidationError = false;
                response.Message = "Failed to fetch to get Threshold Application Summary.";
            }
            return response;
        }
        private PaymentScheduleSummaryDetails GetPaymentScheduleSummaryDetail(long businessID, long programID, long applicationID)
        {
            var summary = new PaymentScheduleSummaryDetails();
            var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(businessID, applicationID);


            var lstLoanDetails = this._fundingApplicationRepository.GetAll().Where(x => x.LoanApplicationID == applicationID).ToList();

            var lstsummary = _fundingSourceRepository.GetPaymentScheduleSummary(businessID, applicationID);
            //if(applicationID > 0 && lstsummary.Count == 0)
            //{
            //    var paymentScheduleSummary = this._fundingSourceRepository.GetPaymentScheduleSummaryByLoanID(applicationID);
            //    lstsummary.Add(paymentScheduleSummary);
            //}
            //loan level calculation
            if (lstsummary != null && lstsummary.Count > 0)
            {
                summary.FundPaymentScheduleID = lstsummary.Where(w => w.LoanApplicationID == applicationID).FirstOrDefault().ID;
                summary.ProgramID = programID;
                summary.BusinessID = businessID;

               
                summary.FundAllocatedAmount = lstsummary.Where(w => w.LoanApplicationID == applicationID).OrderByDescending(x => x.ID).FirstOrDefault().FundAllocatedAmount.ToString();//if dublicate records
                summary.FundAllocatedSummaryAmount = Convert.ToDecimal(summary.FundAllocatedAmount);
                summary.FundAllocatedAmount = "$ " + string.Format("{0:#,#}", Decimal.Truncate(Convert.ToDecimal(summary.FundAllocatedAmount)));

                summary.FundRequestedAmount = lstLoanDetails.Where(w => w.LoanApplicationID == applicationID).FirstOrDefault().RequestedFundAmount.ToString();

                summary.FundRequestedSummaryAmount = Convert.ToDecimal(summary.FundRequestedAmount);
                summary.FundRequestedAmount = "$ " + string.Format("{0:#,#}", Decimal.Truncate(Convert.ToDecimal(summary.FundRequestedAmount)));

                summary.FundDisbursedAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 2 && w.LoanApplicationID == applicationID).Sum(p => p.FundedAmount).ToString();
                summary.FundDisbursedSummaryAmount = Convert.ToDecimal(summary.FundDisbursedAmount);
                summary.FundDisbursedAmount = "$ " + string.Format("{0:#,#}", Decimal.Truncate(Convert.ToDecimal(summary.FundDisbursedAmount)));

                
                summary.FundPendingAmount = lstsummary.Where(w => w.LoanApplicationID == applicationID).OrderByDescending(x => x.ID).FirstOrDefault().FundPendingAmount.ToString();//if dublicate records
               
                summary.FundPendingSummaryAmount = lstsummary.Where(w => w.LoanApplicationID == applicationID).OrderByDescending(x => x.ID).FirstOrDefault().FundPendingAmount;//if dublicate records
                summary.FundPendingAmount = "$ " + string.Format("{0:#,#}", Decimal.Truncate(Convert.ToDecimal(summary.FundPendingAmount)));

            }
            else
            {
                summary.ProgramID = programID;
                summary.BusinessID = businessID;
                summary.FundAllocatedSummaryAmount = 0;
                summary.FundAllocatedAmount = "0";
            }
            
            
            

            return summary;
        }

        private void SavePaymentScheduleStatus(long loanApplicationID, int paymentScheduleCount, UserSessionEntity userSessionEntity)
        {
            var paymentScheduleStatus = this._LoanApplicationRepository.GetPaymentScheduleStatusById(loanApplicationID);
            if (paymentScheduleCount > 0)
            {
                if (paymentScheduleStatus != null)
                {
                    paymentScheduleStatus.DisbursementCount += 1;
                    paymentScheduleStatus.Status = paymentScheduleStatus.DisbursementCount.Ordinal() + " Disbursement";
                }
                else
                {
                    paymentScheduleStatus = new PaymentScheduleStatus();
                    paymentScheduleStatus.LoanApplicationID = loanApplicationID;
                    paymentScheduleStatus.DisbursementCount = 1;
                    paymentScheduleStatus.Status = paymentScheduleStatus.DisbursementCount.Ordinal() + " Disbursement";
                    paymentScheduleStatus.ApplicationStatusID = 40;
                }
                
            }
            else
            {
                if (paymentScheduleStatus != null)
                {
                    paymentScheduleStatus.DisbursementCount += 1;
                    paymentScheduleStatus.Status = "Final Disbursement";
                }
                else
                {
                    paymentScheduleStatus = new PaymentScheduleStatus();
                    paymentScheduleStatus.LoanApplicationID = loanApplicationID;
                    paymentScheduleStatus.DisbursementCount = 1;
                    paymentScheduleStatus.Status = "Fund Disbursed";
                }
            }
            this._LoanApplicationRepository.SaveOrUpdateLoanPaymentScheduledStatus(paymentScheduleStatus, userSessionEntity.UserID);
        }
        public PaymentScheduleTransactionResponse GetPaymentScheduleTransactionByApplicationId(long applicationID)
        {
            var transactionResponse = new PaymentScheduleTransactionResponse();
            var lstTransactionDetails = new List<PaymentScheduleTransactionDetail>();

            try
            {
                // parameter validation

                if (applicationID <= 0)
                {
                    transactionResponse.IsSuccess = false;
                    transactionResponse.IsValidationError = true;
                    transactionResponse.ValidationError = new ValidationErrorResponse();
                    transactionResponse.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "application ID";
                    validationError.ErrorMessage = "Please provide valid application ID";
                    transactionResponse.ValidationError.ValidationError = new List<ValidationError>();
                    transactionResponse.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                    /** Get transaction Response  list **/
                    var listfundingType = _fundingTypeRepository.GetAllFundingType();
                    var listTrasactionStatus = _genaralOptionRepository.GetMasterOption("PaymentSchedule");
                    var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransactionByApplicationId( applicationID);             

                    foreach (var transaction in lstPaymentSchedule)
                    {
                        var transactionDetail = new PaymentScheduleTransactionDetail();
                        transactionDetail.PaymentScheduleID = transaction.PaymentScheduleID;
                        transactionDetail.LoanApplicationID = transaction.LoanApplicationID;
                        transactionDetail.BusinessID = transaction.BusinessID;
                        transactionDetail.ProgramID = transaction.ProgramID;
                        transactionDetail.ContactID = transaction.ContactID;
                        // transactionDetail.TransactionDate = transaction.TransactionDate;
                        transactionDetail.FundingTypeID = transaction.FundingTypeID;
                        // transactionDetail.FundingTypeName = transaction.FundingTypeName;
                        transactionDetail.FundedAmount = transaction.FundedAmount;
                        // transactionDetail.FundedAmountFormat = transaction.FundedAmountFormat;
                        transactionDetail.TransactionStatusID = transaction.TransactionStatusID;
                        // transactionDetail.TransactionStatusName = transaction.TransactionStatusName;
                        transactionDetail.CreatedDateTime = transaction.CreatedDateTime;
                        transactionDetail.CreatedByUserID = transaction.CreatedByUserID;
                        transactionDetail.LastModifiedDateTime = transaction.LastModifiedDateTime;
                        transactionDetail.LastModifiedByUserID = transaction.LastModifiedByUserID;
                        transactionDetail.LastModifiedDateTime = transaction.LastModifiedDateTime;


                        //DateTime date = DateTime.Parse(transaction.TransactionDate);
                        transactionDetail.TransactionDate = transaction.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");
                        transactionDetail.TransactionStatusName = listTrasactionStatus != null ? listTrasactionStatus.FirstOrDefault(x => x.OptionID == transaction.TransactionStatusID).OptionValue.ToString() : String.Empty;
                        transactionDetail.FundingTypeName = listfundingType != null ? listfundingType.FirstOrDefault(x => x.FundingTypeID == transaction.FundingTypeID).Type.ToString() : "";
                        transactionDetail.FundedAmountFormat = "$ " + string.Format("{0:#,#}", Decimal.Truncate(transaction.FundedAmount));

                        lstTransactionDetails.Add(transactionDetail);
                    }
                    transactionResponse.PaymentScheduleTransactionList = lstTransactionDetails;
                    transactionResponse.IsSuccess = true;
                    transactionResponse.IsValidationError = false;
                    transactionResponse.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetPaymentScheduleTransactionByApplicationId ", null);
                transactionResponse.IsSuccess = false;
                transactionResponse.IsValidationError = false;
                transactionResponse.Message = "Failed to fetch the payment schedule transactions.";
            }
            return transactionResponse;
        }

        private bool SendFundDisbursedEmailNotifiaction(LoanApplication loanApplication, UserSessionEntity userSessionEntity)
        {
            var email = false;
            var sms = false;

            try
            {
                string callBack = _appSettings.BaseUrl + "/form/0/" + loanApplication.LoanApplicationID;
                userSessionEntity.UserID = userSessionEntity.UserID;
                //string callBackURL = "<tr> <td style='padding: 20px 0 20px 100px;'> <table class='buttonwrapper' border='0' cellspacing='3' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> </td> </tr> </table> </td> </tr> ";
                string callBackURL = "<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> <br /><br /> </td>";
                email = _notificationService.SendSPAEmail("Fund Disbursed", callBackURL, (long)EmailTemplateNameID.FUNDDISBURSED, loanApplication.ProgramInvitationID, userSessionEntity.ContactID, loanApplication.LoanApplicationID, "Borrower", userSessionEntity);
                sms = _notificationService.SendSMS(loanApplication, 28, userSessionEntity);
            }           
            catch (Exception ex)
            {
                ex = null;
                return email;
            }
            return email;
        }

        private decimal GetAvailableLimit(long programId)
        {
            decimal availableLimit = 0;
            List<FundingSource> fundingSources = this._fundingSourceRepository.GetAll().Where(c => c.FundingSourceID == programId && c.IsActive == true).ToList();
            if(fundingSources != null && fundingSources.Count > 0)
            {
                if (fundingSources.FirstOrDefault().FundTransactions != null && fundingSources.FirstOrDefault().FundTransactions.Count > 0)
                {
                    decimal totalFundAdded = fundingSources.FirstOrDefault().FundTransactions.Where(x => x.IsActive == true && x.TransactionTypeID == 1).Sum(y => y.TransactionAmount);
                    decimal totalFundRemoved = fundingSources.FirstOrDefault().FundTransactions.Where(x => x.IsActive == true && x.TransactionTypeID == 2).Sum(y => y.TransactionAmount);
                    decimal totalFundAllocated = fundingSources.FirstOrDefault().FundTransactions.Where(x => x.IsActive == true && x.TransactionTypeID == 3).Sum(y => y.TransactionAmount);
                    availableLimit = Decimal.Truncate((totalFundAdded - (totalFundRemoved + totalFundAllocated)));
                }
            }
            return availableLimit;
        }

        private void AddPersistenceStateHistory(ProcessInstance processInstance)
        {
            var paramIdentityId = processInstance.GetParameter(DefaultDefinitions.ParameterIdentityId.Name);
            var paramImpIdentityId = processInstance.GetParameter(DefaultDefinitions.ParameterImpersonatedIdentityId.Name);

            string identityId = paramIdentityId == null || paramIdentityId.Value == null
                ? string.Empty
                : paramIdentityId.Value.ToString();
            string impIdentityId = paramImpIdentityId == null || paramImpIdentityId.Value == null
                ? identityId
                : paramImpIdentityId.Value.ToString();
            var paymentScheduleStatus = this._LoanApplicationRepository.GetPaymentScheduleStatusById(processInstance.ProcessId);
            WorkflowProcessTransitionHistory history = new WorkflowProcessTransitionHistory()
            {
                ActorIdentityId = impIdentityId,
                ExecutorIdentityId = identityId,
                WorkflowProcessTransitionHistoryID = Guid.NewGuid(),
                IsFinalised = false,
                WorkFlowDefinationID = processInstance.WorkFlowID,
                //TODO Зачем на м финализед тут????
                ProcessInstanceID = processInstance.ProcessId,
                FromActivityName = paymentScheduleStatus.Status,
                FromStateName = paymentScheduleStatus.Status,
                ToActivityName = paymentScheduleStatus.Status,
                ToStateName = paymentScheduleStatus.Status,
                TransitionClassifier = "2",
                TransitionTime = DateTime.Now, //_runtime.RuntimeDateTimeNow,//Was Not Defined Just added in WorkFlowRuntime--
                TriggerName = string.IsNullOrEmpty(processInstance.ExecutedTimer) ? processInstance.CurrentCommand : processInstance.ExecutedTimer
            };
            WorkflowInit.Runtime.AddPersistenceStateHistory(history);
           
        }

        public CommonResponse RequestedAddFundAllocation(LoanApplicationRequest loanApplicationParam,UserSessionEntity userSessionEntity)
        {
            CommonResponse commonCreationParam = null;

            try
            {
                var isUpdated = false;
                this.UpdateProcessInstanceState(loanApplicationParam.LoanApplicationID, 2);
                loanApplicationParam.CommandName = "UWReview";
                var createResponse = this.ExecuteRequestPaymentWorkflowCommand(loanApplicationParam.LoanApplicationID, loanApplicationParam, 2, "SetState", userSessionEntity);
                var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(loanApplicationParam.LoanApplicationID);
                isUpdated = this._LoanApplicationRepository.UpdateLoanApplicationApplicationStatus(loanApplication, userSessionEntity.UserID, 18);
                if (isUpdated)
                {
                    this.RequestedAddFundAllocationEmailNotifiaction(loanApplication, userSessionEntity);
                    commonCreationParam = new CommonResponse(ResponseStatus.Success, "Payment requested successfully.", null);
                    commonCreationParam.ID = loanApplicationParam.LoanApplicationID;
                }
                else
                {
                    commonCreationParam = new CommonResponse(ResponseStatus.Fail, "Payment requested failed", null);
                    commonCreationParam.ID = loanApplicationParam.LoanApplicationID;
                }
            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> RequestedAddFundAllocation ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while Request Payment", null);
            }
            return commonCreationParam;
        }

        private bool RequestedAddFundAllocationEmailNotifiaction(LoanApplication loanApplication, UserSessionEntity userSessionEntity)
        {
            var email = false;
            //var sms = false;

            try
            {
                string callBack = _appSettings.BaseUrl + "/form/0/" + loanApplication.LoanApplicationID;
                userSessionEntity.UserID = userSessionEntity.UserID;
                //string callBackURL = "<tr> <td style='padding: 20px 0 20px 100px;'> <table class='buttonwrapper' border='0' cellspacing='3' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> </td> </tr> </table> </td> </tr> ";
                string callBackURL = "<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> <br /><br /> </td>";
                email = _notificationService.SendSPAEmail("Further payment request", callBackURL, (long)EmailTemplateNameID.FurtherPaymentRequest, loanApplication.ProgramInvitationID, userSessionEntity.ContactID, loanApplication.LoanApplicationID, "UnderWriter", userSessionEntity);
               // sms = _notificationService.SendSMS(loanApplication, 28, userSessionEntity);
            }
            catch (Exception ex)
            {
                ex = null;
                return email;
            }
            return email;
        }

        private string GetWorkFlowMessage(string commandName)
        {
            switch (commandName)
            {
                case "Save":
                    return "Grant Application created successfully.";
                case "Submit":
                    return "Application submitted successfully.";
                case "Accept":
                    return "Application accepted successfully.";
                case "Request More Info":
                    return "Request completed successfully.";
                case "Approve":
                    return "Application has been approved.";
                case "Reject":
                    return "Application has been rejected.";
                case "Accept Agreement":
                    return "Agreement accepted.";
                case "Complete Funding":
                    return "Fund disbursement completed.";
                case "SPAMessage":
                    return "{n} disbursement completed.";
                case "Read Agreement":
                    return "Agreement accepted.";                
                default:
                    return "Transaction completed successfully.";
            }

        }

        public ApplicationListResponse ExportGetLoanApplications(LoanExportRequest request, UserSessionEntity userSessionEntity)
        {
            //var response = this.GetLoanApplications(request, userSessionEntity);
            PageFilterEntity pageFilter = new PageFilterEntity();
            //var Filters = new List<FilterParameter>();
            //var filter = new FilterParameter();
            //filter.Key = "ProgramID";
            //filter.Value = request.ProgramId.ToString();
           // Filters.Add(request.FilterParameters);
            //pageFilter.FilterParameters.FirstOrDefault().Key = "ProgramID";
            //pageFilter.FilterParameters.FirstOrDefault().Value = request.ProgramId.ToString();
            pageFilter.FilterParameters= request.FilterParameters;
            var response = this.GetAllLoanApplicationInformation(pageFilter, userSessionEntity);
            if (response == null)
            {
                return response;
            }
            if (request != null && !string.IsNullOrEmpty(request.SearchBy))
            {
               
                        string searchBy = request.SearchBy;
                        if (!string.IsNullOrEmpty(searchBy))
                        {
                            searchBy = searchBy.Trim().ToLower();
                            string searchValue = string.Empty;
                            var kayValue = request.SearchValue;
                            if (kayValue != null)
                            {
                                searchValue = kayValue;
                            }
                            if (searchBy == "loannumber")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.LoanNumber.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "dateapplied")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.DateApplied.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "businessname")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.BusinessName.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "affiliatename")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.AffiliateName.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "loanprogramname")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.LoanProgramName.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "loanamount")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.LoanAmount.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "fundallocatedamount")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.FundAllocatedAmount.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                            else if (searchBy == "applicationstatus")
                            {
                        // response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.ApplicationStatus.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                        response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.ApplicationStatus.Equals(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                    }
                            else if (searchBy == "contactinfo")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.ContactInfo.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }                            
                            else if (searchBy == "all")
                            {
                                response.ApplicationPageResultEntity.DataList = response.ApplicationPageResultEntity.DataList.Where(x => x.LoanNumber.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)
                                || x.DateApplied.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase) || x.BusinessName.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase) || x.AffiliateName.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase) || x.LoanProgramName.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase) || x.LoanAmount.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)
                                || x.FundAllocatedAmount.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase) || x.ApplicationStatus.Contains(searchValue, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
                            }
                        }

                 

                

            }

            return response;
        }

        #endregion Methods
    }

    public static class NumberOrdinal
    {
        public static string Ordinal(this long number)
        {
            const string TH = "th";
            var s = number.ToString();

            number %= 100;

            if ((number >= 11) && (number <= 13))
            {
                return s + TH;
            }

            switch (number % 10)
            {
                case 1:
                    return s + "st";
                case 2:
                    return s + "nd";
                case 3:
                    return s + "rd";
                default:
                    return s + TH;
            }
        }
    }
}