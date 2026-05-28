using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Business.Interfaces.FundSource;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Repository.Interfaces.User;
using FluentValidation;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Constants;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.Validations.InputParameterValidation.FundingSource;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.Admin;
using System.Threading.Tasks;
using ThoughtFocusRepository.Interfaces.Master;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.DataAccess.Models.User;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.DataAccess.Models;
using Microsoft.Extensions.Options;
using ThoughtFocus.Services.Interfaces;
using ThoughtFocus.Workflow;
using ThoughtFocus.Validations.InputParameterValidation.Admin;
using ThoughtFocus.Common.Exceptions;
using FluentValidation.Results;
using ThoughtFocus.Validations.Impl.RuleHandler;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.Service.Impl
{
    public class FundingSourceService : IFundingSourceService
    {
        #region Fields
        private readonly IMapper _mapper;
        private IFundingSourceRepository _fundingSourceRepository;
        private readonly ILogger<FundingSourceService> _Logger;
        private IUserRepository _userRepository;
        private IFundingSource _fundingSource;
        private IFundUtilizationRepository _fundUtilizationRepository;
        private IFundingEntityRepository _fundingEntityRepository;
        private IDocumentTypeRepository _documentTypeRepository;
        private IHelpfulGuideTemplateRepository _helpfulGuideTemplateRepository;
        private IQuestionsRepository _questionsRepository;
        private INotificationRepository _notificationRepository;
        private IProgramInvitationRepository _programInvitationRepository;
        private IContactRepository _contactRepository;
        private IFundingTypeRepository _fundingTypeRepository;
        private IFundingApplicationRepository _fundingApplicationRepository;
        private IGenaralOptionRepository _genaralOptionRepository;
        private readonly ILoanBusinessDetailRepository _loanBusinessDetailRepository;
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        private readonly IProgramInviteeRepository _ProgramInviteeRepository;
        private readonly AppSettings _appSettings;
        private INotificationService _notificationService;
        private readonly IMasterService _masterService;
        #endregion Fields

        #region Constructors
        public FundingSourceService(
         ILogger<FundingSourceService> logger,
         IFundingSourceRepository fundingSourceRepository,
         IUserRepository userRepository,
         IFundingSource fundingSource,
         IMapper _mapper,
         IFundUtilizationRepository fundUtilizationRepository,
         IFundingEntityRepository fundingEntityRepository,
         IDocumentTypeRepository documentTypeRepository,
         IHelpfulGuideTemplateRepository helpfulGuideTemplateRepository,
         IQuestionsRepository questionsRepository,
         INotificationRepository notificationRepository,
         IProgramInvitationRepository programInvitationRepository, IContactRepository contactRepository, IFundingTypeRepository fundingTypeRepository, IFundingApplicationRepository fundingApplicationRepository,
         IGenaralOptionRepository genaralOptionRepository,ILoanBusinessDetailRepository loanBusinessDetailRepository, ILoanApplicationRepository loanApplicationRepository, IProgramInviteeRepository programInviteeRepository, IOptions<AppSettings> appSettings, INotificationService notificationService, IMasterService masterService)
        {
            this._Logger = logger;
            this._mapper = _mapper;
            this._fundingSourceRepository = fundingSourceRepository;
            this._userRepository = userRepository;
            this._fundingSource = fundingSource;
            this._fundUtilizationRepository = fundUtilizationRepository;
            this._fundingEntityRepository = fundingEntityRepository;
            this._documentTypeRepository = documentTypeRepository;
            this._helpfulGuideTemplateRepository = helpfulGuideTemplateRepository;
            this._questionsRepository = questionsRepository;
            this._notificationRepository = notificationRepository;
            this._programInvitationRepository = programInvitationRepository;
            this._contactRepository = contactRepository;
            this._fundingTypeRepository = fundingTypeRepository;
            this._fundingApplicationRepository = fundingApplicationRepository;
            this._genaralOptionRepository = genaralOptionRepository;
            this._loanBusinessDetailRepository = loanBusinessDetailRepository;
            this._LoanApplicationRepository = loanApplicationRepository;
            this._ProgramInviteeRepository = programInviteeRepository;
            this._appSettings = appSettings.Value;
            this._notificationService = notificationService;
            this._masterService = masterService;
        }

        #endregion Constructors

        #region Methods

        #region GetFundTransaction
        public FundTransactionResponse GetFundTransaction(long FundingSourceID)
        {
            FundTransactionResponse FundTransactionResponse = new FundTransactionResponse();
            List<FundTransactionDetail> fundTransactionDetails = new List<FundTransactionDetail>();

            try
            {
                // parameter validation

                if (!(FundingSourceID > 0))
                {
                    FundTransactionResponse.IsSuccess = false;
                    FundTransactionResponse.IsValidationError = true;
                    FundTransactionResponse.ValidationError = new ValidationErrorResponse();
                    FundTransactionResponse.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "FundingSourceID";
                    validationError.ErrorMessage = "Please provide valid funding source Id";
                    FundTransactionResponse.ValidationError.ValidationError = new List<ValidationError>();
                    FundTransactionResponse.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                    /** Get fund transactions list **/
                    List<FundTransaction> FundTransactionList = _fundingSourceRepository.GetFundTransaction(FundingSourceID);
                    FundTransactionList = FundTransactionList.Where(x => x.TransactionTypeID == (int)TransactionTypeEnumeration.Added || x.TransactionTypeID == (int)TransactionTypeEnumeration.Removed).OrderByDescending(x => x.TransactionDate).ToList();
                    fundTransactionDetails = _mapper.Map<List<FundTransactionDetail>>(FundTransactionList);
                    FundTransactionResponse.FundTransactionList = fundTransactionDetails;

                    //Assign total funded amount
                    FundTransactionResponse.TotalFundedAmount = Decimal.Truncate(_fundingSource.CalculateTotalFundedAmount(FundTransactionList));
                    foreach (var transaction in FundTransactionResponse.FundTransactionList)
                    {
                        DateTime date = DateTime.Parse(transaction.TransactionDate);
                        string dateString = date.ToShortDateString().Replace("-", "/");

                        transaction.CreatedDateTime = date.ToString("MM/dd/yyyy").Replace("-", "/");
                        transaction.Comment = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(transaction.Comment);

                        if (transaction.FundTransactionDocumentID != 0)
                        {
                            foreach (var fundTransactionList in FundTransactionList)
                            {
                                foreach (var fundTransactionDocuments in fundTransactionList.FundTransactionDocuments)
                                {
                                    if (fundTransactionDocuments.ID == transaction.FundTransactionDocumentID)
                                    {
                                        transaction.physicalFileStorageKey = fundTransactionDocuments.PhysicalFileStorageKey;
                                        transaction.FileName = fundTransactionDocuments.FileName;
                                    }
                                }
                            }
                        }
                        else
                        {
                            transaction.physicalFileStorageKey = "";
                            transaction.FileName = "";
                        }

                        var TransactionAmount = !string.IsNullOrEmpty(transaction.TransactionAmount) ? Convert.ToDecimal(transaction.TransactionAmount) : 0;
                        transaction.TransactionAmount = "$ " + string.Format("{0:#,#}", Decimal.Truncate(TransactionAmount));

                    }
                    FundTransactionResponse.IsSuccess = true;
                    FundTransactionResponse.IsValidationError = false;
                    FundTransactionResponse.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundTransaction ", null);
                FundTransactionResponse.IsSuccess = false;
                FundTransactionResponse.IsValidationError = false;
                FundTransactionResponse.Message = "Failed to fetch the fund transactions";
            }
            return FundTransactionResponse;
        }

        #endregion GetFundTransaction

        #region CreateFundingSource
        public CommonResponse CreateFundingSource(FundingSourceParam fundingSourceParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            List<FundingSourceStates> fundingSourceStates = new List<FundingSourceStates>();
            List<FundingSourceBusinessTypes> fundingSourceBusinessTypes = new List<FundingSourceBusinessTypes>();
            FundingSource fundingSource = new FundingSource();
            Logo uploadLogo = new Logo();
            CommonResponse commonCreationParam = null;
            if (fundingSourceParam == null)
            {
                _Logger.LogError("Input Parameter FundingSourceParam is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                var validator = new FundingSourceValidation();
                var modelValidationResults = validator.Validate(fundingSourceParam, options => { options.IncludeRuleSets("mandatoryFields", "invalidInput"); });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                if (Convert.ToDecimal(fundingSourceParam.MinimumLoanAmount) > Convert.ToDecimal(fundingSourceParam.MaximumLoanAmount))
                {
                    validationMessages.Add("Minimum amount cann't be greater than maximum amount.");
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                var checkProgramNameExists = this._fundingSourceRepository.GetFundingSourceByProgramName(fundingSourceParam.ProgramName, fundingSourceParam.FundingEntityID);

                if (checkProgramNameExists != null && fundingSourceParam.FundingSourceID != checkProgramNameExists.FundingSourceID && fundingSourceParam.FundingEntityID == checkProgramNameExists.FundingEntityID)
                {
                    validationMessages.Add("Program name already exists.");
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                if (fundingSourceParam.FundingSourceID > 0)
                {
                    fundingSource = _fundingSourceRepository.GetFundingSourceByID(fundingSourceParam.FundingSourceID);

                    fundingSource.FundingSourceID = fundingSourceParam.FundingSourceID;
                    fundingSource.ProgramName = fundingSourceParam.ProgramName;
                    fundingSource.FundingEntityID = fundingSourceParam.FundingEntityID;
                    fundingSource.FundingTypeID = fundingSourceParam.FundingTypeID;
                    fundingSource.MinimumLoanAmount = fundingSourceParam.MinimumLoanAmount;
                    fundingSource.MaximumLoanAmount = fundingSourceParam.MaximumLoanAmount;
                    fundingSource.IsActive = true;

                    //This will be done for one to one mapping
                    foreach (var state in fundingSource.FundingSourceStates)
                    {
                        state.StateID = fundingSourceParam.States.FirstOrDefault();
                        state.LastModifiedByUserID = userSessionEntity.UserID;
                        state.LastModifiedDateTime = System.DateTime.Now;
                    }

                    foreach (var businessType in fundingSource.FundingSourceBusinessTypes)
                    {
                        businessType.BusinessTypeID = fundingSourceParam.BusinessTypes.FirstOrDefault();
                        businessType.LastModifiedByUserID = userSessionEntity.UserID;
                        businessType.LastModifiedDateTime = System.DateTime.Now;
                    }

                    if (fundingSource.LogoID != null && fundingSource.Logo != null && fundingSource.Logo.ID == fundingSource.LogoID && fundingSourceParam.UploadProgramLogo != null && fundingSourceParam.UploadProgramLogo.PhysicalFileStorageKey != "")
                    {
                        fundingSource.Logo.Name = fundingSourceParam.UploadProgramLogo.FileName;
                        fundingSource.Logo.PhysicalFileStorageKey = fundingSourceParam.UploadProgramLogo.PhysicalFileStorageKey;
                        fundingSource.Logo.CreatedByUserID = userSessionEntity.UserID;
                        fundingSource.Logo.LastModifiedByUserID = userSessionEntity.UserID;
                        fundingSource.Logo.LastModifiedDateTime = System.DateTime.Now;
                        fundingSource.Logo.IsActive = true;
                        fundingSource.Logo.LogoTypeID = 2;
                        fundingSource.Logo.DocumentGUID = fundingSourceParam.UploadProgramLogo.DocumentGUID;
                        fundingSource.Logo.Source = fundingSourceParam.UploadProgramLogo.FileSource;
                    }
                    else
                    {
                        uploadLogo.Name = fundingSourceParam.UploadProgramLogo.FileName;
                        uploadLogo.PhysicalFileStorageKey = fundingSourceParam.UploadProgramLogo.PhysicalFileStorageKey;
                        uploadLogo.CreatedByUserID = userSessionEntity.UserID;
                        uploadLogo.CreatedDateTime = System.DateTime.Now;
                        uploadLogo.LastModifiedByUserID = userSessionEntity.UserID;
                        uploadLogo.LastModifiedDateTime = System.DateTime.Now;
                        uploadLogo.IsActive = true;
                        uploadLogo.LogoTypeID = 2;
                        uploadLogo.DocumentGUID = fundingSourceParam.UploadProgramLogo.DocumentGUID;
                        uploadLogo.Source = fundingSourceParam.UploadProgramLogo.FileSource;
                        if (uploadLogo != null)
                        {
                            fundingSource.Logo = uploadLogo;
                        }
                    }
                }
                else
                {
                    fundingSource = _mapper.Map<FundingSource>(fundingSourceParam);
                    fundingSource.IsActive = true;

                    if (fundingSourceParam.UploadProgramLogo != null && fundingSourceParam.UploadProgramLogo.PhysicalFileStorageKey != "")
                    {
                        uploadLogo.Name = fundingSourceParam.UploadProgramLogo.FileName;
                        uploadLogo.PhysicalFileStorageKey = fundingSourceParam.UploadProgramLogo.PhysicalFileStorageKey;
                        uploadLogo.CreatedByUserID = userSessionEntity.UserID;
                        uploadLogo.CreatedDateTime = System.DateTime.Now;
                        uploadLogo.LastModifiedByUserID = userSessionEntity.UserID;
                        uploadLogo.LastModifiedDateTime = System.DateTime.Now;
                        uploadLogo.IsActive = true;
                        uploadLogo.LogoTypeID = 2;
                        uploadLogo.DocumentGUID = fundingSourceParam.UploadProgramLogo.DocumentGUID;
                        uploadLogo.Source = fundingSourceParam.UploadProgramLogo.FileSource;
                        if (uploadLogo != null && fundingSource.LogoID == null)
                        {
                            fundingSource.Logo = uploadLogo;
                        }

                    }

                    foreach (var fsStates in fundingSourceParam.States)
                    {
                        FundingSourceStates fundingSourceState = new FundingSourceStates();
                        fundingSourceState.StateID = fsStates;
                        fundingSourceState.FundingSourceID = fundingSource.FundingSourceID;
                        fundingSourceState.IsActive = true;
                        fundingSourceState.CreatedByUserID = userSessionEntity.UserID;
                        fundingSourceState.CreatedDateTime = System.DateTime.Now;
                        fundingSourceState.LastModifiedByUserID = userSessionEntity.UserID;
                        fundingSourceState.LastModifiedDateTime = System.DateTime.Now;
                        fundingSourceStates.Add(fundingSourceState);
                    }

                    if (fundingSourceStates != null && fundingSourceStates.Count > 0)
                        fundingSource.FundingSourceStates = fundingSourceStates;


                    foreach (var fsBusinessType in fundingSourceParam.BusinessTypes)
                    {
                        FundingSourceBusinessTypes fundingSourceBusinessType = new FundingSourceBusinessTypes();
                        fundingSourceBusinessType.BusinessTypeID = fsBusinessType;
                        fundingSourceBusinessType.FundingSourceID = fundingSource.FundingSourceID;
                        fundingSourceBusinessType.IsActive = true;
                        fundingSourceBusinessType.CreatedByUserID = userSessionEntity.UserID;
                        fundingSourceBusinessType.CreatedDateTime = System.DateTime.Now;
                        fundingSourceBusinessType.LastModifiedByUserID = userSessionEntity.UserID;
                        fundingSourceBusinessType.LastModifiedDateTime = System.DateTime.Now;
                        fundingSourceBusinessTypes.Add(fundingSourceBusinessType);
                    }
                    if (fundingSourceBusinessTypes != null && fundingSourceBusinessTypes.Count > 0)
                        fundingSource.FundingSourceBusinessTypes = fundingSourceBusinessTypes;
                }

                _fundingSourceRepository.SaveOrUpdateFundingSource(fundingSource, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                commonCreationParam.ID = fundingSource.FundingSourceID;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateFundingSource ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateFundingSource", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public CommonResponse UpdateFundingSource(FundingSourceParam fundingSourceParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            if (fundingSourceParam == null)
            {
                _Logger.LogError("Input Parameter FundingSourceParam is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                var validator = new FundingSourceValidation();
                var modelValidationResults = validator.Validate(fundingSourceParam, options => { options.IncludeRuleSets("mandatoryFields", "invalidInput"); });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                var fundingSource = _fundingSourceRepository.GetFundingSourceByID(fundingSourceParam.FundingSourceID);

                var fundingSourceStateMapping = fundingSource.FundingSourceStates.ToList();

                if (fundingSource != null)
                {
                    foreach (var stateID in fundingSourceParam.States)
                    {
                        var stateExists = fundingSourceStateMapping.Where(x => x.StateID == stateID && x.IsActive == true).LastOrDefault();
                        if (stateExists == null)
                        {
                            FundingSourceStates state = new FundingSourceStates();
                            state.FundingSourceID = fundingSource.FundingSourceID;
                            state.StateID = stateID;
                            state.IsActive = true;
                        }
                    }
                }

                // List<FundingSourceAndStateMapping> fundingSourceAndStateMappings = _mapper.Map<List<FundingSourceAndStateMapping>>(fundingSourceParam.States);
                // List<FundingSourceAndBusinessTypeMapping> fundingSourceAndBusinessTypeMappings = _mapper.Map<List<FundingSourceAndBusinessTypeMapping>>(fundingSourceParam.BusinessTypes);
                // _fundingSourceRepository.SaveOrUpdateFundingSource(fundingSource,userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateFundingSource ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateFundingSource", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public CommonResponse CreateOrUpdateFundingEntity(FundingEntityRequest fundingEntityRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            Logo uploadLogo = new Logo();
            CommonResponse commonCreationParam = null;
            if (fundingEntityRequest == null)
            {
                _Logger.LogError("Input Parameter fundingEntityRequest is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                var validator = new FundingEntityValidation();
                var modelValidationResults = validator.Validate(fundingEntityRequest, options => { options.IncludeRuleSets("mandatoryFields", "invalidInput"); });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                FundingEntity fundingEntity = null;
                if (fundingEntityRequest.FundingEntityID > 0)
                {
                    fundingEntity = this._fundingEntityRepository.GetFundingEntityByID(fundingEntityRequest.FundingEntityID);
                    fundingEntity.FundingEntityName = fundingEntityRequest.FundingEntityName;
                    fundingEntity.EIN = fundingEntityRequest.EIN;
                    fundingEntity.TIN = fundingEntityRequest.TIN;
                    fundingEntity.Address = fundingEntityRequest.Address;
                    fundingEntity.City = fundingEntityRequest.City;
                    fundingEntity.StateID = fundingEntityRequest.StateID;
                    fundingEntity.ZipCode = fundingEntityRequest.ZipCode;
                    fundingEntity.LastModifiedDateTime = DateTime.Now;
                    fundingEntity.IsActive = true;

                    if (fundingEntity.LogoID != null && fundingEntity.Logo != null && fundingEntity.Logo.ID == fundingEntity.LogoID && fundingEntityRequest.UploadFundingEntityLogo != null && fundingEntityRequest.UploadFundingEntityLogo.PhysicalFileStorageKey != "")
                    {
                        fundingEntity.Logo.Name = fundingEntityRequest.UploadFundingEntityLogo.FileName;
                        fundingEntity.Logo.PhysicalFileStorageKey = fundingEntityRequest.UploadFundingEntityLogo.PhysicalFileStorageKey;
                        fundingEntity.Logo.CreatedByUserID = userSessionEntity.UserID;
                        fundingEntity.Logo.LastModifiedByUserID = userSessionEntity.UserID;
                        fundingEntity.Logo.LastModifiedDateTime = System.DateTime.Now;
                        fundingEntity.Logo.IsActive = true;
                        fundingEntity.Logo.LogoTypeID = 1;
                        fundingEntity.Logo.DocumentGUID = fundingEntityRequest.UploadFundingEntityLogo.DocumentGUID;
                        fundingEntity.Logo.Source = fundingEntityRequest.UploadFundingEntityLogo.FileSource;
                    }
                    else
                    {
                        uploadLogo.Name = fundingEntityRequest.UploadFundingEntityLogo.FileName;
                        uploadLogo.PhysicalFileStorageKey = fundingEntityRequest.UploadFundingEntityLogo.PhysicalFileStorageKey;
                        uploadLogo.CreatedByUserID = userSessionEntity.UserID;
                        uploadLogo.CreatedDateTime = System.DateTime.Now;
                        uploadLogo.LastModifiedByUserID = userSessionEntity.UserID;
                        uploadLogo.LastModifiedDateTime = System.DateTime.Now;
                        uploadLogo.IsActive = true;
                        uploadLogo.LogoTypeID = 1;
                        uploadLogo.DocumentGUID = fundingEntityRequest.UploadFundingEntityLogo.DocumentGUID;
                        uploadLogo.Source = fundingEntityRequest.UploadFundingEntityLogo.FileSource;

                        if (uploadLogo != null)
                        {
                            fundingEntity.Logo = uploadLogo;
                        }
                    }
                }
                else
                {
                    fundingEntity = _mapper.Map<FundingEntity>(fundingEntityRequest);
                    fundingEntity.IsActive = true;

                    if (fundingEntityRequest.UploadFundingEntityLogo != null && fundingEntityRequest.UploadFundingEntityLogo.PhysicalFileStorageKey != "")
                    {
                        uploadLogo.Name = fundingEntityRequest.UploadFundingEntityLogo.FileName;
                        uploadLogo.PhysicalFileStorageKey = fundingEntityRequest.UploadFundingEntityLogo.PhysicalFileStorageKey;
                        uploadLogo.CreatedByUserID = userSessionEntity.UserID;
                        uploadLogo.CreatedDateTime = System.DateTime.Now;
                        uploadLogo.LastModifiedByUserID = userSessionEntity.UserID;
                        uploadLogo.LastModifiedDateTime = System.DateTime.Now;
                        uploadLogo.IsActive = true;
                        uploadLogo.LogoTypeID = 1;
                        uploadLogo.DocumentGUID = fundingEntityRequest.UploadFundingEntityLogo.DocumentGUID;
                        uploadLogo.Source = fundingEntityRequest.UploadFundingEntityLogo.FileSource;
                        if (uploadLogo != null && fundingEntity.LogoID == null)
                        {
                            fundingEntity.Logo = uploadLogo;
                        }
                    }
                }

                var checkfundingEntityEINExists = this._fundingEntityRepository.GetFundingEntityEin(fundingEntityRequest.EIN);
                if (checkfundingEntityEINExists != null && checkfundingEntityEINExists.FundingEntityID != fundingEntityRequest.FundingEntityID)
                {
                    validationMessages.Add("EIN already exists.");
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                _fundingEntityRepository.SaveOrUpdateFundingEntity(fundingEntity, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateOrUpdateFundingEntity ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateOrUpdateFundingEntity", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        #endregion CreateFundingSource
        public CommonResponse AddFundTransaction(FundTransactionParam fundTransactionParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            if (fundTransactionParam == null)
            {

                _Logger.LogError("Input Parameter fundTransactionParam is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                var validator = new FundTransactionValidation();
                var modelValidationResults = validator.Validate(fundTransactionParam, options => { options.IncludeRuleSets("mandatoryFields", "invalidInput"); });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                FundingSource fundSource = this._fundingSourceRepository.GetFundingSourceByID(fundTransactionParam.FundingSourceID);

                if (fundSource != null && fundSource.FundingSourceID > 0)
                {
                    if (fundSource.InitialFundedAmount <= 0)
                    {
                        fundSource.InitialFundedAmount = fundTransactionParam.TransactionAmount;
                    }
                    FundTransaction fundTransaction = this._mapper.Map<FundTransaction>(fundTransactionParam);
                    fundTransaction.TransactionDate = fundTransactionParam.dateOfFunding;
                    fundTransaction.CreatedByUserID = userSessionEntity.UserID;
                    fundTransaction.LastModifiedByUserID = userSessionEntity.UserID;
                    fundTransaction.TransactionTypeID = (int)TransactionTypeEnumeration.Added;

                    fundSource.FundTransactions.Add(fundTransaction);

                    if (fundTransactionParam.TransactionDocument != null && fundTransactionParam.TransactionDocument.DocumentGUID != Guid.Empty)
                    {
                        FundTransactionDocument fundTransactionDocument = new FundTransactionDocument();
                        fundTransactionDocument.FundTransactionID = fundTransaction.ID;
                        fundTransactionDocument.DocumentGUID = fundTransactionParam.TransactionDocument.DocumentGUID;
                        fundTransactionDocument.DocumentName = fundTransactionParam.TransactionDocument.DocumentName;
                        fundTransactionDocument.DocumentTypeID = fundTransactionParam.TransactionDocument.DocumentTypeID;
                        fundTransactionDocument.FileName = fundTransactionParam.TransactionDocument.FileName;
                        fundTransactionDocument.FileSize = fundTransactionParam.TransactionDocument.FileSize;
                        fundTransactionDocument.PhysicalFileStorageKey = fundTransactionParam.TransactionDocument.PhysicalFileStorageKey;
                        fundTransactionDocument.CreatedByUserID = userSessionEntity.UserID;
                        fundTransactionDocument.LastModifiedByUserID = userSessionEntity.UserID;
                        fundTransactionDocument.CreatedDateTime = DateTime.Now;
                        fundTransactionDocument.LastModifiedDateTime = DateTime.Now;
                        fundTransactionDocument.IsActive = true;
                        fundTransaction.FundTransactionDocuments.Add(fundTransactionDocument);
                    }
                    this._fundingSourceRepository.SaveOrUpdateFundingSource(fundSource, userSessionEntity.UserID);

                    commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                }
                else
                {
                    validationMessages.Add("Invalid funding source id.");
                    commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateFundingSource ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> CreateFundingSource", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public CommonResponse RemoveFundTransaction(FundTransactionParam fundTransactionParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            if (fundTransactionParam == null)
            {
                _Logger.LogError("Input Parameter FundTransactionParam is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                var validator = new FundTransactionValidation();
                var modelValidationResults = validator.Validate(fundTransactionParam, options => { options.IncludeRuleSets("mandatoryFields", "invalidInput"); });

                if (!modelValidationResults.IsValid)
                {
                    foreach (var error in modelValidationResults.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                    commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                    return commonCreationParam;
                }

                FundingSource fundSource = this._fundingSourceRepository.GetFundingSourceByID(fundTransactionParam.FundingSourceID);

                if (fundSource != null && fundSource.FundingSourceID > 0)
                {

                    // if (fundTransactionParam.TransactionAmount > getAvailableFundAmmount(fundSource.FundTransactions.ToList()))
                    if (fundTransactionParam.TransactionAmount > getAvailableFundAmmount(fundSource.FundTransactions.ToList()))
                    {
                        validationMessages.Add("Removal amount is greater than available limit");
                        commonCreationParam = new CommonResponse(
                        ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                        return commonCreationParam;
                    }

                    FundTransaction fundTransaction = this._mapper.Map<FundTransaction>(fundTransactionParam);

                    fundTransaction.CreatedByUserID = userSessionEntity.UserID;
                    fundTransaction.LastModifiedByUserID = userSessionEntity.UserID;
                    fundTransaction.LastModifiedByUserID = userSessionEntity.UserID;
                    fundTransaction.TransactionTypeID = (int)TransactionTypeEnumeration.Removed;
                    fundTransaction.TransactionDate = fundTransactionParam.dateOfFunding;
                    fundSource.FundTransactions.Add(fundTransaction);

                    if (fundTransactionParam.TransactionDocument != null && fundTransactionParam.TransactionDocument.DocumentGUID != Guid.Empty)
                    {
                        FundTransactionDocument fundTransactionDocument = new FundTransactionDocument();

                        fundTransactionDocument.FundTransactionID = fundTransaction.ID;
                        fundTransactionDocument.DocumentGUID = fundTransactionParam.TransactionDocument.DocumentGUID;
                        fundTransactionDocument.DocumentName = fundTransactionParam.TransactionDocument.DocumentName;
                        fundTransactionDocument.DocumentTypeID = fundTransactionParam.TransactionDocument.DocumentTypeID;
                        fundTransactionDocument.FileName = fundTransactionParam.TransactionDocument.FileName;
                        fundTransactionDocument.FileSize = fundTransactionParam.TransactionDocument.FileSize;
                        fundTransactionDocument.PhysicalFileStorageKey = fundTransactionParam.TransactionDocument.PhysicalFileStorageKey;
                        fundTransaction.FundTransactionDocuments.Add(fundTransactionDocument);
                    }
                    this._fundingSourceRepository.SaveOrUpdateFundingSource(fundSource, userSessionEntity.UserID);

                    commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                }
                else
                {
                    validationMessages.Add("Invalid funding source id.");
                    commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> RemoveFundTransaction ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {

                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> RemoveFundTransaction", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        private decimal getAvailableFundAmmount(List<FundTransaction> fundTransaction)
        {
            try
            {
                if (fundTransaction != null && fundTransaction.Count() > 0)
                {
                    decimal totalFundAdded = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 1).Sum(y => y.TransactionAmount);
                    decimal totalFundRemoved = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 2).Sum(y => y.TransactionAmount);
                    decimal totalFundAllocated = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 3).Sum(y => y.TransactionAmount);
                    //added - removed -allocated
                    return Decimal.Truncate((totalFundAdded - (totalFundRemoved + totalFundAllocated)));
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getAvailableFundAmmount", null);
            }

            return 0;
        }

        #region ViewFundUtilizationDetails
        public FundingSourceResponse GetFundUtilization(long fundingSourceID)
        {
            FundingSourceResponse fundingSourceResponse = new FundingSourceResponse();

            if (fundingSourceID <= 0)
            {
                _Logger.LogError("Input Parameter fundingSourceID is null");
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingSourceResponse;
            }
            try
            {
                List<FundUtilization> listOfFundUtilizations = this._fundUtilizationRepository.GetFundUtilization(fundingSourceID).OrderByDescending(x => x.TransactionDate).ToList();

                List<FundUtilizationParam> listOfFundUtilizationParam = new List<FundUtilizationParam>();

                foreach (var fundUtilization in listOfFundUtilizations)
                {
                    FundUtilizationParam fundUtilizationParam = this._mapper.Map<FundUtilizationParam>(fundUtilization);
                    fundUtilizationParam.FundingType = fundUtilization.FundingSource.FundingType.Type;
                    fundUtilizationParam.BusinessName = fundUtilization.LoanApplication.LoanBusinessDetail.BusinessName.ToString();
                    fundUtilizationParam.BusinessType = fundUtilization.LoanApplication.LoanBusinessDetail.BusinessType.Type;
                    fundUtilizationParam.Comment = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(fundUtilizationParam.Comment);


                    if (fundUtilization.FundTransactionDocuments.Count > 0)
                    {
                        foreach (var fundTransactionList in listOfFundUtilizations)
                        {
                            foreach (var fundTransactionDocuments in fundTransactionList.FundTransactionDocuments)
                            {
                                if (!string.IsNullOrEmpty(fundTransactionDocuments.PhysicalFileStorageKey) && !string.IsNullOrEmpty(fundTransactionDocuments.FileName))
                                {
                                    fundUtilizationParam.PhysicalFileStorageKey = fundTransactionDocuments.PhysicalFileStorageKey;

                                    fundUtilizationParam.FileName = fundTransactionDocuments.FileName;
                                }

                            }
                        }

                    }
                    else
                    {
                        fundUtilizationParam.PhysicalFileStorageKey = "";
                        fundUtilizationParam.FileName = "";
                    }

                    fundUtilizationParam.FundUtilizedTransactionDate = fundUtilization.TransactionDate != null ? fundUtilization.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/") : "";
                    fundUtilizationParam.FundUtilizedTransactionAmount = fundUtilization.TransactionAmount > 0 ? "$ " + string.Format("{0:#,#}", fundUtilization.TransactionAmount) : "";

                    listOfFundUtilizationParam.Add(fundUtilizationParam);
                }

                fundingSourceResponse.FundUtilization = listOfFundUtilizationParam;
                fundingSourceResponse.IsSuccess = true;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundUtilization", null);
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundUtilization", null);
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return fundingSourceResponse;
        }

        #endregion ViewFundUtilizationDetails

        public FundingEntityListResponse GetAllFundingEntitiesInformation(PageFilterEntity pageFilter, UserSessionEntity userSessionEntity)
        {
            FundingEntityListResponse fundingEntityListResponse = new FundingEntityListResponse();

            if (pageFilter == null)
            {
                _Logger.LogError("Input Parameter PageFilter is null");
                fundingEntityListResponse.IsSuccess = false;
                fundingEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";

                return fundingEntityListResponse;
            }

            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                fundingEntityListResponse.IsSuccess = false;
                fundingEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";

                return fundingEntityListResponse;
            }

            if (userSessionEntity.UserID == 0)
            {
                _Logger.LogError("Input Parameter UserSessionEntity.UserID is null");
                fundingEntityListResponse.IsSuccess = false;
                fundingEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingEntityListResponse;
            }

            try
            {
                fundingEntityListResponse = this._fundingSource.GetAllFundingEntitiesInformation(pageFilter, userSessionEntity.UserID);

                if (fundingEntityListResponse.FundingEntityPageResultEntity != null && fundingEntityListResponse.IsSuccess)
                {
                    fundingEntityListResponse.IsSuccess = true;
                }
                else
                {
                    fundingEntityListResponse.IsSuccess = false;
                    fundingEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetAllFundingEntitiesInformation", null);
                fundingEntityListResponse.IsSuccess = false;
                fundingEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetAllFundingEntitiesInformation", null);
                fundingEntityListResponse.IsSuccess = false;
                fundingEntityListResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return fundingEntityListResponse;
        }

        public FundingEntityResponse GetFundingEntity(long fundingEntityID, UserSessionEntity userSessionEntity)
        {
            FundingEntityResponse fundingEntityResponse = new FundingEntityResponse();
            fundingEntityResponse.FundingEntityViewEntity = new FundingEntityViewEntity();
            var fundingSourceList = new List<FundingSourceListResponse>();

            if (fundingEntityID <= 0)
            {
                _Logger.LogError("Input Parameter fundingEntityID is null");
                fundingEntityResponse.IsSuccess = false;
                fundingEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingEntityResponse;
            }

            try
            {
                FundingEntity fundingEntity = this._fundingEntityRepository.GetFundingEntityByID(fundingEntityID);

                if (fundingEntity == null)
                {
                    fundingEntityResponse.IsSuccess = false;
                    fundingEntityResponse.Message = "Entity doesnot exist";
                    return fundingEntityResponse;
                }
                fundingEntityResponse.FundingEntityViewEntity.FundingEntityID = fundingEntity.FundingEntityID;
                fundingEntityResponse.FundingEntityViewEntity.FundingEntityName = fundingEntity.FundingEntityName;
                fundingEntityResponse.FundingEntityViewEntity.Address = fundingEntity.Address;
                fundingEntityResponse.FundingEntityViewEntity.ZipCode = fundingEntity.ZipCode;
                fundingEntityResponse.FundingEntityViewEntity.City = fundingEntity.City;
                fundingEntityResponse.FundingEntityViewEntity.StateID = (long)fundingEntity.StateID;
                fundingEntityResponse.FundingEntityViewEntity.EIN = fundingEntity.EIN;
                fundingEntityResponse.FundingEntityViewEntity.TIN = fundingEntity.TIN;
                fundingEntityResponse.FundingEntityViewEntity.FundingSources = this._mapper.Map<List<FundingSourceListResponse>>(fundingEntity.FundingSources.Where(x => x.IsActive == true));

                if(fundingEntityResponse.FundingEntityViewEntity.FundingSources != null && fundingEntityResponse.FundingEntityViewEntity.FundingSources.Count > 0)
                {
                    var programList = this._contactRepository.GetContactsByID(userSessionEntity.ContactID);
                    if(programList != null && programList.ProgramInvitationContactRoles != null && programList.ProgramInvitationContactRoles.Count > 0)
                    {
                        var programs = programList.ProgramInvitationContactRoles.Where(pc => pc.IsActive);

                        if(programs != null && programs.Count() > 0)
                        {
                            if(programs.FirstOrDefault().ProgramID > 0)
                            {
                                foreach (var cr in programs)
                                {
                                    var fs = fundingEntityResponse.FundingEntityViewEntity.FundingSources.Where(c => c.FundingSourceID == cr.ProgramID).FirstOrDefault();
                                    if (fs != null)
                                    {
                                        fundingSourceList.Add(fs);
                                    }

                                }
                                if(fundingSourceList != null & fundingSourceList.Count > 0)
                                {
                                    fundingEntityResponse.FundingEntityViewEntity.FundingSources = fundingSourceList;
                                }
                            }
                            
                        }
                        
                        
                    }

                }
                foreach (var source in fundingEntityResponse.FundingEntityViewEntity.FundingSources)
                {
                    source.FundType = fundingEntity.FundingSources.Where(x => x.FundingTypeID == source.FundingTypeID).Select(y => y.FundingType?.Type).FirstOrDefault();
                }

                if (fundingEntity != null && fundingEntity.LogoID != null && fundingEntity.Logo != null)
                {
                    fundingEntityResponse.FundingEntityViewEntity.LogoPhysicalFileStorageKey = fundingEntity.Logo.PhysicalFileStorageKey == null ? "" : fundingEntity.Logo.PhysicalFileStorageKey;
                    fundingEntityResponse.FundingEntityViewEntity.LogoFileName = string.IsNullOrEmpty(fundingEntity.Logo.Name) ? "" : fundingEntity.Logo.Name;
                    fundingEntityResponse.FundingEntityViewEntity.LogoID = fundingEntity.LogoID;
                    fundingEntityResponse.FundingEntityViewEntity.LogoName = string.IsNullOrEmpty(fundingEntity.Logo.LogoTypes.LogoTypeName) ? "" : fundingEntity.Logo.LogoTypes.LogoTypeName;
                    fundingEntityResponse.FundingEntityViewEntity.LogSource = string.IsNullOrEmpty(fundingEntity.Logo.Source) ? "" : fundingEntity.Logo.Source;
                }

                if (fundingEntityResponse.FundingEntityViewEntity != null)
                {
                    fundingEntityResponse.IsSuccess = true;
                }
                else
                {
                    fundingEntityResponse.IsSuccess = false;
                    fundingEntityResponse.Message = "Funding entity doesn't exist.";
                }

                foreach (var fundingSource in fundingEntityResponse.FundingEntityViewEntity.FundingSources)
                {
                    List<FundTransaction> fundTransactions = fundingEntity.FundingSources.Where(x => x.FundingSourceID == fundingSource.FundingSourceID && x.IsActive == true).FirstOrDefault()?.FundTransactions.Where(y => y.IsActive == true).ToList();
                    if (fundTransactions != null && fundTransactions.Count > 0)
                    {
                        var initialFundedAmount = Convert.ToDecimal(fundingSource.InitialFundedAmount);
                        fundingSource.InitialFundedAmount = initialFundedAmount == 0 ? "0" : string.Format("{0:#,#}", Decimal.Truncate(initialFundedAmount));
                        fundingSource.AvailableLimit = getAvailableFundAmmount(fundTransactions) == 0 ? "0" : string.Format("{0:#,#}", getAvailableFundAmmount(fundTransactions));
                        fundingSource.TotalFundedAmount = getTotalFundedAmount(fundTransactions) == 0 ? "0" : string.Format("{0:#,#}", getTotalFundedAmount(fundTransactions));
                        fundingSource.UtilizedAmount = getTotalUtilizedAmount(fundTransactions) == 0 ? "0" : string.Format("{0:#,#}", getTotalUtilizedAmount(fundTransactions));
                    }
                    else
                    {
                        fundingSource.InitialFundedAmount = "0";
                        fundingSource.AvailableLimit = "0";
                        fundingSource.TotalFundedAmount = "0";
                        fundingSource.UtilizedAmount = "0";
                    }
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundingEntity", null);
                fundingEntityResponse.IsSuccess = false;
                fundingEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundingEntity", null);
                fundingEntityResponse.IsSuccess = false;
                fundingEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return fundingEntityResponse;
        }

        public FundingSourceResponse GetFundingSource(long fundingSourceID)
        {
            FundingSourceResponse fundingSourceResponse = new FundingSourceResponse();
            fundingSourceResponse.FundingSource = new FundingSourceParam();

            if (fundingSourceID <= 0)
            {
                _Logger.LogError("Input Parameter fundingEntityID is null");
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingSourceResponse;
            }

            try
            {
                FundingSource fundingSource = this._fundingSourceRepository.GetFundingSourceByID(fundingSourceID);
                List<long> businessTypes = new List<long>();
                List<long> states = new List<long>();

                if (fundingSource == null)
                {
                    fundingSourceResponse.IsSuccess = false;
                    fundingSourceResponse.Message = "Source doesnot exist";
                    return fundingSourceResponse;
                }

                fundingSourceResponse.FundingSource = this._mapper.Map<FundingSourceParam>(fundingSource);

                foreach (var businessType in fundingSource.FundingSourceBusinessTypes)
                {
                    businessTypes.Add(businessType.BusinessTypeID);
                }

                fundingSourceResponse.FundingSource.BusinessTypes = businessTypes;

                foreach (var state in fundingSource.FundingSourceStates)
                {
                    states.Add(state.StateID);
                }

                fundingSourceResponse.FundingSource.States = states;
                List<FundTransaction> fundTransactions = fundingSource?.FundTransactions.Where(y => y.IsActive == true).ToList();

                if (fundTransactions != null && fundTransactions.Count > 0)
                {
                    fundingSourceResponse.FundingSource.InitialFundedAmount = string.Format("{0:n0}", Decimal.Truncate(fundingSource.InitialFundedAmount));
                    fundingSourceResponse.FundingSource.AvailableLimit = string.Format("{0:n0}", getAvailableFundAmmount(fundTransactions));
                    fundingSourceResponse.FundingSource.TotalFundedAmount = string.Format("{0:n0}", getTotalFundedAmount(fundTransactions));
                    fundingSourceResponse.FundingSource.UtilizedAmount = string.Format("{0:n0}", getTotalUtilizedAmount(fundTransactions));
                }

                if (fundingSource != null && fundingSource.LogoID != null && fundingSource.Logo != null)
                {
                    fundingSourceResponse.FundingSource.LogoPhysicalFileStorageKey = fundingSource.Logo.PhysicalFileStorageKey == null ? "" : fundingSource.Logo.PhysicalFileStorageKey;
                    fundingSourceResponse.FundingSource.LogoFileName = string.IsNullOrEmpty(fundingSource.Logo.Name) ? "" : fundingSource.Logo.Name;
                    fundingSourceResponse.FundingSource.LogoID = fundingSource.LogoID;
                    fundingSourceResponse.FundingSource.LogoName = string.IsNullOrEmpty(fundingSource.Logo.LogoTypes.LogoTypeName) ? "" : fundingSource.Logo.LogoTypes.LogoTypeName;
                    fundingSourceResponse.FundingSource.LogSource = string.IsNullOrEmpty(fundingSource.Logo.Source) ? "" : fundingSource.Logo.Source;
                }
                else
                {
                    fundingSourceResponse.FundingSource.LogoID = 0;
                }

                if (fundingSourceResponse.FundingSource != null)
                {
                    fundingSourceResponse.IsSuccess = true;
                }
                else
                {
                    fundingSourceResponse.IsSuccess = false;
                    fundingSourceResponse.Message = "Funding entity doesn't exist.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundingEntity", null);
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetFundingEntity", null);
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return fundingSourceResponse;
        }

        private decimal getTotalFundedAmount(List<FundTransaction> fundTransaction)
        {
            try
            {
                if (fundTransaction != null && fundTransaction.Count() > 0)
                {
                    decimal totalFundAdded = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 1).Sum(y => y.TransactionAmount);
                    decimal totalFundRemoved = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 2).Sum(y => y.TransactionAmount);
                    //added - removed 
                    return Decimal.Truncate((totalFundAdded - (totalFundRemoved)));
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getTotalFundedAmount", null);
            }
            return 0;
        }

        private decimal getInitialFundedAmount(List<FundTransaction> fundTransactions)
        {
            try
            {
                if (fundTransactions != null && fundTransactions.Count() > 0)
                {
                    return Decimal.Truncate(fundTransactions.Where(x => x.TransactionTypeID == 1).OrderBy(o => o.ID).FirstOrDefault().TransactionAmount);
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getInitialFundedAmount", null);
            }
            return 0;
        }
        private decimal getTotalUtilizedAmount(List<FundTransaction> fundTransactions)
        {
            try
            {
                if (fundTransactions != null && fundTransactions.Count() > 0)
                {
                    return Decimal.Truncate(fundTransactions.Where(x => x.TransactionTypeID == 3).Sum(x => x.TransactionAmount));
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getTotalUtilizedAmount", null);
            }
            return 0;
        }

        public CommonResponse SaveOrUpdateProgramDocument(ProgramDocumentRequest programDocumentRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;

            if (programDocumentRequest == null)
            {
                _Logger.LogError("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                if (programDocumentRequest.FundingSourceID > 0)
                {
                    long[] documentsId = programDocumentRequest.ProgramDocuments.Select(x => x.ProgramDocumentID).ToArray();
                    var programDocuments = this._fundingSourceRepository.GetProgramDocumentsByProgramID(programDocumentRequest.FundingSourceID).ToList();
                    var deletedDocuments = programDocuments.Where(x => !documentsId.Contains(x.ID)).ToList();

                    //delete unchecked document
                    foreach (var programDocument in deletedDocuments)
                    {
                        programDocument.LastModifiedByUserID = userSessionEntity.UserID;
                        programDocument.LastModifiedDateTime = System.DateTime.Now;
                        programDocument.IsActive = false;
                        this._fundingSourceRepository.SaveOrUpdateProgramDocument(programDocument, userSessionEntity.UserID);

                    }

                    foreach (var document in programDocumentRequest.ProgramDocuments)
                    {
                        if (document.ProgramDocumentID > 0)
                        {
                            var programDocument = programDocuments.Where(x => x.ID == document.ProgramDocumentID).FirstOrDefault();
                            if (programDocument != null && programDocument.ID > 0)
                            {
                                programDocument.LastModifiedByUserID = userSessionEntity.UserID;
                                programDocument.LastModifiedDateTime = System.DateTime.Now;
                                programDocument.DisplayOrder = document.DisplayOrder;
                                programDocument.DocumentTypeID = document.DocumentID;
                                programDocument.IsActive = true;
                                programDocument.IsMandatory = document.IsMandatory;
                                this._fundingSourceRepository.SaveOrUpdateProgramDocument(programDocument, userSessionEntity.UserID);
                                validationMessages.Add("Program document updated successfully.");
                                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                                commonCreationParam.ID = programDocumentRequest.FundingSourceID;
                                commonCreationParam.StatusMessage = "Program document updated successfully.";
                            }
                        }
                        else
                        {
                            ProgramDocument programDocument = new ProgramDocument();

                            programDocument.CreatedByUserID = userSessionEntity.UserID;
                            programDocument.CreatedDateTime = System.DateTime.Now;
                            programDocument.LastModifiedByUserID = userSessionEntity.UserID;
                            programDocument.LastModifiedDateTime = System.DateTime.Now;
                            programDocument.ProgramID = programDocumentRequest.FundingSourceID;
                            programDocument.DisplayOrder = document.DisplayOrder;
                            programDocument.DocumentTypeID = document.DocumentID;
                            programDocument.IsActive = true;
                            programDocument.IsMandatory = document.IsMandatory;

                            this._fundingSourceRepository.SaveOrUpdateProgramDocument(programDocument, userSessionEntity.UserID);
                            validationMessages.Add("Program document saved successfully.");
                            commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                            commonCreationParam.ID = programDocumentRequest.FundingSourceID;
                            commonCreationParam.StatusMessage = "Program document saved successfully.";
                        }

                    }

                }
                return commonCreationParam;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramDocument", null);
                commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramDocument", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public ProgramDocumentResponse GetProgramDocuments(long programID)
        {
            ProgramDocumentResponse programDocumentResponse = new ProgramDocumentResponse();

            try
            {

                List<ProgramDocumentViewEntity> listOfProgramDocumentViewEntity = new List<ProgramDocumentViewEntity>();

                var documentTypeDocuments = this._documentTypeRepository.GetAll().Where(x => x.DocumentCategoryID == 1 && x.IsActive == true && x.DocumentTypeID != 6).ToList();

                foreach (var document in documentTypeDocuments)
                {

                    var programDocument = this._fundingSourceRepository.GetProgramDocumentsByProgram(programID, document.DocumentTypeID);

                    ProgramDocumentViewEntity ProgramDocumentViewEntity = new ProgramDocumentViewEntity();

                    if (programDocument != null)
                    {
                        ProgramDocumentViewEntity.ProgramDocumentID = programDocument.ID;
                        ProgramDocumentViewEntity.DocumentID = document.DocumentTypeID;
                        ProgramDocumentViewEntity.IsActive = programDocument.IsActive;
                        ProgramDocumentViewEntity.DisplayOrder = programDocument.DisplayOrder;
                        ProgramDocumentViewEntity.IsMandatory = programDocument.IsActive == true ? programDocument.IsMandatory : false;
                        ProgramDocumentViewEntity.DocumentName = string.IsNullOrEmpty(document.Name) ? "" : document.Name;
                        ProgramDocumentViewEntity.ProgramID = programDocument.ProgramID;
                    }
                    else
                    {
                        ProgramDocumentViewEntity.DocumentID = document.DocumentTypeID;
                        ProgramDocumentViewEntity.ProgramDocumentID = 0;
                        ProgramDocumentViewEntity.DisplayOrder = 0;
                        ProgramDocumentViewEntity.IsMandatory = false;
                        ProgramDocumentViewEntity.IsActive = false;
                        ProgramDocumentViewEntity.DocumentName = string.IsNullOrEmpty(document.Name) ? "" : document.Name;
                        ProgramDocumentViewEntity.ProgramID = 0;

                    }
                    listOfProgramDocumentViewEntity.Add(ProgramDocumentViewEntity);
                }

                if (listOfProgramDocumentViewEntity.Count > 0)
                {
                    programDocumentResponse.IsSuccess = true;
                    programDocumentResponse.ProgramDocumentsResponse = listOfProgramDocumentViewEntity.OrderBy(x => x.DocumentID).ToList();

                }
                else
                {
                    programDocumentResponse.IsSuccess = false;
                    programDocumentResponse.Message = "Couldn't find the program documents for this program.";

                }
            }

            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetProgramDocuments", null);
                programDocumentResponse.IsSuccess = false;
                programDocumentResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetProgramDocuments", null);
                programDocumentResponse.IsSuccess = false;
                programDocumentResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programDocumentResponse;
        }

        public CommonResponse SaveOrUpdateProgramQuestions(ProgramQuestionRequest programQuestionRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;

            if (programQuestionRequest == null)
            {
                _Logger.LogError("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                if (programQuestionRequest.ProgramID > 0)
                {
                    long[] questionsId = programQuestionRequest.ProgramQuestions.Select(x => x.ProgramQuestionID).ToArray();
                    var programquestions = this._fundingSourceRepository.GetProgramQuestionsByProgramID(programQuestionRequest.ProgramID).ToList();
                    var deletedQuestions = programquestions.Where(x => !questionsId.Contains(x.ID)).ToList();

                    //delete unchecked questions
                    foreach (var programDocument in deletedQuestions)
                    {
                        programDocument.LastModifiedByUserID = userSessionEntity.UserID;
                        programDocument.LastModifiedDateTime = System.DateTime.Now;
                        programDocument.IsActive = false;
                        this._fundingSourceRepository.SaveOrUpdateProgramQuestions(programDocument, userSessionEntity.UserID);
                        validationMessages.Add("Question saved successfully.");
                        commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                        commonCreationParam.ID = programQuestionRequest.ProgramID;
                        commonCreationParam.StatusMessage = "Question saved successfully.";
                    }

                    foreach (var question in programQuestionRequest.ProgramQuestions)
                    {
                        if (question.ProgramQuestionID > 0)
                        {
                            var programQuestion = programquestions.Where(x => x.ID == question.ProgramQuestionID).FirstOrDefault();
                            if (programQuestion != null && programQuestion.ID > 0)
                            {
                                programQuestion.LastModifiedByUserID = userSessionEntity.UserID;
                                programQuestion.LastModifiedDateTime = System.DateTime.Now;
                                programQuestion.QuestionID = question.QuestionID;
                                programQuestion.IsActive = true;
                                programQuestion.IsMadatory = question.IsMandatory;
                                programQuestion.DisplayOrder = question.DisplayOrder;

                                this._fundingSourceRepository.SaveOrUpdateProgramQuestions(programQuestion, userSessionEntity.UserID);

                                validationMessages.Add("Question updated successfully.");
                                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                                commonCreationParam.ID = programQuestionRequest.ProgramID;
                                commonCreationParam.StatusMessage = "Question updated successfully.";
                            }
                        }
                        else
                        {

                            ThoughtFocus.DataAccess.Models.FundingSource.ProgramQuestion programQuestion = new ThoughtFocus.DataAccess.Models.FundingSource.ProgramQuestion();
                            programQuestion.CreatedByUserID = userSessionEntity.UserID;
                            programQuestion.CreatedDateTime = System.DateTime.Now;
                            programQuestion.LastModifiedByUserID = userSessionEntity.UserID;
                            programQuestion.LastModifiedDateTime = System.DateTime.Now;
                            programQuestion.ProgramID = programQuestionRequest.ProgramID;
                            programQuestion.QuestionID = question.QuestionID;
                            programQuestion.IsActive = true;
                            programQuestion.IsMadatory = question.IsMandatory;
                            programQuestion.DisplayOrder = question.DisplayOrder;

                            this._fundingSourceRepository.SaveOrUpdateProgramQuestions(programQuestion, userSessionEntity.UserID);

                            validationMessages.Add("Question saved successfully.");
                            commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                            commonCreationParam.ID = programQuestionRequest.ProgramID;
                            commonCreationParam.StatusMessage = "Question saved successfully.";
                        }
                    }


                }
                return commonCreationParam;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramQuestions", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramQuestions", null);
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public ProgramQuestionsResponse GetProgramQuestions(long programID)
        {
            ProgramQuestionsResponse programQuestionsResponse = new ProgramQuestionsResponse();
            try
            {
                List<ProgramQuestionsViewEntity> listOfProgramQuestionsViewEntity = new List<ProgramQuestionsViewEntity>();
                var questions = this._questionsRepository.GetAll().Where(x => x.ResponseTypeID == 1 && x.IsActive == true).ToList();


                foreach (var question in questions)
                {

                    var programquestions = this._fundingSourceRepository.GetProgramQuestions(programID, question.QuestionID);

                    ProgramQuestionsViewEntity programQuestionsViewEntity = new ProgramQuestionsViewEntity();


                    if (programquestions != null)
                    {
                        programQuestionsViewEntity.QuestionID = programquestions.QuestionID;
                        programQuestionsViewEntity.ProgramID = programquestions.ProgramID;
                        programQuestionsViewEntity.IsMandatory = programquestions.IsActive == true ? programquestions.IsMadatory : false;
                        programQuestionsViewEntity.IsActive = programquestions.IsActive;
                        programQuestionsViewEntity.ProgramQuestionID = programquestions.ID;
                        programQuestionsViewEntity.DisplayOrder = programquestions.DisplayOrder;
                        programQuestionsViewEntity.QuestionText = string.IsNullOrEmpty(question.QuestionText) ? "" : question.QuestionText;
                    }
                    else
                    {
                        programQuestionsViewEntity.QuestionID = question.QuestionID;
                        programQuestionsViewEntity.ProgramID = 0;
                        programQuestionsViewEntity.IsMandatory = false;
                        programQuestionsViewEntity.IsActive = false;
                        programQuestionsViewEntity.ProgramQuestionID = 0;
                        programQuestionsViewEntity.DisplayOrder = 0;
                        programQuestionsViewEntity.QuestionText = string.IsNullOrEmpty(question.QuestionText) ? "" : question.QuestionText;
                    }

                    listOfProgramQuestionsViewEntity.Add(programQuestionsViewEntity);

                }

                if (listOfProgramQuestionsViewEntity.Count > 0)
                {
                    programQuestionsResponse.IsSuccess = true;
                    programQuestionsResponse.ProgramQuestionResponse = listOfProgramQuestionsViewEntity.OrderBy(x => x.QuestionID).ToList();

                }
                else
                {
                    programQuestionsResponse.IsSuccess = false;
                    programQuestionsResponse.Message = "Couldn't find the program questions for this program.";

                }

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetProgramQuestions", null);
                programQuestionsResponse.IsSuccess = false;
                programQuestionsResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetProgramQuestions", null);
                programQuestionsResponse.IsSuccess = false;
                programQuestionsResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programQuestionsResponse;
        }

        public CommonResponse SaveOrUpdateHelpfulGuideTemplate(HelpfulGuideTextRequest helpfulGuideTextRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;

            if (helpfulGuideTextRequest == null)
            {
                _Logger.LogError("Input Parameter is null.");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                if (helpfulGuideTextRequest.ProgramID > 0)
                {
                    var programHelpfulGuides = this._fundingSourceRepository.GetProgramHelpfulGuideByProgramId(helpfulGuideTextRequest.ProgramID).ToList();


                    if (programHelpfulGuides.Count > 0)
                    {

                        foreach (var templates in programHelpfulGuides)
                        {
                            var helpfulGuideTemplate = this._helpfulGuideTemplateRepository.GetHelpfulGuideById(templates.TamplateID);

                            if (templates.ProgramID > 0 && helpfulGuideTemplate.TypeID > 0)
                            {
                                if (helpfulGuideTemplate.TypeID == 1 && templates.TamplateID == helpfulGuideTemplate.ID)
                                {
                                    helpfulGuideTemplate.Body = helpfulGuideTextRequest.BusinessProfileHelpfulGuideText;
                                }
                                if (helpfulGuideTemplate.TypeID == 2 && templates.TamplateID == helpfulGuideTemplate.ID)
                                {
                                    helpfulGuideTemplate.Body = helpfulGuideTextRequest.FundingApplicationHelpfulGuideText;
                                }
                                if (helpfulGuideTemplate.TypeID == 3 && templates.TamplateID == helpfulGuideTemplate.ID)
                                {
                                    helpfulGuideTemplate.Body = helpfulGuideTextRequest.DocumentsHelpfulGuideText;
                                }
                                if (helpfulGuideTemplate.TypeID == 4 && templates.TamplateID == helpfulGuideTemplate.ID)
                                {
                                    helpfulGuideTemplate.Body = helpfulGuideTextRequest.ReviewHelpfulGuideText;
                                }
                            }

                            this._helpfulGuideTemplateRepository.SaveOrUpdateHelpfulGuideTemplate(helpfulGuideTemplate, userSessionEntity.UserID);

                            validationMessages.Add("Helpful Guide text updated successfully.");
                            commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                            commonCreationParam.ID = helpfulGuideTextRequest.ProgramID;
                            commonCreationParam.StatusMessage = "Helpful Guide text updated successfully.";
                        }
                    }
                    else
                    {
                        int[] templateTypes = { 1, 2, 3, 4 };
                        foreach (var template in templateTypes)
                        {
                            HelpfulGuideTemplate helpfulGuideTemplates = new HelpfulGuideTemplate();

                            if (template == 1)
                            {
                                helpfulGuideTemplates.Body = helpfulGuideTextRequest.BusinessProfileHelpfulGuideText;
                                helpfulGuideTemplates.TypeID = template;
                                helpfulGuideTemplates.IsActive = true;
                            }

                            if (template == 2)
                            {
                                helpfulGuideTemplates.Body = helpfulGuideTextRequest.FundingApplicationHelpfulGuideText;
                                helpfulGuideTemplates.TypeID = template;
                                helpfulGuideTemplates.IsActive = true;
                            }

                            if (template == 3)
                            {
                                helpfulGuideTemplates.Body = helpfulGuideTextRequest.DocumentsHelpfulGuideText;
                                helpfulGuideTemplates.TypeID = template;
                                helpfulGuideTemplates.IsActive = true;
                            }

                            if (template == 4)
                            {
                                helpfulGuideTemplates.Body = helpfulGuideTextRequest.ReviewHelpfulGuideText;
                                helpfulGuideTemplates.TypeID = template;
                                helpfulGuideTemplates.IsActive = true;
                            }

                            ProgramHelpfulGuide programHelpfulGuide = new ProgramHelpfulGuide();
                            programHelpfulGuide.ProgramID = helpfulGuideTextRequest.ProgramID;
                            programHelpfulGuide.IsActive = true;
                            programHelpfulGuide.TamplateID = helpfulGuideTemplates.ID;
                            programHelpfulGuide.CreatedByUserID = userSessionEntity.UserID;
                            programHelpfulGuide.CreatedDateTime = System.DateTime.Now;
                            programHelpfulGuide.LastModifiedByUserID = userSessionEntity.UserID;
                            programHelpfulGuide.LastModifiedDateTime = System.DateTime.Now;

                            programHelpfulGuide.HelpfulGuideTemplate = helpfulGuideTemplates;
                            this._fundingSourceRepository.SaveOrUpdateHelpfulGuideText(programHelpfulGuide, userSessionEntity.UserID);

                            validationMessages.Add("Helpful Guide text saved successfully.");
                            commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                            commonCreationParam.ID = helpfulGuideTextRequest.ProgramID;
                            commonCreationParam.StatusMessage = "Helpful Guide text saved successfully.";
                        }
                    }
                }
                return commonCreationParam;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateHelpfulGuideTemplate", null);
                commonCreationParam = new CommonResponse(
                    ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateHelpfulGuideTemplate", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public HelpfulGuideTextResponse GetHelpfulGuideTemplate(long programID)
        {
            HelpfulGuideTextResponse helpfulGuideTextResponse = new HelpfulGuideTextResponse();
            try
            {
                var programHelpfulGuides = this._fundingSourceRepository.GetProgramHelpfulGuideByProgramId(programID).ToList();

                foreach (var templates in programHelpfulGuides)
                {
                    var listProgramQuestionViewEntity = programHelpfulGuides.Select(
                                        programHelpfulGuideText => new HelpfulGuideTextViewEntity
                                        {
                                            HelpfulGuideTemplateID = programHelpfulGuideText.TamplateID,
                                            Body = programHelpfulGuideText.HelpfulGuideTemplate.Body,
                                            ProgramID = programHelpfulGuideText.ProgramID,
                                            TypeID = programHelpfulGuideText.HelpfulGuideTemplate.TypeID,
                                            TemplateName = programHelpfulGuideText.HelpfulGuideTemplate.TemplateType.Name
                                        }).ToList();

                    if (listProgramQuestionViewEntity.Count > 0)
                    {
                        helpfulGuideTextResponse.IsSuccess = true;
                        helpfulGuideTextResponse.HelpfulGuideTextViewResponse = listProgramQuestionViewEntity.ToList();

                    }
                    else
                    {
                        helpfulGuideTextResponse.IsSuccess = false;
                        helpfulGuideTextResponse.Message = "Couldn't find the Helpful Guide text for this program.";

                    }
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetHelpfulGuideTemplate", null);
                helpfulGuideTextResponse.IsSuccess = false;
                helpfulGuideTextResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetHelpfulGuideTemplate", null);
                helpfulGuideTextResponse.IsSuccess = false;
                helpfulGuideTextResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }

            return helpfulGuideTextResponse;
        }

        public ProgramInvitationEmailResponse GetProgramInvitationEmail(long programID)
        {
            var programInvitationEmailResponse = new ProgramInvitationEmailResponse();
            var notificationRes = this._notificationRepository.GetNotificationByNotificationType((long)EmailTemplateNameID.PROGRAMINVITATION);
            var programInvitationEmailPlaceHolder = this._programInvitationRepository.GetProgramInvitationEmailPlaceHolder(programID);
            FundingSource fundingSource = this._fundingSourceRepository.GetFundingSourceByID(programID);
            if (programInvitationEmailPlaceHolder != null)
            {
                programInvitationEmailResponse.ProgramInvitationEmail = new ProgramInvitationEmail
                {
                    MessageSubject = programInvitationEmailPlaceHolder.MessageSubject,
                    Body = programInvitationEmailPlaceHolder.MessageBody.Replace("<p id='callBackURL' class='bodycopy'>", "<p id='callBackURL' class='bodycopy' style='display: none'>").Replace("<p id=\"callBackURL\" class=\"bodycopy\">", "<p id=\"callBackURL\" class=\"bodycopy\" style=\"display: none\">").Replace("text-align: center;", "text-align: center; display: none"),
                    DefaultTemplate = notificationRes.Body.Replace("<p id='callBackURL' class='bodycopy'>", "<p id='callBackURL' class='bodycopy' style='display: none'>").Replace("<p id=\"callBackURL\" class=\"bodycopy\">", "<p id=\"callBackURL\" class=\"bodycopy\" style=\"display: none\">")
                };
                if (!string.IsNullOrEmpty(fundingSource?.Logo?.Source))
                {
                    programInvitationEmailResponse.ProgramInvitationEmail.Body = programInvitationEmailResponse.ProgramInvitationEmail.Body.Replace("src='@logo' style='float: right;display: none'", "src='@logo' style='float:right;display:block'").Replace("@logo", fundingSource?.Logo?.Source == string.Empty ? string.Empty : fundingSource?.Logo?.Source);
                    programInvitationEmailResponse.ProgramInvitationEmail.DefaultTemplate = programInvitationEmailResponse.ProgramInvitationEmail.DefaultTemplate.Replace("src='@logo' style='float: right;display: none'", "src='@logo' style='float:right;display:block'").Replace("@logo", fundingSource?.Logo?.Source == string.Empty ? string.Empty : fundingSource?.Logo?.Source);
                }

                programInvitationEmailResponse.IsSuccess = true;
            }
            else
            {
                
                if (notificationRes != null)
                {
                    programInvitationEmailResponse.ProgramInvitationEmail = new ProgramInvitationEmail
                    {
                        MessageSubject = notificationRes.MessageSubject,
                        
                        Body = notificationRes.Body.Replace("<p  id='callBackURL' class='bodycopy'>", "<p class='bodycopy' style='display: none'><br /> "),
                        DefaultTemplate = notificationRes.Body.Replace("<p  id='callBackURL' class='bodycopy'>", "<p  id='callBackURL' class='bodycopy' style='display: none'>")
                    };
                    if (!string.IsNullOrEmpty(fundingSource?.Logo?.Source))
                    {
                        programInvitationEmailResponse.ProgramInvitationEmail.Body = programInvitationEmailResponse.ProgramInvitationEmail.Body.Replace("src='@logo' style='float: right;display: none'", "src='@logo' style='float:right;display:block'").Replace("@logo", fundingSource?.Logo?.Source == string.Empty ? string.Empty : fundingSource?.Logo?.Source);
                        programInvitationEmailResponse.ProgramInvitationEmail.DefaultTemplate = programInvitationEmailResponse.ProgramInvitationEmail.DefaultTemplate.Replace("src='@logo' style='float: right;display: none'", "src='@logo' style='float:right;display:block'").Replace("@logo", fundingSource?.Logo?.Source == string.Empty ? string.Empty : fundingSource?.Logo?.Source);
                    }
                    programInvitationEmailResponse.IsSuccess = true;
                }
                else
                {
                    programInvitationEmailResponse.IsSuccess = false;
                    programInvitationEmailResponse.Message = "Couldn't find the Program Invitation Email text for this program.";
                }
            }

            return programInvitationEmailResponse;

        }
        public CommonResponse SaveOrUpdateProgramInvitationEmail(ProgramInvitationEmailRequest programInvitationEmailRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            var programInvitationEmailPlaceHolder = new ProgramInvitationEmailPlaceHolder();
            CommonResponse commonCreationParam = null;
            if (programInvitationEmailRequest == null)
            {
                _Logger.LogError("Input Parameter FundingSourceParam is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                programInvitationEmailPlaceHolder = this._programInvitationRepository.GetProgramInvitationEmailPlaceHolder(programInvitationEmailRequest.ProgramID);

                if (programInvitationEmailPlaceHolder != null)
                {
                    programInvitationEmailPlaceHolder.ProgramID = programInvitationEmailRequest.ProgramID;
                    programInvitationEmailPlaceHolder.MessageBody = programInvitationEmailRequest.MessageBody;
                    programInvitationEmailPlaceHolder.LastModifiedByUserID = userSessionEntity.UserID;
                    programInvitationEmailPlaceHolder.LastModifiedDateTime = DateTime.Now;
                    programInvitationEmailPlaceHolder.MessageSubject = string.Empty;   

                }
                else
                {
                    programInvitationEmailPlaceHolder = new ProgramInvitationEmailPlaceHolder();
                    programInvitationEmailPlaceHolder.ProgramID = programInvitationEmailRequest.ProgramID;
                    programInvitationEmailPlaceHolder.MessageBody = programInvitationEmailRequest.MessageBody;
                    programInvitationEmailPlaceHolder.MessageSubject = string.Empty;
                }
                _fundingSourceRepository.SaveOrUpdateProgramInvitationEmail(programInvitationEmailPlaceHolder, userSessionEntity.UserID);
                commonCreationParam = new CommonResponse(ResponseStatus.Success, "Invitation saved successfully.", null);
                commonCreationParam.ID = programInvitationEmailPlaceHolder.ProgramID;
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramInvitationEmail ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramInvitationEmail", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            
            return commonCreationParam;
        }

        public void SaveOrUpdateAfterProgramInvitationEmail(ProgramInvitationEmailRequest programInvitationEmailRequest, UserSessionEntity userSessionEntity)
        {
            var programInvitationEmailPlaceHolder = new ProgramInvitationEmailPlaceHolder();
            try
            {
                programInvitationEmailPlaceHolder = this._programInvitationRepository.GetProgramInvitationEmailPlaceHolder(programInvitationEmailRequest.ProgramID);

                if (programInvitationEmailPlaceHolder != null)
                {
                    programInvitationEmailPlaceHolder.ProgramID = programInvitationEmailRequest.ProgramID;
                    programInvitationEmailPlaceHolder.MessageBody = programInvitationEmailRequest.MessageBody;
                    programInvitationEmailPlaceHolder.LastModifiedByUserID = userSessionEntity.UserID;
                    programInvitationEmailPlaceHolder.LastModifiedDateTime = DateTime.Now;
                    programInvitationEmailPlaceHolder.MessageSubject = string.Empty;

                }
                else
                {
                    programInvitationEmailPlaceHolder = new ProgramInvitationEmailPlaceHolder();
                    programInvitationEmailPlaceHolder.ProgramID = programInvitationEmailRequest.ProgramID;
                    programInvitationEmailPlaceHolder.MessageBody = programInvitationEmailRequest.MessageBody;
                    programInvitationEmailPlaceHolder.MessageSubject = string.Empty;
                }
                _fundingSourceRepository.SaveOrUpdateProgramInvitationEmail(programInvitationEmailPlaceHolder, userSessionEntity.UserID);
               
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramInvitationEmail ", null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramInvitationEmail", null);
            }
        }
        public List<ProgramResponse> GetAllProgramInvitations()
        {
            var Programs = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true)
                    .Select(x => new ProgramResponse
                    {
                        ProgramId = x.FundingSourceID,
                        ProgramName = x.ProgramName
                    }).ToList();
            return Programs;
        }

        public PaymentScheduleTransResponse AddPaymentScheduleTransaction(PaymentScheduleTransParam paymentScheduleTransParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            var result = new PaymentScheduleTransResponse();
            CommonResponse commonCreationParam = null;
            long businessID = 0; long programID = 0;
            businessID = paymentScheduleTransParam.BusinessID;
            programID = paymentScheduleTransParam.ProgramID;
            if (paymentScheduleTransParam == null)
            {

                _Logger.LogError("Input Parameter PaymentScheduleTransaction is null");
                result.commonResponse = new CommonResponse( ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);    
              
                return result;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                 commonCreationParam = new CommonResponse( ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                result.commonResponse = commonCreationParam;
                return result;
            }
            try
            {
              

                var paymentSchedule = new PaymentScheduleTransaction();
                if (paymentScheduleTransParam != null && paymentScheduleTransParam.PaymentScheduleID > 0)//update existing
                {


                    paymentSchedule.PaymentScheduleID = Convert.ToInt64(paymentScheduleTransParam.PaymentScheduleID);
                    paymentSchedule.LoanApplicationID = paymentScheduleTransParam.LoanApplicationID;
                    paymentSchedule.BusinessID = paymentScheduleTransParam.BusinessID;
                    paymentSchedule.ProgramID = paymentScheduleTransParam.ProgramID;
                    paymentSchedule.TransactionDate = paymentScheduleTransParam.TransactionDate;
                    paymentSchedule.FundingTypeID = paymentScheduleTransParam.FundingTypeID;
                    paymentSchedule.FundedAmount = paymentScheduleTransParam.FundedAmount;
                    paymentSchedule.TransactionStatusID = paymentScheduleTransParam.TransactionStatusID;
                    paymentSchedule.ContactID = paymentScheduleTransParam.ContactID;
                    paymentSchedule.CreatedByUserID = userSessionEntity.UserID;
                    paymentSchedule.CreatedDateTime = DateTime.Now;
                    paymentSchedule.LastModifiedByUserID = userSessionEntity.UserID;
                    paymentSchedule.LastModifiedDateTime = DateTime.Now;
                    paymentSchedule.IsActive = true;
                }
                else if (paymentScheduleTransParam != null)//Add new
                {

                    paymentSchedule.LoanApplicationID = paymentScheduleTransParam.LoanApplicationID;
                    paymentSchedule.BusinessID = paymentScheduleTransParam.BusinessID;
                    paymentSchedule.ProgramID = paymentScheduleTransParam.ProgramID;
                    paymentSchedule.TransactionDate = paymentScheduleTransParam.TransactionDate;
                    paymentSchedule.FundingTypeID = paymentScheduleTransParam.FundingTypeID;
                    paymentSchedule.FundedAmount = paymentScheduleTransParam.FundedAmount;
                    paymentSchedule.TransactionStatusID = paymentScheduleTransParam.TransactionStatusID;
                    paymentSchedule.ContactID = paymentScheduleTransParam.ContactID;
                    paymentSchedule.CreatedByUserID = userSessionEntity.UserID;
                    paymentSchedule.CreatedDateTime = DateTime.Now;
                    paymentSchedule.LastModifiedByUserID = userSessionEntity.UserID;
                    paymentSchedule.LastModifiedDateTime = DateTime.Now;
                    paymentSchedule.IsActive = true;
                }

                if (paymentSchedule != null)
                {

                    // get application Id based on business id & program ID
                    //var listLoanIds  = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.FundingApplication.ProgramID== paymentSchedule.ProgramID && x.ProgramInvitation.BusinessID == paymentSchedule.BusinessID &&  x.ProgramInvitation.ProgramID == paymentSchedule.ProgramID && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Drafted && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Initialized).FirstOrDefault();
                    //if (listLoanIds!=null)
                    //{
                       // paymentSchedule.LoanApplicationID = listLoanIds.LoanApplicationID;
                        this._fundingSourceRepository.SaveOrUpdatePaymentScheduleTransaction(paymentSchedule, userSessionEntity.UserID);

                        commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);

                    //}
                    //else
                    //{
                    //    validationMessages.Add("Invalid details.");
                    //    commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                    //}
                  
                }


                else
                {
                    validationMessages.Add("Invalid details.");
                    commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdatePaymentScheduleTransaction ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdatePaymentScheduleTransaction", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }

            var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(businessID, paymentScheduleTransParam.LoanApplicationID);
            if (lstPaymentSchedule != null && lstPaymentSchedule.Count > 0)
            {
                string fundDisbursedAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 2).Sum(p => p.FundedAmount).ToString();
                result.FundDisbursedAmount = Decimal.Truncate(Convert.ToDecimal(fundDisbursedAmount)).ToString();

                string fundPendingAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 1).Sum(p => p.FundedAmount).ToString();
                result.FundPendingAmount = Decimal.Truncate(Convert.ToDecimal(fundPendingAmount)).ToString();

                result.BusinessID = businessID;
                result.ProgramID = programID;
                result.LoanApplicationID = lstPaymentSchedule.FirstOrDefault().LoanApplicationID;
            }
            



            result.commonResponse= commonCreationParam;
            return result;
        }
        public PaymentScheduleTransResponse RemovePaymentScheduleTransaction(PaymentScheduleTransParam paymentScheduleTransParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            var result = new PaymentScheduleTransResponse();
            CommonResponse commonCreationParam = null;
            long businessID = 0; long programID = 0;
            businessID = paymentScheduleTransParam.BusinessID;
            programID = paymentScheduleTransParam.ProgramID;
            if (paymentScheduleTransParam == null && paymentScheduleTransParam.PaymentScheduleID<1)
            {

                _Logger.LogError("Input Parameter PaymentScheduleTransaction or PaymentScheduleID is null");
                result.commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);

                return result;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                result.commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);

                return result;
            }
            try
            {
              
                var paymentSchedule = new List<PaymentScheduleTransaction>();
                if (paymentScheduleTransParam != null && paymentScheduleTransParam.LoanApplicationID > 0)// soft deleted
                {

                     paymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID);

                   
                    if (paymentSchedule!=null && paymentSchedule.Count<1)
                    {
                        _Logger.LogError("NO records found!");
                        result.commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                        return result;
                    }
                    //filter only pending payment schedule.
                    var ps = paymentSchedule.Where(x=>x.TransactionStatusID==1 && x.PaymentScheduleID== paymentScheduleTransParam.PaymentScheduleID).FirstOrDefault();
                    if (ps!=null)
                    {
                        ps.IsActive = false;
                        //paymentSchedule.ForEach(ps => { ps.IsActive = false; ps.LastModifiedByUserID = userSessionEntity.UserID; ps.LastModifiedDateTime = DateTime.Now; });
                        this._fundingSourceRepository.RemovePaymentScheduleTransaction(ps, userSessionEntity.UserID);
                    }                  
                   
                }

                if (paymentSchedule != null)
                {
                   commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                    var paymentScheduleSummary = new PaymentScheduleSummary();
                    var lstsummary = _fundingSourceRepository.GetPaymentScheduleSummary(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID);
                    if(lstsummary!=null && lstsummary.Count > 0)
                    {
                        var transDetailsVal = _fundingSourceRepository.GetPaymentScheduleTransaction(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID);
                        var SP = lstsummary.Where(x => x.LoanApplicationID == paymentScheduleTransParam.LoanApplicationID).FirstOrDefault();                        
                            SP.FundPendingAmount = transDetailsVal.Where(w => w.TransactionStatusID == 1 && w.IsActive == true).Sum(p => p.FundedAmount);
                            this._fundingSourceRepository.SaveOrUpdatePaymentScheduleSummary(SP, userSessionEntity.UserID);                      
                        
                    }
                }
                else
                {
                    validationMessages.Add("Invalid details.");
                    commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> RemovePaymentScheduleTransaction ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> RemovePaymentScheduleTransaction", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(businessID, programID);
            if (lstPaymentSchedule != null && lstPaymentSchedule.Count>0)
            {
                string fundDisbursedAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 2).Sum(p => p.FundedAmount).ToString();
                result.FundDisbursedAmount = Decimal.Truncate(Convert.ToDecimal(fundDisbursedAmount)).ToString();

                string fundPendingAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 1).Sum(p => p.FundedAmount).ToString();
                result.FundPendingAmount = Decimal.Truncate(Convert.ToDecimal(fundPendingAmount)).ToString();

                result.BusinessID = businessID;
                result.ProgramID = programID;
                result.LoanApplicationID = lstPaymentSchedule.FirstOrDefault().LoanApplicationID;
            }
            result.commonResponse = commonCreationParam;
            return result;
           
        }
        
        public PaymentScheduleTransResponse DeleteAllPaymentScheduleTransactionByLoan(PaymentScheduleTransParam paymentScheduleTransParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            var result = new PaymentScheduleTransResponse();
            CommonResponse commonCreationParam = null;
            long businessID = 0; long programID = 0;
            businessID = paymentScheduleTransParam.BusinessID;
            programID = paymentScheduleTransParam.ProgramID;
            if (paymentScheduleTransParam == null)
            {

                _Logger.LogError("Input Parameter PaymentScheduleTransaction is null");
                result.commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);

                return result;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                result.commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);

                return result;
            }
            try
            {

                var paymentSchedule = new List<PaymentScheduleTransaction>();
                if (paymentScheduleTransParam != null && paymentScheduleTransParam.LoanApplicationID > 0)// soft deleted
                {

                    paymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID);


                    if (paymentSchedule != null && paymentSchedule.Count < 1)
                    {
                        _Logger.LogError("NO records found!");
                        result.commonResponse = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                        return result;
                    }
                    //filter only pending payment schedule.
                    paymentSchedule = paymentSchedule.Where(x => x.TransactionStatusID == 1).ToList();
                    if (paymentSchedule.Count > 0)
                    {
                        paymentSchedule.ForEach(ps => { ps.IsActive = false; ps.LastModifiedByUserID = userSessionEntity.UserID; ps.LastModifiedDateTime = DateTime.Now; });
                        this._fundingSourceRepository.DeleteAllPaymentScheduleTransactionByLoan(paymentSchedule, userSessionEntity.UserID);
                    }

                }

                if (paymentSchedule != null)
                {
                    commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                    var paymentScheduleSummary = new PaymentScheduleSummary();
                    var lstsummary = _fundingSourceRepository.GetPaymentScheduleSummary(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID);
                    if (lstsummary != null && lstsummary.Count > 0)
                    {
                        var SP = lstsummary.Where(x => x.LoanApplicationID == paymentScheduleTransParam.LoanApplicationID).FirstOrDefault();
                        var transDetailsVal = _fundingSourceRepository.GetPaymentScheduleTransaction(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID);
                        if(transDetailsVal==null || transDetailsVal.Count < 1)
                        {
                            SP.FundAllocatedAmount = 0;
                            SP.AdditionalNotesAgreement = string.Empty;
                            SP.FundPendingAmount = 0;
                            this.ResetIsPaymentSchedule(paymentScheduleTransParam.LoanApplicationID, userSessionEntity);
                            this.DeletePaymentScheduleDocuments(paymentScheduleTransParam.BusinessID, paymentScheduleTransParam.LoanApplicationID, userSessionEntity);
                        }
                        
                        SP.FundPendingAmount = transDetailsVal.Where(w => w.TransactionStatusID == 1 && w.IsActive == true).Sum(p => p.FundedAmount);
                        this._fundingSourceRepository.SaveOrUpdatePaymentScheduleSummary(SP, userSessionEntity.UserID);

                    }
                }
                else
                {
                    validationMessages.Add("Invalid details.");
                    commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> RemovePaymentScheduleTransaction ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> RemovePaymentScheduleTransaction", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(businessID, programID);
            if (lstPaymentSchedule != null && lstPaymentSchedule.Count > 0)
            {
                string fundDisbursedAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 2).Sum(p => p.FundedAmount).ToString();
                result.FundDisbursedAmount = Decimal.Truncate(Convert.ToDecimal(fundDisbursedAmount)).ToString();

                string fundPendingAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 1).Sum(p => p.FundedAmount).ToString();
                result.FundPendingAmount = Decimal.Truncate(Convert.ToDecimal(fundPendingAmount)).ToString();

                result.BusinessID = businessID;
                result.ProgramID = programID;
                result.LoanApplicationID = lstPaymentSchedule.FirstOrDefault().LoanApplicationID;
            }
            result.commonResponse = commonCreationParam;
            return result;

        }
        public PaymentScheduleTransactionResponse GetPaymentScheduleTransaction(long businessID, long applicationID)
        {
            var transactionResponse = new PaymentScheduleTransactionResponse();
            var lstTransactionDetails = new List<PaymentScheduleTransactionDetail>();

            try
            {
                // parameter validation

                if (businessID <= 0 || applicationID <= 0)
                {
                    transactionResponse.IsSuccess = false;
                    transactionResponse.IsValidationError = true;
                    transactionResponse.ValidationError = new ValidationErrorResponse();
                    transactionResponse.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "businessID/applicationID";
                    validationError.ErrorMessage = "Please provide valid business ID & application ID";
                    transactionResponse.ValidationError.ValidationError = new List<ValidationError>();
                    transactionResponse.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                    /** Get transaction Response  list **/
                    var listfundingType = _fundingTypeRepository.GetAllFundingType();
                    var listTrasactionStatus = _genaralOptionRepository.GetMasterOption("PaymentSchedule");
                    var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(businessID, applicationID);

                    //transactionDetails = _mapper.Map<List<PaymentScheduleTransactionDetail>>(lstPaymentSchedule);

                    //transactionResponse.PaymentScheduleTransactionList = transactionDetails;

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
                        // to make enabled for send notification
                        if ((transaction.TransactionDate.Date - DateTime.Now.Date).TotalDays <= CommonConstants.NotificationEnabledDays)//if Transaction date one date before from today date or post date.
                        {
                            transactionDetail.IsEnabled = true;
                        }

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
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetPaymentScheduleTransaction ", null);
                transactionResponse.IsSuccess = false;
                transactionResponse.IsValidationError = false;
                transactionResponse.Message = "Failed to fetch the payment schedule transactions.";
            }
            return transactionResponse;
        }

        public MasterOptionResponse GetProgramList(long businessID)
        {
            var masterOptionResponse = new MasterOptionResponse();
            try
            {              
                
                //CommonConstants.ThresholdRequestAmount = 0;             
                //var result = this._genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                //if (result != null && result.Count > 0)
                //{
                //    CommonConstants.ThresholdRequestAmount = long.Parse(result.FirstOrDefault().OptionValue);
                //}
                var programList = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitation.BusinessID == businessID && x.FundingApplication.IsPaymentSchedule==true)            
                    
                    .Select(x => new MasterOptionDetail
                    {
                        Key = x.ProgramInvitation.ProgramID.ToString(),
                        Values = x.ProgramInvitation != null ? x.FundingApplication.FundingSource.ProgramName : string.Empty,
                    }).Distinct().OrderBy(x => x.Values).ToList();

                    masterOptionResponse.IsSuccess = true;
                    masterOptionResponse.IsValidationError = false;
                    masterOptionResponse.Message = "Success";
                    masterOptionResponse.MasterOptionDetails = programList;
                

            }
            catch (Exception ex)
            {
                masterOptionResponse.IsSuccess = false;
                masterOptionResponse.IsValidationError = true;
                masterOptionResponse.Message = "failed to get data.";
            }
            return masterOptionResponse;
        }

        public PaymentScheduleSummaryResponse GetPaymentScheduleSummary(long businessID, long applicationId)
        {
            var response = new PaymentScheduleSummaryResponse();
            var summary = new PaymentScheduleSummaryDetails();
  
            try
            {
                // parameter validation

                if (businessID <= 0 || applicationId <= 0)
                {
                    response.IsSuccess = false;
                    response.IsValidationError = true;
                    response.ValidationError = new ValidationErrorResponse();
                    response.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "businessID/applicationId";
                    validationError.ErrorMessage = "Please provide valid business ID & application ID";
                    response.ValidationError.ValidationError = new List<ValidationError>();
                    response.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                    /*Get document if any*/
                    var document = _fundingSourceRepository.GetPaymentAgreementDocument(businessID, applicationId);
                    if (document != null)
                    {
                        
                           var agrementDocument = new PaymentScheduleSummaryDocument();

                            agrementDocument.DocumentID = document.DocumentID;
                            agrementDocument.DocumentGUID = document.DocumentGUID.ToString();
                            agrementDocument.DocumentTypeID = document.DocumentTypeID;
                            agrementDocument.DocumentName = document.DocumentName;
                            agrementDocument.FileName = document.FileName;
                            agrementDocument.PhysicalFileStorageKey = document.PhysicalFileStorageKey;
                            agrementDocument.FileSize = document.FileSize;
                            agrementDocument.FileUploadedSourceUrl = document.FileUploadedSourceUrl;

                           
                        
                        response.paymentScheduleDocument = agrementDocument;
                    }
                    /** Get transaction Response  list **/                
                    var lstLoanDetails = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.LoanApplicationID == applicationId && x.ProgramInvitation.BusinessID == businessID  && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Drafted && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Initialized).ToList();
                   
                    var lstsummary = _fundingSourceRepository.GetPaymentScheduleSummary(businessID, applicationId);
                   //Get contactId
                    var programInvitations = this._ProgramInviteeRepository.GetProgramInvitees().Where(x => x.ProgramInvitation.BusinessID == businessID && x.ProgramInvitation.IsActive == true);
                    if (programInvitations != null)
                    {
                        summary.ContactID = programInvitations.FirstOrDefault().ContactID;
                    }
                    if (lstsummary != null && lstsummary.Count > 0)
                    {
                        //summary.FundAllocatedAmount = lstsummary.Sum(fa => fa.FundAllocatedAmount).ToString();.OrderByDescending(x => x.ID).FirstOrDefault();
                        summary.FundAllocatedAmount = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().FundAllocatedAmount.ToString();
                        //summary.FundAllocatedAmount = "$ " + string.Format("{0:#,#}", Decimal.Truncate(Convert.ToDecimal(summary.FundAllocatedAmount)));
                        summary.FundAllocatedAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundAllocatedAmount)).ToString();
                        summary.FundAllocatedSummaryAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundAllocatedAmount));
                        summary.FundPaymentScheduleID = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().ID;                      
                        summary.FundDisbursedAmount = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().FundDisbursedAmount.ToString();
                        summary.FundPendingAmount = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().FundPendingAmount.ToString();
                        if(lstsummary?.OrderByDescending(x => x.ID).FirstOrDefault().AdditionalNotesAgreement != null)
                        {
                            summary.AdditionalNotesAgreement = lstsummary?.OrderByDescending(x => x.ID).FirstOrDefault().AdditionalNotesAgreement?.ToString();
                        }
                        else
                        {
                            summary.AdditionalNotesAgreement = String.Empty;
                        }
                        summary.ProgramID = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().ProgramID;
                        summary.LoanApplicationID = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().LoanApplicationID;
                        summary.BusinessID = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().BusinessID;
                    }
                    else
                    {
                        summary.FundAllocatedAmount = "0";
                        summary.FundAllocatedSummaryAmount = 0;
                        summary.AdditionalNotesAgreement = "";
                    }

                    if (lstLoanDetails != null)
                    {
                        summary.FundRequestedAmount = lstLoanDetails.Sum(s => s.FundingApplication.RequestedFundAmount).ToString();
                        summary.ProgramName = lstLoanDetails?.FirstOrDefault().ProgramInvitation?.FundingSource?.ProgramName;
                    }                  
                
                    summary.FundRequestedAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundRequestedAmount)).ToString();
                    summary.FundRequestedSummaryAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundRequestedAmount));                 
                    summary.FundDisbursedAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundDisbursedAmount)).ToString();
                    summary.FundDisbursedSummaryAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundDisbursedAmount));               
                    summary.FundPendingAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundPendingAmount)).ToString();
                    summary.FundPendingSummaryAmount = Decimal.Truncate(Convert.ToDecimal(summary.FundPendingAmount));



                    var rslt = _LoanApplicationRepository.GetLoanApplicationByApplicationID(applicationId);
                    if (rslt != null)
                    {
                        response.ApplicationStatusId = rslt.ApplicationStatusID;
                    }

                    response.paymentScheduleSummary = summary;
                    response.IsSuccess = true;
                    response.IsValidationError = false;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetPaymentScheduleSummary ", null);
                response.IsSuccess = false;
                response.IsValidationError = false;
                response.Message = "Failed to fetch to get payment schedule summary.";
            }
            return response;
        }

        public CommonResponse SaveorUpdatePaymentScheduleAndDocument(FundPaymentScheduleParam fundPaymentScheduleParam, UserSessionEntity userSessionEntity, FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            if (fundPaymentScheduleParam == null)
            {

                _Logger.LogError("Input Parameter fundPaymentScheduleParam is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSessionEntity is null");
                commonCreationParam = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonCreationParam;
            }
            try
            {
                //update flag IsPaymentSchedule only if this request come from pop up at UW.
                //IsLoanApplicationFundDetails: if it is true then this request come from Accept pop up.
                if (fundBulkPaymentScheduleParam.IsLoanApplicationFundDetails)
                {
                    commonCreationParam = this.SaveLoanPaymentType(fundBulkPaymentScheduleParam, userSessionEntity);
                }
                
                //update SPA
                var lstPaymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransaction(fundPaymentScheduleParam.BusinessID, fundPaymentScheduleParam.LoanApplicationID);
                if(lstPaymentSchedule!=null && lstPaymentSchedule.Count > 0)
                {
                    fundPaymentScheduleParam.FundPendingAmount = lstPaymentSchedule.Where(w => w.TransactionStatusID == 1 && w.IsActive==true).Sum(p => p.FundedAmount);
                }
                commonCreationParam = SaveorUpdateFundPaymentScheduleSummary(fundPaymentScheduleParam, userSessionEntity);
                if (commonCreationParam.StatusMessage == "Success")
                {
                    var contact = this._contactRepository.FirstOrDefault(a => a.ContactID == fundPaymentScheduleParam.ContactID);
                    if (contact != null)
                    {
                        var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(fundPaymentScheduleParam.LoanApplicationID);
                        if (loanApplication != null)
                        {
                            var programInvitation = this._programInvitationRepository.GetProgramInvitation(loanApplication.ProgramInvitationID);
                            //if (!fundBulkPaymentScheduleParam.IsLoanApplicationFundDetails)
                            //{
                            //    this.SendFundingDetailEmailNotification(contact.EmailAddress, fundPaymentScheduleParam.BusinessID, programInvitation.ProgramInvitationID, loanApplication.LoanApplicationID);
                            //}
                        }
                     
                    }

                    if (fundPaymentScheduleParam != null & fundPaymentScheduleParam.DocumentGUID != "" && fundPaymentScheduleParam.FileUploadedSourceUrl.Trim() != "")
                    {
                        commonCreationParam = SaveorUpdateFundPaymentScheduleDocuments(fundPaymentScheduleParam, userSessionEntity);
                    }
                }
              
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveorUpdatePaymentSchedule ", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveorUpdatePaymentSchedule", null);
                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonCreationParam;
        }

        public void SendPaymentScheduleEmailNotification(FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                var contact = this._contactRepository.FirstOrDefault(a => a.ContactID == fundBulkPaymentScheduleParam.ContactID);
                if(contact != null)
                {
                    var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(fundBulkPaymentScheduleParam.LoanApplicationID);
                    if(loanApplication != null)
                    {
                        var programInvitation = this._programInvitationRepository.GetProgramInvitation(loanApplication.ProgramInvitationID);
                        var rslt = SendFundingDetailEmailNotification(contact.EmailAddress, fundBulkPaymentScheduleParam.BusinessID, programInvitation.ProgramInvitationID,loanApplication.LoanApplicationID);
                    }
                    
                }
                
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
        }
        private CommonResponse SaveorUpdateFundPaymentScheduleSummary(FundPaymentScheduleParam fundPaymentScheduleParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            var paymentScheduleSummary = new PaymentScheduleSummary();
            var listLoanIds = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.FundingApplication.ProgramID == fundPaymentScheduleParam.ProgramID && x.ProgramInvitation.BusinessID == fundPaymentScheduleParam.BusinessID && x.ProgramInvitation.ProgramID == fundPaymentScheduleParam.ProgramID && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Drafted && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Initialized).FirstOrDefault();
            if (listLoanIds != null)
            {
                fundPaymentScheduleParam.LoanApplicationID = listLoanIds.LoanApplicationID;
            }
            /******Double check if exist and from UI missed passing FundPaymentScheduleID********/
            var lstsummary = _fundingSourceRepository.GetPaymentScheduleSummary(fundPaymentScheduleParam.BusinessID, fundPaymentScheduleParam.LoanApplicationID);
            if(lstsummary!=null && lstsummary.Count > 0)
            {
              // var  psResult= new PaymentScheduleSummary();
                paymentScheduleSummary = lstsummary.Where(x=>x.LoanApplicationID== fundPaymentScheduleParam.LoanApplicationID).OrderByDescending(x=>x.ID).FirstOrDefault();//if double with same loan
                if (paymentScheduleSummary != null)
                {
                   
                    paymentScheduleSummary.FundRequestedAmount = fundPaymentScheduleParam.FundRequestedAmount;
                    paymentScheduleSummary.FundAllocatedAmount = fundPaymentScheduleParam.FundAllocatedAmount;
                    paymentScheduleSummary.FundDisbursedAmount = fundPaymentScheduleParam.FundDisbursedAmount;
                    paymentScheduleSummary.FundPendingAmount = fundPaymentScheduleParam.FundPendingAmount;
                    paymentScheduleSummary.ContactID = fundPaymentScheduleParam.ContactID;
                    paymentScheduleSummary.CreatedByUserID = userSessionEntity.UserID;
                    paymentScheduleSummary.CreatedDateTime = DateTime.Now;
                    paymentScheduleSummary.LastModifiedByUserID = userSessionEntity.UserID;
                    paymentScheduleSummary.LastModifiedDateTime = DateTime.Now;
                    paymentScheduleSummary.IsActive = true;
                    paymentScheduleSummary.AdditionalNotesAgreement = fundPaymentScheduleParam.AdditionalNotesAgreement;
                }
                
            }           

            else if (fundPaymentScheduleParam != null)//Add new
            {

                paymentScheduleSummary.LoanApplicationID = fundPaymentScheduleParam.LoanApplicationID;
                paymentScheduleSummary.BusinessID = fundPaymentScheduleParam.BusinessID;
                paymentScheduleSummary.ProgramID = fundPaymentScheduleParam.ProgramID;
                paymentScheduleSummary.FundRequestedAmount = fundPaymentScheduleParam.FundRequestedAmount;
                paymentScheduleSummary.FundAllocatedAmount = fundPaymentScheduleParam.FundAllocatedAmount;
                paymentScheduleSummary.FundDisbursedAmount = fundPaymentScheduleParam.FundDisbursedAmount;
                paymentScheduleSummary.FundPendingAmount = fundPaymentScheduleParam.FundPendingAmount;
                paymentScheduleSummary.ContactID = fundPaymentScheduleParam.ContactID;
                paymentScheduleSummary.CreatedByUserID = userSessionEntity.UserID;
                paymentScheduleSummary.CreatedDateTime = DateTime.Now;
                paymentScheduleSummary.LastModifiedByUserID = userSessionEntity.UserID;
                paymentScheduleSummary.LastModifiedDateTime = DateTime.Now;
                paymentScheduleSummary.IsActive = true;
                paymentScheduleSummary.AdditionalNotesAgreement = fundPaymentScheduleParam.AdditionalNotesAgreement;
            }
            if (paymentScheduleSummary != null)
            {
                this._fundingSourceRepository.SaveOrUpdatePaymentScheduleSummary(paymentScheduleSummary, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }

            else
            {
                validationMessages.Add("Invalid Payment Schedule Summary.");
                commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
            }
            return commonCreationParam;
        }

        private CommonResponse SaveorUpdateFundPaymentScheduleDocuments(FundPaymentScheduleParam fundPaymentScheduleParam, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            var fundAgreementDocuments = new FundAgreementDocuments();
            var listLoanIds = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.FundingApplication.ProgramID == fundPaymentScheduleParam.ProgramID && x.ProgramInvitation.BusinessID == fundPaymentScheduleParam.BusinessID && x.ProgramInvitation.ProgramID == fundPaymentScheduleParam.ProgramID && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Drafted && x.ApplicationStatusID != (long)ApplicationStatusEnumeration.Initialized).FirstOrDefault();
            if (listLoanIds != null)
            {
                fundPaymentScheduleParam.LoanApplicationID = listLoanIds.LoanApplicationID;
            }

            if (fundPaymentScheduleParam != null && fundPaymentScheduleParam.DocumentID > 0)//update existing
            {

                fundAgreementDocuments.DocumentID = Convert.ToInt64(fundPaymentScheduleParam.DocumentID);
                fundAgreementDocuments.LoanApplicationID = fundPaymentScheduleParam.LoanApplicationID;
                fundAgreementDocuments.BusinessID = fundPaymentScheduleParam.BusinessID;
                fundAgreementDocuments.ProgramID = fundPaymentScheduleParam.ProgramID;
                fundAgreementDocuments.DocumentGUID = new Guid(fundPaymentScheduleParam.DocumentGUID);
                fundAgreementDocuments.DocumentTypeID = fundPaymentScheduleParam.DocumentTypeID;
                fundAgreementDocuments.DocumentName = fundPaymentScheduleParam.DocumentName;
                fundAgreementDocuments.FileName = fundPaymentScheduleParam.FileName;
                fundAgreementDocuments.PhysicalFileStorageKey = fundPaymentScheduleParam.PhysicalFileStorageKey;
                fundAgreementDocuments.FileSize = fundPaymentScheduleParam.FileSize;
                fundAgreementDocuments.FileUploadedSourceUrl = fundPaymentScheduleParam.FileUploadedSourceUrl;
                fundAgreementDocuments.ContactID = fundPaymentScheduleParam.ContactID;
                fundAgreementDocuments.CreatedByUserID = userSessionEntity.UserID;
                fundAgreementDocuments.CreatedDateTime = DateTime.Now;
                fundAgreementDocuments.LastModifiedByUserID = userSessionEntity.UserID;
                fundAgreementDocuments.LastModifiedDateTime = DateTime.Now;
                fundAgreementDocuments.IsActive = true;
            }
            else if (fundPaymentScheduleParam != null)//Add new
            {

                fundAgreementDocuments.LoanApplicationID = fundPaymentScheduleParam.LoanApplicationID;
                fundAgreementDocuments.BusinessID = fundPaymentScheduleParam.BusinessID;
                fundAgreementDocuments.ProgramID = fundPaymentScheduleParam.ProgramID;
                fundAgreementDocuments.DocumentGUID = new Guid(fundPaymentScheduleParam.DocumentGUID);
                fundAgreementDocuments.DocumentTypeID = fundPaymentScheduleParam.DocumentTypeID;
                fundAgreementDocuments.DocumentName = fundPaymentScheduleParam.DocumentName;
                fundAgreementDocuments.FileName = fundPaymentScheduleParam.FileName;
                fundAgreementDocuments.PhysicalFileStorageKey = fundPaymentScheduleParam.PhysicalFileStorageKey;
                fundAgreementDocuments.FileSize = fundPaymentScheduleParam.FileSize;
                fundAgreementDocuments.FileUploadedSourceUrl = fundPaymentScheduleParam.FileUploadedSourceUrl;
                fundAgreementDocuments.ContactID = fundPaymentScheduleParam.ContactID;
                fundAgreementDocuments.CreatedByUserID = userSessionEntity.UserID;
                fundAgreementDocuments.CreatedDateTime = DateTime.Now;
                fundAgreementDocuments.LastModifiedByUserID = userSessionEntity.UserID;
                fundAgreementDocuments.LastModifiedDateTime = DateTime.Now;
                fundAgreementDocuments.IsActive = true;
            }
            if (fundAgreementDocuments != null)
            {
                this._fundingSourceRepository.SaveOrUpdatePaymentScheduleDocument(fundAgreementDocuments, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }

            else
            {
                validationMessages.Add("Invalid Payment Schedule document.");
                commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
            }
            return commonCreationParam;
        }

        private CommonResponse DeletePaymentScheduleDocuments(long businessID,long applicationId, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonCreationParam = null;
            

            var fundAgreementDocuments = _fundingSourceRepository.GetAllPaymentAgreementDocumentByApplication( businessID,  applicationId);          
            
            if (fundAgreementDocuments != null && fundAgreementDocuments.Count>0)
            {
                fundAgreementDocuments.ForEach(ps => { ps.IsActive = false; ps.LastModifiedByUserID = userSessionEntity.UserID; ps.LastModifiedDateTime = DateTime.Now; });
                this._fundingSourceRepository.DeletePaymentScheduleDocuments(fundAgreementDocuments, userSessionEntity.UserID);

                commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
            }

            else
            {
                validationMessages.Add("Invalid Payment Schedule document.");
                commonCreationParam = new CommonResponse(ResponseStatus.Fail, MessageConstants.Failure, validationMessages);
            }
            return commonCreationParam;
        }
        public PaymentScheduleItemResponse GetPaymentScheduleTransactionById(long paymentScheduleID)
        {
            var transactionResponse = new PaymentScheduleItemResponse();
            var transactionDetails = new PaymentScheduleTransactionDetail();

            try
            {
                // parameter validation

                if (paymentScheduleID <= 0)
                {
                    transactionResponse.IsSuccess = false;
                    transactionResponse.IsValidationError = true;
                    transactionResponse.ValidationError = new ValidationErrorResponse();
                    transactionResponse.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "Payment Schedule ID";
                    validationError.ErrorMessage = "Please provide valid Payment Schedule ID";
                    transactionResponse.ValidationError.ValidationError = new List<ValidationError>();
                    transactionResponse.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                    /** Get transaction Response  list **/
                    var listfundingType = _fundingTypeRepository.GetAllFundingType();
                    var listTrasactionStatus = _genaralOptionRepository.GetMasterOption("PaymentSchedule");
                    var paymentSchedule = _fundingSourceRepository.GetPaymentScheduleTransactionById(paymentScheduleID);

                  
                        var transactionDetail = new PaymentScheduleTransactionDetail();
                        transactionDetail.PaymentScheduleID = paymentSchedule.PaymentScheduleID;
                        transactionDetail.LoanApplicationID = paymentSchedule.LoanApplicationID;
                        transactionDetail.BusinessID = paymentSchedule.BusinessID;
                        transactionDetail.ProgramID = paymentSchedule.ProgramID;
                        transactionDetail.ContactID = paymentSchedule.ContactID;
                     
                        transactionDetail.FundingTypeID = paymentSchedule.FundingTypeID;
                     
                        transactionDetail.FundedAmount = paymentSchedule.FundedAmount;
                    
                        transactionDetail.TransactionStatusID = paymentSchedule.TransactionStatusID;
                      
                        transactionDetail.CreatedDateTime = paymentSchedule.CreatedDateTime;
                        transactionDetail.CreatedByUserID = paymentSchedule.CreatedByUserID;
                        transactionDetail.LastModifiedDateTime = paymentSchedule.LastModifiedDateTime;
                        transactionDetail.LastModifiedByUserID = paymentSchedule.LastModifiedByUserID;
                        transactionDetail.LastModifiedDateTime = paymentSchedule.LastModifiedDateTime;

                        transactionDetail.TransactionDate = paymentSchedule.TransactionDate.ToString("MM/dd/yyyy").Replace("-", "/");
                        transactionDetail.TransactionStatusName = listTrasactionStatus != null ? listTrasactionStatus.FirstOrDefault(x => x.OptionID == paymentSchedule.TransactionStatusID).OptionValue.ToString() : String.Empty;
                        transactionDetail.FundingTypeName = listfundingType != null ? listfundingType.FirstOrDefault(x => x.FundingTypeID == paymentSchedule.FundingTypeID).Type.ToString() : "";
                        transactionDetail.FundedAmountFormat = "$ " + string.Format("{0:#,#}", Decimal.Truncate(paymentSchedule.FundedAmount));

                  
                    transactionResponse.PaymentScheduleTransaction = transactionDetail;
                    transactionResponse.IsSuccess = true;
                    transactionResponse.IsValidationError = false;
                    transactionResponse.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetPaymentScheduleTransaction ", null);
                transactionResponse.IsSuccess = false;
                transactionResponse.IsValidationError = false;
                transactionResponse.Message = "Failed to fetch the payment schedule transactions.";
            }
            return transactionResponse;
        }

        public CommonResponse SendFundingDetailEmailNotification(string emailId, long businessID, long programInvitationID, long loanApplicationID)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {

                string tokenID = "";
                string callBack = "";
                string callBackURL = "";
                string agreementData = string.Empty;
                User user = _userRepository.GetActiveByUserName(emailId);
                UserSessionEntity userSessionEntity = new UserSessionEntity();
                if (user != null && emailId == user.UserName)
                {
                    if (loanApplicationID > 0)
                    {
                        agreementData = this._masterService.GetEmailSPAHTMLText(loanApplicationID);
                        agreementData= agreementData.Substring(121);

                    } 
                    var processInstance = WorkflowInit.GetProcessInstanceByProcessID(loanApplicationID);
                    tokenID = CommonUtility.CreateUniqueID(user.ContactID.ToString());
                    callBack = _appSettings.BaseUrl + "/form/0/" + processInstance.ProcessId;
                    userSessionEntity.UserID = user.UserID;
                    callBackURL = agreementData+ "<tr> <td style='padding: 20px 0 20px 100px;'> <table class='buttonwrapper' border='0' cellspacing='3' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> </td> </tr> </table> </td> </tr> ";
                    //callBackURL = "<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> <br /><br /> </td>";
                    var email = _notificationService.SendPSEmail("Schedule of Payment Agreement Updated", emailId, callBackURL, (long)EmailTemplateNameID.FUNDDETAILS, programInvitationID, user.ContactID, loanApplicationID, "Borrower", userSessionEntity);
                    validationMessages.Add("Schedule of Payment Agreement Updated");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "Schedule of Payment Agreement uploaded mail sent successfully.";
                    commonResponse.ID = user.ContactID;
                }
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

        public CommonResponse NotifyPaymentSummaryTransaction(TransactionNotifyRequest transactionNotifyRequest, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            if (transactionNotifyRequest == null)
            {
                _Logger.LogError("Input Parameter is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            if (userSessionEntity == null)
            {
                _Logger.LogError("Input Parameter UserSession is null");
                validationMessages.Add("User session entity is null.");
                commonResponse = new CommonResponse(
                      ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return commonResponse;
            }
            try
            {

                    var transactionNotify = this._fundingSourceRepository.GetPaymentSummaryTransactionNotify( transactionNotifyRequest, userSessionEntity.UserID);
                if (transactionNotify != null && transactionNotify.NotifyID > 0)// if exist then update & call here sent email methods
                {
                    //Need to call email methods if  get success status after send email then set its record for audit

                    transactionNotify.IsSent = true;
                    transactionNotify.LastModifiedDateTime = DateTime.Now;
                    transactionNotify.LastModifiedByUserID = userSessionEntity.UserID;
                    transactionNotify.IsActive = true;
                    this._fundingSourceRepository.SavePaymentSummaryTransactionNotify(transactionNotify, userSessionEntity.UserID);

                    validationMessages.Add("Notification activated successfully!");
                }
                else
                {
                    //Need to call email methods if  get success status after send email then set its record for audit

                    transactionNotify = new PaymentSummaryTransactionNotify();
                    transactionNotify.ApplicationID = transactionNotifyRequest.ApplicationID;
                    transactionNotify.TransactionDate = transactionNotifyRequest.TransactionDate;
                    transactionNotify.ToNotifyUserId = userSessionEntity.UserID;
                    transactionNotify.IsSent = true;
                    transactionNotify.CreatedDateTime = DateTime.Now;
                    transactionNotify.CreatedByUserID = userSessionEntity.UserID;
                    transactionNotify.LastModifiedDateTime = DateTime.Now;
                    transactionNotify.LastModifiedByUserID = userSessionEntity.UserID;
                    transactionNotify.IsActive = true;
                    this._fundingSourceRepository.SavePaymentSummaryTransactionNotify(transactionNotify, userSessionEntity.UserID);
                    var contact = this._contactRepository.FirstOrDefault(a => a.ContactID == transactionNotifyRequest.ContactID);
                    this.SendNotifyPaymentSummaryTransaction(contact.EmailAddress, transactionNotifyRequest.BusinessId, transactionNotifyRequest.ProgramId, transactionNotifyRequest.ApplicationID, transactionNotifyRequest.ContactID, userSessionEntity);
                    validationMessages.Add("Notification activated successfully!");
                }
                //}
                commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
            }
            catch (BusinessException ex)
            {
                _Logger.LogDebug("Error at AdminService -> SavePaymentSummaryTransactionNotify " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in AdminService-> SavePaymentSummaryTransactionNotify ", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            catch (Exception ex)
            {
                _Logger.LogDebug("Error at AdminService -> SavePaymentSummaryTransactionNotify " + ex.InnerException == null
             ? ex.Message : ex.Message + " --> " + ex.InnerException.GetFullMessage());

                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in AdminService-> SavePaymentSummaryTransactionNotify", null);
                commonResponse = new CommonResponse(
                       ResponseStatus.Fail, MessageConstants.EditFailure, null);
            }
            return commonResponse;
        }

        private CommonResponse SendNotifyPaymentSummaryTransaction(string emailId, long businessID, long programInvitationID, long loanApplicationID, long contactID, UserSessionEntity userSessionEntity)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse commonResponse = null;
            try
            {
                string tokenID = "";
                string callBack = "";
                string callBackURL = "";
                
                User user = _userRepository.GetActiveByUserName(emailId);
                if (user != null && emailId == user.UserName)
                {
                    var processInstance = WorkflowInit.GetProcessInstanceByProcessID(loanApplicationID);
                    tokenID = CommonUtility.CreateUniqueID("");
                    callBack = _appSettings.BaseUrl + "/form/0/" + processInstance.ProcessId;
                    userSessionEntity.UserID = user.UserID;
                    callBackURL = "<tr> <td style='padding: 20px 0 20px 100px;'> <table class='buttonwrapper' border='0' cellspacing='3' cellpadding='0'> <tr> <td style='font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;' class='btn-primary'> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> </td> </tr> </table> </td> </tr> ";
                    //callBackURL = "<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " + callBack + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> <br /><br /> </td>";
                    var email = _notificationService.SendSPAEmail("Notify Payment Summary Transaction", callBackURL, (long)EmailTemplateNameID.REMINDERPENDINGDISBURSMENT, programInvitationID, contactID, loanApplicationID, null, userSessionEntity);
                    validationMessages.Add("Notify Payment Summary Transaction");
                    commonResponse = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, validationMessages);
                    commonResponse.StatusMessage = "Notify Payment Summary Transaction mail sent successfully.";
                    commonResponse.ID = 0;//user.ContactID;
                }
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

        public PaymentScheduleSummaryResponse GetPaymentScheduleSummaryData(long businessID, long applicationId)
        {
            var response = new PaymentScheduleSummaryResponse();
            var summary = new PaymentScheduleSummaryDetails();

            try
            {
                // parameter validation

                if (businessID <= 0 || applicationId <= 0)
                {
                    response.IsSuccess = false;
                    response.IsValidationError = true;
                    response.ValidationError = new ValidationErrorResponse();
                    response.ValidationError.IsValidationError = true;
                    ValidationError validationError = new ValidationError();
                    validationError.FieldName = "businessID/programID";
                    validationError.ErrorMessage = "Please provide valid business ID & program ID";
                    response.ValidationError.ValidationError = new List<ValidationError>();
                    response.ValidationError.ValidationError.Add(validationError);
                }
                else
                {
                   
                    var lstsummary = _fundingSourceRepository.GetPaymentScheduleSummary(businessID, applicationId);
                    if (lstsummary != null)
                    {
                        summary.FundDisbursedAmount = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().FundDisbursedAmount.ToString();
                        summary.FundPendingAmount = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().FundPendingAmount.ToString();
                        summary.AdditionalNotesAgreement = lstsummary.OrderByDescending(x => x.ID).FirstOrDefault().AdditionalNotesAgreement;
                    }

                    response.paymentScheduleSummary = summary;
                    response.IsSuccess = true;
                    response.IsValidationError = false;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetPaymentScheduleSummary ", null);
                response.IsSuccess = false;
                response.IsValidationError = false;
                response.Message = "Failed to fetch to get payment schedule summary.";
            }
            return response;
        }

        
        private CommonResponse SaveLoanPaymentType(FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam, UserSessionEntity userSessionEntity)
        {
            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;                 

            try
            {                
                LoanApplication loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(fundBulkPaymentScheduleParam.LoanApplicationID);
                if (loanApplication != null && loanApplication.FundingApplication != null && loanApplication.FundingApplication.IsPaymentSchedule !=true)
                {
                    FundingApplication fundingApplication = loanApplication.FundingApplication;
                    fundingApplication.IsPaymentSchedule = true;
                    this._fundingApplicationRepository.SaveOrUpdateLoanFundingApplication(fundingApplication, userSessionEntity.UserID);
                    commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                    commonCreationParam.ID = fundBulkPaymentScheduleParam.LoanApplicationID;
                }


            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }
            return commonCreationParam;
        }

        public MasterOptionResponse GetPaymentScheduleLoanNoList(long businessID)
        {
            var masterOptionResponse = new MasterOptionResponse();
            try
            {               
                var applicationList = this._LoanApplicationRepository.GetAll().Where(x => x.IsActive == true && x.ProgramInvitation.BusinessID == businessID && x.FundingApplication.IsPaymentSchedule == true)

                    .Select(x => new MasterOptionDetail
                    {
                        Key = x.LoanApplicationID.ToString(),
                        Values = x.LoanNumber != null ? x.LoanNumber : string.Empty,
                    }).Distinct().OrderBy(x => x.Values).ToList();

                masterOptionResponse.IsSuccess = true;
                masterOptionResponse.IsValidationError = false;
                masterOptionResponse.Message = "Success";
                masterOptionResponse.MasterOptionDetails = applicationList;


            }
            catch (Exception ex)
            {
                masterOptionResponse.IsSuccess = false;
                masterOptionResponse.IsValidationError = true;
                masterOptionResponse.Message = "failed to get data.";
            }
            return masterOptionResponse;
        }

        public bool IsPaymentScheduleExist(long businessID)
        {
            try
            {
                return _fundingSourceRepository.IsPaymentScheduleExist(businessID);
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getTotalFundedAmount", null);
            }
            return false;
        }

        private CommonResponse ResetIsPaymentSchedule(long applicationId, UserSessionEntity userSessionEntity)
        {
            var currentDate = DateTime.Now;
            CommonResponse commonCreationParam = null;

            try
            {
                LoanApplication loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(applicationId);
                if (loanApplication != null && loanApplication.FundingApplication != null)
                {
                    FundingApplication fundingApplication = loanApplication.FundingApplication;
                    fundingApplication.IsPaymentSchedule = false;
                    this._fundingApplicationRepository.SaveOrUpdateLoanFundingApplication(fundingApplication, userSessionEntity.UserID);
                    commonCreationParam = new CommonResponse(ResponseStatus.Success, MessageConstants.Success, null);
                    commonCreationParam.ID = applicationId;
                }


            }
            catch (Exception Exception)
            {
                LoggerExtensions.LogInformation(_Logger, null, Exception, "Exception in ApplicationServiceImpl-> SaveBorrowerConsent ", null);

                commonCreationParam = new CommonResponse(
                       ResponseStatus.Fail, "Error while saving borrower consent", null);
            }
            return commonCreationParam;
        }
        public decimal GetAvailableLimit(long programId)
        {
            decimal availableLimit = 0;
            List<FundingSource> fundingSources = this._fundingSourceRepository.GetAll().Where(c => c.FundingSourceID == programId && c.IsActive == true).ToList();
            if (fundingSources != null && fundingSources.Count > 0)
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
        #endregion Methods



    }
}