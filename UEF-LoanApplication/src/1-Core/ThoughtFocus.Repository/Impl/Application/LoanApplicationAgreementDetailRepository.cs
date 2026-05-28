namespace ThoughtFocus.Repository.Impl.Application
{ 
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess.Models;
    using System.Linq;
    using ThoughtFocus.Constants;

    public class LoanApplicationAgreementDetailRepository : AbstractEFApplicationBaseRepository<LoanApplicationAgreementDetail>, ILoanApplicationAgreementDetailRepository
    {
        #region Fields

        private ApplicationDBContext _Context;
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        #endregion Fields

        #region Constructors

        public LoanApplicationAgreementDetailRepository(ApplicationDBContext context, ILoanApplicationRepository loanApplicationRepository) : base(context)
        {
            this._Context = context;
            this._LoanApplicationRepository = loanApplicationRepository;
        }

        #endregion Constructors

        #region Methods

        public void SaveLoanApplicationAgreementDetail(LoanApplicationAgreementDetail loanApplicationAgreementDetail)
        {
            try
            {
                var loanApplication = this._LoanApplicationRepository.GetLoanApplicationByApplicationID(loanApplicationAgreementDetail.ApplicationID);

                if (loanApplication != null)
                {
                    //SPA                 
                    //var loanRequestAmount = loanApplication.FundingApplication.RequestedFundAmount;
                    ////if (loanRequestAmount > 250000)
                    ///if (loanRequestAmount > thresholdRequestAmount)
                    if (loanApplication.FundingApplication.IsPaymentSchedule==true)
                    {
                        var spa = new LoanApplicationSchedulePaymentAreementDetail();
                        spa.ApplicationID = loanApplicationAgreementDetail.ApplicationID;
                        spa.CreatedDateTime = DateTime.Now;
                        spa.CreatedByUserID = loanApplicationAgreementDetail.CreatedByUserID;
                        spa.IsActive = true;
                        spa.LastModifiedDateTime = DateTime.Now;
                        spa.LastModifiedByUserID = loanApplicationAgreementDetail.LastModifiedByUserID;
                        spa.IPAddress = loanApplicationAgreementDetail.IPAddress;
                        spa.TransitionID = loanApplicationAgreementDetail.TransitionID;

                        _Context.LoanApplicationSchedulePaymentAreementDetails.Add(spa);
                        this._Context.SaveChanges();
                    }
                }

                if (loanApplicationAgreementDetail.LoanApplicationAgreementDetailID > 0)
                {
                    _Context.Entry(loanApplicationAgreementDetail).State = EntityState.Modified;
                }
                else
                {
                    _Context.LoanApplicationAgreementDetails.Add(loanApplicationAgreementDetail);
                }

                this._Context.SaveChanges();

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationAgreementDetailRepository-> SaveLoanApplicationAgreementDetail", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationAgreementDetailRepository-> SaveLoanApplicationAgreementDetail", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationAgreementDetailRepository-> SaveLoanApplicationAgreementDetail", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationAgreementDetailRepository-> SaveLoanApplicationAgreementDetail", ex);
            }
            
        }


        public LoanApplicationSchedulePaymentAreementDetail GetLoanApplicationSchedulePaymentAreementDetail( long applicationID)
        {
            return this._Context.LoanApplicationSchedulePaymentAreementDetails.Where(x => x.IsActive == true && x.ApplicationID == applicationID).FirstOrDefault();            
        }
        #endregion Methods

    }
}