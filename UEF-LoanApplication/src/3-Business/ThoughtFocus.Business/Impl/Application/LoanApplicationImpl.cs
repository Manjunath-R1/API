namespace ThoughtFocus.Business.Impl.Application
{
    using System;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.Business.Interfaces.Application;
    using ThoughtFocus.Domain.Params;
    using ThoughtFocus.Domain.Response;
    using ThoughtFocus.Common.Exceptions.BusinessException;
    using ThoughtFocus.Domain.Common;
    using ThoughtFocus.Domain.CustomView;
    using System.Collections.Generic;
    using ThoughtFocus.Repository.Interfaces.Application;
    using System.Linq;
    using ThoughtFocus.Domain.Enumeration;
    using ThoughtFocus.Repository.Interfaces.FundingSource;

    public class LoanApplicationImpl : ILoanApplication
    {
        #region Fields

        /// <summary>
        /// ILog instance for logging.
        /// </summary>
        private readonly ILogger<LoanApplicationImpl> _logger;
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        private readonly IFundingSourceRepository _fundingSourceRepository;
        private readonly ILoanBusinessDetailRepository _loanBusinessDetailRepository;
        #endregion Fields

        #region Constructors
        public LoanApplicationImpl(ILogger<LoanApplicationImpl> logger, ILoanApplicationRepository loanApplicationRepository, IFundingSourceRepository fundingSourceRepository, ILoanBusinessDetailRepository loanBusinessDetailRepository)
        {
            this._logger = logger;
            this._LoanApplicationRepository = loanApplicationRepository;
            this._fundingSourceRepository = fundingSourceRepository;
            this._loanBusinessDetailRepository = loanBusinessDetailRepository;
        }

        #endregion Constructors

        public string GenerateLoanNumber(LoanApplicationRequest LoanApplicationParam, long applicationCount)
        {
            string LoanNumber = null;
            try
            {
                LoanNumber = "NUL-UEF-#000" + applicationCount.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while generating loan number LoanApplicationImpl -> GenerateLoanNumber ", ex);
            }
            return LoanNumber;
        }

        public ApplicationListResponse GetAllLoanApplicationInformation(PageFilterEntity PageFilter, long UserID)
        {
            ApplicationListResponse applicationListResponse = new ApplicationListResponse();
            applicationListResponse.ApplicationPageResultEntity = new PageResultEntity<ApplicationListingViewEntity>();
            List<ApplicationListingViewEntity> listOfApplicationListingViewEntity = null;
            int totalRecordCount = 0;
            string sortExpression = "";
            try
            {

                List<long> ProgramIDs = PageFilter.FilterParameters.Where(a => a.Key == "ProgramID").Select(a => Convert.ToInt64(a.Value)).ToList();

                IQueryable<ThoughtFocus.DataAccess.Models.Application.LoanApplication> query = this._LoanApplicationRepository.GetLoanApplications(UserID, ProgramIDs);

                if (query.Count() == 0 && ProgramIDs.FirstOrDefault() > 0)
                {
                    applicationListResponse.IsSuccess = false;
                    applicationListResponse.Message = "Couldn't find the grant allocation for this program.";
                    applicationListResponse.ID = ProgramIDs.FirstOrDefault();
                    return applicationListResponse;
                }

                totalRecordCount = query.Count();
                if (PageFilter.SortBy != null)
                {
                    if (PageFilter.SortDirection == "ascending")
                    {
                        sortExpression = PageFilter.SortBy;
                    }
                    else if (PageFilter.SortDirection == "descending")
                    {
                        sortExpression = PageFilter.SortBy + " DESC";
                    }
                }
                else
                {
                    sortExpression = "BusinessName" + " ASC";
                }
                if (PageFilter.IsColumnFilter)
                    PageFilter.PageNumber = 1;

                if (PageFilter.FilterParameters != null)
                {
                    //First Name filter
                    if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.LoanNumber.ToString()))
                    {
                        string pageFilterLoanNumber = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.LoanNumber.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterLoanNumber != null)
                        {
                            string LoanNumber = pageFilterLoanNumber.Trim();
                            query = query.Where(a => a.LoanNumber.Contains(LoanNumber));
                        }
                    }

                    // if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.DateApplied.ToString()))
                    // {
                    //     string pageFilterDateApplied = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.DateApplied.ToString()).Select(a => a.Value).FirstOrDefault();
                    //     if (pageFilterDateApplied != null)
                    //     {
                    //         string DateApplied = pageFilterDateApplied.Trim();
                    //         query = query.Where(a => a.DateApplied == Convert.ToDateTime(DateApplied));
                    //     }
                    // }

                    if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.BusinessName.ToString()))
                    {
                        string pageFilterBusinessName = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.BusinessName.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterBusinessName != null)
                        {
                            string BusinessName = pageFilterBusinessName.Trim();
                            query = query.Where(a => a.LoanBusinessDetail.BusinessName.Contains(BusinessName));
                        }
                    }

                    //  if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.LoanProgramName.ToString()))
                    // {
                    //     string pageFilterLoanProgramName = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.LoanProgramName.ToString()).Select(a => a.Value).FirstOrDefault();
                    //     if (pageFilterLoanProgramName != null)
                    //     {
                    //         string BusinessName = pageFilterLoanProgramName.Trim();
                    //         query = query.Where(a => a.LoanProgramName.Contains(LoanProgramName));
                    //     }
                    // }


                    if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.LoanType.ToString()))
                    {
                        string pageFilterLoanType = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.LoanType.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterLoanType != null)
                        {
                            string LoanType = pageFilterLoanType.Trim();
                            query = query.Where(a => a.ApplicationType.ApplicationTypeName == LoanType);
                        }
                    }

                    //  if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.LoanAmount.ToString()))
                    // {
                    //     string pageFilterLoanAmount = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.LoanAmount.ToString()).Select(a => a.Value).FirstOrDefault();
                    //     if (pageFilterLoanAmount != null)
                    //     {
                    //         string LoanType = pageFilterLoanAmount.Trim();
                    //         query = query.Where(a => a.ApplicationType.ApplicationTypeName == LoanType);
                    //     }
                    // }

                    if (PageFilter.FilterParameters.Any(a => a.Key == ApplicationListFilters.ApplicationStatus.ToString()))
                    {
                        string pageFilterApplicationStatus = PageFilter.FilterParameters.Where(a => a.Key == ApplicationListFilters.ApplicationStatus.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterApplicationStatus != null)
                        {
                            string ApplicationStatus = pageFilterApplicationStatus.Trim();
                            query = query.Where(a => a.ApplicationStatus.ApplicationStatusName == ApplicationStatus);
                        }
                    }
                }

                applicationListResponse.ApplicationPageResultEntity.FilteredRecord = query.Count();

                listOfApplicationListingViewEntity = query
                .Select(x => new ApplicationListingViewEntity
                {
                    LoanApplicationID = x.LoanApplicationID,
                    LoanNumber = x.LoanNumber,
                    DateApplied = x.DateApplied.ToString("MM/dd/yyyy"),
                    BusinessName = x.ProgramInvitation.BusinessEntity.BusinessName != null ? x.ProgramInvitation.BusinessEntity.BusinessName : string.Empty,
                    BusinessId = x.ProgramInvitation.BusinessEntity.ID,
                    AffiliateName = x.ProgramInvitation.BusinessEntity.Affiliate.AffiliateName != null ? x.ProgramInvitation.BusinessEntity.Affiliate.AffiliateName : string.Empty,
                    LoanProgramName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.FundingApplication == null ? string.Empty : x.FundingApplication.FundingSource != null ? x.FundingApplication.FundingSource.ProgramName : string.Empty),
                    LoanType = x.ApplicationType.ApplicationTypeName != null ? x.ApplicationType.ApplicationTypeName : string.Empty,
                    LoanAmount = x.FundingApplication == null ? "0" : string.Format("{0:#,#}", x.FundingApplication.RequestedFundAmount),
                    ApplicationStatus = x.PaymentScheduleStatus != null ? (x.ApplicationStatusID == 40 ? x.PaymentScheduleStatus.Status : x.ApplicationStatus.Description) : x.ApplicationStatus.Description,
                    ApplicationStatusID = x.ApplicationStatusID,
                    ContactInfo = x.ProgramInvitation.ProgramInvitee.Where(pi => pi.ProgramInvitationID == x.ProgramInvitationID && pi.ProgramInvitation.ProgramID == x.ProgramInvitation.ProgramID).FirstOrDefault().Contact.FirstName+ " "
                    + x.ProgramInvitation.ProgramInvitee.Where(pi => pi.ProgramInvitationID == x.ProgramInvitationID && pi.ProgramInvitation.ProgramID == x.ProgramInvitation.ProgramID).FirstOrDefault().Contact.LastName,                    
                })
                //ToBeUncommented
                .OrderByDescending(x => x.LoanApplicationID)
                //.Skip((PageFilter.PageNumber - 1) * PageFilter.TakeRecordCount)
                //.Take(PageFilter.TakeRecordCount)
                .ToList();
                var paymentScheduleSummarys = this._fundingSourceRepository.GetAllPaymentScheduleSummary();
                foreach (var pss in paymentScheduleSummarys)
                {
                    var listOfApplication = listOfApplicationListingViewEntity.Find(l => l.LoanApplicationID == pss.LoanApplicationID);
                    if(listOfApplication != null)
                    {
                        listOfApplication.FundAllocatedAmount = pss.FundAllocatedAmount > 0 ? string.Format("{0:#,#}", pss.FundAllocatedAmount) : "0";
                    }
                }
                //var loanBusinessDetails = this._loanBusinessDetailRepository.GetAll().ToList();
                //if(loanBusinessDetails.Count > 0)
                //{
                //    foreach(var businessDetail in loanBusinessDetails)
                //    {
                //        var listOfApplication = listOfApplicationListingViewEntity.Find(l => l.LoanApplicationID == businessDetail.LoanApplicationID);
                //        if(listOfApplication != null)
                //        {
                //            listOfApplication.ContactInfo = businessDetail.EmailAddress;
                //        }
                //    }
                    
                //}
                applicationListResponse.ApplicationPageResultEntity.DataList = listOfApplicationListingViewEntity;
                applicationListResponse.ApplicationPageResultEntity.TotalRecordCount = totalRecordCount;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Exception in ContactImpl-> GetAllContactInformation", ex);
            }
            applicationListResponse.IsSuccess = true;
            return applicationListResponse;
        }
    }
}