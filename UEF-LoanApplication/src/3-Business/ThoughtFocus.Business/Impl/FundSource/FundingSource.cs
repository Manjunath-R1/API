namespace ThoughtFocus.Business.Impl.FundSource
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using ThoughtFocus.DataAccess.Models.FundingSource;
    using ThoughtFocus.Common.Exceptions.BusinessException;
    using ThoughtFocus.Business.Interfaces.FundSource;
    using ThoughtFocus.Domain.Response;
    using ThoughtFocus.Domain.Common;
    using ThoughtFocus.Domain.CustomView;
    using System.Linq;
    using ThoughtFocus.Repository.Interfaces.FundingSource;
    using ThoughtFocus.Domain.Enumeration;
    using ThoughtFocus.Common.Utilities;
    using ThoughtFocus.Repository.Interfaces.Contact;

    public class FundingSource : IFundingSource
    {
        #region Fields
        private readonly ILogger<FundingSource> _logger;
        private IFundingEntityRepository _fundingEntityRepository;
        private IContactRepository _contactRepository;
        private IFundingSourceRepository _fundingSourceRepository;
        #endregion Fields

        #region Constructors
        public FundingSource(ILogger<FundingSource> logger,IFundingEntityRepository fundingEntityRepository, IContactRepository contactRepository, IFundingSourceRepository fundingSourceRepository)
        {
            this._logger = logger;
            this._fundingEntityRepository = fundingEntityRepository;
            this._contactRepository = contactRepository;
            this._fundingSourceRepository = fundingSourceRepository;
        }

        #endregion Constructors

        #region Methods

        /**
         * Calculate total funded amount using list of all transactions for a funding source
        */
        public decimal CalculateTotalFundedAmount(List<FundTransaction> FundTransactionList)
        {
            if (FundTransactionList == null)
                return 0;
            decimal TotalFundedAmount = 0;
            try
            {
                foreach (var fundTransaction in FundTransactionList)
                {
                    if (fundTransaction.TransactionTypeID == 1)
                        TotalFundedAmount += fundTransaction.TransactionAmount;
                    else
                        TotalFundedAmount -= fundTransaction.TransactionAmount;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException("Exception in FundingSource -> CalculateTotalFundedAmount ", ex);
            }

            return TotalFundedAmount;
        }

        public FundingEntityListResponse GetAllFundingEntitiesInformation(PageFilterEntity PageFilter, long UserID)
        {
            FundingEntityListResponse fundingEntityListResponse = new FundingEntityListResponse();
            fundingEntityListResponse.FundingEntityPageResultEntity = new PageResultEntity<FundingEntityListingViewEntity>();
            List<FundingEntityListingViewEntity> listOfFundingEntityListingViewEntity = null;
            var fundingEntitylist = new List<FundingEntity>();
            int totalRecordCount = 0;
            string sortExpression = "";
            try
            {
                List<FundingEntity> query = this._fundingEntityRepository.GetAllFundingEntities().OrderBy(x=>x.FundingEntityName).ToList();
                var programList = this._contactRepository.GetProgramInvitationContactRoles(UserID);
                if(programList != null && programList.Count > 0)
                {
                    if(programList.FirstOrDefault().ProgramID > 0)
                    {
                        foreach(var p in programList)
                        {
                            var fs = this._fundingSourceRepository.GetFundingSourceByID(p.ProgramID);
                            if(fs != null)
                            {
                                var fn = query.Where(c => c.FundingEntityID == fs.FundingEntityID).FirstOrDefault();
                                if(fn != null)
                                {
                                    fundingEntitylist.Add(fn);
                                }
                                
                            }
                           
                        }
                        if(fundingEntitylist != null && fundingEntitylist.Count > 0)
                        {
                            query = fundingEntitylist.GroupBy(elem => elem.FundingEntityID).Select(group => group.First()).ToList();
                        }
                        
                    }
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
                    if (PageFilter.FilterParameters.Any(a => a.Key == FundingEntityListFilters.FundingEntityName.ToString()))
                    {
                        string pageFilterFundingEntityName = PageFilter.FilterParameters.Where(a => a.Key == FundingEntityListFilters.FundingEntityName.ToString()).Select(a => a.Value).FirstOrDefault();
                        if (pageFilterFundingEntityName != null)
                        {
                            string fundingEntityName = pageFilterFundingEntityName.Trim();
                            query = query.Where(a => a.FundingEntityName.Contains(fundingEntityName)).ToList();
                        }
                    }

                }

                fundingEntityListResponse.FundingEntityPageResultEntity.FilteredRecord = query.Count();

                listOfFundingEntityListingViewEntity = query
                .Select(x => new FundingEntityListingViewEntity
                {
                    FundingEntityID = x.FundingEntityID,
                    FundingEntityName = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.FundingEntityName),
                    City = ThoughtFocus.Common.Utilities.CommonUtility.FirstCharToUpper(x.City),
                    State = x.State.StateName,
                    ZipCode = x.ZipCode,
                    EIN = x.EIN,
                    TIN = x.TIN
                                       
                })
                //ToBeUncommented
                //.OrderBy(sortExpression)
                .Skip((PageFilter.PageNumber - 1) * PageFilter.TakeRecordCount)
                .Take(PageFilter.TakeRecordCount).ToList();

                fundingEntityListResponse.FundingEntityPageResultEntity.DataList = listOfFundingEntityListingViewEntity;
                fundingEntityListResponse.FundingEntityPageResultEntity.TotalRecordCount = totalRecordCount;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Exception in FundingSource-> GetAllFundingEntitiesInformation", ex);
            }
            fundingEntityListResponse.IsSuccess = true;
            return fundingEntityListResponse;
        }

        #endregion Methods
    }
}