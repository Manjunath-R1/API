using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Request;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IDashboardService
    {
        #region Methods

        /// <summary>
        /// This method is used to get the FoundAllocation AvailableLimit and UtilizedAmount amount
        /// </summary>
        /// <param name="programId">programId</param>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DashboardResponse GetFoundAllocationStatus(long programId);

        /// <summary>
        /// This method is used to get the FoundAllocation AvailableLimit and UtilizedAmount amount for all programs
        /// </summary>
        /// <param name="programId">programId</param>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DashboardResponse GetFundAllocationProgramStatus(long ProgramId, int pageSize, int pageNumber);
        /// <summary>
        /// This method is used to get the ApplicationStatus all programs
        /// </summary>
        /// <param name="programId">programId</param>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DashboardResponse GetApplicationStatus(long programId);
        /// <summary>
        /// This method is used to get the ApplicationStatus based on program
        /// </summary>
        /// <param name="programId">programId</param>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DashboardProgramWiseApplicationStatusResponse GetApplicationProgramStatus(long programId, int pageSize, int pageNumber);

        /// <summary>
        /// This method is used to get the ApplicationStatus based on affiliate
        /// </summary>
        /// <param name="programId">programId</param>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DashboardAffiliateWiseApplicationStatusResponse GetAffiliateApplicationStatus(long programId, int pageSize, int pageNumber);

        /// <summary>
        /// This method is used to get the FoundAllocation UtilizedAmount amount for all programs basis on affiliate
        /// </summary>
        /// <param name="programId">programId</param>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DashboardAffiliateWiseFundAllocationStatusResponse GetAffiliateFundAllocation(long programID, int pageSize, int pageNumber);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="programId">programId</param> 
        DashboardReportDetailResponse FetchDashboardDetailsByReportType(DashboardDetailRequest request);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="programId">programId</param> 
        /// <param name="status">programId</param> 
        DashboardReportDetailResponse FetchDashboardFundAllocationDetail(StatusRequest request);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="programId">programId</param> 
        /// <param name="status">programId</param> 
        DashboardReportDetailResponse FetchDashboardApplicationStatsDetail(StatusRequest request);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="programId">programId</param> 
        /// <param name="status">programId</param> 
        DashboardReportDetailResponse FetchDashboardProgramWiseFundAllocation(StatusRequest request);

        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="ProgramID">programId</param> 
        DashboardReportDetailResponse FetchDashboardFundAllocationDetailByProgram(long ProgramID);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="ProgramID">programId</param> 
        DashboardReportDetailResponse FetchDashboardApplicationStatsDetailByProgram(long ProgramID);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="ProgramID">programId</param> 
        DashboardReportDetailResponse FetchDashboardAffiliateWiseFundUtilizedDetailByProgram(long ProgramID);
        /// <summary>
        /// This method is used to get the Found & application stats detail for programs.
        /// </summary>
        /// <param name="ProgramID">programId</param> 
        DashboardReportDetailResponse FetchDashboardAffiliateWiseApplicationStatsDetailByProgram(long ProgramID);
        #endregion Methods


    }
}