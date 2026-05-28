using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain;
using ThoughtFocus.Validations.InputParameterValidation.Application;
using System.Collections.Generic;
using FluentValidation;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.CustomView;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using ThoughtFocus.DataAccess.Models.Application;
using System.Linq;
using ThoughtFocus.Common.Utilities;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoanApplicationController : ControllerBase
    {
        #region Fields
        public ILogger<ContactController> _logger;
        private IApplicationService _applicationService;
        private ThoughtFocus.Repository.Interfaces.User.IUserRepository _userRepository;
        #endregion Fields

        #region Constructors
        public LoanApplicationController(IApplicationService applicationService,
         ILogger<ContactController> logger,
         ThoughtFocus.Repository.Interfaces.User.IUserRepository userRepository
         )
        {
            _logger = logger;
            this._applicationService = applicationService;
            this._userRepository = userRepository;
        }
        #endregion Constructors

        #region Methods
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveLoanApplication")]
        public LoanApplicationDataResponse SaveLoanApplication(LoanApplicationRequest loanApplicationRequest)
        {
            LoanApplicationDataResponse loanApplicationDataResponse = new LoanApplicationDataResponse();
            loanApplicationDataResponse.IsSuccess = false;
            CommonResponse commonCreationParam = null;
            commonCreationParam = _applicationService.SaveLoanApplication(loanApplicationRequest,
                                            LoginUserInformation.getLoggedInUser(HttpContext));

            if (commonCreationParam.ResponseStatus == ResponseStatus.Success)
            {
                loanApplicationDataResponse.IsSuccess = true;
                loanApplicationDataResponse.Message = "Grant Application saved successfully.";
            }
            else
            {
                loanApplicationDataResponse.ValidationErrors = commonCreationParam.ValidationErrors;
                loanApplicationDataResponse.Message = "Error occurred while saving Grant Application.";
            }

            return loanApplicationDataResponse;
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("FetchLoanApplications")]
        public ApplicationResponse FetchLoanApplications(PagingFilterModel filterRequest)
        {
            ApplicationResponse applicationResponse = new ApplicationResponse();
            applicationResponse.IsSuccess = false;
            applicationResponse.recordsTotal = 0;
            applicationResponse.recordsFiltered = 0;
            applicationResponse.Start = 0;
            applicationResponse.Length = 0;
            applicationResponse.data = null;

            #region InputValidation
            if (filterRequest == null)
            {
                _logger.LogError("Input Parameter PagingFilterModel is null");
                applicationResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return applicationResponse;
            }

            #endregion InputValidation

            Domain.Common.PageFilterEntity PageFilter = new Domain.Common.PageFilterEntity();
            PageFilter.PageNumber = filterRequest.Start;
            PageFilter.TakeRecordCount = filterRequest.Length;
            PageFilter.IsColumnFilter = filterRequest.IsColumnFilter;
            PageFilter.SortDirection = filterRequest.SortDesc == true ? "descending" : "ascending";
            PageFilter.SortBy = filterRequest.SortBy;

            PageFilter.FilterParameters = filterRequest.FilterParameters;

            ApplicationListResponse response = this._applicationService.GetAllLoanApplicationInformation(PageFilter, LoginUserInformation.getLoggedInUser(HttpContext));

            if (response != null && response.IsSuccess == false && response.ApplicationPageResultEntity !=null
                && response.ApplicationPageResultEntity.DataList == null)
            {
                applicationResponse.IsSuccess = false;
                applicationResponse.Message = "Couldn't find the Grant Application for this program.";
                applicationResponse.ProgramID = response.ID;
                return applicationResponse;

            }
            if (response != null && response.IsSuccess)
            {
                applicationResponse.data = response.ApplicationPageResultEntity.DataList;
                applicationResponse.recordsTotal = response.ApplicationPageResultEntity.TotalRecordCount;
                applicationResponse.recordsFiltered = response.ApplicationPageResultEntity.FilteredRecord;
                applicationResponse.IsSuccess = true;
                applicationResponse.ProgramID = response.ID;
                return applicationResponse;
            }
            else
            {
                _logger.LogError("returned ApplicationListResponse  is incorrect {0}", response);
                applicationResponse.IsSuccess = false;
                return applicationResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetWorkFlowCommands")]
        public WorkFlowCommandResponse GetWorkFlowCommands(int applicationID)
        {
            WorkFlowCommandResponse workFlowCommandResponse = new Domain.CustomView.WorkFlowCommandResponse();
            try
            {
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);

                workFlowCommandResponse = this._applicationService.GetWorkFlowCommands(applicationID, userSession);
                workFlowCommandResponse.IsSuccess = true;
                return workFlowCommandResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at workFlowCommandResponse GetWorkFlowCommands >> ", ex);
                workFlowCommandResponse.IsSuccess = false;
                workFlowCommandResponse.Message = "Exception occurred while getting Workflow Commands";
                return workFlowCommandResponse;
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("ApplicationCommandHandler")]
        public BaseResponse ApplicationCommandHandler(LoanApplicationRequest loanApplicationParam)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                #region Validation

                if (loanApplicationParam == null)
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = "Unable to draft grant application at this moment. Please try after sometime.";
                    return baseResponse;
                }
                #endregion

                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
               
                baseResponse = this._applicationService.ApplicationCommandHandler(loanApplicationParam, userSession);

                if (baseResponse.ValidationErrors != null && baseResponse.ValidationErrors.Count > 0)
                {
                    baseResponse.ValidationErrors = baseResponse.ValidationErrors;
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = baseResponse.Message;
                    baseResponse.ID = baseResponse.ID;
                    baseResponse.StackTrace = baseResponse.StackTrace;
                    baseResponse.InvalidInput= true;
                    return baseResponse;

                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at BaseResponse ApplicationCommandHandler >> ", ex);
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
                return baseResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetPrePopulatedApplicationData")]
        public ActionResult<ApplicationCreationPreRequiredData> GetPrePopulatedApplicationData(long ProgramInvitationID)
        {
            ApplicationCreationPreRequiredData AppCreationPreRequiredData = new ApplicationCreationPreRequiredData();
            var userId = LoginUserInformation.getLoggedInUser(HttpContext).UserID;

            var roles = _userRepository.GetByUserID(userId)?.UserRoles?.ToList();
            var programStatusId = this._applicationService.GetProgramStatusId(ProgramInvitationID);
            

            //Only borrower and business contacts allowed to see, initiated loan application no other users
            if (roles != null && roles.Count > 0 && roles[0].RoleID == 2 && programStatusId == 1)
            {
                var businessContact = _applicationService.GetBussinessUsers(ProgramInvitationID);
                int userIndex = 0;
                if (businessContact != null)
                {
                    userIndex = businessContact.FindIndex(f => f.ContactID == userId);
                }
                if (userIndex == -1)
                {
                    AppCreationPreRequiredData.IsSuccess = false;
                    AppCreationPreRequiredData.Message = "Grant application doesn't exist";
                    return AppCreationPreRequiredData;
                }
               
            }

            //Only borrower is allowed to see, initiated loan application no other users
            if (roles != null && roles.Count > 0 && roles[0].RoleID != 2 && programStatusId == 1)
            {
                AppCreationPreRequiredData.IsSuccess = false;
                AppCreationPreRequiredData.Message = "Grant application doesn't exist";
                return AppCreationPreRequiredData;
            }

            


            AppCreationPreRequiredData = _applicationService.GetPrePopulatedApplicationData(ProgramInvitationID);
            if (AppCreationPreRequiredData.IsSuccess)
            {
                return AppCreationPreRequiredData;
            }
            else
            {
                AppCreationPreRequiredData.IsSuccess = false;
                AppCreationPreRequiredData.Message = "Grant application doesn't exist";
                return AppCreationPreRequiredData;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("FetchApplicationDocuments")]
        public ApplicationDocumentResponse FetchApplicationDocuments(long applicationID)
        {
            ApplicationDocumentResponse applicationDocumentResponse = new ApplicationDocumentResponse();
            if (applicationID == 0)
            {
                _logger.LogError("ApplicationDocumentResponse is null");
                applicationDocumentResponse.IsSuccess = false;
                applicationDocumentResponse.Message = "Unable to fetch documents. Please try after sometime.";
                return applicationDocumentResponse;
            }
            return this._applicationService.GetApplicationDocuments(applicationID);
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetLoanApplicationData")]
        public ApplicationDataResponse GetLoanApplicationData(long applicationID)
        {
            ApplicationDataResponse applicationDataResponse = new ApplicationDataResponse();
            var userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            try
            {
                var listLoadApplicationData = new List<LoanApplication>(_applicationService.GetAllLoanApplicationsForUser(LoginUserInformation.getLoggedInUser(HttpContext).UserID));

                var index = listLoadApplicationData.FindIndex(a=>a.LoanApplicationID == applicationID);

                if (index == -1)
                {
                    _logger.LogError("Application doesn't exist");
                    applicationDataResponse.Message = "User not authorized to view this application.";
                    applicationDataResponse.IsSuccess = false;
                    return applicationDataResponse;
                }

                ApplicationViewEntityResponse applicationViewEntityResponse = this._applicationService.GetLoanApplicationData(applicationID, userSessionEntity);

                //contactViewEntityResponse = this.contactServiceImpl.GetContactInfoById(contactID, LoginUserInformation.getLoggedInUser(HttpContext));
                if (applicationViewEntityResponse != null && applicationViewEntityResponse.IsSuccess)
                {
                    applicationDataResponse.IsSuccess = true;
                    applicationDataResponse.LoanApplication = applicationViewEntityResponse.applicationViewEntity;
                    return applicationDataResponse;
                }
                else
                {
                    _logger.LogError("Application doesn't exist.");
                    applicationDataResponse.Message = "User not authorized to view this application.";
                    applicationDataResponse.IsSuccess = false;
                    return applicationDataResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ApplicationDataResponse GetLoanApplicationData >> ", ex);

                applicationDataResponse.IsSuccess = false;
                applicationDataResponse.Message = "Exception occurred while fetching application data.";
                return applicationDataResponse;
            }
        }

        [HttpGet]
        [Route("GetApplicationSummary")]
        public ApplicationSummaryResponse GetApplicationSummary(long applicationID)
        {
            ApplicationSummaryResponse applicationDataResponse = new ApplicationSummaryResponse();
            var userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            try
            {
                ApplicationViewEntityResponse applicationViewEntityResponse = this._applicationService.GetLoanApplicationData(applicationID, userSessionEntity);
                if (applicationViewEntityResponse != null && applicationViewEntityResponse.IsSuccess)
                {
                    applicationDataResponse.IsSuccess = true;
                    applicationDataResponse.LoanApplication = applicationViewEntityResponse.applicationViewEntity;
                    return applicationDataResponse;
                }
                else
                {
                    _logger.LogError("Application doesn't exist.");
                    applicationDataResponse.Message = "Application doesn't exist.";
                    applicationDataResponse.LoanApplication = applicationViewEntityResponse.applicationViewEntity;
                    applicationDataResponse.IsSuccess = false;
                    return applicationDataResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ApplicationDataResponse GetLoanApplicationData >> ", ex);

                applicationDataResponse.IsSuccess = false;
                applicationDataResponse.Message = "Exception occurred while fetching application data.";
                return applicationDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("ExportLoanApplications")]
        public async Task<IActionResult> ExportLoanApplications(LoanExportRequest request)
        {
            //var rst = filterRequest;           
            try
            {
                await Task.Yield();
                ApplicationListResponse response = _applicationService.ExportGetLoanApplications(request, LoginUserInformation.getLoggedInUser(HttpContext));


                if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
                {
                    response.IsSuccess = false;
                }

                if (response != null && response.ApplicationPageResultEntity.DataList != null)
                {
                    await Task.Yield();
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    string excelSheetName = $"LoanApplications_{DateTime.Now.ToString("MM_dd_yyyy")}";

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add(excelSheetName);

                        // Header Fields
                        workSheet.Cells["A1"].Value = "Grant Number";
                        workSheet.Cells["B1"].Value = "Date Applied";
                        workSheet.Cells["C1"].Value = "Business Name";
                        workSheet.Cells["D1"].Value = "Affiliate Name";
                        workSheet.Cells["E1"].Value = "Grant Program Name";
                        workSheet.Cells["F1"].Value = "Funds Allocated";
                        workSheet.Cells["G1"].Value = "Disbursed Amount";
                        workSheet.Cells["H1"].Value = "Application Status";
                        workSheet.Cells["I1"].Value = "Contact";
                        string tableSelection;
                        int i = 1;

                        tableSelection = "A" + i.ToString() + ":I" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                        workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                        int tablestart = i++;

                        foreach (var item in response.ApplicationPageResultEntity.DataList)
                        {
                            workSheet.Cells[i, 1].Value = item.LoanNumber;
                            workSheet.Cells[i, 2].Value = item.DateApplied;
                            workSheet.Cells[i, 3].Value = item.BusinessName;
                            workSheet.Cells[i, 4].Value = item.AffiliateName;
                            workSheet.Cells[i, 5].Value = item.LoanProgramName;
                            workSheet.Cells[i, 6].Value = item.FundAllocatedAmount;
                            workSheet.Cells[i, 7].Value = item.LoanAmount;//need to check
                            workSheet.Cells[i, 8].Value = item.ApplicationStatus;
                            workSheet.Cells[i, 9].Value = item.ContactInfo;

                            tableSelection = "A" + i.ToString() + ":I" + i.ToString();
                            workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            i++;
                        }

                        workSheet.Cells.AutoFitColumns();
                        package.Save();
                    }

                    stream.Position = 0;
                    string excelName = $"LoanApplications-{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }


                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ApplicationDataResponse GetLoanApplicationData >> ", ex);

                return BadRequest();
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]    
        [HttpPost]
        [Route("SaveBusinessProfileData")]
        public BusinessProfileSaveResponse SaveBusinessProfileData(BusinessProfileRequest businessProfileRequest)
        {
            var userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
            BusinessProfileSaveResponse businessProfileResponse = new BusinessProfileSaveResponse();
            businessProfileResponse.IsSuccess = false;

            var response =  _applicationService.SaveBusinessProfileData(businessProfileRequest, userSessionEntity);

            if (response == null) {

                businessProfileResponse.Message = "Error occurred while saving Business Profile.";
                return businessProfileResponse;
            }

            if (response.ResponseStatus == ResponseStatus.Success)
            {
                businessProfileResponse.IsSuccess = true;
                businessProfileResponse.Message = "Business Profile saved successfully.";
            }
            else
            {
                businessProfileResponse.ValidationErrors = response.ValidationErrors;
                businessProfileResponse.Message = "Error occurred while saving Business Profile.";
            }
            return businessProfileResponse;
        }


        [HttpGet]
        [Route("GetThresholdApplicationSummary")]
        public PaymentScheduleSummaryResponse GetThresholdApplicationSummary( long businessID, long programID, long applicationID)
        {
            var applicationDataResponse = new PaymentScheduleSummaryResponse();
            try
            {
                applicationDataResponse = this._applicationService.GetThresholdApplicationSummary( businessID,  programID,  applicationID);
                if (applicationDataResponse != null && applicationDataResponse.IsSuccess)
                {
                    applicationDataResponse.IsSuccess = true;                   
                    return applicationDataResponse;
                }
                else
                {
                    _logger.LogError("Application doesn't exist.");
                    applicationDataResponse.Message = "Application doesn't exist.";                   
                    applicationDataResponse.IsSuccess = false;
                    return applicationDataResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at PaymentScheduleSummaryResponse GetThresholdApplicationSummary >> ", ex);
                applicationDataResponse.IsSuccess = false;
                applicationDataResponse.Message = "Exception occurred while fetching threshold application data.";
                return applicationDataResponse;
            }
        }

        [HttpGet]
        [Route("GetPaymentScheduleTransactionByApplicationId")]
        public ActionResult<PaymentScheduleTransactionResponse> GetPaymentScheduleTransactionByApplicationId(long applicationID)
        {

            var transactionResponse = new PaymentScheduleTransactionResponse();
            try
            {
                transactionResponse = _applicationService.GetPaymentScheduleTransactionByApplicationId(applicationID);
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

        //[TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("RequestedAddFundAllocation")]
        public IActionResult RequestedAddFundAllocation(LoanApplicationRequest loanApplicationParam)
        {
            ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);
            var response = _applicationService.RequestedAddFundAllocation(loanApplicationParam, userSession);
            return Ok(response);
        }
     }

    
        #endregion Methods
}