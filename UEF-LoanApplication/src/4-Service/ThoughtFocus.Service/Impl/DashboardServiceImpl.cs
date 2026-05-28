using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Service.Interfaces;
using FluentValidation;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Domain.Request;
using ThoughtFocus.Constants;

namespace ThoughtFocus.Service.Impl
{
    public class DashboardServiceImpl : IDashboardService
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly ILogger<FundingSourceService> _Logger;
        private IFundingSourceRepository _fundingSourceRepository;
        private readonly ILoanApplicationRepository _loanApplicationRepository;
        private readonly IProgramInvitationRepository _ProgramInvitationRepository;
        private readonly IFundUtilizationRepository _fundUtilizationRepository;

        #endregion Fields

        #region Constructors
        public DashboardServiceImpl(
         ILogger<FundingSourceService> logger,
         IMapper _mapper,
         IFundingSourceRepository fundingSourceRepository,
         ILoanApplicationRepository loanApplicationRepository,
         IProgramInvitationRepository ProgramInvitationRepository,
         IFundUtilizationRepository fundUtilizationRepository)
        {
            this._Logger = logger;
            this._mapper = _mapper;
            this._fundingSourceRepository = fundingSourceRepository;
            this._loanApplicationRepository = loanApplicationRepository;
            this._ProgramInvitationRepository = ProgramInvitationRepository;
            this._fundUtilizationRepository = fundUtilizationRepository;
        }

        #endregion Constructors

        #region Methods

        public DashboardResponse GetFoundAllocationStatus(long ProgramId)
        {
            DashboardResponse programFundingSourceResponse = new DashboardResponse();
            try
            {
                List<FundingSource> fundingSources = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (ProgramId != 0)
                {
                    fundingSources = fundingSources.Where(c => c.FundingSourceID == ProgramId).ToList();
                }

                var listFundAllocationViewEntity = fundingSources.Select(
                    fundingSource => new FundAllocationViewEntity
                    {
                        AvailableLimit = (fundingSource.FundTransactions == null || fundingSource.FundTransactions.Count == 0) ? 0 : getAvailableFundAmount(fundingSource.FundTransactions.ToList()),
                        UtilizedAmount = (fundingSource.FundTransactions == null || fundingSource.FundTransactions.Count == 0 ? 0 : getTotalUtilizedAmount(fundingSource.FundTransactions.ToList())),
                    }).ToList();

                if (listFundAllocationViewEntity != null)
                {
                    programFundingSourceResponse.IsSuccess = true;
                    programFundingSourceResponse.AvailableLimit = listFundAllocationViewEntity.Sum(x => x.AvailableLimit);
                    programFundingSourceResponse.UtilizedAmount = Decimal.Truncate(listFundAllocationViewEntity.Sum(x => x.UtilizedAmount));
                }
                else
                {
                    programFundingSourceResponse.IsSuccess = false;
                    programFundingSourceResponse.Message = "Couldn't find the fund allocation status for this program.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programFundingSourceResponse;
        }

        private decimal getAvailableFundAmount(List<FundTransaction> fundTransaction)
        {
            try
            {
                if (fundTransaction != null && fundTransaction.Count() > 0)
                {
                    decimal totalFundAdded = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 1).Sum(y => y.TransactionAmount);
                    decimal totalFundRemoved = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 2).Sum(y => y.TransactionAmount);
                    decimal totalFundAllocated = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 3).Sum(y => y.TransactionAmount);
                    return Decimal.Truncate((totalFundAdded - (totalFundRemoved + totalFundAllocated)));
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getAvailableFundAmmount", null);
            }

            return 0;
        }

        private decimal getTotalFundedAmount(List<FundTransaction> fundTransaction)
        {
            try
            {
                if (fundTransaction != null && fundTransaction.Count() > 0)
                {
                    decimal totalFundAdded = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 1).Sum(y => y.TransactionAmount);
                    decimal totalFundRemoved = fundTransaction.Where(x => x.IsActive == true && x.TransactionTypeID == 2).Sum(y => y.TransactionAmount);
                    return Decimal.Truncate((totalFundAdded - (totalFundRemoved)));
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getTotalFundedAmount", null);
            }
            return 0;
        }

        private decimal getTotalUtilizedAmount(List<FundTransaction> fundTransactions)
        {
            try
            {
                if (fundTransactions != null && fundTransactions.Count() > 0)
                {
                    return fundTransactions.Where(x => x.TransactionTypeID == 3).Sum(x => x.TransactionAmount);
                }
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> getTotalUtilizedAmount", null);
            }
            return 0;
        }

