using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Service.Impl;
using System.Collections.Generic;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FundingSourceController : ControllerBase
    {
        #region Fields

        public ILogger<FundingSourceController> _logger;
        private IFundingSourceService _fundingSourceService;
        private IDocumentService _documentService;
        
        #endregion Fields
        #region Constructors
        public FundingSourceController(IFundingSourceService fundingSourceService,
         ILogger<FundingSourceController> logger, IDocumentService documentService)
        {
            _logger = logger;
            this._fundingSourceService = fundingSourceService;
            this._documentService= documentService;
        }
        #endregion Constructors

        #region Methods


        //[TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllUtilizedAmountDetails")]
        public FundingSourceResponse GetAllUtilizedAmountDetails(long fundingSourceID)
        {
            FundingSourceResponse fundingSourceResponse = new FundingSourceResponse();

            fundingSourceResponse = this._fundingSourceService.GetFundUtilization(fundingSourceID);

            if (fundingSourceResponse != null && fundingSourceResponse.IsSuccess)
            {
                fundingSourceResponse.IsSuccess = true;
                return fundingSourceResponse;
            }
            else
            {
                _logger.LogInformation("returned fundingSourceResponse is incorrect {0}", fundingSourceResponse);
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("AddFundTransaction")]
        public BaseResponse AddFundTransaction(FundTransactionParam fundTransactionParam)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                commonCreationParam = this._fundingSourceService.AddFundTransaction(fundTransactionParam, userSession);
                if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
                {
                    baseResponse.IsSuccess = true;
                    baseResponse.Message = "Fund Added Successfully.";
                    baseResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return baseResponse;
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Failed to Add Fund.";
                    baseResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult AddFundTransaction(FundTransactionParam fundTransactionParam) >> ", ex);

                baseResponse.IsSuccess = false;
                baseResponse.Message = "Exception occurred while adding Fund.";
                return baseResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("RemoveFund")]
        public BaseResponse RemoveFund(FundTransactionParam fundTransactionParam)
        {
            BaseResponse baseResponse = new BaseResponse();
            baseResponse.IsSuccess = false;
            try
            {
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                commonCreationParam = this._fundingSourceService.RemoveFundTransaction(fundTransactionParam, userSession);
                if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
                {
                    baseResponse.IsSuccess = true;
                    baseResponse.Message = "Fund Removed successfully.";
                    baseResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return baseResponse;
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Failed to Remove Fund.";
                    baseResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at FundingSourcecontroller-->RemoveFund mothod >> ", ex.InnerException.ToString());
                baseResponse.Message = "Exception occurred while removing Fund.";
                return baseResponse;

            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("AddOrUpdateFundingEntity")]
        public FundingEntityDataResponse AddOrUpdateFundingEntity(FundingEntityRequest fundingEntityRequest)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
            FundingEntityDataResponse fundingEntityDataResponse = new FundingEntityDataResponse();
            try
            {
                CommonResponse commonCreationParam = null;

                commonCreationParam = this._fundingSourceService.CreateOrUpdateFundingEntity(fundingEntityRequest, userSession);
                if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
                {

                    fundingEntityDataResponse.IsSuccess = true;
                    fundingEntityDataResponse.Message = "Funding Entity saved successfully.";
                    fundingEntityDataResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return fundingEntityDataResponse;
                }
                else
                {
                    fundingEntityDataResponse.IsSuccess = false;
                    fundingEntityDataResponse.Message = "Failed to save Funding Entity.";
                    fundingEntityDataResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return fundingEntityDataResponse;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at FundingEntityDataResponse AddFundingEntity(FundingEntityParam fundingEntityParam) >> ", ex);

                fundingEntityDataResponse.IsSuccess = false;
                fundingEntityDataResponse.Message = "Exception occurred while saving fundingEntity.";
                return fundingEntityDataResponse;
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetFundTransaction")]
        public ActionResult<FundTransactionResponse> GetFundTransaction(long FundingSourceID)
        {

            FundTransactionResponse FundTransactionResponse = new FundTransactionResponse();
            try
            {
                FundTransactionResponse = _fundingSourceService.GetFundTransaction(FundingSourceID);
                if (FundTransactionResponse.IsValidationError)
                {
                    return BadRequest(FundTransactionResponse.ValidationError);
                }
                return Ok(FundTransactionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch the fund transactions.");
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("AddFundingSource")]
        public FundingSourceResponse AddFundingSource(FundingSourceParam fundingSourceEntity)
        {
            FundingSourceResponse fundingEntityDataResponse = new FundingSourceResponse();
            try
            {
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);


                commonCreationParam = this._fundingSourceService.CreateFundingSource(fundingSourceEntity, userSession);
                if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
                {

                    fundingEntityDataResponse.IsSuccess = true;
                    fundingEntityDataResponse.Message = "Funding source saved successfully.";
                    fundingEntityDataResponse.ID = commonCreationParam.ID;
                    return fundingEntityDataResponse;
                }
                else
                {
                    fundingEntityDataResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    fundingEntityDataResponse.IsSuccess = false;
                    fundingEntityDataResponse.Message = "Failed to save funding source.";
                    return fundingEntityDataResponse;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at FundingEntityDataResponse AddFundingEntity(FundingEntityParam fundingEntityParam) >> ", ex);

                fundingEntityDataResponse.IsSuccess = false;
                fundingEntityDataResponse.Message = "Exception occurred while saving funding Entity.";
                return fundingEntityDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("FetchFundingEntities")]
        public ViewModels.FundingEntityResponse FetchFundingEntities(PagingFilterModel filterRequest)
        {
            ViewModels.FundingEntityResponse fundingEntityResponse = new ViewModels.FundingEntityResponse();
            fundingEntityResponse.IsSuccess = false;
            fundingEntityResponse.recordsTotal = 0;
            fundingEntityResponse.recordsFiltered = 0;
            fundingEntityResponse.Start = 0;
            fundingEntityResponse.Length = 0;
            fundingEntityResponse.data = null;

            #region InputValidation
            if (filterRequest == null)
            {
                _logger.LogError("Input Parameter PagingFilterModel is null");
                fundingEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingEntityResponse;
            }

            #endregion InputValidation

            Domain.Common.PageFilterEntity PageFilter = new Domain.Common.PageFilterEntity();
            PageFilter.PageNumber = filterRequest.Start;
            PageFilter.TakeRecordCount = filterRequest.Length;
            PageFilter.IsColumnFilter = filterRequest.IsColumnFilter;
            PageFilter.SortDirection = filterRequest.SortDesc == true ? "descending" : "ascending";
            PageFilter.SortBy = filterRequest.SortBy;

            PageFilter.FilterParameters = filterRequest.FilterParameters;

            FundingEntityListResponse response = this._fundingSourceService.GetAllFundingEntitiesInformation(PageFilter, LoginUserInformation.getLoggedInUser(HttpContext));

            if (response != null && response.IsSuccess)
            {
                fundingEntityResponse.data = response.FundingEntityPageResultEntity.DataList;
                fundingEntityResponse.recordsTotal = response.FundingEntityPageResultEntity.TotalRecordCount;
                fundingEntityResponse.recordsFiltered = response.FundingEntityPageResultEntity.FilteredRecord;
                fundingEntityResponse.IsSuccess = true;
                return fundingEntityResponse;
            }
            else
            {
                _logger.LogError("returned FundingEntityResponse  is incorrect {0}", response);
                fundingEntityResponse.IsSuccess = false;
                return fundingEntityResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetFundingEntityDetails")]
        public ViewModels.FundingEntityDataResponse GetFundingEntityDetails(long fundingEntityID)
        {
            ViewModels.FundingEntityDataResponse fundingEntityResponse = new ViewModels.FundingEntityDataResponse();
            ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
            var response = this._fundingSourceService.GetFundingEntity(fundingEntityID, userSession);

            if (response != null && response.IsSuccess)
            {
                fundingEntityResponse.IsSuccess = true;
                fundingEntityResponse.FundingEntity = response.FundingEntityViewEntity;
                return fundingEntityResponse;
            }
            else
            {
                _logger.LogInformation("returned fundingEntityResponse is incorrect {0}", fundingEntityResponse);
                fundingEntityResponse.IsSuccess = false;
                fundingEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingEntityResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetFundingSourceDetails")]
        public FundingSourceResponse GetFundingSourceDetails(long fundingSourceID)
        {
            FundingSourceResponse fundingSourceResponse = new FundingSourceResponse();

            var response = this._fundingSourceService.GetFundingSource(fundingSourceID);

            if (response != null && response.IsSuccess)
            {
                fundingSourceResponse.IsSuccess = true;
                fundingSourceResponse.FundingSource = response.FundingSource;
                return fundingSourceResponse;
            }
            else
            {
                _logger.LogInformation("returned fundingEntityResponse is incorrect {0}", fundingSourceResponse);
                fundingSourceResponse.IsSuccess = false;
                fundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return fundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateProgramDocument")]
        public ProgramDocumentResponse SaveOrUpdateProgramDocument(ProgramDocumentRequest programDocumentRequest)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            ProgramDocumentResponse programDocumentResponse = new ProgramDocumentResponse();
            CommonResponse commonProgramDocumentResponse = null;

            if (userSessionEntity == null)
            {
                programDocumentResponse.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonProgramDocumentResponse = this._fundingSourceService.SaveOrUpdateProgramDocument(programDocumentRequest, userSessionEntity);
                    if (commonProgramDocumentResponse != null && commonProgramDocumentResponse.ResponseStatus == ResponseStatus.Success)
                    {

                        programDocumentResponse.IsSuccess = true;
                        programDocumentResponse.Message = commonProgramDocumentResponse.StatusMessage;
                        return programDocumentResponse;
                    }
                    else
                    {
                        programDocumentResponse.IsSuccess = false;
                        programDocumentResponse.Message = "Failed to save program document.";
                        return programDocumentResponse;

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at ProgramDocumentResponse AddProgramDocuments(programDocuments programDocuments) >> ", ex);

                    programDocumentResponse.IsSuccess = false;
                    programDocumentResponse.Message = "Exception occurred while saving program document.";
                    return programDocumentResponse;
                }
            }
            return programDocumentResponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetProgramDocuments")]
        public ProgramDocumentResponse GetProgramDocuments(long programID)
        {
            ProgramDocumentResponse programDocumentResponse = new ProgramDocumentResponse();
            try
            {
                programDocumentResponse = this._fundingSourceService.GetProgramDocuments(programID);

                if (programDocumentResponse.ProgramDocumentsResponse != null && programDocumentResponse.IsSuccess)
                {
                    programDocumentResponse.IsSuccess = true;
                    return programDocumentResponse;
                }
                else
                {
                    programDocumentResponse.IsSuccess = false;
                    programDocumentResponse.Message = "Couldn't find the fund document for this program.";
                    return programDocumentResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ProgramDocumentResponse returned is incorrect {0}>> ", ex);
                programDocumentResponse.IsSuccess = false;
                programDocumentResponse.Message = "Exception occurred while fetching the program documents.";
                return programDocumentResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateProgramQuestions")]
        public ProgramQuestionsResponse SaveOrUpdateProgramQuestions(ProgramQuestionRequest programQuestionRequest)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
            ProgramQuestionsResponse programQuestionsResponse = new ProgramQuestionsResponse();
            CommonResponse commonCreationParam = null;
            if (userSession == null)
            {
                programQuestionsResponse.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonCreationParam = this._fundingSourceService.SaveOrUpdateProgramQuestions(programQuestionRequest, userSession);
                    if (commonCreationParam != null && commonCreationParam.ResponseStatus == ResponseStatus.Success)
                    {
                        programQuestionsResponse.IsSuccess = true;
                        programQuestionsResponse.Message = commonCreationParam.StatusMessage;
                    }
                    else
                    {
                        programQuestionsResponse.IsSuccess = false;
                        programQuestionsResponse.Message = "Failed to save questions";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at ProgramQuestionsResponse AddQuestionsResponse(ProgramQuestionRequest programQuestionRequest) >> ", ex);
                    programQuestionsResponse.IsSuccess = false;
                    programQuestionsResponse.Message = "Exception occurred while saving program questions.";
                }
            }
            return programQuestionsResponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetProgramQuestions")]
        public ProgramQuestionsResponse GetProgramQuestions(long programID)
        {
            ProgramQuestionsResponse programQuestionsResponse = new ProgramQuestionsResponse();
            try
            {
                programQuestionsResponse = this._fundingSourceService.GetProgramQuestions(programID);

                if (programQuestionsResponse.ProgramQuestionResponse != null && programQuestionsResponse.IsSuccess)
                {
                    programQuestionsResponse.IsSuccess = true;
                    return programQuestionsResponse;
                }
                else
                {
                    programQuestionsResponse.IsSuccess = false;
                    programQuestionsResponse.Message = "Couldn't find the fund question for this program.";
                    return programQuestionsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ProgramDocumentResponse returned is incorrect {0}>> ", ex);
                programQuestionsResponse.IsSuccess = false;
                programQuestionsResponse.Message = "Exception occurred while fetching the Program Questions.";
                return programQuestionsResponse;
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateHelpfulGuideTemplate")]
        public HelpfulGuideTextResponse SaveOrUpdateHelpfulGuideTemplate(HelpfulGuideTextRequest helpfulGuideTextRequest)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            HelpfulGuideTextResponse helpfulGuideTextResponse = new HelpfulGuideTextResponse();
            CommonResponse commonProgramDocumentResponse = null;
            if (userSessionEntity == null)
            {
                helpfulGuideTextResponse.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonProgramDocumentResponse = this._fundingSourceService.SaveOrUpdateHelpfulGuideTemplate(helpfulGuideTextRequest, userSessionEntity);
                    if (commonProgramDocumentResponse.ResponseStatus == ResponseStatus.Success)
                    {

                        helpfulGuideTextResponse.IsSuccess = true;  
                        helpfulGuideTextResponse.Message = commonProgramDocumentResponse.StatusMessage;
                    }
                    else
                    {
                        helpfulGuideTextResponse.IsSuccess = false;
                        helpfulGuideTextResponse.Message = "Failed to save Helpful Guide text.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at HelpfulGuideTextResponse AddHelpfulGuideTextResponse(HelpfulGuideTextResponse HelpfulGuideTextResponse) >> ", ex);
                    helpfulGuideTextResponse.IsSuccess = false;
                    helpfulGuideTextResponse.Message = "Exception occurred while saving program document.";
                }
            }
            return helpfulGuideTextResponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetHelpfulGuideTemplate")]
        public HelpfulGuideTextResponse GetHelpfulGuideTemplate(long programID)
        {
            HelpfulGuideTextResponse programQuestionsResponse = new HelpfulGuideTextResponse();
            try
            {
                programQuestionsResponse = this._fundingSourceService.GetHelpfulGuideTemplate(programID);

                if (programQuestionsResponse.HelpfulGuideTextViewResponse != null && programQuestionsResponse.IsSuccess)
                {
                    programQuestionsResponse.IsSuccess = true;
                    return programQuestionsResponse;
                }
                else
                {
                    programQuestionsResponse.IsSuccess = false;
                    programQuestionsResponse.Message = "Couldn't find the Helpful Guide text for this program.";
                    return programQuestionsResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at HelpfulGuideTextResponse returned is incorrect {0}>> ", ex);
                programQuestionsResponse.IsSuccess = false;
                programQuestionsResponse.Message = "Couldn't find the Helpful Guide text for this program.";
                return programQuestionsResponse;
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetProgramInvitationEmail")]
        public IActionResult GetProgramInvitationEmail(long programId)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            var programInvitationEmailResponse = new ProgramInvitationEmailResponse();
            if (userSessionEntity == null)
            {
                programInvitationEmailResponse.IsSuccess = false;
                return Ok(programInvitationEmailResponse);
            }
            programInvitationEmailResponse = this._fundingSourceService.GetProgramInvitationEmail(programId);
            
            return Ok(programInvitationEmailResponse);
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateProgramInvitationEmail")]
        public IActionResult SaveOrUpdateProgramInvitationEmail(ProgramInvitationEmailRequest programInvitationEmailRequest)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            ProgramInvitationEmailSaveOrUpdateResponse programInvitationEmailSaveOrUpdate = new ProgramInvitationEmailSaveOrUpdateResponse();
            CommonResponse commonProgramDocumentResponse = null;
            if (userSessionEntity == null)
            {
                programInvitationEmailSaveOrUpdate.IsSuccess = false;
                return Ok(programInvitationEmailSaveOrUpdate);
            }

            try
            {
                commonProgramDocumentResponse = this._fundingSourceService.SaveOrUpdateProgramInvitationEmail(programInvitationEmailRequest, userSessionEntity);
                if (commonProgramDocumentResponse.ResponseStatus == ResponseStatus.Success)
                {

                    programInvitationEmailSaveOrUpdate.IsSuccess = true;
                    programInvitationEmailSaveOrUpdate.Message = commonProgramDocumentResponse.StatusMessage;
                }
                else
                {
                    programInvitationEmailSaveOrUpdate.IsSuccess = false;
                    programInvitationEmailSaveOrUpdate.Message = "Failed to save Helpful Guide text.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at programInvitationEmailSaveOrUpdate AddHelpfulGuideTextResponse(programInvitationEmailSaveOrUpdate programInvitationEmailSaveOrUpdate) >> ", ex);
                programInvitationEmailSaveOrUpdate.IsSuccess = false;
                programInvitationEmailSaveOrUpdate.Message = "Exception occurred while saving program document.";
            }
            return Ok(programInvitationEmailSaveOrUpdate);
        }
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllPrograms")]
        public IActionResult GetAllPrograms()
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            var programInvitationResponse = new AllProgramInvitationResponse();
            try
            {
                if (userSessionEntity == null)
                {
                    programInvitationResponse.IsSuccess = false;
                }
                var Programs = this._fundingSourceService.GetAllProgramInvitations();
                if (Programs != null && Programs.Count > 0)
                {
                    programInvitationResponse.IsSuccess = true;
                    programInvitationResponse.Programs = Programs;
                    return Ok(programInvitationResponse);
                }
                else
                {
                    programInvitationResponse.IsSuccess = false;
                    programInvitationResponse.Message = "Couldn't find the Program Invitation text.";
                }

                return Ok(programInvitationResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ProgramInvitationResponse returned is incorrect {0}>> ", ex);
                programInvitationResponse.IsSuccess = false;
                programInvitationResponse.Message = "Couldn't find the Program Invitation text.";
                return Ok(programInvitationResponse);
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdatePaymentScheduleTransaction")]
        public PaymentScheduleTransResponse SaveOrUpdatePaymentScheduleTransaction(PaymentScheduleTransParam paymentScheduleTransParam)
        {
            //BaseResponse baseResponse = new BaseResponse();
            var baseResponse = new PaymentScheduleTransResponse();
            try
            {
               
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                baseResponse = this._fundingSourceService.AddPaymentScheduleTransaction(paymentScheduleTransParam, userSession);
                if (baseResponse.commonResponse.ResponseStatus == ResponseStatus.Success)
                {
                    baseResponse.IsSuccess = true;
                    if (paymentScheduleTransParam.PaymentScheduleID > 0)
                    {
                        baseResponse.Message = "Payment Schedule Updated Successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Payment Schedule Added Successfully.";
                    }

                    baseResponse.ValidationErrors = baseResponse.commonResponse.ValidationErrors;
                    return baseResponse;
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Failed to Add Payment Schedule.";
                    baseResponse.ValidationErrors = baseResponse.commonResponse.ValidationErrors;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult AddPaymentScheduleTransaction(paymentScheduleTransParam, userSession) >> ", ex);

                baseResponse.IsSuccess = false;
                baseResponse.Message = "Exception occurred while adding Payment Schedule.";
                return baseResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("RemovePaymentScheduleTransaction")]
        public PaymentScheduleTransResponse RemovePaymentScheduleTransaction(PaymentScheduleTransParam paymentScheduleTransParam)
        {
            var baseResponse = new PaymentScheduleTransResponse();
            try
            {
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                baseResponse = this._fundingSourceService.RemovePaymentScheduleTransaction(paymentScheduleTransParam, userSession);
                if (baseResponse.commonResponse.ResponseStatus == ResponseStatus.Success)
                {
                    baseResponse.IsSuccess = true;
                    if (paymentScheduleTransParam.PaymentScheduleID > 0)
                    {
                        baseResponse.Message = "Payment Schedule deleted Successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Payment Schedule deleted Successfully.";
                    }

                    baseResponse.ValidationErrors = baseResponse.commonResponse.ValidationErrors;
                    return baseResponse;
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Failed to delete Payment Schedule.";
                    baseResponse.ValidationErrors = baseResponse.commonResponse.ValidationErrors;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult RemovePaymentScheduleTransaction(paymentScheduleTransParam, userSession) >> ", ex);

                baseResponse.IsSuccess = false;
                baseResponse.Message = "Exception occurred while deleting Payment Schedule.";
                return baseResponse;
            }
        }
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("DeleteAllPaymentScheduleTransactionByLoan")]
        public PaymentScheduleTransResponse DeleteAllPaymentScheduleTransactionByLoan(PaymentScheduleTransParam paymentScheduleTransParam)
        {
            var baseResponse = new PaymentScheduleTransResponse();
            try
            {
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                baseResponse = this._fundingSourceService.DeleteAllPaymentScheduleTransactionByLoan(paymentScheduleTransParam, userSession);
                if (baseResponse.commonResponse.ResponseStatus == ResponseStatus.Success)
                {
                    baseResponse.IsSuccess = true;
                    if (paymentScheduleTransParam.PaymentScheduleID > 0)
                    {
                        baseResponse.Message = "Payment Schedule deleted Successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Payment Schedule deleted Successfully.";
                    }

                    baseResponse.ValidationErrors = baseResponse.commonResponse.ValidationErrors;
                    return baseResponse;
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Failed to delete Payment Schedule.";
                    baseResponse.ValidationErrors = baseResponse.commonResponse.ValidationErrors;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult RemovePaymentScheduleTransaction(paymentScheduleTransParam, userSession) >> ", ex);

                baseResponse.IsSuccess = false;
                baseResponse.Message = "Exception occurred while deleting Payment Schedule.";
                return baseResponse;
            }
        }
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetPaymentScheduleTransaction")]
        public ActionResult<PaymentScheduleTransactionResponse> GetPaymentScheduleTransaction(long businessID, long applicationID)
        {

            PaymentScheduleTransactionResponse transactionResponse = new PaymentScheduleTransactionResponse();
            try
            {
                transactionResponse = _fundingSourceService.GetPaymentScheduleTransaction(businessID, applicationID);
                if (transactionResponse.IsValidationError)
                {
                    return BadRequest(transactionResponse.ValidationError);
                }
                return Ok(transactionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch the Payment schedule transactions.");
            }
        }
        [HttpGet]
        [Route("GetProgramList")]
        public IActionResult GetProgramList(long businessID)
        {
            try
            {
                var masterOptionResponse = this._fundingSourceService.GetProgramList(businessID);

                return Ok(masterOptionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Failed to fetch Master Option Data");
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetPaymentScheduleSummary")]
        public ActionResult<PaymentScheduleSummaryResponse> GetPaymentScheduleSummary(long businessID, long applicationId)
        {

            var response = new PaymentScheduleSummaryResponse();
            try
            {
                response = _fundingSourceService.GetPaymentScheduleSummary(businessID, applicationId);
                if (response.IsValidationError)
                {
                    return BadRequest(response.ValidationError);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch the Payment schedule transactions.");
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("UploadFundAgreementDocument")]
        public async Task<DocumentResponse> UploadFundAgreementDocument(IFormFile file)
        {
            ApplicationDocumentEntity applicationDocumentEntity = new ApplicationDocumentEntity();
            DocumentResponse documentUploadResponse = new DocumentResponse();
            FileEntity fileEntity = new FileEntity();


            #region InputValidation

            #region FileInputStreamValidation
            if (file == null)
            {
                _logger.LogError("HttpRequest is null.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "Aggreement File Upload failed.";
                return documentUploadResponse;
            }
            else if (file.OpenReadStream() == null)
            {
                _logger.LogError("Input Stream is missing - httpRequest.Files['file'].InputStream is null.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "File format is not correct.";
                return documentUploadResponse;
            }
            else if (String.IsNullOrEmpty(file.FileName))
            {
                _logger.LogError("FileName is missing fileName is Empty.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "No File found.";
                return documentUploadResponse;
            }
            #endregion


            #region DocumentTypeValidation

            if (file.Length == 0)
            {
                _logger.LogError("ContentLength is Empty.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "Unable to upload file at this moment. Try after sometime.";
                return documentUploadResponse;
            }
            #endregion

            #region FileSizeValidation
            #endregion

            #endregion

            try
            {
                if (file != null)
                {
                    fileEntity.FileName = file.FileName;
                    fileEntity.ContentType = file.ContentType;
                    fileEntity.ContentLength = file.Length;
                    fileEntity.InputStream = file.OpenReadStream();
                    fileEntity.FileSize = file.Length;
                    applicationDocumentEntity.FileEntity = fileEntity;
                }
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                applicationDocumentEntity.UserID = userSession.UserID;
                applicationDocumentEntity.DocumentTypeID = Convert.ToInt32(DocumentTypeEnumeration.FundAgreementDocumentType);

                documentUploadResponse = await this._documentService.UploadDocument(applicationDocumentEntity);
                return documentUploadResponse;

            }
            catch (Exception)
            {
                return new DocumentResponse
                {
                    IsSuccess = false,
                    Message = "Unable to upload file at this moment. Try after sometime."
                };
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveorUpdatePaymentSchedule")]
        public BaseResponse SaveorUpdatePaymentSchedule(FundPaymentScheduleParam fundPaymentScheduleParam)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam = null;
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                commonCreationParam = this._fundingSourceService.SaveorUpdatePaymentScheduleAndDocument(fundPaymentScheduleParam, userSession,  fundBulkPaymentScheduleParam);
                if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
                {
                    baseResponse.IsSuccess = true;
                    if (fundPaymentScheduleParam.FundPaymentScheduleID > 0)
                    {
                        baseResponse.Message = "Payment Schedule Updated Successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Payment Schedule Saved Successfully.";
                    }

                    baseResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return baseResponse;
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Failed to Saved Payment Schedule.";
                    baseResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult AddorUpdatePaymentSchedule(fundPaymentScheduleParam, userSession) >> ", ex);

                baseResponse.IsSuccess = false;
                baseResponse.Message = "Exception occurred while Saving Payment Schedule.";
                return baseResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetPaymentScheduleTransactionById")]
        public ActionResult<PaymentScheduleTransactionResponse> GetPaymentScheduleTransactionById(long paymentScheduleID)
        {

            PaymentScheduleItemResponse transactionResponse = new PaymentScheduleItemResponse();
            try
            {
                transactionResponse = _fundingSourceService.GetPaymentScheduleTransactionById(paymentScheduleID);
                if (transactionResponse.IsValidationError)
                {
                    return BadRequest(transactionResponse.ValidationError);
                }
                return Ok(transactionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Unable to fetch the Payment schedule transactions.");
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveorUpdatePaymentScheduleAndTransaction")]
        public PaymentScheduleTransResponse SaveorUpdatePaymentScheduleAndTransaction(FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam)
        {
            var paymentScheduleTransResponse = new PaymentScheduleTransResponse();
            //BaseResponse baseResponse = new BaseResponse();
            try
            {
                CommonResponse commonCreationParam = null;
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
                decimal availableLimit = 0;
                availableLimit = fundBulkPaymentScheduleParam != null ? this._fundingSourceService.GetAvailableLimit(fundBulkPaymentScheduleParam.ProgramID) : 0;
                if (availableLimit <= 0 || ( fundBulkPaymentScheduleParam!=null && fundBulkPaymentScheduleParam.FundAllocatedAmount > availableLimit))
                {
                    paymentScheduleTransResponse.Message = "Please add fund to the Program to proceed with payment schedule.";
                    paymentScheduleTransResponse.ID = fundBulkPaymentScheduleParam.LoanApplicationID;
                    paymentScheduleTransResponse.IsSuccess = false;
                    paymentScheduleTransResponse.IsAvailableLimitExceeds = true;
                    return paymentScheduleTransResponse;
                   
                }


                //Transaction
                ///decimal totalPendingAmount = 0;
                var paymentScheduleTransParam = new PaymentScheduleTransParam();
                if (fundBulkPaymentScheduleParam != null && fundBulkPaymentScheduleParam.PaymentScheduleTransParam != null)
                {
                    if (fundBulkPaymentScheduleParam.PaymentScheduleTransParam.Count > 0)
                    {

                        
                       

                        foreach (var transaction in fundBulkPaymentScheduleParam.PaymentScheduleTransParam)
                        {
                            paymentScheduleTransParam.PaymentScheduleID = Convert.ToInt64(transaction.PaymentScheduleID);
                            paymentScheduleTransParam.LoanApplicationID = transaction.LoanApplicationID;
                            paymentScheduleTransParam.BusinessID = transaction.BusinessID;
                            paymentScheduleTransParam.ProgramID = transaction.ProgramID;
                            paymentScheduleTransParam.TransactionDate = transaction.TransactionDate;
                            paymentScheduleTransParam.FundingTypeID = transaction.FundingTypeID;
                            paymentScheduleTransParam.FundedAmount = transaction.FundedAmount;
                            paymentScheduleTransParam.TransactionStatusID = transaction.TransactionStatusID;
                            paymentScheduleTransParam.ContactID = transaction.ContactID;
                            if (transaction.LoanApplicationID > 0 && transaction.ProgramID > 0 && transaction.BusinessID > 0)
                            {
                                //if (transaction.TransactionStatusID==1)
                                //{
                                //    totalPendingAmount = totalPendingAmount + transaction.FundedAmount;
                                //}
                                
                                paymentScheduleTransResponse = this._fundingSourceService.AddPaymentScheduleTransaction(paymentScheduleTransParam, userSession);
                            }

                        }
                    }
                }
                //Payment Schedule
                var fundPaymentScheduleParam = new FundPaymentScheduleParam();
                
                if (fundBulkPaymentScheduleParam != null && fundBulkPaymentScheduleParam.BusinessID>0 & fundBulkPaymentScheduleParam.ProgramID>0 && fundBulkPaymentScheduleParam.LoanApplicationID > 0)
                {//summary table & document table
                    fundPaymentScheduleParam.FundPaymentScheduleID = Convert.ToInt64(fundBulkPaymentScheduleParam.FundPaymentScheduleID);
                    fundPaymentScheduleParam.LoanApplicationID = fundBulkPaymentScheduleParam.LoanApplicationID;
                    fundPaymentScheduleParam.BusinessID = fundBulkPaymentScheduleParam.BusinessID;
                    fundPaymentScheduleParam.ProgramID = fundBulkPaymentScheduleParam.ProgramID;
                    fundPaymentScheduleParam.FundRequestedAmount = fundBulkPaymentScheduleParam.FundRequestedAmount;
                    fundPaymentScheduleParam.FundAllocatedAmount = fundBulkPaymentScheduleParam.FundAllocatedAmount;
                    fundPaymentScheduleParam.FundDisbursedAmount = fundBulkPaymentScheduleParam.FundDisbursedAmount;
                    fundPaymentScheduleParam.FundPendingAmount = fundBulkPaymentScheduleParam.FundPendingAmount; 
                    fundPaymentScheduleParam.ContactID = fundBulkPaymentScheduleParam.ContactID;
                    fundPaymentScheduleParam.AdditionalNotesAgreement = fundBulkPaymentScheduleParam.AdditionalNotesAgreement;

                    //Document
                    fundPaymentScheduleParam.DocumentID = Convert.ToInt64(fundBulkPaymentScheduleParam.DocumentID);
                    fundPaymentScheduleParam.LoanApplicationID = fundBulkPaymentScheduleParam.LoanApplicationID;
                    fundPaymentScheduleParam.BusinessID = fundBulkPaymentScheduleParam.BusinessID;
                    fundPaymentScheduleParam.ProgramID = fundBulkPaymentScheduleParam.ProgramID;
                    fundPaymentScheduleParam.DocumentGUID = fundBulkPaymentScheduleParam.DocumentGUID;
                    fundPaymentScheduleParam.DocumentTypeID = fundBulkPaymentScheduleParam.DocumentTypeID;
                    fundPaymentScheduleParam.DocumentName = fundBulkPaymentScheduleParam.DocumentName;
                    fundPaymentScheduleParam.FileName = fundBulkPaymentScheduleParam.FileName;
                    fundPaymentScheduleParam.PhysicalFileStorageKey = fundBulkPaymentScheduleParam.PhysicalFileStorageKey;
                    fundPaymentScheduleParam.FileSize = fundBulkPaymentScheduleParam.FileSize;
                    fundPaymentScheduleParam.FileUploadedSourceUrl = fundBulkPaymentScheduleParam.FileUploadedSourceUrl;

                    commonCreationParam = this._fundingSourceService.SaveorUpdatePaymentScheduleAndDocument(fundPaymentScheduleParam, userSession, fundBulkPaymentScheduleParam);

                    var result= _fundingSourceService.GetPaymentScheduleSummaryData(fundPaymentScheduleParam.BusinessID, fundPaymentScheduleParam.LoanApplicationID);
                    if (result != null)
                    {
                        paymentScheduleTransResponse.FundDisbursedAmount = result.paymentScheduleSummary.FundDisbursedAmount;
                        paymentScheduleTransResponse.FundPendingAmount = result.paymentScheduleSummary.FundPendingAmount;
                        paymentScheduleTransResponse.AdditionalNotesAgreement = result.paymentScheduleSummary.AdditionalNotesAgreement;
                    }
                    

                    if (commonCreationParam.ResponseStatus == ResponseStatus.Fail)
                    {
                        paymentScheduleTransResponse.IsSuccess = false;
                        paymentScheduleTransResponse.Message = "Failed to Saved Payment Schedule.";
                        paymentScheduleTransResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                        return paymentScheduleTransResponse;
                    }
                }
                else
                {
                    paymentScheduleTransResponse.IsSuccess = false;
                    paymentScheduleTransResponse.Message = "Something went wrong! Please check your inputs.";
                    
                    return paymentScheduleTransResponse;
                }

                
                    

                if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
                {
                    paymentScheduleTransResponse.IsSuccess = true;
                    if (fundPaymentScheduleParam.FundPaymentScheduleID > 0)
                    {
                        paymentScheduleTransResponse.Message = "Payment Schedule Updated Successfully.";
                    }
                    else
                    {
                        paymentScheduleTransResponse.Message = "Payment Schedule Saved Successfully.";
                    }
                    _fundingSourceService.SendPaymentScheduleEmailNotification(fundBulkPaymentScheduleParam);
                    paymentScheduleTransResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return paymentScheduleTransResponse;
                }
                else
                {
                    paymentScheduleTransResponse.IsSuccess = false;
                    paymentScheduleTransResponse.Message = "Failed to Saved Payment Schedule.";
                    paymentScheduleTransResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                    return paymentScheduleTransResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult SaveorUpdatePaymentScheduleAndTransaction(fundBulkPaymentScheduleParam, userSession) >> ", ex);

                paymentScheduleTransResponse.IsSuccess = false;
                paymentScheduleTransResponse.Message = "Exception occurred while Saving Payment Schedule & transaction.";
                return paymentScheduleTransResponse;
            }
        }

        //[TypeFilter(typeof(AuthorizeAttribute))] //need to apply once testing done
        [HttpPost]
        [Route("NotifyPaymentSummaryTransaction")]
        public ContactDataResponse NotifyPaymentSummaryTransaction(TransactionNotifyRequest transactionNotifyRequest)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();
            CommonResponse commonResponse = null;
            commonResponse = this._fundingSourceService.NotifyPaymentSummaryTransaction(transactionNotifyRequest, LoginUserInformation.getLoggedInUser(HttpContext));
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {

                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Notification activated successfully!";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Failed to activate Notify!";
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                return contactDataResponse;
            }
        }

        [HttpGet]
        [Route("GetPaymentScheduleLoanNoList")]
        public IActionResult GetPaymentScheduleLoanNoList(long businessID)
        {
            try
            {
                var masterOptionResponse = this._fundingSourceService.GetPaymentScheduleLoanNoList(businessID);

                return Ok(masterOptionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Failed to fetch Master Option Data");
            }

        }
        #endregion Methods
    }
}
