using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Repository.Interfaces.Admin;



namespace ThoughtFocus.Repository.Impl.Admin
{
    public class CleanUpApplicationDocumentRepository : ICleanUpApplicationDocumentRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields
        #region Constructors

        public CleanUpApplicationDocumentRepository(ApplicationDBContext context)
        {
            this._Context = context;

        }

        public async Task<long> UpdateApplicationDocument(GrantApplication grantApplication)
        {
            var rslt = 0;
            var loanApplication = await _Context.LoanApplications.FirstOrDefaultAsync(x => x.LoanNumber == grantApplication.GrantNumber);
            try
            {
                
                if(loanApplication != null)
                {
                    loanApplication.ApplicationStatusID = 40;
                    _Context.LoanApplications.Update(loanApplication);

                    var programInvitation = _Context.ProgramInvitations.FirstOrDefault(x => x.ProgramInvitationID == loanApplication.ProgramInvitationID);
                    if(programInvitation != null)
                    {
                        var fundTran = _Context.FundUtilizations.FirstOrDefault(x => x.ApplicationID == loanApplication.LoanApplicationID);
                        if (fundTran != null)
                        {    

                            var paymentScheduleSummary = _Context.PaymentScheduleSummary.FirstOrDefault(x => x.LoanApplicationID == loanApplication.LoanApplicationID);
                            if(paymentScheduleSummary != null)
                            {
                                paymentScheduleSummary.FundPendingAmount = 0;
                                paymentScheduleSummary.FundDisbursedAmount = paymentScheduleSummary.FundDisbursedAmount + grantApplication.DisbursedAmount;

                                paymentScheduleSummary.LastModifiedByUserID = 147;
                                paymentScheduleSummary.LastModifiedDateTime = DateTime.Now;
                                _Context.PaymentScheduleSummary.Update(paymentScheduleSummary);
                            }
                            var paymentScheduleTransactions = _Context.PaymentScheduleTransaction.Where(x => x.BusinessID == programInvitation.BusinessID && x.LoanApplicationID == loanApplication.LoanApplicationID && x.IsActive == true && x.TransactionStatusID == 1).ToList();
                            if(paymentScheduleTransactions.Count > 0)
                            {
                                foreach(var item in paymentScheduleTransactions)
                                {
                                    item.TransactionStatusID = 2;
                                    item.DisbursedDate = grantApplication.DateApplied;
                                    _Context.PaymentScheduleTransaction.Update(item);

                                    var fundTransaction = new FundUtilization
                                    {
                                        TransactionTypeID = 3,
                                        TransactionAmount = item.FundedAmount,
                                        Comment = "2",
                                        CreatedDateTime = DateTime.Now,
                                        CreatedByUserID = 147,
                                        LastModifiedDateTime = DateTime.Now,
                                        LastModifiedByUserID = 147,
                                        IsActive = true,
                                        FundingSourceID = programInvitation.ProgramID,
                                        ApplicationID = loanApplication.LoanApplicationID,
                                        DateofDisbursement = grantApplication.DateApplied,
                                        TransactionDate = grantApplication.DateApplied,
                                        DestinationBankAccount = fundTran.DestinationBankAccount,
                                        BankRoutingNumber = fundTran.BankRoutingNumber
                                    };
                                    _Context.FundUtilizations.Add(fundTransaction);
                                }

                                var paymentScheduleStatus = _Context.PaymentScheduleStatus.FirstOrDefault(x => x.LoanApplicationID == loanApplication.LoanApplicationID);
                                if(paymentScheduleStatus != null)
                                {
                                    paymentScheduleStatus.DisbursementCount = 2;
                                    paymentScheduleStatus.Status = "Final Disbursement";
                                    _Context.PaymentScheduleStatus.Update(paymentScheduleStatus);
                                }
                                rslt = await _Context.SaveChangesAsync();
                            }
                           
                        }
                    }                    
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
            return rslt > 0 ? loanApplication.LoanApplicationID : 0;
        }


        #endregion Constructors

    }
}
