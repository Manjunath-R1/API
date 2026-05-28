using Microsoft.AspNetCore.Mvc;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.Domain.Request;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DashboardController : ControllerBase
    {
        #region Fields

        private IDashboardService _dashboardService;

        #endregion Fields
        #region Constructors



        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }
        #endregion Constructors
        #region Methods

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetFoundAllocation")]
        public DashboardResponse GetFoundAllocation(long ProgramId)
        {
            DashboardResponse programFundingSourceResponse = new DashboardResponse();


            programFundingSourceResponse = this._dashboardService.GetFoundAllocationStatus(ProgramId);

            if (programFundingSourceResponse.IsSuccess)
            {
                programFundingSourceResponse.IsSuccess = true;
                return programFundingSourceResponse;
            }
            else
            {
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Couldn't find the fund allocation status for this program.";
                return programFundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetFundAllocationProgram")]
        public DashboardResponse GetFundAllocationProgram(long programID, int pageNumber)
        {
            var pageSize = 10;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            DashboardResponse programFundingSourceResponse = new DashboardResponse();

            programFundingSourceResponse = this._dashboardService.GetFundAllocationProgramStatus(programID, pageSize, pageNumber);

            if (programFundingSourceResponse.FundAllocationResponse != null && programFundingSourceResponse.IsSuccess)
            {
                programFundingSourceResponse.IsSuccess = true;
                return programFundingSourceResponse;
            }
            else
            {
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Couldn't find the fund allocation status for this program.";
                return programFundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetApplicationStatus")]
        public DashboardResponse GetApplicationStatus(long programID)
        {
            DashboardResponse programFundingSourceResponse = new DashboardResponse();


            programFundingSourceResponse = this._dashboardService.GetApplicationStatus(programID);

            if (programFundingSourceResponse.IsSuccess)
            {
                programFundingSourceResponse.IsSuccess = true;
                return programFundingSourceResponse;
            }
            else
            {
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Couldn't find the Fund Allocation Status for this Program.";
                return programFundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetProgramApplicationStatus")]
        public DashboardProgramWiseApplicationStatusResponse GetProgramApplicationStatus(long ProgramId, int pageNumber)
        {
            var pageSize = 10;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            DashboardProgramWiseApplicationStatusResponse programFundingSourceResponse = new DashboardProgramWiseApplicationStatusResponse();


            programFundingSourceResponse = this._dashboardService.GetApplicationProgramStatus(ProgramId, pageSize, pageNumber);

            if (programFundingSourceResponse.IsSuccess && programFundingSourceResponse.ProgramApplicationStatus.Count > 0)
            {
                programFundingSourceResponse.IsSuccess = true;
                return programFundingSourceResponse;
            }
            else
            {
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Couldn't find the Fund Allocation Status for this Program.";
                return programFundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAffiliateFundAllocation")]
        public DashboardAffiliateWiseFundAllocationStatusResponse GetAffiliateFundAllocation(long programID, int pageNumber)
        {
            var pageSize = 10;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            DashboardAffiliateWiseFundAllocationStatusResponse affiliateFundingSourceResponse = new DashboardAffiliateWiseFundAllocationStatusResponse();

            affiliateFundingSourceResponse = this._dashboardService.GetAffiliateFundAllocation(programID, pageSize, pageNumber);

            if (affiliateFundingSourceResponse.IsSuccess)
            {
                affiliateFundingSourceResponse.IsSuccess = true;
                return affiliateFundingSourceResponse;
            }
            else
            {
                affiliateFundingSourceResponse.IsSuccess = false;
                affiliateFundingSourceResponse.Message = "Couldn't find the affiliate fund allocation status for this program.";
                return affiliateFundingSourceResponse;
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAffiliateApplicationStatus")]
        public DashboardAffiliateWiseApplicationStatusResponse GetAffiliateApplicationStatus(long ProgramId, int pageNumber)
        {
            var pageSize = 10;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            DashboardAffiliateWiseApplicationStatusResponse affiliateFundingSourceResponse = new DashboardAffiliateWiseApplicationStatusResponse();


            affiliateFundingSourceResponse = this._dashboardService.GetAffiliateApplicationStatus(ProgramId, pageSize, pageNumber);

            if (affiliateFundingSourceResponse.IsSuccess && affiliateFundingSourceResponse.affiliateApplicationStatus.Count > 0)
            {
                affiliateFundingSourceResponse.IsSuccess = true;
                return affiliateFundingSourceResponse;
            }
            else
            {
                affiliateFundingSourceResponse.IsSuccess = false;
                affiliateFundingSourceResponse.Message = "Couldn't find the affiliate application status for this program.";
                return affiliateFundingSourceResponse;
            }

        }

        [HttpPost]
        [Route("FetchDashboardDetailsByReportType")]
        public DashboardReportDetailResponse FetchDashboardDetailsByReportType(DashboardDetailRequest request)
        {
            var response = new DashboardReportDetailResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }

            response = this._dashboardService.FetchDashboardDetailsByReportType(request);

            return response;
        }
        
        [HttpPost]
        [Route("FetchDashboardFundAllocationDetail")]
        public DashboardReportDetailResponse FetchDashboardFundAllocationDetail(StatusRequest request)
        {
            var response = new DashboardReportDetailResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }
            response = this._dashboardService.FetchDashboardFundAllocationDetail(request);
            return response;
        }
        [HttpPost]
        [Route("FetchDashboardApplicationStatsDetail")]
        public DashboardReportDetailResponse FetchDashboardApplicationStatsDetail(StatusRequest request)
        {
            var response = new DashboardReportDetailResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }
            response = this._dashboardService.FetchDashboardApplicationStatsDetail(request);
            return response;
        }

        [HttpPost]
        [Route("FetchDashboardProgramWiseFundAllocation")]
        public DashboardReportDetailResponse FetchDashboardProgramWiseFundAllocation(StatusRequest request)
        {
            var response = new DashboardReportDetailResponse();
            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }
            response = this._dashboardService.FetchDashboardProgramWiseFundAllocation(request);
            return response;
        }
        [HttpGet]
        [Route("FetchDashboardFundAllocationDetailByProgram")]
        public DashboardReportDetailResponse FetchDashboardFundAllocationDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }

            response = this._dashboardService.FetchDashboardFundAllocationDetailByProgram(ProgramID);

            return response;
        }
        [HttpGet]
        [Route("FetchDashboardApplicationStatsDetailByProgram")]
        public DashboardReportDetailResponse FetchDashboardApplicationStatsDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }

            response = this._dashboardService.FetchDashboardApplicationStatsDetailByProgram(ProgramID);

            return response;
        }

        [HttpGet]
        [Route("FetchDashboardAffiliateWiseFundUtilizedDetailByProgram")]
        public DashboardReportDetailResponse FetchDashboardAffiliateWiseFundUtilizedDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }

            response = this._dashboardService.FetchDashboardAffiliateWiseFundUtilizedDetailByProgram(ProgramID);

            return response;
        }
        [HttpGet]
        [Route("FetchDashboardAffiliateWiseApplicationStatsDetailByProgram")]
        public DashboardReportDetailResponse FetchDashboardAffiliateWiseApplicationStatsDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();

            if (LoginUserInformation.getLoggedInUser(HttpContext) == null)
            {
                response.IsSuccess = false;
                response.Message = "Token Expired.";
                return response;
            }

            response = this._dashboardService.FetchDashboardAffiliateWiseApplicationStatsDetailByProgram(ProgramID);

            return response;
        }
        #endregion Methods
    }
}