        public DashboardResponse GetFundAllocationProgramStatus(long ProgramId, int pageSize, int pageNumber)
        {
            DashboardResponse programFundingSourceResponse = new DashboardResponse();
            try
            {
                List<FundingSource> fundingSources = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (ProgramId != 0)
                {
                    fundingSources = fundingSources.Where(c => c.FundingSourceID == ProgramId).ToList();
                }
                if(fundingSources != null && fundingSources.Count > 0)
                {
                    var totalPages = ((double)fundingSources.Count / (double)pageSize);
                    int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                    programFundingSourceResponse.IsSuccess = true;
                    programFundingSourceResponse.TotalSize = fundingSources.Count;
                    programFundingSourceResponse.PageNumber = pageNumber;
                    programFundingSourceResponse.NextPage = pageNumber < roundedTotalPages ? true : false;
                    programFundingSourceResponse.PreviousPage = pageNumber > 1 ? true : false;
                    programFundingSourceResponse.TotalPages = roundedTotalPages;
                }
                var listFundAllocationViewEntity = fundingSources.Select(
                    fundingSource => new FundAllocationViewEntity
                    {
                        ProgramName = fundingSource.ProgramName,
                        AvailableLimit = (fundingSource.FundTransactions == null || fundingSource.FundTransactions.Count == 0) ? 0 : getAvailableFundAmount(fundingSource.FundTransactions.ToList()),
                        UtilizedAmount = (fundingSource.FundTransactions == null || fundingSource.FundTransactions.Count == 0 ? 0 : getTotalUtilizedAmount(fundingSource.FundTransactions.ToList())),
                        FundingSourceID = fundingSource.FundingSourceID                      

                    }).ToList();

                if (listFundAllocationViewEntity != null && listFundAllocationViewEntity.Count > 0)
                {
                    foreach (var item in listFundAllocationViewEntity)
                    {
                        item.TotalFundAllocation = item.UtilizedAmount + item.AvailableLimit;
                    }

                    var sortedListFundAllocation = listFundAllocationViewEntity.OrderByDescending(f => f.TotalFundAllocation).ToList();
                    //var sortedListFundAllocation = listFundAllocationViewEntity.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                    var fundAllocation = sortedListFundAllocation.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                    programFundingSourceResponse.IsSuccess = true;
                    programFundingSourceResponse.FundAllocationResponse = fundAllocation;

                }
                else
                {
                    programFundingSourceResponse.IsSuccess = false;
                    programFundingSourceResponse.Message = "Couldn't find the fund allocation status for this program.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                programFundingSourceResponse.IsSuccess = false;
                programFundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programFundingSourceResponse;
        }

        public DashboardResponse GetApplicationStatus(long ProgramID)
        {
            DashboardResponse programApplicationStatusResponse = new DashboardResponse();
            try
            {
                List<LoanApplication> loanApplication = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                List<ProgramInvitation> programInvitation = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true && c.ProgramStatusID == 1).ToList();

                if (ProgramID != 0)
                {
                    loanApplication = loanApplication.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                    programInvitation = programInvitation.Where(c => c.ProgramID == ProgramID).ToList();
                }

                int[] submittedStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31,32,33,34,35,37,38,39,41,43,44,45,46,47,48,49,50,51,52,53,54 };
                int[] activatedAccountStatus = { 1, 2, 3, 28, 29, 30 };
                int[] amountdisbursedStatus = { 13, 40 };

                int submittedApplicationCount = loanApplication.Where(x => submittedStatus.Contains(x.ApplicationStatusID)).Count();
                int activatedAccountCount = loanApplication.Where(x => activatedAccountStatus.Contains(x.ApplicationStatusID)).Count();
                int amountdisbursedCount = loanApplication.Where(x => amountdisbursedStatus.Contains(x.ApplicationStatusID)).Count();
                int noActionTaken = programInvitation.Count();

                if (submittedApplicationCount > 0 || activatedAccountCount > 0 || amountdisbursedCount > 0 || noActionTaken > 0)
                {
                    programApplicationStatusResponse.IsSuccess = true;
                    programApplicationStatusResponse.Submitted = submittedApplicationCount == 0 ? 0 : submittedApplicationCount;
                    programApplicationStatusResponse.ActivatedAccount = activatedAccountCount == 0 ? 0 : activatedAccountCount;
                    programApplicationStatusResponse.Amountdisbursed = amountdisbursedCount == 0 ? 0 : amountdisbursedCount;
                    programApplicationStatusResponse.InvitedNoActionTaken = noActionTaken == 0 ? 0 : noActionTaken;
                }
                else
                {
                    programApplicationStatusResponse.IsSuccess = false;
                    programApplicationStatusResponse.Message = "Couldn't find the Fund Allocation Status for this Program.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatus", null);
                programApplicationStatusResponse.IsSuccess = false;
                programApplicationStatusResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatus", null);
                programApplicationStatusResponse.IsSuccess = false;
                programApplicationStatusResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programApplicationStatusResponse;
        }


        public DashboardProgramWiseApplicationStatusResponse GetApplicationProgramStatus(long ProgramID, int pageSize, int pageNumber)
        {
            DashboardProgramWiseApplicationStatusResponse programApplicationStatusResponse = new DashboardProgramWiseApplicationStatusResponse();

            try
            {
                List<LoanApplication> loanApplication = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                List<ProgramInvitation> programInvitation = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (ProgramID != 0)
                {
                    loanApplication = loanApplication.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                    programInvitation = programInvitation.Where(c => c.ProgramID == ProgramID).ToList();
                }

                int[] submittedStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31, 32, 33, 34, 35, 37, 38, 39, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                int[] activatedAccountStatus = { 1, 2, 3, 28, 29, 30 };
                int[] amountdisbursedStatus = { 13, 40 };

                long[] programInvitationList = programInvitation.Select(x => x.ProgramID).Distinct().ToArray();

                Dictionary<string, ProgramApplicationStatus> programApplicationStatus = new Dictionary<string, ProgramApplicationStatus>();

                foreach (var programID in programInvitationList)
                {
                    ProgramApplicationStatus programStat = new ProgramApplicationStatus();

                    programStat.Submitted = loanApplication.Where(c => submittedStatus.Contains(c.ApplicationStatusID) && c.IsActive == true
                                            && c.ProgramInvitation.ProgramID == programID).Count();

                    programStat.ActivatedAccount = loanApplication.Where(c => activatedAccountStatus.Contains(c.ApplicationStatusID) && c.IsActive == true
                                                   && c.ProgramInvitation.ProgramID == programID).Count();

                    programStat.Amountdisbursed = loanApplication.Where(c => amountdisbursedStatus.Contains(c.ApplicationStatusID) && c.IsActive == true
                                                  && c.ProgramInvitation.ProgramID == programID).Count();

                    string programName = programInvitation.Where(x => x.ProgramID == programID).Select(x => x.FundingSource?.ProgramName).FirstOrDefault();

                    programStat.NoActionTaken = programInvitation.Where(x => x.ProgramID == programID && x.ProgramStatusID == 1).Count();

                    //Total programwise applications status
                    programStat.TotalProgramwiseApplicationStatus = Convert.ToInt64(programStat.Submitted)
                     + Convert.ToInt64(programStat.ActivatedAccount)
                     + programStat.Amountdisbursed
                     + programStat.NoActionTaken;
                    if (!programApplicationStatus.ContainsKey(programName))
                    {
                        programApplicationStatus.Add(programName, programStat);
                    }
                  
                }

                if (programApplicationStatus!= null && programApplicationStatus.Count > 0)
                {
                   var sortedProgramApplicationStatus = programApplicationStatus.OrderByDescending(p => p.Value.TotalProgramwiseApplicationStatus).ToDictionary(k => k.Key,v=>v.Value);
                    // var sortedProgramApplicationStatus = programApplicationStatus.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToDictionary(k => k.Key, k => k.Value);
                    var programApplication = sortedProgramApplicationStatus.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToDictionary(k => k.Key, k => k.Value);
                    programApplicationStatusResponse.IsSuccess = true;
                    programApplicationStatusResponse.ProgramApplicationStatus = programApplication;
                    programApplicationStatusResponse.PageNumber = pageNumber;

                    var totalPages = ((double)programApplicationStatus.Count / (double)pageSize);
                    int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                    programApplicationStatusResponse.TotalSize = programApplicationStatus.Count;
                    programApplicationStatusResponse.NextPage = pageNumber < roundedTotalPages ? true : false;
                    programApplicationStatusResponse.PreviousPage = pageNumber > 1 ? true : false;
                    programApplicationStatusResponse.TotalPages = roundedTotalPages;


                }
                else
                {
                    programApplicationStatusResponse.IsSuccess = false;
                    programApplicationStatusResponse.Message = "Couldn't find the Fund Allocation Status for this Program.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                programApplicationStatusResponse.IsSuccess = false;
                programApplicationStatusResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                programApplicationStatusResponse.IsSuccess = false;
                programApplicationStatusResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programApplicationStatusResponse;
        }

        public DashboardAffiliateWiseFundAllocationStatusResponse GetAffiliateFundAllocation(long ProgramID, int pageSize, int pageNumber)
        {
            DashboardAffiliateWiseFundAllocationStatusResponse affiliateFundingSourceResponse = new DashboardAffiliateWiseFundAllocationStatusResponse();

            try
            {
                List<LoanApplication> loanApplication = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true && (c.ApplicationStatusID == 13 || c.ApplicationStatusID == 40)).ToList();

                if (ProgramID != 0)
                {
                    loanApplication = loanApplication.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                }
                long[] affiliateIDs = loanApplication.Select(x => x.ProgramInvitation.BusinessEntity.AffiliateID).Distinct().ToArray();

                Dictionary<string, AffiliateFundAllocationStatus> affiliateFundAllocationStatus = new Dictionary<string, AffiliateFundAllocationStatus>();

                foreach (long id in affiliateIDs)
                {
                    AffiliateFundAllocationStatus affiliateFundAllocationRecords = new AffiliateFundAllocationStatus();
                    string affiliateName = loanApplication.Where(x => x.ProgramInvitation.BusinessEntity.AffiliateID == id).Select(x => x.ProgramInvitation.BusinessEntity.Affiliate.AffiliateName).FirstOrDefault();

                    long[] applicationIDs = loanApplication.Where(x => x.ProgramInvitation.BusinessEntity.AffiliateID == id).Select(x => x.LoanApplicationID).ToArray();
                    affiliateFundAllocationRecords.UtilizedAmount = _fundUtilizationRepository.GetAll().Where(x => x.TransactionTypeID == 3 && x.IsActive == true && x.FundingSource.IsActive == true && applicationIDs.Contains(x.ApplicationID)).Sum(s => s.TransactionAmount);
                    if (!affiliateFundAllocationStatus.ContainsKey(affiliateName))
                    {
                        affiliateFundAllocationStatus.Add(affiliateName, affiliateFundAllocationRecords);
                    }
                        
                }

                if (affiliateFundAllocationStatus != null && affiliateFundAllocationStatus.Count > 0)
                {
                    var sortedAffiliateFundAllocationStatus = affiliateFundAllocationStatus.OrderByDescending(o => o.Value.UtilizedAmount).ToDictionary(k=>k.Key,k=>k.Value);
                    //var sortedAffiliateFundAllocationStatus = affiliateFundAllocationStatus.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToDictionary(k => k.Key, k => k.Value);
                    var affiliateFundAllocation = sortedAffiliateFundAllocationStatus.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToDictionary(k => k.Key, k => k.Value);
                    affiliateFundingSourceResponse.affiliateFundAllocationStatus = affiliateFundAllocation;
                    var totalPages = ((double)affiliateFundAllocationStatus.Count / (double)pageSize);
                    int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                    affiliateFundingSourceResponse.IsSuccess = true;
                    affiliateFundingSourceResponse.PageNumber = pageNumber;
                    affiliateFundingSourceResponse.TotalSize = affiliateFundAllocationStatus.Count;
                    affiliateFundingSourceResponse.NextPage = pageNumber < roundedTotalPages ? true : false;
                    affiliateFundingSourceResponse.PreviousPage = pageNumber > 1 ? true : false;
                    affiliateFundingSourceResponse.TotalPages = roundedTotalPages;

                }
                else
                {
                    affiliateFundingSourceResponse.IsSuccess = false;
                    affiliateFundingSourceResponse.Message = "Couldn't find the affiliate fund allocation status for this program.";
                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                affiliateFundingSourceResponse.IsSuccess = false;
                affiliateFundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetApplicationStatusAllProgram", null);
                affiliateFundingSourceResponse.IsSuccess = false;
                affiliateFundingSourceResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return affiliateFundingSourceResponse;
        }

        public DashboardAffiliateWiseApplicationStatusResponse GetAffiliateApplicationStatus(long ProgramID, int pageSize, int pageNumber)
        {
            DashboardAffiliateWiseApplicationStatusResponse programApplicationStatusResponse = new DashboardAffiliateWiseApplicationStatusResponse();

            try
            {
                List<LoanApplication> loanApplication = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                List<ProgramInvitation> programInvitation = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                var programInvitationPage = new List<ProgramInvitation>();
                if (ProgramID != 0)
                {
                    loanApplication = loanApplication.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                    programInvitation = programInvitation.Where(c => c.ProgramID == ProgramID).ToList();

                }
                int[] submittedStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31, 32, 33, 34, 35, 37, 38, 39, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                int[] activatedAccountStatus = { 1, 2, 3, 28, 29, 30 };
                int[] amountdisbursedStatus = { 13, 40 };

                long[] AffiliateIDs = programInvitation.Select(x => x.BusinessEntity.AffiliateID).Distinct().ToArray();

                Dictionary<string, AffiliateApplicationStatus> affiliateApplicationStatus = new Dictionary<string, AffiliateApplicationStatus>();

                foreach (var AffiliateID in AffiliateIDs)
                {
                    AffiliateApplicationStatus affiliateApplicationRecords = new AffiliateApplicationStatus();

                    affiliateApplicationRecords.Submitted = loanApplication.Where(c => submittedStatus.Contains(c.ApplicationStatusID) && c.IsActive == true
                                            && c.ProgramInvitation.BusinessEntity.AffiliateID == AffiliateID).Count();

                    affiliateApplicationRecords.ActivatedAccount = loanApplication.Where(c => activatedAccountStatus.Contains(c.ApplicationStatusID) && c.IsActive == true
                                                   && c.ProgramInvitation.BusinessEntity.AffiliateID == AffiliateID).Count();

                    affiliateApplicationRecords.Amountdisbursed = loanApplication.Where(c => amountdisbursedStatus.Contains(c.ApplicationStatusID) && c.IsActive == true
                                                  && c.ProgramInvitation.BusinessEntity.AffiliateID == AffiliateID).Count();

                    string affiliateName = programInvitation.Where(x => x.BusinessEntity.AffiliateID == AffiliateID).Select(x => x.BusinessEntity.Affiliate?.AffiliateName).FirstOrDefault();
                    affiliateApplicationRecords.NoActionTaken = programInvitation.Where(x => x.BusinessEntity.AffiliateID == AffiliateID && x.ProgramStatusID == 1).Count();

                    affiliateApplicationRecords.TotalProgramInvitationCount = Convert.ToInt64(affiliateApplicationRecords.Submitted)
                                                                              + Convert.ToInt64(affiliateApplicationRecords.ActivatedAccount)
                                                                              + affiliateApplicationRecords.Amountdisbursed
                                                                              + affiliateApplicationRecords.NoActionTaken;
                    if (!affiliateApplicationStatus.ContainsKey(affiliateName))
                    {
                        affiliateApplicationStatus.Add(affiliateName, affiliateApplicationRecords);
                    }


                }

                if (affiliateApplicationStatus != null && affiliateApplicationStatus.Count > 0)
                {
                    var sortAffiliateByProgramInvitation = affiliateApplicationStatus.OrderByDescending(x => x.Value.TotalProgramInvitationCount).ToDictionary(k=>k.Key,k=>k.Value);
                    //var affiliateByProgramInvitation = sortAffiliateByProgramInvitation.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToDictionary(k => k.Key, k => k.Value);
                    var affiliateByProgramInvitation = sortAffiliateByProgramInvitation.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToDictionary(k => k.Key, k => k.Value);
                    programApplicationStatusResponse.TotalSize = affiliateApplicationStatus.Count;
                    var totalPages = ((double)affiliateApplicationStatus.Count / (double)pageSize);
                    int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                    programApplicationStatusResponse.IsSuccess = true;
                    programApplicationStatusResponse.affiliateApplicationStatus = affiliateByProgramInvitation;
                    programApplicationStatusResponse.PageNumber = pageNumber;
                    programApplicationStatusResponse.NextPage = pageNumber < roundedTotalPages ? true : false;
                    programApplicationStatusResponse.PreviousPage = pageNumber > 1 ? true : false;
                    programApplicationStatusResponse.TotalPages = roundedTotalPages;

                }
                else
                {
                    programApplicationStatusResponse.IsSuccess = false;
                    programApplicationStatusResponse.Message = "Couldn't find the affiliate application status for this program.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetAffiliateApplicationStatus", null);
                programApplicationStatusResponse.IsSuccess = false;
                programApplicationStatusResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> GetAffiliateApplicationStatus", null);
                programApplicationStatusResponse.IsSuccess = false;
                programApplicationStatusResponse.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return programApplicationStatusResponse;
        }
        public DashboardReportDetailResponse FetchDashboardDetailsByReportType(DashboardDetailRequest request)
        {
            var response = new DashboardReportDetailResponse();
            try
            {
                
                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && request.ProgramID != 0)
                {
                    FundingSource= FundingSource.Where(c => c.FundingSourceID== request.ProgramID).ToList();
                }
               
                var LoanApplications = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                var ProgramInvitations = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                
                if (request.ProgramID != 0)
                {
                    LoanApplications = LoanApplications.Where(c => c.ProgramInvitation.ProgramID == request.ProgramID).ToList();
                    ProgramInvitations = ProgramInvitations.Where(c => c.ProgramID == request.ProgramID).ToList();
                }
                int[] submittedOnWardsApplicationStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31, 32, 33, 34, 35, 37, 38, 39, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                int[] startedApplicationStatus = { 1, 2, 3, 28, 29, 30 };
                int[] fundedApplicationStatus = { 13, 40 };

                if (request.ReportType == CommonConstants.FundAllocationFlag)
                {
                    response.FundAllocationDetail = this.FetchFundDetailsProgramWise(FundingSource);
                    response.IsSuccess = true;
                }
                else if (request.ReportType == CommonConstants.ApplicationStatsFlag)
                {
                    response.ApplicationStatsDetail = this.FetchApplicationStatsDetailsProgramWise(startedApplicationStatus, submittedOnWardsApplicationStatus, fundedApplicationStatus, FundingSource, LoanApplications, ProgramInvitations);
                    response.IsSuccess = true;
                }
               else if (request.ReportType== CommonConstants.ProgramWiseFundAllocationFlag)
                {
                    response.FundDetailProgramWise = this.FetchFundDetailsProgramWise(FundingSource);
                    response.IsSuccess = true;
                }
                else if (request.ReportType == CommonConstants.ProgramWiseApplicationStatsFlag)
                {
                    response.ApplicationStatsDetailProgramWise = this.FetchApplicationStatsDetailsProgramWise(startedApplicationStatus, submittedOnWardsApplicationStatus, fundedApplicationStatus, FundingSource, LoanApplications, ProgramInvitations);
                    response.IsSuccess = true;
                }
                else if(request.ReportType == CommonConstants.AffiliateWiseFundUtilizedFlag)
                {
                    response.FundDetailAffiliateWise = this.FetchFundDetailsAffiliateWise(LoanApplications, ProgramInvitations);
                    response.IsSuccess = true;
                }                
                else if(request.ReportType == CommonConstants.AffiliateWiseApplicationStatsFlag)
                {
                    response.ApplicationStatsDetailProgramWise = this.FetchApplicationStatsDetailsAffiliateWise(startedApplicationStatus, submittedOnWardsApplicationStatus, fundedApplicationStatus, LoanApplications, ProgramInvitations);
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardDetailsByReportType", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardDetailsByReportType", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        public DashboardReportDetailResponse FetchDashboardFundAllocationDetail(StatusRequest request)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && request.ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == request.ProgramID).ToList();
                }
                if (request.Status == CommonConstants.AvailableLimitFlag)
                {
                    response.FundAllocationDetail = this.FetchFundDetailsAvailableLimit(FundingSource);
                    response.IsSuccess = true;
                }
                else if (request.Status == CommonConstants.UtilizedFlag)
                {
                    response.FundAllocationDetail = this.FetchFundDetailsUtilizedAmount(FundingSource);
                    response.IsSuccess = true;
                }
                
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardDetailsByReportType", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardDetailsByReportType", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        private List<ApplicationStatsDetailReport> FetchApplicationStatsDetailsProgramWise(int[] startedApplicationStatus, int[] submittedOnWardsApplicationStatus, int[] fundedApplicationStatus, List<ThoughtFocus.DataAccess.Models.FundingSource.FundingSource> FundingSource, List<LoanApplication> LoanApplication, List<ProgramInvitation> ProgramInvitation)
        {
            var report = new List<ApplicationStatsDetailReport>();
            try
            {  
                foreach (var program in FundingSource)
                {
                    var statsReport = new ApplicationStatsDetailReport();
                    statsReport.FundingEntityName= program.FundingEntity.FundingEntityName;
                    statsReport.ProgramName = program.ProgramName;
                    statsReport.NoAction = ProgramInvitation.Where(x => x.ProgramID== program.FundingSourceID && x.ProgramStatusID == 1).Count().ToString("#,##0");
                    statsReport.Started = LoanApplication.Where(c => startedApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.FundingSource.FundingSourceID == program.FundingSourceID).Count().ToString("#,##0");
                    statsReport.Inprogress = LoanApplication.Where(c => submittedOnWardsApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.FundingSource.FundingSourceID == program.FundingSourceID).Count().ToString("#,##0");
                    statsReport.Funded = LoanApplication.Where(c => fundedApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.FundingSource.FundingSourceID == program.FundingSourceID).Count().ToString("#,##0");

                    report.Add(statsReport);
                }
                return report;
            }
            catch
            {
                return report;
            }
             
        }
        private List<ApplicationStatsDetailReport> FetchApplicationStatsDetailsAffiliateWise(int[] startedApplicationStatus, int[] submittedOnWardsApplicationStatus, int[] fundedApplicationStatus, List<LoanApplication> LoanApplication, List<ProgramInvitation> ProgramInvitation)
        {
            var report = new List<ApplicationStatsDetailReport>();
            try
            {
                long[] AffiliateIDs = ProgramInvitation.Select(x => x.BusinessEntity.AffiliateID).Distinct().ToArray();
                foreach (var id in AffiliateIDs)
                {
                    var statsReport = new ApplicationStatsDetailReport();
                    var FundingSource = ProgramInvitation.Where(x => x.BusinessEntity.AffiliateID == id).Select(s => s.FundingSource).ToList();

                    if (FundingSource != null)
                    {
                        statsReport.FundingEntityName = FundingSource.FirstOrDefault().FundingEntity.FundingEntityName;
                        statsReport.ProgramName = FundingSource.FirstOrDefault().ProgramName;
                    }
                    statsReport.AffiliateName= ProgramInvitation.Where(x => x.BusinessEntity.AffiliateID == id).Select(x => x.BusinessEntity.Affiliate?.AffiliateName).FirstOrDefault();
                    statsReport.NoAction = ProgramInvitation.Where(x => x.BusinessEntity.AffiliateID == id && x.ProgramStatusID == 1).Count().ToString("#,##0");
                    statsReport.Started = LoanApplication.Where(c => startedApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.BusinessEntity.AffiliateID == id).Count().ToString("#,##0");
                    statsReport.Inprogress = LoanApplication.Where(c => submittedOnWardsApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.BusinessEntity.AffiliateID == id).Count().ToString("#,##0");
                    statsReport.Funded = LoanApplication.Where(c => fundedApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.BusinessEntity.AffiliateID == id).Count().ToString("#,##0");

                    report.Add(statsReport);
                }
                return report;
            }
            catch
            {
                return report;
            }

        }
        #endregion Methods
        private List<FundDetailReport> FetchFundDetailsProgramWise(List<ThoughtFocus.DataAccess.Models.FundingSource.FundingSource> FundingSource)
        {
            var report = new List<FundDetailReport>();
            try
            {              
                if (FundingSource != null && FundingSource.Count > 0)
                {

                    report = FundingSource.Select(
                    fs => new FundDetailReport
                    {
                        FundingEntityName = fs.FundingEntity.FundingEntityName,
                        ProgramName = fs.ProgramName,
                        AvailableLimit = (fs.FundTransactions == null || fs.FundTransactions.Count == 0) ? "$ 0" : "$ "+ this.getAvailableFundAmount(fs.FundTransactions.ToList()).ToString("#,##0"),
                        UtilizedAmount = (fs.FundTransactions == null || fs.FundTransactions.Count == 0) ? "$ 0" : "$ " + this.getTotalUtilizedAmount(fs.FundTransactions.ToList()).ToString("#,##0"),
                        
                    }).ToList();
                }
                return report;
            }
            catch
            {
                return report;
            }
        }
        private List<FundDetailReport> FetchFundDetailsAvailableLimit(List<ThoughtFocus.DataAccess.Models.FundingSource.FundingSource> FundingSource)
        {
            var report = new List<FundDetailReport>();
            try
            {
                if (FundingSource != null && FundingSource.Count > 0)
                {

                    report = FundingSource.Select(
                    fs => new FundDetailReport
                    {
                        FundingEntityName = fs.FundingEntity.FundingEntityName,
                        ProgramName = fs.ProgramName,
                        AvailableLimit = (fs.FundTransactions == null || fs.FundTransactions.Count == 0) ? "$ 0" : "$ " + this.getAvailableFundAmount(fs.FundTransactions.ToList()).ToString("#,##0"),

                    }).ToList();
                }
                return report;
            }
            catch
            {
                return report;
            }
        }
        private List<FundDetailReport> FetchFundDetailsUtilizedAmount(List<ThoughtFocus.DataAccess.Models.FundingSource.FundingSource> FundingSource)
        {
            var report = new List<FundDetailReport>();
            try
            {
                if (FundingSource != null && FundingSource.Count > 0)
                {

                    report = FundingSource.Select(
                    fs => new FundDetailReport
                    {
                        FundingEntityName = fs.FundingEntity.FundingEntityName,
                        ProgramName = fs.ProgramName,                        
                        UtilizedAmount = (fs.FundTransactions == null || fs.FundTransactions.Count == 0) ? "$ 0" : "$ " + this.getTotalUtilizedAmount(fs.FundTransactions.ToList()).ToString("#,##0"),
                    }).ToList();
                }
                return report;
            }
            catch
            {
                return report;
            }
        }
        private List<FundDetailReport> FetchFundDetailsAffiliateWise(List<LoanApplication> LoanApplications, List<ProgramInvitation> ProgramInvitation)
        {
            var report = new List<FundDetailReport>();
            try
            {
                
                long[] fundUtilizedAffiliateIDs = LoanApplications.Where(c => c.ApplicationStatusID == 13 || c.ApplicationStatusID == 40).Select(x => x.ProgramInvitation.BusinessEntity.AffiliateID).Distinct().ToArray();
                foreach (long id in fundUtilizedAffiliateIDs)
                {
                    var detail = new FundDetailReport();
                    detail.AffiliateName = LoanApplications.Where(x => x.ProgramInvitation.BusinessEntity.AffiliateID == id).Select(x => x.ProgramInvitation.BusinessEntity.Affiliate.AffiliateName).FirstOrDefault();

                    long[] applicationIDs = LoanApplications.Where(x => x.ProgramInvitation.BusinessEntity.AffiliateID == id).Select(x => x.LoanApplicationID).ToArray();
                    detail.UtilizedAmount = "$ "+_fundUtilizationRepository.GetAll().Where(x => x.TransactionTypeID == 3 && x.IsActive == true && x.FundingSource.IsActive == true && applicationIDs.Contains(x.ApplicationID)).Sum(s => s.TransactionAmount).ToString("#,##0");

                    var FundingSource = ProgramInvitation.Where(x => x.BusinessEntity.AffiliateID == id).Select(s => s.FundingSource).ToList();
                    
                    if (FundingSource != null)
                    {

                        detail.FundingEntityName = FundingSource.FirstOrDefault().FundingEntity.FundingEntityName;
                        detail.ProgramName = FundingSource.FirstOrDefault().ProgramName;
                        var FundTransactions = FundingSource.Select(
                           fs => new
                           {                               
                               AvailableLimit = (fs.FundTransactions == null || fs.FundTransactions.Count == 0) ? 0 : this.getAvailableFundAmount(fs.FundTransactions.ToList()),
                               
                           }).ToList();
                         
                        if(FundTransactions!=null || FundTransactions.Count()> 0)
                        {

                            detail.AvailableLimit ="$ "+ FundTransactions.Sum(s=>s.AvailableLimit).ToString("#,##0");
                        }
                        else
                        {
                            detail.AvailableLimit = "$ 0";
                        }
                    }
                    report.Add(detail);
                    report = report?.OrderByDescending(o => o.UtilizedAmount).ToList();
                }

                return report;
            }
            catch
            {
                return report;
            }
        }
        public DashboardReportDetailResponse FetchDashboardApplicationStatsDetail(StatusRequest request)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && request.ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == request.ProgramID).ToList();
                }

                var LoanApplications = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                var ProgramInvitations = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (request.ProgramID != 0)
                {
                    LoanApplications = LoanApplications.Where(c => c.ProgramInvitation.ProgramID == request.ProgramID).ToList();
                    ProgramInvitations = ProgramInvitations.Where(c => c.ProgramID == request.ProgramID).ToList();
                }
                int[] submittedOnWardsApplicationStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31, 32, 33, 34, 35, 37, 38, 39, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                int[] startedApplicationStatus = { 1, 2, 3, 28, 29, 30 };
                int[] fundedApplicationStatus = { 13, 40 };

              
               if(!string.IsNullOrEmpty(request.Status))
                {
                    response.ApplicationStatsDetail = this.FetchApplicationStatsDetail(request.Status,startedApplicationStatus, submittedOnWardsApplicationStatus, fundedApplicationStatus, FundingSource, LoanApplications, ProgramInvitations);
                    response.IsSuccess = true;
                }                
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardApplicationStatsDetail", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardApplicationStatsDetail", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        private List<ApplicationStatsDetailReport> FetchApplicationStatsDetail(string status, int[] startedApplicationStatus, int[] submittedOnWardsApplicationStatus, int[] fundedApplicationStatus, List<ThoughtFocus.DataAccess.Models.FundingSource.FundingSource> FundingSource, List<LoanApplication> LoanApplication, List<ProgramInvitation> ProgramInvitation)
        {
            var report = new List<ApplicationStatsDetailReport>();
            try
            {
                foreach (var program in FundingSource)
                {
                    var statsReport = new ApplicationStatsDetailReport();
                    statsReport.FundingEntityName = program.FundingEntity.FundingEntityName;
                    statsReport.ProgramName = program.ProgramName;
                    if (status == CommonConstants.NoActionFlag)
                    {
                        statsReport.NoAction = ProgramInvitation.Where(x => x.ProgramID == program.FundingSourceID && x.ProgramStatusID == 1).Count().ToString("#,##0");

                    }
                    else if (status == CommonConstants.StartedFlag)
                    {
                        statsReport.Started = LoanApplication.Where(c => startedApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.FundingSource.FundingSourceID == program.FundingSourceID).Count().ToString("#,##0");

                    }
                    else if (status == CommonConstants.InprogressFlag)
                    {
                        statsReport.Inprogress = LoanApplication.Where(c => submittedOnWardsApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.FundingSource.FundingSourceID == program.FundingSourceID).Count().ToString("#,##0");

                    }
                    else if (status == CommonConstants.FundedFlag)
                    {
                        statsReport.Funded = LoanApplication.Where(c => fundedApplicationStatus.Contains(c.ApplicationStatusID) && c.IsActive == true && c.ProgramInvitation.FundingSource.FundingSourceID == program.FundingSourceID).Count().ToString("#,##0");
                    }
                    report.Add(statsReport);
                }
                return report;
            }
            catch
            {
                return report;
            }

        }
        public DashboardReportDetailResponse FetchDashboardProgramWiseFundAllocation(StatusRequest request)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && request.ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == request.ProgramID).ToList();
                }
                if (request.Status == CommonConstants.AvailableLimitFlag)
                {
                    response.FundDetailProgramWise = this.FetchFundDetailsAvailableLimit(FundingSource);
                    response.IsSuccess = true;
                }
                else if (request.Status == CommonConstants.UtilizedFlag)
                {
                    response.FundDetailProgramWise = this.FetchFundDetailsUtilizedAmount(FundingSource);
                    response.IsSuccess = true;
                }

                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardProgramWiseFundAllocation", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardProgramWiseFundAllocation", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        public DashboardReportDetailResponse FetchDashboardFundAllocationDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == ProgramID).ToList();
                }

                if (FundingSource!=null && FundingSource.Count>0)
                {
                    response.Programs = FundingSource.Select(s => new ProgramResponse
                    {
                        ProgramId = s.FundingSourceID,
                        ProgramName = s.ProgramName
                    }).ToList();
                    response.Programs = response.Programs.Distinct().OrderBy(o=>o.ProgramName).ToList();

                    response.FundAllocationDetail = this.FetchFundDetailsProgramWise(FundingSource);
                    if (response.FundAllocationDetail != null && response.FundAllocationDetail.Count() > 0)
                    {
                        response.FundAllocationDetail = response.FundAllocationDetail.OrderBy(o => o.FundingEntityName).ToList();
                    }
                    response.IsSuccess = true;                
                }                
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardFundAllocationDetailByProgram", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardFundAllocationDetailByProgram", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        public DashboardReportDetailResponse FetchDashboardApplicationStatsDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == ProgramID).ToList();
                }

                var LoanApplications = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                var ProgramInvitations = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (ProgramID != 0)
                {
                    LoanApplications = LoanApplications.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                    ProgramInvitations = ProgramInvitations.Where(c => c.ProgramID == ProgramID).ToList();
                }
                int[] submittedOnWardsApplicationStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31, 32, 33, 34, 35, 37, 38, 39, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                int[] startedApplicationStatus = { 1, 2, 3, 28, 29, 30 };
                int[] fundedApplicationStatus = { 13, 40 };


                if (FundingSource!=null && FundingSource.Count>0)
                {
                    response.Programs = FundingSource.Select(s => new ProgramResponse
                    {
                        ProgramId = s.FundingSourceID,
                        ProgramName = s.ProgramName
                    }).ToList();
                    response.Programs = response.Programs.Distinct().OrderBy(o => o.ProgramName).ToList();

                    response.ApplicationStatsDetail = this.FetchApplicationStatsDetailsProgramWise(startedApplicationStatus, submittedOnWardsApplicationStatus, fundedApplicationStatus, FundingSource, LoanApplications, ProgramInvitations);
                    if(response.ApplicationStatsDetail!=null && response.ApplicationStatsDetail.Count() > 0)
                    {
                        response.ApplicationStatsDetail = response.ApplicationStatsDetail.OrderBy(o => o.FundingEntityName).ToList();
                    }
                    
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardApplicationStatsDetail", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardApplicationStatsDetail", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        public DashboardReportDetailResponse FetchDashboardAffiliateWiseFundUtilizedDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == ProgramID).ToList();
                }
                var LoanApplications = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                var ProgramInvitations = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (ProgramID != 0)
                {
                    LoanApplications = LoanApplications.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                    ProgramInvitations = ProgramInvitations.Where(c => c.ProgramID == ProgramID).ToList();
                }
                if (FundingSource != null && FundingSource.Count > 0)
                {
                    response.Programs = FundingSource.Select(s => new ProgramResponse
                    {
                        ProgramId = s.FundingSourceID,
                        ProgramName = s.ProgramName
                    }).ToList();
                    response.Programs = response.Programs.Distinct().OrderBy(o => o.ProgramName).ToList();

                    response.FundDetailAffiliateWise = this.FetchFundDetailsAffiliateWise(LoanApplications, ProgramInvitations);
                    if (response.FundDetailAffiliateWise != null && response.FundDetailAffiliateWise.Count() > 0)
                    {
                        response.FundDetailAffiliateWise = response.FundDetailAffiliateWise.OrderBy(o => o.AffiliateName).ToList();
                    }
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardFundAllocationDetailByProgram", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardFundAllocationDetailByProgram", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
        public DashboardReportDetailResponse FetchDashboardAffiliateWiseApplicationStatsDetailByProgram(long ProgramID)
        {
            var response = new DashboardReportDetailResponse();
            try
            {

                var FundingSource = this._fundingSourceRepository.GetAll().Where(c => c.IsActive == true).ToList();
                if (FundingSource != null && ProgramID != 0)
                {
                    FundingSource = FundingSource.Where(c => c.FundingSourceID == ProgramID).ToList();
                }
                var LoanApplications = this._loanApplicationRepository.GetAll().Where(c => c.IsActive == true).ToList();
                var ProgramInvitations = this._ProgramInvitationRepository.GetAll().Where(c => c.IsActive == true).ToList();

                if (ProgramID != 0)
                {
                    LoanApplications = LoanApplications.Where(c => c.ProgramInvitation.ProgramID == ProgramID).ToList();
                    ProgramInvitations = ProgramInvitations.Where(c => c.ProgramID == ProgramID).ToList();
                }
                int[] submittedOnWardsApplicationStatus = { 4, 5, 6, 7, 8, 10, 11, 12, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 31, 32, 33, 34, 35, 37, 38, 39, 41, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
                int[] startedApplicationStatus = { 1, 2, 3, 28, 29, 30 };
                int[] fundedApplicationStatus = { 13, 40 };
                if (FundingSource != null && FundingSource.Count > 0)
                {
                    response.Programs = FundingSource.Select(s => new ProgramResponse
                    {
                        ProgramId = s.FundingSourceID,
                        ProgramName = s.ProgramName
                    }).ToList();
                    response.Programs = response.Programs.Distinct().OrderBy(o => o.ProgramName).ToList();

                    response.ApplicationStatsDetailAffiliateWise = this.FetchApplicationStatsDetailsAffiliateWise(startedApplicationStatus, submittedOnWardsApplicationStatus, fundedApplicationStatus, LoanApplications, ProgramInvitations);
                    if (response.ApplicationStatsDetailAffiliateWise != null && response.ApplicationStatsDetailAffiliateWise.Count() > 0)
                    {
                        response.ApplicationStatsDetailAffiliateWise = response.ApplicationStatsDetailAffiliateWise.OrderBy(o => o.AffiliateName).ToList();
                    }
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Couldn't find the details.";

                }
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardFundAllocationDetailByProgram", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in DashboardService-> FetchDashboardFundAllocationDetailByProgram", null);
                response.IsSuccess = false;
                response.Message = "Unable to retrieve data at this moment. Please try after sometime.";
            }
            return response;
        }
    }   
}

