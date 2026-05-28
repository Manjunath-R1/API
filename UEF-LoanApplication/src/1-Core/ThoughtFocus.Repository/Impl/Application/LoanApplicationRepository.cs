namespace ThoughtFocus.Repository.Impl.Application
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq;

    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.Application;
    using ThoughtFocus.Repository.Interfaces.Application;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess.Models;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Enumeration;
    using System.Threading.Tasks;

    public class LoanApplicationRepository : AbstractEFApplicationBaseRepository<LoanApplication>, ILoanApplicationRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public LoanApplicationRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        #endregion Constructors

        #region Methods

        public void SaveLoanApplicationDetails(LoanApplication LoanApplication, long? userID)
        {
            try
            {
                if (LoanApplication.LoanApplicationID > 0)
                {
                    _Context.Entry(LoanApplication).State = EntityState.Modified;
                }
                else
                    _Context.LoanApplications.Add(LoanApplication);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at LoanApplicationRepository-> SaveLoanApplicationDetails", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at LoanApplicationRepository-> SaveLoanApplicationDetails", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at LoanApplicationRepository-> SaveLoanApplicationDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> SaveLoanApplicationDetails", ex);
            }

        }

        public LoanApplication GetLoanApplicationByApplicationIDForNotifications(Int64 applicationID)
        {
            var query = new LoanApplication();
            try
            {
                query = _Context.LoanApplications.Where(x => x.LoanApplicationID == applicationID && x.IsActive == true).FirstOrDefault();

                if (query != null)
                {
                    // Detach the object to remove it from context’s cache.
                    _Context.Entry(query).State = EntityState.Detached;
                    // Then load it. We will get a new object with data
                    // freshly loaded from the database.
                    query = _Context.LoanApplications.Find(query.LoanApplicationID);

                }

                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
        }

        public LoanApplication GetLoanApplicationByApplicationID(Int64 applicationID)
        {

            try
            {
                var query = GetAll().FirstOrDefault(x => x.LoanApplicationID == applicationID && x.IsActive == true);
            
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplicationByApplicationID", ex);
            }
        }

        public IQueryable<LoanApplication> GetLoanApplications(long UserID, List<long> programIds)
        {
            IQueryable<LoanApplication> loanApplications = null;
            ThoughtFocus.DataAccess.Models.User.UserRole userRole = new ThoughtFocus.DataAccess.Models.User.UserRole();
            try
            {
                userRole = _Context.RUserRoles.FirstOrDefault(x => x.UserID == UserID && x.IsActive == true);

                if (userRole != null && userRole.RoleID > 0)
                {
                    if (userRole.RoleID == (long)RoleIDEnumerations.NULTreasury || userRole.RoleID == (long)RoleIDEnumerations.Controller)
                    {
                        loanApplications = GetAll().Where(a => a.IsActive == true && (a.ApplicationStatusID == (long)ApplicationStatusEnumeration.AgreementSubmitted || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.CFOApproved || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.AccountDisbursed
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.RequestedMoreDetails || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.RequestMoreDeatailsCompleted || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.UWApproved || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.RequestMoreDeatailsCompletedByBorrower
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.UWReviewToRequestedMoreDetails || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.AgreementSubmittedByReInitiate
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.RequestedMoreInfoToSave || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.UWReviewRequestedMoreDetailsToSave 
                                                                                
                                                                                
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAAgreementSubmitted || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPACFOApproved || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAAccountDisbursed
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPARequestedMoreDetails || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPARequestMoreDeatailsCompleted || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAUWApproved || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPARequestMoreDeatailsCompletedByBorrower
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAUWReviewToRequestedMoreDetails || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAAgreementSubmittedByReInitiate
                                                                                || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPARequestedMoreInfoToSave || a.ApplicationStatusID == (long)ApplicationStatusEnumeration.SPAUWReviewRequestedMoreDetailsToSave                                                                                                                                            ));

                    }
                    else if (userRole.RoleID == (long)RoleIDEnumerations.Borrower)
                    {
                        loanApplications = (from user in _Context.BusinessUsers
                                            join loanApplication in _Context.LoanApplications
                                               on user.BusinessID equals loanApplication.ProgramInvitation.BusinessID
                                            where user.IsActive == true &&
                                               loanApplication.IsActive == true &&
                                               user.ContactID == userRole.User.ContactID
                                            select loanApplication
                                           ).AsQueryable();
                    }
                    else if (userRole.RoleID == (long)RoleIDEnumerations.UnderWriter || userRole.RoleID == (long)RoleIDEnumerations.LoanProcessor)
                    {
                        loanApplications = GetAll().Where(a => a.IsActive == true && a.ApplicationStatusID >= (long)ApplicationStatusEnumeration.Submitted);
                    }
                    else if (userRole.RoleID == (long)RoleIDEnumerations.SiteAdmin)
                    {
                        loanApplications = GetAll().Where(a => a.IsActive == true);
                    }
                    else
                    {
                        loanApplications = GetAll().Where(a => a.IsActive == true && a.ApplicationStatusID >= (long)ApplicationStatusEnumeration.Submitted);
                    }
                }

                if (loanApplications != null && programIds.Count > 0)
                {
                    if(programIds.FirstOrDefault() > 0)
                    {
                        loanApplications = loanApplications.Where(x => programIds.Contains(x.ProgramInvitation.ProgramID));
                    }
                }

                return loanApplications;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplications", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplications", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplications", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetLoanApplications", ex);
            }
        }

        #endregion Methods

        public long GetProgramInvitationByLoanApplicationID(long loanAllicationID)
        {
            long programInvitationID = 0;
            try
            {
                var programInvitation = GetAll().Where(l => l.LoanApplicationID == loanAllicationID && l.IsActive == true).FirstOrDefault();
                programInvitationID = programInvitation != null ? programInvitation.ProgramInvitationID : 0;
               
                return programInvitationID;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramInvitationByLoanApplicationID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramInvitationByLoanApplicationID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramInvitationByLoanApplicationID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> GetProgramInvitationByLoanApplicationID", ex);
            }
        }
        public bool UpdateLoanApplicationApplicationStatus(LoanApplication loanApplication, long? userID, int applicationStatusID)
        {
            var isUpdate = false;
            try
            {
                if (loanApplication.LoanApplicationID > 0)
                {
                    loanApplication.ApplicationStatusID = applicationStatusID;
                    _Context.Entry(loanApplication).State = EntityState.Modified;
                    this._Context.SaveChanges(userID);
                    return isUpdate = true;
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at LoanApplicationRepository-> UpdateLoanApplicationApplicationStatus", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at LoanApplicationRepository-> UpdateLoanApplicationApplicationStatus", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at LoanApplicationRepository-> SaveLoanApplicationWorkFlowDetails", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> SaveLoanApplicationWorkFlowDetails", ex);
            }
            return isUpdate;
        }

        public PaymentScheduleStatus GetPaymentScheduleStatusById(long loanApplicationID)
        {
            var paymentScheduleStatus = this._Context.PaymentScheduleStatus.Where(ps => ps.LoanApplicationID == loanApplicationID).FirstOrDefault();
            return paymentScheduleStatus;
        }
        public void SaveOrUpdateLoanPaymentScheduledStatus(PaymentScheduleStatus paymentScheduleStatus, long? userID)
        {
            try
            {
                if(paymentScheduleStatus != null)
                {
                    if (paymentScheduleStatus.ID > 0)
                    {
                        _Context.Entry(paymentScheduleStatus).State = EntityState.Modified;
                    }
                    else
                        _Context.PaymentScheduleStatus.Add(paymentScheduleStatus);

                    this._Context.SaveChanges(userID);
                }

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at LoanApplicationRepository-> SaveOrUpdateLoanPaymentScheduledStatus", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at LoanApplicationRepository-> SaveOrUpdateLoanPaymentScheduledStatus", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at LoanApplicationRepository-> SaveOrUpdateLoanPaymentScheduledStatus", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in LoanApplicationRepository-> SaveOrUpdateLoanPaymentScheduledStatus", ex);
            }

        }

        public int GetProgressReportId(string name)
        {
            try
            {
                int id = 0;
                var documentTypeId =this._Context.DocumentTypes.Where(a => a.Name == name && a.IsActive == true).FirstOrDefault();
                if (documentTypeId != null )
                {
                    id = documentTypeId.DocumentTypeID;
                }
                return id;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundsourceRepository-> GetPaymentAgreementDocumentByApplicationId", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundsourceRepository-> GetPaymentAgreementDocumentByApplicationId", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundsourceRepository-> GetPaymentAgreementDocumentByApplicationId", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundsourceRepository-> GetPaymentAgreementDocumentByApplicationId", ex);
            }
        }
    }
}
