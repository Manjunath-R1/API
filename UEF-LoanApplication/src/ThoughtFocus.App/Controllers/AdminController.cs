using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.App.ViewModels;
using ThoughtFocus.Constants;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.RoleProvider.Interfaces;
using ThoughtFocus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using ThoughtFocus.Domain.Request;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AdminController : ControllerBase
    {
        #region Fields

        public ILogger<AdminController> _logger;
        private IAdminService adminService;

        #endregion Fields
        #region Constructors
        public AdminController(IAdminService adminService,
         ILogger<AdminController> logger)
        {
            _logger = logger;
            this.adminService = adminService;
        }
        #endregion Constructors

        #region Methods

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("AddBusinessEntity")]
        public BusinessEntityDataResponse AddBusinessEntity(BusinessEntityRequest saveBusinessEntityParam)
        {
            BusinessEntityDataResponse businessEntityDataResponse = new BusinessEntityDataResponse();

            var result = this.adminService.AddBusinessEntity(saveBusinessEntityParam, LoginUserInformation.getLoggedInUser(HttpContext));
            businessEntityDataResponse.ID = result.ID;
            if (result.IsSuccess == true)
            {
                businessEntityDataResponse.IsSuccess = true;
                businessEntityDataResponse.Message = "Business Entity saved successfully.";
                return businessEntityDataResponse;
            }
            else
            {
                businessEntityDataResponse.IsSuccess = false;
                businessEntityDataResponse.Message = "Error occurred while saving business entity.";
                businessEntityDataResponse.ValidationErrors = result.ValidationErrors;
                return businessEntityDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetBusinessEntityDetails")]
        public BusinessEntityDataResponse GetBusinessEntityDetails(long businessEntityID)
        {
            BusinessEntityDataResponse businessEntityDataResponse = new BusinessEntityDataResponse();

            var response = this.adminService.GetBusinessEntity(businessEntityID);

            if (response != null && response.IsSuccess)
            {
                businessEntityDataResponse.IsSuccess = true;
                businessEntityDataResponse.businessViewEntity = response.businessViewEntity;
                businessEntityDataResponse.CanBeDeleted = response.CanBeDeleted;
                businessEntityDataResponse.IsPaymentSchedule = response.IsPaymentSchedule;
                return businessEntityDataResponse;
            }
            else
            {
                _logger.LogInformation("returned businessEntityDataResponse is incorrect {0}", businessEntityDataResponse);
                businessEntityDataResponse.IsSuccess = false;
                businessEntityDataResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return businessEntityDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("FetchBusinessEntity")]
        public BusinessEntityResponse FetchBusinessEntity()
        {
            BusinessEntityResponse businessEntityResponse = new BusinessEntityResponse();
            BusinessEntityListResponse response = this.adminService.GetAllBusinessEntityInformation();

            if (response != null && response.IsSuccess)
            {
                businessEntityResponse.data = response.businessEntityListResponse;
                businessEntityResponse.IsSuccess = true;
                businessEntityResponse.Message = "Business Entity retrieved successfully.";
                return businessEntityResponse;
            }
            else
            {
                _logger.LogError("returned businessEntityResponse  is incorrect {0}", response);
                businessEntityResponse.IsSuccess = false;
                businessEntityResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
                return businessEntityResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("FetchProgramInvitations")]
        public ProgramInvitationViewModel FetchProgramInvitations()
        {
            ProgramInvitationViewModel programInvitationViewmodel = new ProgramInvitationViewModel();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                programInvitationViewmodel.IsSuccess = false;
            }

            ProgramInvitationResponse response = this.adminService.GetProgramInvitation(LoginUserInformation.getLoggedInUser(HttpContext));

            if (response != null && response.IsSuccess)
            {
                programInvitationViewmodel.data = response.ProgramInvitations;
                programInvitationViewmodel.IsSuccess = true;
                return programInvitationViewmodel;
            }
            else
            {
                _logger.LogError("returned programInvitationViewmodel  is incorrect {0}", response);
                programInvitationViewmodel.IsSuccess = false;

            }
            return programInvitationViewmodel;
        }
        
        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveProgramInvitation")]
        public ContactDataResponse SaveProgramInvitation(ProgramInvitationRequest programRequest)
        {
            ContactDataResponse contactDataResponse = new ContactDataResponse();

            CommonResponse commonResponse = null;

            commonResponse = this.adminService.SaveProgramInvitation(programRequest, LoginUserInformation.getLoggedInUser(HttpContext));
            if (commonResponse.ResponseStatus == ResponseStatus.Success)
            {

                contactDataResponse.IsSuccess = true;
                contactDataResponse.Message = "Invitation sent successfully.";
                return contactDataResponse;
            }
            else
            {
                contactDataResponse.IsSuccess = false;
                contactDataResponse.Message = "Failed to send Invitation.";
                contactDataResponse.ValidationErrors = commonResponse.ValidationErrors;
                return contactDataResponse;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("FetchProgramInvitationPerRequiredData")]
        public ProgramInvitationPreRequiredDataResponse FetchProgramInvitationPerRequiredData()
        {
            ProgramInvitationPreRequiredDataResponse programInvitationViewmodel = new ProgramInvitationPreRequiredDataResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                programInvitationViewmodel.IsSuccess = false;

            }
            programInvitationViewmodel = this.adminService.GetProgramInvitationPerRequiredData(LoginUserInformation.getLoggedInUser(HttpContext));
            if (!programInvitationViewmodel.IsSuccess)
            {
                programInvitationViewmodel = new ProgramInvitationPreRequiredDataResponse();
                programInvitationViewmodel.IsSuccess = false;
            }
            return programInvitationViewmodel;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("FetchBusinessUsers")]
        public BusinessUserResponse FetchBusinessUsers(long businessID)
        {
            BusinessUserResponse businessUserResponse = new BusinessUserResponse();

            businessUserResponse = this.adminService.GetBusinessUsers(businessID);
            if (!businessUserResponse.IsSuccess)
            {
                businessUserResponse.IsSuccess = false;
            }
            return businessUserResponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("FetchBusinessProgramInvitations")]
        public ProgramInvitationViewModel FetchBusinessProgramInvitations(long businessId)
        {
            ProgramInvitationViewModel programInvitationViewmodel = new ProgramInvitationViewModel();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                programInvitationViewmodel.IsSuccess = false;
            }

            BusinessProgramInvitationResponse response = this.adminService.GetProgramInvitationByBusinessID(businessId, LoginUserInformation.getLoggedInUser(HttpContext));

            if (response != null && response.IsSuccess)
            {
                programInvitationViewmodel.programInvitations = response.BusinessProgramInvitations;
                programInvitationViewmodel.IsSuccess = true;
                //return programInvitationViewmodel;
            }
            else
            {
                _logger.LogError("returned programInvitationViewmodel  is incorrect {0}", response);
                programInvitationViewmodel.IsSuccess = false;
                programInvitationViewmodel.Message = "This Business doesn't have any Program Invitations.";
            }
            return programInvitationViewmodel;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("GetConsolidatedReportData")]
        public ConsolidatedReportDataResponse GetConsolidatedReportData(CumulativeReportRequest cumulativeReportRequest)
        {
            ConsolidatedReportDataResponse response = new ConsolidatedReportDataResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
            }
            else
            {
                response = this.adminService.GetCumulativeReportData(cumulativeReportRequest);
            }

            return response;
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllAffiliates")]
        public AffiliateContactResponse GetAllAffiliates()
        {
            AffiliateContactResponse response = new AffiliateContactResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                return response;
            }
            else
            {
                try
                {
                    response.IsSuccess = false;
                    response.recordsTotal = 0;
                    response.data = null;

                    AffiliateContactListResponse affiliatesResponse = this.adminService.GetAllAffiliates();

                    if (affiliatesResponse != null && affiliatesResponse.IsSuccess == true)
                    {
                        response.data = affiliatesResponse.AffiliateContactPageResultEntity.DataList;
                        response.recordsTotal = affiliatesResponse.AffiliateContactPageResultEntity.TotalRecordCount;
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        _logger.LogError("returned Affiliate ListResponse  is incorrect {0}", affiliatesResponse);
                        response.IsSuccess = false;
                        response.Message = "No Affiliates found.";
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetAllAffiliateContactById() >> ", ex);
                    response.IsSuccess = false;
                    response.Message = "Exception occurred while fetching Affiliates.";
                    return response;
                }
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAffiliate")]
        public AffiliateContactResponse GetAffiliate(long? AffiliateID)
        {
            AffiliateContactResponse response = new AffiliateContactResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                return response;
            }
            else
            {
                try
                {
                    response.IsSuccess = false;
                    response.recordsTotal = 0;
                    response.data = null;


                    AffiliateContactListResponse affiliatecontactResponse = this.adminService.GetAffiliate(AffiliateID);

                    if (affiliatecontactResponse != null && affiliatecontactResponse.IsSuccess == true)
                    {
                        response.data = affiliatecontactResponse.AffiliateContactPageResultEntity.DataList;
                        response.recordsTotal = affiliatecontactResponse.AffiliateContactPageResultEntity.TotalRecordCount;
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        _logger.LogError("returned AffiliateContactListResponse  is incorrect {0}", affiliatecontactResponse);
                        response.IsSuccess = false;
                        response.Message = "No Affiliate found.";
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetAllAffiliateContactById() >> ", ex);
                    response.IsSuccess = false;
                    response.Message = "Exception occurred while fetching Affiliate.";
                    return response;
                }
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateAffiliate")]
        public AffiliateContactResponse SaveOrUpdateAffiliate(AffiliateContactRequest affiliatecontactRequest)
        {
            AffiliateContactResponse response = new AffiliateContactResponse();
            CommonResponse commonResponse = null;

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
            }
            else
            {
                commonResponse = this.adminService.SaveOrUpdateAffiliate(affiliatecontactRequest, LoginUserInformation.getLoggedInUser(HttpContext));

                if (commonResponse.ResponseStatus == ResponseStatus.Success)
                {

                    response.IsSuccess = true;
                    response.Message = commonResponse.StatusMessage;
                    response.ValidationErrors = commonResponse.ValidationErrors;
                    response.AffiliateID = commonResponse.ID;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = commonResponse.StatusMessage;
                    response.ValidationErrors = commonResponse.ValidationErrors;
                    response.AffiliateID = commonResponse.ID;
                    return response;
                }
            }
            return response;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("ExportConsolidatedReport")]
        public async Task<IActionResult> ExportConsolidatedReport(CumulativeReportRequest cumulativeReportRequest)
        {

            ConsolidatedReportDataResponse summaryData = this.adminService.GetCumulativeReportData(cumulativeReportRequest);
            List<ConsolidatedReportExportDataResponse> detailsData = this.adminService.GetCumulativeReportExcelData(cumulativeReportRequest);
          
            var reportDetailRequest = new ReportDetailRequest();
            reportDetailRequest.FromDate = cumulativeReportRequest.FromDate;
            reportDetailRequest.ToDate= cumulativeReportRequest.ToDate;
            reportDetailRequest.FundingEntityID= cumulativeReportRequest.FundingEntityID;
            reportDetailRequest.ProgramID= cumulativeReportRequest.ProgramID;
            reportDetailRequest.ReportType = CommonConstants.ActiveAccountReportFlag;
            var activeAccountReport = this.adminService.FetchReportDetailByReportType(reportDetailRequest);

            if (summaryData != null && summaryData.CumulativeReportData.Count() > 0)
            {
                DateTime _fromDate = DateTime.Parse(summaryData.Fromdate);
                DateTime _toDate = DateTime.Parse(summaryData.Todate);
                await Task.Yield();
                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Cumulative Report");

                    //Main header
                    workSheet.Cells["A1:F1"].Merge = true;
                    workSheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1:F1"].Value = "Cumulative Report Summary";
                    workSheet.Cells["A3:E3"].Style.Font.Bold = true;
                    workSheet.Cells[1, 1, 1, 7].Style.Font.Size = 14;
                    workSheet.Cells["A1:F1"].Style.Font.Bold = true;
                    workSheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                    workSheet.Cells["A1:F1"].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                    workSheet.Cells[3, 1].Value = "From Date:";
                    workSheet.Cells[3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    workSheet.Cells[3, 2].Value = _fromDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    workSheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    workSheet.Cells[3, 3].Value = "To Date:";
                    workSheet.Cells[3, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[3, 4].Value = _toDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    workSheet.Cells[3, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    workSheet.Cells[5, 1].Value = "Summary";
                    workSheet.Cells[5, 1].Style.Font.Size = 12;
                    workSheet.Cells[5, 1].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#E74C3C"));

                    workSheet.Cells[6, 1].Value = "Details";
                    workSheet.Cells[6, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[6, 1].Style.Font.Size = 12;

                    workSheet.Cells[6, 2].Value = "Count";
                    workSheet.Cells[6, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[6, 2].Style.Font.Size = 12;

                    workSheet.Cells[4, 1, 6, 2].Style.Font.Bold = true;

                    workSheet.Cells["A6:B6"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells["A6:B6"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                    workSheet.Cells["A6:B6"].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                    workSheet.Cells["A6:B6"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells["A6:B6"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells["A6:B6"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells["A6:B6"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    string tableSelection;

                    int i = 7;

                    foreach (var item in summaryData.CumulativeReportData)
                    {
                        workSheet.Cells[i, 1].Value = item.Key;
                        workSheet.Cells[i, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[i, 2].Value = item.Value;
                        workSheet.Cells[i, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        tableSelection = "A" + i.ToString() + ":B" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        i++;
                    }

                    i = i + 2;
                    // //workSheet.Cells[i, 1].Value = "Details";
                    // workSheet.Cells[i, 1].Value = "";
                    // workSheet.Cells[i, 1].Style.Font.Bold = true;
                    // workSheet.Cells[i, 1].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#E74C3C"));
                    // workSheet.Cells[i, 1].Style.Font.Size = 12;

                    // i++;
                    // Details grid
                    workSheet.Cells[i, 1].Value = "Business Name";
                    workSheet.Cells[i, 2].Value = "Affiliate Name";
                    workSheet.Cells[i, 3].Value = "Program Name";
                    workSheet.Cells[i, 4].Value = "Invitations";
                   // workSheet.Cells[i, 5].Value = "Activated Accounts";
                    workSheet.Cells[i, 5].Value = "Application Started";
                    workSheet.Cells[i, 6].Value = "Application Submitted";
                    workSheet.Cells[i, 7].Value = "Application Funded";
                    workSheet.Cells[i, 8].Value = "Fund Released (USD)";
                    workSheet.Cells[i, 1, i, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[i, 1, i, 8].Style.Font.Bold = true;
                    workSheet.Cells[i, 1, i, 8].Style.Font.Size = 12;

                    tableSelection = "A" + i.ToString() + ":I" + i.ToString();
                    workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                    workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                    int tablestart = i++;
                    foreach (var item in detailsData)
                    {

                        workSheet.Cells[i, 1].Value = item.BusinessName;
                        workSheet.Cells[i, 2].Value = item.AffiliateName;
                        workSheet.Cells[i, 3].Value = item.ProgramName;
                        workSheet.Cells[i, 4].Value = item.ApplicationInvited;
                       // workSheet.Cells[i, 5].Value = item.ActivatedAccounts;
                        workSheet.Cells[i, 5].Value = item.ApplicationStarted;
                        workSheet.Cells[i, 6].Value = item.ApplicationSubmitted;
                        workSheet.Cells[i, 7].Value = item.ApplicationFunded;
                        workSheet.Cells[i, 8].Value = item.Fundreleased != 0 ? string.Format("{0:#,#}", (item.Fundreleased)) : "0";

                        tableSelection = "A" + i.ToString() + ":I" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        i++;
                    }

                    workSheet.Cells.AutoFitColumns();

                    int totalCols = workSheet.Dimension.End.Column;
                    workSheet.Cells[tablestart, 3, i, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[tablestart, 1, i, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                    /**********************************/
                    i = i + 2;
                    // Details grid
                    workSheet.Cells[i, 1].Value = "Activation Date";
                    workSheet.Cells[i, 2].Value = "Contact";
                    workSheet.Cells[i, 3].Value = "Phone Numbe";
                    workSheet.Cells[i, 4].Value = "Email";
                    workSheet.Cells[i, 5].Value = "Account Activated";
                 
                    workSheet.Cells[i, 1, i, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[i, 1, i, 5].Style.Font.Bold = true;
                    workSheet.Cells[i, 1, i, 5].Style.Font.Size = 12;

                    tableSelection = "A" + i.ToString() + ":E" + i.ToString();
                    workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                    workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                    int table2start = i++;
                    if(activeAccountReport.ActiveAccountDetail!=null && activeAccountReport.ActiveAccountDetail.Count > 0)
                    {
                        foreach (var item in activeAccountReport.ActiveAccountDetail.OrderBy(o => o.FullName))
                        {

                            workSheet.Cells[i, 1].Value = item.AccountActivationDate;
                            workSheet.Cells[i, 2].Value = item.FullName;
                            workSheet.Cells[i, 3].Value = item.PhoneNo;
                            workSheet.Cells[i, 4].Value = item.EmailAddress;
                            workSheet.Cells[i, 5].Value = item.IsAccountActivated;

                            tableSelection = "A" + i.ToString() + ":I" + i.ToString();
                            workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            i++;
                        }

                        
                    }


                    /****************End*****************/
                    workSheet.Cells.AutoFitColumns();

                    int totalCols2 = workSheet.Dimension.End.Column;
                    workSheet.Cells[table2start, 3, i, totalCols2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[table2start, 1, i, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    package.Save();
                }

                stream.Position = 0;
                string excelName = $"NUL_UEF_CumulativeReport-{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            else
            {
                return BadRequest();
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("DeleteBusinessEntity")]
        public CommonResponse DeleteBusinessEntity(long businessId)
        {
            List<string> validationMessages = new List<string>();
            CommonResponse response = null;
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                validationMessages.Add("User token is invalid.");
                response = new CommonResponse(ResponseStatus.Fail, MessageConstants.EditFailure, validationMessages);
                return response;
            }
            response = this.adminService.DeleteBusinessEntity(businessId, LoginUserInformation.getLoggedInUser(HttpContext));
            return response;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllQuestions")]
        public QuestionsResponse GetAllQuestions()
        {
            QuestionsResponse questionsResponse = new QuestionsResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                questionsResponse.IsSuccess = false;
                return questionsResponse;
            }
            else
            {
                try
                {
                    questionsResponse.IsSuccess = false;
                    questionsResponse.recordsTotal = 0;
                    questionsResponse.QuestionsRecords = null;

                    QuestionsResponse questionResponse = this.adminService.GetAllQuestions();

                    if (questionResponse != null && questionResponse.IsSuccess == true)
                    {
                        questionsResponse.QuestionsRecords = questionResponse.QuestionsPageResultEntity.DataList;
                        questionsResponse.recordsTotal = questionResponse.QuestionsPageResultEntity.TotalRecordCount;
                        questionsResponse.IsSuccess = true;
                        return questionsResponse;
                    }
                    else
                    {
                        _logger.LogError("returned questions listresponse  is incorrect {0}", questionResponse);
                        questionsResponse.IsSuccess = false;
                        questionsResponse.Message = "No Questions found.";
                        return questionsResponse;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetAllQuestions() >> ", ex);
                    questionsResponse.IsSuccess = false;
                    questionsResponse.Message = "Exception occurred while fetching Questions.";
                    return questionsResponse;
                }
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetQuestion")]
        public QuestionsResponse GetQuestion(long? QuestionID)
        {
            QuestionsResponse questionsResponse = new QuestionsResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                questionsResponse.IsSuccess = false;
                return questionsResponse;
            }
            else
            {
                try
                {
                    questionsResponse.IsSuccess = false;
                    questionsResponse.recordsTotal = 0;
                    questionsResponse.QuestionsRecords = null;


                    QuestionsResponse questionResponse = this.adminService.GetQuestion(QuestionID);

                    if (questionResponse != null && questionResponse.IsSuccess == true)
                    {
                        questionResponse.QuestionsRecords = questionResponse.QuestionsPageResultEntity.DataList;
                        questionResponse.recordsTotal = questionResponse.QuestionsPageResultEntity.TotalRecordCount;
                        questionResponse.IsSuccess = true;
                        return questionResponse;
                    }
                    else
                    {
                        _logger.LogError("returned question is incorrect {0}", questionResponse);
                        questionResponse.IsSuccess = false;
                        questionResponse.Message = "There are no questions";
                        return questionResponse;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetQuestion() >> ", ex);
                    questionsResponse.IsSuccess = false;
                    questionsResponse.Message = "Exception occurred while getting getting questions";
                    return questionsResponse;
                }
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateQuestions")]
        public QuestionsResponse SaveOrUpdateQuestions(QuestionsRequest questionsRequest)
        {
            QuestionsResponse response = new QuestionsResponse();
            CommonResponse commonResponse = null;

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonResponse = this.adminService.SaveOrUpdateQuestions(questionsRequest, LoginUserInformation.getLoggedInUser(HttpContext));

                    if (commonResponse != null && commonResponse.ResponseStatus == ResponseStatus.Success)
                    {

                        response.IsSuccess = true;
                        response.Message = commonResponse.StatusMessage;
                        response.ValidationErrors = commonResponse.ValidationErrors;
                        response.QuestionID = commonResponse.ID;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = commonResponse.StatusMessage;
                        response.ValidationErrors = commonResponse.ValidationErrors;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult SaveOrUpdateQuestions() >> ", ex);
                    response.IsSuccess = false;
                    response.Message = "Exception occurred while saving Questions.";
                }
            }

            return response;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllDocumentTypes")]
        public DocumentsResponse GetAllDocumentTypes()
        {
            DocumentsResponse documentsResponse = new DocumentsResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                documentsResponse.IsSuccess = false;
                return documentsResponse;
            }
            else
            {
                try
                {
                    documentsResponse.IsSuccess = false;
                    documentsResponse.recordsTotal = 0;
                    documentsResponse.DocumentRecords = null;

                    DocumentsResponse documentResponse = this.adminService.GetAllDocumentTypes();

                    if (documentResponse != null && documentResponse.IsSuccess == true)
                    {
                        documentsResponse.DocumentRecords = documentResponse.DocumentsPageResultEntity.DataList;
                        documentsResponse.recordsTotal = documentResponse.DocumentsPageResultEntity.TotalRecordCount;
                        documentsResponse.IsSuccess = true;
                        return documentsResponse;
                    }
                    else
                    {
                        _logger.LogError("returned documents list is incorrect {0}", documentResponse);
                        documentsResponse.IsSuccess = false;
                        documentsResponse.Message = "No Questions found.";
                        return documentsResponse;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetAllDocumentTypes() >> ", ex);
                    documentsResponse.IsSuccess = false;
                    documentsResponse.Message = "Exception occurred while fetching Document Types.";
                    return documentsResponse;
                }
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetDocument")]
        public DocumentsResponse GetDocument(long? documentTypeID)
        {
            DocumentsResponse documentsResponse = new DocumentsResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                documentsResponse.IsSuccess = false;
                return documentsResponse;
            }
            else
            {
                try
                {
                    documentsResponse.IsSuccess = false;
                    documentsResponse.recordsTotal = 0;
                    documentsResponse.DocumentRecords = null;

                    DocumentsResponse documentResponse = this.adminService.GetDocument(documentTypeID);

                    if (documentResponse != null && documentResponse.IsSuccess == true)
                    {
                        documentsResponse.DocumentRecords = documentResponse.DocumentsPageResultEntity.DataList;
                        documentsResponse.recordsTotal = documentResponse.DocumentsPageResultEntity.TotalRecordCount;
                        documentsResponse.IsSuccess = true;
                        return documentsResponse;
                    }
                    else
                    {
                        _logger.LogError("returned documents list is incorrect {0}", documentResponse);
                        documentsResponse.IsSuccess = false;
                        documentsResponse.Message = "No Questions found.";
                        return documentsResponse;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetDocument() >> ", ex);
                    documentsResponse.IsSuccess = false;
                    documentsResponse.Message = "Exception occurred while fetching Document Types.";
                    return documentsResponse;
                }
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateDocuments")]
        public DocumentsResponse SaveOrUpdateDocuments(DocumentsRequest documentsRequest)
        {
            DocumentsResponse documentresponse = new DocumentsResponse();
            CommonResponse commonResponse = null;

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                documentresponse.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonResponse = this.adminService.SaveOrUpdateDocuments(documentsRequest, LoginUserInformation.getLoggedInUser(HttpContext));

                    if (commonResponse != null && commonResponse.ResponseStatus == ResponseStatus.Success)
                    {

                        documentresponse.IsSuccess = true;
                        documentresponse.Message = commonResponse.StatusMessage;
                        documentresponse.ValidationErrors = commonResponse.ValidationErrors;
                        documentresponse.DocumentTypeID = commonResponse.ID;
                    }
                    else
                    {
                        documentresponse.IsSuccess = false;
                        documentresponse.Message = commonResponse.StatusMessage;
                        documentresponse.ValidationErrors = commonResponse.ValidationErrors;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult SaveOrUpdateDocuments() >> ", ex);
                    documentresponse.IsSuccess = false;
                    documentresponse.Message = "Exception occurred while saving Document Types.";
                }
            }
            return documentresponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("SaveOrUpdateAgreementName")]
        public ProgramAgreementResponse SaveOrUpdateAgreementName(AgreementRequest agreementRequest)
        {
            ProgramAgreementResponse programAgreementResponse = new ProgramAgreementResponse();
            CommonResponse commonResponse = null;

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                programAgreementResponse.IsSuccess = false;
            }
            else
            {
                try
                {
                    commonResponse = this.adminService.SaveOrUpdateAgreementName(agreementRequest, LoginUserInformation.getLoggedInUser(HttpContext));

                    if (commonResponse != null && commonResponse.ResponseStatus == ResponseStatus.Success)
                    {
                        programAgreementResponse.IsSuccess = true;
                        programAgreementResponse.Message = commonResponse.StatusMessage;
                        programAgreementResponse.ValidationErrors = commonResponse.ValidationErrors;
                        programAgreementResponse.AgreementID = commonResponse.ID;
                    }
                    else
                    {
                        programAgreementResponse.IsSuccess = false;
                        programAgreementResponse.Message = "Failed to save User Agreement.";
                        programAgreementResponse.ValidationErrors = null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered while SaveOrUpdateAgreementName() >> ", ex);
                    programAgreementResponse.IsSuccess = false;
                    programAgreementResponse.Message = "Exception occurred while saving Agreement.";
                }
            }
            return programAgreementResponse;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetProgramWiseAgreement")]
        public ProgramAgreementResponse GetProgramWiseAgreement(long programID)
        {
            ProgramAgreementResponse programAgreementResponse = new ProgramAgreementResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                programAgreementResponse.IsSuccess = false;
                return programAgreementResponse;
            }
            else
            {
                try
                {
                    programAgreementResponse = this.adminService.ProgramWiseAgreement(programID);

                    if (programAgreementResponse != null && programAgreementResponse.IsSuccess == true)
                    {
                        programAgreementResponse.IsSuccess = true;
                        return programAgreementResponse;
                    }
                    else
                    {
                        _logger.LogError("returned agreement is incorrect {0}", programAgreementResponse);
                        programAgreementResponse.IsSuccess = false;
                        programAgreementResponse.Message = "Couldn't find the Agreement for this Program.";
                        return programAgreementResponse;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error encountered at JsonResult GetProgramWiseAgreement() >> ", ex);
                    programAgreementResponse.IsSuccess = false;
                    programAgreementResponse.Message = "Exception occurred while fetching Program-wise Agreement.";
                    return programAgreementResponse;
                }
            }
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("ExportCiviCRMData")]
        public async Task<IActionResult> ExportCiviCRMData(int type)
        {
            if (type == 1)
            {
                List<CiviCRMOrganizationDataResponse> civiCRMData = this.adminService.GetCiviCRMOrganizationExportData(LoginUserInformation.getLoggedInUser(HttpContext));

                if (civiCRMData != null && civiCRMData.Count > 0)
                {
                    await Task.Yield();
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    string excelSheetName = $"NUL_Org_{DateTime.Now.ToString("MM_dd_yyyy")}";

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add(excelSheetName);

                        // Header Fields
                        workSheet.Cells["A1"].Value = "Organization Name";
                        workSheet.Cells["B1"].Value = "Street Address";
                        workSheet.Cells["C1"].Value = "City";
                        workSheet.Cells["D1"].Value = "State";
                        workSheet.Cells["E1"].Value = "Zip";
                        workSheet.Cells["F1"].Value = "Phone Number";
                        workSheet.Cells["G1"].Value = "Email";
                        workSheet.Cells["H1"].Value = "Affiliate";
                        workSheet.Cells["I1"].Value = "EIN Number";
                        workSheet.Cells["J1"].Value = "SIC Code";
                        workSheet.Cells["K1"].Value = "NAICS";
                        workSheet.Cells["L1"].Value = "Business Type";
                        workSheet.Cells["M1"].Value = "Industry";
                        workSheet.Cells["N1"].Value = "Url";
                        workSheet.Cells["O1"].Value = "Contact Type";
                        workSheet.Cells["P1"].Value = "External ID (Org)";
                        workSheet.Cells["Q1"].Value = "Grant 1 Program";
                        workSheet.Cells["R1"].Value = "Grant 1 Amount Funded (USD)";
                        workSheet.Cells["S1"].Value = "Grant 1 Received Date";
                        workSheet.Cells["T1"].Value = "Grant 2 Program";
                        workSheet.Cells["U1"].Value = "Grant 2 Amount Funded (USD)";
                        workSheet.Cells["V1"].Value = "Grant 2 Received Date";
                        workSheet.Cells["W1"].Value = "Grant 3 Program";
                        workSheet.Cells["X1"].Value = "Grant 3 Amount Funded (USD)";
                        workSheet.Cells["Y1"].Value = "Grant 3 Received Date";

                        string tableSelection;
                        int i = 1;

                        tableSelection = "A" + i.ToString() + ":Y" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                        workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                        int tablestart = i++;

                        foreach (var item in civiCRMData)
                        {
                            workSheet.Cells[i, 1].Value = item.Organization_Name;
                            workSheet.Cells[i, 2].Value = item.Street_Address;
                            workSheet.Cells[i, 3].Value = item.City;
                            workSheet.Cells[i, 4].Value = item.State;
                            workSheet.Cells[i, 5].Value = item.Zip;
                            workSheet.Cells[i, 6].Value = item.Phone_Number;
                            workSheet.Cells[i, 7].Value = item.Email;
                            workSheet.Cells[i, 8].Value = item.Affiliate;
                            workSheet.Cells[i, 9].Value = item.EIN_Number;
                            workSheet.Cells[i, 10].Value = item.SIC_Code;
                            workSheet.Cells[i, 11].Value = item.NAICS;
                            workSheet.Cells[i, 12].Value = item.Business_Type;
                            workSheet.Cells[i, 13].Value = item.Industry;
                            workSheet.Cells[i, 14].Value = item.Url;
                            workSheet.Cells[i, 15].Value = item.Contact_Type;
                            workSheet.Cells[i, 16].Value = item.External_ID_OrdID;
                            workSheet.Cells[i, 17].Value = item.Grant1_Program;
                            workSheet.Cells[i, 18].Value = item.Grant1_AmountFunded > 0 ? string.Format("{0:#,#}", (item.Grant1_AmountFunded)) : string.Empty;
                            workSheet.Cells[i, 19].Value = item.Grant1_ReceivedDate;
                            workSheet.Cells[i, 20].Value = item.Grant2_Program;
                            workSheet.Cells[i, 21].Value = item.Grant2_AmountFunded > 0 ? string.Format("{0:#,#}", (item.Grant2_AmountFunded)) : string.Empty;
                            workSheet.Cells[i, 22].Value = item.Grant2_ReceivedDate;
                            workSheet.Cells[i, 23].Value = item.Grant3_Program;
                            workSheet.Cells[i, 24].Value = item.Grant3_AmountFunded > 0 ? string.Format("{0:#,#}", (item.Grant3_AmountFunded)) : string.Empty;
                            workSheet.Cells[i, 25].Value = item.Grant3_ReceivedDate;

                            tableSelection = "A" + i.ToString() + ":Y" + i.ToString();
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
                    string excelName = $"NUL_Org_{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }
            else if (type == 2)
            {
                List<CiviCRMContactsDataResponse> civiCRMData = this.adminService.GetCiviCRMContactExportData(LoginUserInformation.getLoggedInUser(HttpContext));

                if (civiCRMData != null && civiCRMData.Count > 0)
                {
                    await Task.Yield();
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    string excelSheetName = $"NUL_Contact_{DateTime.Now.ToString("MM_dd_yyyy")}";

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add(excelSheetName);

                        // Header Fields
                        workSheet.Cells["A1"].Value = "Prefix";
                        workSheet.Cells["B1"].Value = "First Name";
                        workSheet.Cells["C1"].Value = "Middle Name";
                        workSheet.Cells["D1"].Value = "Last Name";
                        workSheet.Cells["E1"].Value = "Affiliate";
                        workSheet.Cells["F1"].Value = "Organization";
                        workSheet.Cells["G1"].Value = "Role";
                        workSheet.Cells["H1"].Value = "Street Address";
                        workSheet.Cells["I1"].Value = "City";
                        workSheet.Cells["J1"].Value = "State";
                        workSheet.Cells["K1"].Value = "Zip";
                        workSheet.Cells["L1"].Value = "Phone Number";
                        workSheet.Cells["M1"].Value = "Phone Type";
                        workSheet.Cells["N1"].Value = "Email";
                        workSheet.Cells["O1"].Value = "Race";
                        workSheet.Cells["P1"].Value = "Ethnicity";
                        workSheet.Cells["Q1"].Value = "Gender";
                        workSheet.Cells["R1"].Value = "Veteran";
                        workSheet.Cells["S1"].Value = "External ID (Contact)";
                        workSheet.Cells["T1"].Value = "External ID (Org)";

                        string tableSelection;
                        int i = 1;

                        tableSelection = "A" + i.ToString() + ":T" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                        workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                        int tablestart = i++;

                        foreach (var item in civiCRMData)
                        {
                            workSheet.Cells[i, 1].Value = item.Prefix;
                            workSheet.Cells[i, 2].Value = item.First_Name;
                            workSheet.Cells[i, 3].Value = item.Middle_Name;
                            workSheet.Cells[i, 4].Value = item.Last_Name;
                            workSheet.Cells[i, 5].Value = item.Affiliate;
                            workSheet.Cells[i, 6].Value = item.Organization_Name;
                            workSheet.Cells[i, 7].Value = item.Role;
                            workSheet.Cells[i, 8].Value = item.Street_Address;
                            workSheet.Cells[i, 9].Value = item.City;
                            workSheet.Cells[i, 10].Value = item.State;
                            workSheet.Cells[i, 11].Value = item.Zip;
                            workSheet.Cells[i, 12].Value = item.Phone_Number;
                            workSheet.Cells[i, 13].Value = item.Phone_Type;
                            workSheet.Cells[i, 14].Value = item.Email;
                            workSheet.Cells[i, 15].Value = item.Race;
                            workSheet.Cells[i, 16].Value = item.Ethnicity;
                            workSheet.Cells[i, 17].Value = item.Gender;
                            workSheet.Cells[i, 18].Value = item.Veteran;
                            workSheet.Cells[i, 19].Value = item.ContactID;
                            workSheet.Cells[i, 20].Value = item.OrganizationID;

                            tableSelection = "A" + i.ToString() + ":T" + i.ToString();
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
                    string excelName = $"NUL_Contact_{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
            }

            return BadRequest();
        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetCiviCRMDataExportLog")]
        public CiviCRMDataExportLogResponse GetCiviCRMDataExportLog()
        {
            CiviCRMDataExportLogResponse response = new CiviCRMDataExportLogResponse();

            try
            {
                response = this.adminService.GetAllCiviCRMDataExportLog();

                if (response != null && response.IsSuccess == true)
                {
                    response.Message = "CiviCRM Data Export Log is retrieved successfully.";
                    response.IsSuccess = true;
                    return response;
                }
                else
                {
                    _logger.LogError("returned CiviCRM Data Export Log Response is incorrect {0}", response);
                    response.IsSuccess = false;
                    response.Message = "No logs found for CiviCRM Data Export.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult GetAllCiviCRMDataExportLog() >> ", ex);
                response.IsSuccess = false;
                response.Message = "Exception occurred while fetching CiviCRM Data Export Log details.";
                return response;
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetBusinessProfileMasterData")]
        public BusinessProfileMasterDataResponse GetBusinessProfileMasterData(long businessID)
        {
            BusinessProfileMasterDataResponse response = new BusinessProfileMasterDataResponse();

            try
            {
                response = this.adminService.GetBusinessProfileMasterData(businessID);

                if (response != null && response.IsSuccess == true)
                {
                    response.Message = "Business Profile Master Data is retrieved successfully.";
                    response.IsSuccess = true;
                    return response;
                }
                else
                {
                    _logger.LogError("returned Business profile master Data Response is incorrect {0}", response);
                    response.IsSuccess = false;
                    response.Message = "No data found for Business Profile Master Data.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult GetBusinessProfileMasterData() >> ", ex);
                response.IsSuccess = false;
                response.Message = "Exception occurred while fetching GetBusinessProfileMasterData.";
                return response;
            }


        }


        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllBusinessEntityByUser")]
        public BusinessEntityListByUserResponse GetAllBusinessEntityByUser(string userName)
        {
            BusinessEntityListByUserResponse response = new BusinessEntityListByUserResponse();

            try
            {

                response = this.adminService.GetAllBusinessEntityByUser(userName);
                if (response != null && response.IsSuccess == true)
                {
                    response.Message = "Business Entity data for User is retrieved successfully.";
                    response.IsSuccess = true;
                    return response;
                }
                else
                {
                    _logger.LogError("returned Business entity data Response is incorrect {0}", response);
                    response.IsSuccess = false;
                    response.Message = "No data found for Business Entity for  user.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult GetAllBusinessEntityData() >> ", ex);
                response.IsSuccess = false;
                response.Message = "Exception occurred while fetching GetAllBusinessEntityData.";
                return response;
            }



        }

        //[TypeFilter(typeof(AuthorizeAttribute))]
        [Route("SendUpdateFundDetailEmailNotifiaction")]
        [HttpPost]
        public IActionResult SendUpdateFundDetailEmailNotifiaction(long loanApplicationID)
        {
            var response = new UpdateFundDetailEmailNotifiactionResponse();
            CommonResponse commonResponse = null;
            try
            {
                var userSessionEntity = LoginUserInformation.getLoggedInUser(HttpContext);
                commonResponse = this.adminService.SendUpdateFundDetailEmailNotifiaction(loanApplicationID, userSessionEntity);
                if (commonResponse != null && commonResponse .ResponseStatus == ResponseStatus.Success)
                {
                    response.Message = "NOTIFY mail has been sent successfully.";
                    response.IsSuccess = true;
                    return Ok(response);
                }
                else
                {
                    _logger.LogError("NOTIFY mail fail to send successfully {0}", response);
                    response.IsSuccess = false;
                    response.Message = "NOTIFY mail fail to send successfully.";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult SendUpdateFundDetailEmailNotifiaction() >> ", ex);
                response.IsSuccess = false;
                response.Message = "Exception occurred while fetching SendUpdateFundDetailEmailNotifiaction.";
                return Ok(response);
            }
        }
        
        [HttpGet]
        [Route("FetchProgramByFundEntityId")]
        public ProgramsResponse FetchAllProgramByFundEntityId(long fundEntityId)
        {
            var response = new ProgramsResponse();
            try
            {
                return response = this.adminService.GetProgramDetailsByFundEntityID(fundEntityId);
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult FetchAllProgramByFundEntityId() >> ", ex);
                response.IsSuccess = false;
                response.Message = "Exception occurred while fetching FetchAllProgramByFundEntityId.";
                return response;
            }

        }

        [HttpPost]
        [Route("FetchReportDetailByReportType")]
        public ReportDetailResponse FetchReportDetailByReportType(ReportDetailRequest reportDetailRequest)
        {
            var response = new ReportDetailResponse();
            try
            {
                if(reportDetailRequest == null || string.IsNullOrEmpty(reportDetailRequest.ReportType))
                {
                    response.IsSuccess = false;
                    response.Message = "Input request is not correct.";
                    return response;
                }
                return response = this.adminService.FetchReportDetailByReportType(reportDetailRequest);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at JsonResult FetchAllProgramByFundEntityId() >> ", ex);
                response.IsSuccess = false;
                response.Message = "Exception occurred while fetching FetchAllProgramByFundEntityId.";
                return response;
            }

        }
       
        
        [HttpPost]
        [Route("ExportProgramInvitations")]
        public async Task<IActionResult> ExportProgramInvitations(ExportProgramInvitationsRequest exportRequest)
        {
            var programInvitationViewmodel = new ProgramInvitationViewModel();
            try
            {
                if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
                {
                    programInvitationViewmodel.IsSuccess = false;
                }

                await Task.Yield();
                var response = this.adminService.GetExportProgramInvitation(exportRequest, LoginUserInformation.getLoggedInUser(HttpContext));
                   if (response!=null && response.ProgramInvitations != null && response.ProgramInvitations.Count > 0)
                    {
                        await Task.Yield();
                        var stream = new MemoryStream();
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        string excelSheetName = $"ProgramInvitations_{DateTime.Now.ToString("MM_dd_yyyy")}";

                        using (var package = new ExcelPackage(stream))
                        {
                            var workSheet = package.Workbook.Worksheets.Add(excelSheetName);

                            // Header Fields
                            workSheet.Cells["A1"].Value = "Business Name";
                            workSheet.Cells["B1"].Value = "Program Name";
                            workSheet.Cells["C1"].Value = "Funding Entity";
                            workSheet.Cells["D1"].Value = "Status";
                            workSheet.Cells["E1"].Value = "Affiliate Name";
                            workSheet.Cells["F1"].Value = "Invited Date";

                            string tableSelection;
                            int i = 1;

                            tableSelection = "A" + i.ToString() + ":F" + i.ToString();
                            workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                            workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                            int tablestart = i++;

                            foreach (var item in response.ProgramInvitations)
                            {
                                workSheet.Cells[i, 1].Value = item.BusinessName;
                                workSheet.Cells[i, 2].Value = item.ProgramName;
                                workSheet.Cells[i, 3].Value = item.FundingEntityName;
                                workSheet.Cells[i, 4].Value = item.ProgramStatus;
                                workSheet.Cells[i, 5].Value = item.AffiliateName;
                                workSheet.Cells[i, 6].Value = item.DateInvited;
                                tableSelection = "A" + i.ToString() + ":F" + i.ToString();
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
                        string excelName = $"ProgramInvitations_{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";
                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                    }
                

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ExportProgramInvitations GetExportProgramInvitation >> ", ex);

                return BadRequest();
            }
        }
       
        [HttpPost]
        [Route("FetchProgramInvitationsByProgram")]
        public ProgramInvitationViewModel FetchProgramInvitationsByProgram(ProgramInvitationsRequest request)
        {
            ProgramInvitationViewModel programInvitationViewmodel = new ProgramInvitationViewModel();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                programInvitationViewmodel.IsSuccess = false;
            }

            ProgramInvitationResponse response = this.adminService.GetProgramInvitationByProgram(request, LoginUserInformation.getLoggedInUser(HttpContext));

            if (response != null && response.IsSuccess)
            {
                programInvitationViewmodel.Programs= response.Programs;
                programInvitationViewmodel.data = response.ProgramInvitations;
                programInvitationViewmodel.IsSuccess = true;
                return programInvitationViewmodel;
            }
            else
            {
                _logger.LogError("returned programInvitationViewmodel  is incorrect {0}", response);
                programInvitationViewmodel.IsSuccess = false;

            }
            return programInvitationViewmodel;
        }

        [HttpPost]
        [Route("ConsolidatedFundReportData")]
        public ConsolidatedFundReportDataResponse ConsolidatedFundReportData(FundReportRequest request)
        {
            var response = new ConsolidatedFundReportDataResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
            }
            else
            {
                response = this.adminService.GetConsolidatedFundReportData(request, LoginUserInformation.getLoggedInUser(HttpContext));
            }
            return response;
        }
        [HttpPost]
        [Route("ExportConsolidatedFundReportData")]
        public async Task<IActionResult> ExportConsolidatedFundReportData(FundReportRequest exportRequest)
        {
            var response = new ConsolidatedFundReportDataResponse();
            try
            {
                if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
                {
                    response.IsSuccess = false;
                }

                await Task.Yield();
                response = this.adminService.GetConsolidatedFundReportData(exportRequest, LoginUserInformation.getLoggedInUser(HttpContext));
                if (response != null && response.ConsolidatedFundDetails != null && response.ConsolidatedFundDetails.Count > 0)
                {
                    await Task.Yield();
                    var stream = new MemoryStream();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    string excelSheetName = $"FundDetails_{DateTime.Now.ToString("MM_dd_yyyy")}";

                    using (var package = new ExcelPackage(stream))
                    {
                        var workSheet = package.Workbook.Worksheets.Add(excelSheetName);

                        // Header Fields
                        workSheet.Cells["A1"].Value = "Business Name";
                        workSheet.Cells["B1"].Value = "Program Name";
                        workSheet.Cells["C1"].Value = "Funded Amount";
                        workSheet.Cells["D1"].Value = "Funds Allocated";
                        workSheet.Cells["E1"].Value = "Funded Disbursed";
                      

                        string tableSelection;
                        int i = 1;

                        tableSelection = "A" + i.ToString() + ":E" + i.ToString();
                        workSheet.Cells[tableSelection].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[tableSelection].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[tableSelection].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#1A5276"));
                        workSheet.Cells[tableSelection].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));

                        int tablestart = i++;

                        foreach (var item in response.ConsolidatedFundDetails)
                        {
                            workSheet.Cells[i, 1].Value = item.BusinessName;
                            workSheet.Cells[i, 2].Value = item.ProgramName;
                            workSheet.Cells[i, 3].Value = item.FundedAmount;
                            workSheet.Cells[i, 4].Value = item.FundsAllocated;
                            workSheet.Cells[i, 5].Value = item.FundedDisbursed;                           
                            tableSelection = "A" + i.ToString() + ":E" + i.ToString();
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
                    string excelName = $"FundDetails_{DateTime.Now.ToString("MM_dd_yyyy")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }


                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error encountered at ExportConsolidatedFundReportData >> ", ex);

                return BadRequest();
            }
        }
        #endregion Methods
    }
}
