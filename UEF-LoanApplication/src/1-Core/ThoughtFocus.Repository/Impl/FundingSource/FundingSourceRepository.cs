namespace ThoughtFocus.Repository.Impl.FundingSource
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.SqlClient;
    using System.Linq;
    using ThoughtFocus.Common.Exceptions;
    using ThoughtFocus.DataAccess;
    using ThoughtFocus.DataAccess.Models.FundingSource;
    using ThoughtFocus.Repository.Interfaces.FundingSource;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.DataAccess.Models.Admin;
    using ThoughtFocus.Domain.Params;

    public class FundingSourceRepository : AbstractEFApplicationBaseRepository<FundingSource>, IFundingSourceRepository
    {
        #region Fields

        private ApplicationDBContext _Context;

        #endregion Fields

        #region Constructors

        public FundingSourceRepository(ApplicationDBContext context)
            : base(context)
        {
            this._Context = context;

        }

        #endregion Constructors

        #region Methods
        public FundingSource GetAllFundingSources()
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at GetAllFundingSources-> GetAllFundingSources", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at GetAllFundingSources-> GetAllFundingSources", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception in GetAllFundingSources-> GetAllFundingSources", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in GetAllFundingSources-> GetAllFundingSources", ex);
            }
        }
        public FundingSource GetFundingSourceByID(Int64 fundingSourceID)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.FundingSourceID == fundingSourceID && x.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at FundingSourceRepository-> GetFundingSourceByID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at FundingSourceRepository-> GetFundingSourceByID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception in FundingSourceRepository-> GetFundingSourceByID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetContactsByID", ex);
            }
        }
        public List<string> GetFundingSourceByProgramIds(List<long> programs)
        {
            try
            {
                var query = GetAll().Where(x => programs.Contains(x.FundingSourceID) && x.IsActive == true).Select(x=>x.ProgramName).ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at FundingSourceRepository-> GetFundingSourceByID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at FundingSourceRepository-> GetFundingSourceByID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception in FundingSourceRepository-> GetFundingSourceByID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetContactsByID", ex);
            }
        }
        public FundingSource GetFundingSourceByProgramName(string programName, long fundingEntityID)
        {
            try
            {
                var query = GetAll().FirstOrDefault(x => x.ProgramName == programName && x.FundingEntityID == fundingEntityID && x.IsActive == true);
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException Exception at FundingSourceRepository-> GetFundingSourceByProgramName", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException Exception at FundingSourceRepository-> GetFundingSourceByProgramName", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException Exception in FundingSourceRepository-> GetFundingSourceByProgramName", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetFundingSourceByProgramName", ex);
            }
        }

        public void SaveOrUpdateFundingSource(FundingSource fundingSource, long userID)
        {
            try
            {
                if (fundingSource.FundingSourceID > 0)
                {
                    _Context.Entry(fundingSource).State = EntityState.Modified;
                }
                else
                {
                    fundingSource.CreatedDateTime = DateTime.Now;
                    fundingSource.CreatedByUserID = userID;
                    fundingSource.LastModifiedDateTime = DateTime.Now;
                    fundingSource.LastModifiedByUserID = userID;
                    _Context.FundingSources.Add(fundingSource);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateFundingSource", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateFundingSource", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateFundingSource", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateFundingSource", ex);
            }
        }

        public void SaveOrUpdateFundingSourceStates(List<FundingSourceStates> fundingSourceStates, long userID)
        {
            try
            {

                _Context.FundingSourceStates.AddRange(fundingSourceStates);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> SaveOrUpdateFundingSourceAndStatesMapping", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> SaveOrUpdateFundingSourceAndStatesMapping", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> SaveOrUpdateFundingSourceAndStatesMapping", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateFundingSourceAndStatesMapping", ex);
            }
        }

        public void SaveOrUpdateFundingSourceBusinessTypes(List<FundingSourceBusinessTypes> fundingSourceBusinessTypes, long userID)
        {
            try
            {

                _Context.FundingSourceBusinessTypes.AddRange(fundingSourceBusinessTypes);

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> SaveOrUpdateFundingSourceAndBusinessTypeMapping", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> SaveOrUpdateFundingSourceAndBusinessTypeMapping", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> SaveOrUpdateFundingSourceAndBusinessTypeMapping", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateFundingSourceAndBusinessTypeMapping", ex);
            }
        }
        public List<FundTransaction> GetFundTransaction(long FundingSourceID)
        {
            try
            {
                var query = GetAll()
                                .FirstOrDefault(x => x.FundingSourceID == FundingSourceID)?
                                .FundTransactions.Where(y => y.IsActive == true)?
                                .ToList();
                return query;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetFundTransaction", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetFundTransaction", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetFundTransaction", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetFundTransaction", ex);
            }
        }


        public void SaveOrUpdateProgramDocument(ProgramDocument programDocumentInfo, long userID)
        {
            try
            {
                if (programDocumentInfo.ID > 0)
                {
                    _Context.Entry(programDocumentInfo).State = EntityState.Modified;
                }
                else
                {
                    _Context.ProgramDocuments.Add(programDocumentInfo);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramDocument", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramDocument", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramDocument", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramDocument", ex);
            }
        }

        public List<ProgramDocument> GetProgramDocumentsByProgramID(long programID)
        {
            try
            {
                return this._Context.ProgramDocuments.Where(a => a.ProgramID == programID).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> GetProgramDocumentsByProgramID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> GetProgramDocumentsByProgramID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> GetProgramDocumentsByProgramID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetProgramDocumentsByProgramID", ex);
            }
        }

        public List<ProgramQuestion> GetProgramQuestionsByProgramID(long programID)
        {
            try
            {
                return this._Context.ProgramQuestions.Where(a => a.ProgramID == programID).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> GetProgramQuestionsByProgramID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> GetProgramQuestionsByProgramID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> GetProgramQuestionsByProgramID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetProgramQuestionsByProgramID", ex);
            }
        }


        public void SaveOrUpdateProgramQuestions(ProgramQuestion programQuestionInfo, long userID)
        {
            try
            {
                if (programQuestionInfo.ID > 0)
                {
                    _Context.Entry(programQuestionInfo).State = EntityState.Modified;
                }
                else
                {
                    _Context.ProgramQuestions.Add(programQuestionInfo);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramQuestions", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramQuestions", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramQuestions", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramQuestions", ex);
            }
        }

        public ProgramDocument GetProgramDocumentsByProgram(long programID, int documentTypeID)
        {
            try
            {
                return this._Context.ProgramDocuments.Where(a => a.ProgramID == programID && a.DocumentTypeID == documentTypeID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> GetProgramDocumentsByDocument", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> GetProgramDocumentsByDocument", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> GetProgramDocumentsByDocument", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetProgramDocumentsByDocument", ex);
            }
        }

        public List<ProgramHelpfulGuide> GetProgramHelpfulGuideByProgramId(long programID)
        {
            try
            {
                return this._Context.ProgramHelpfulGuides.Where(a => a.ProgramID == programID).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> GetProgramHelpfulGuideByProgramId", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> GetProgramHelpfulGuideByProgramId", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> GetProgramHelpfulGuideByProgramId", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetProgramHelpfulGuideByProgramId", ex);
            }
        }

        public ProgramHelpfulGuide GetHelpfulGuideById(long tamplateID)
        {
            try
            {
                return this._Context.ProgramHelpfulGuides.Where(a => a.HelpfulGuideTemplate.ID == tamplateID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> GetHelpfulGuideById", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> GetHelpfulGuideById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> GetHelpfulGuideById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetHelpfulGuideById", ex);
            }
        }

        public void SaveOrUpdateHelpfulGuideText(ProgramHelpfulGuide programHelpfulGuide, long userID)
        {
            try
            {
                if (programHelpfulGuide.ID > 0)
                {
                    _Context.Entry(programHelpfulGuide).State = EntityState.Modified;
                }
                else
                {
                    _Context.ProgramHelpfulGuides.Add(programHelpfulGuide);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateHelpfulGuideText", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateHelpfulGuideText", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateHelpfulGuideText", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateHelpfulGuideText", ex);
            }
        }

        public ProgramQuestion GetProgramQuestions(long programID, long questionID)
        {
            try
            {
                return this._Context.ProgramQuestions.Where(a => a.ProgramID == programID && a.QuestionID == questionID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundingSourceRepository-> GetProgramQuestions", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundingSourceRepository-> GetProgramQuestions", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundingSourceRepository-> GetProgramQuestions", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> GetProgramQuestions", ex);
            }
        }

        public void SaveOrUpdateProgramInvitationEmail(ProgramInvitationEmailPlaceHolder programInvitationEmailPlaceHolder, long userID)
        {
            try
            {
                if (programInvitationEmailPlaceHolder.ProgramInvitationEmailID > 0)
                {
                    _Context.Entry(programInvitationEmailPlaceHolder).State = EntityState.Modified;
                }
                else
                {
                    programInvitationEmailPlaceHolder.CreatedDateTime = DateTime.Now;
                    programInvitationEmailPlaceHolder.CreatedByUserID = userID;
                    programInvitationEmailPlaceHolder.LastModifiedDateTime = DateTime.Now;
                    programInvitationEmailPlaceHolder.LastModifiedByUserID = userID;
                    _Context.ProgramInvitationEmailPlaceHolder.Add(programInvitationEmailPlaceHolder);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramInvitationEmail", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramInvitationEmail", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramInvitationEmail", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdateProgramInvitationEmail", ex);
            }
        }



        public void SaveOrUpdatePaymentScheduleTransaction(PaymentScheduleTransaction paymentSchedule, long userID)
        {
            try
            {
                if (paymentSchedule != null)
                {

                    if (paymentSchedule != null && paymentSchedule.PaymentScheduleID > 0)
                    {
                        _Context.Entry(paymentSchedule).State = EntityState.Modified;
                    }
                    else
                    {

                        _Context.PaymentScheduleTransaction.Add(paymentSchedule);
                    }

                    this._Context.SaveChanges(userID);
                }


            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleTransaction", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleTransaction", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleTransaction", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleTransaction", ex);
            }
        }
        public void RemovePaymentScheduleTransaction(PaymentScheduleTransaction paymentSchedule, long userID)
        {
            try
            {
                _Context.Entry(paymentSchedule).State = EntityState.Modified;
                this._Context.SaveChanges(userID);                
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> RemovePaymentScheduleTransaction", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> RemovePaymentScheduleTransaction", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> RemovePaymentScheduleTransaction", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> RemovePaymentScheduleTransaction", ex);
            }
        }
        public void DeleteAllPaymentScheduleTransactionByLoan(List<PaymentScheduleTransaction> paymentSchedule, long userID)
        {
            try
            {
                _Context.AttachRange(paymentSchedule);
                this._Context.SaveChanges(userID);
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> DeleteAllPaymentScheduleTransactionByLoan", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> DeleteAllPaymentScheduleTransactionByLoan", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> DeleteAllPaymentScheduleTransactionByLoan", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> DeleteAllPaymentScheduleTransactionByLoan", ex);
            }
        }
        public List<PaymentScheduleTransaction> GetPaymentScheduleTransaction(long businessID, long applicationID)
        {
            try
            {              

                return this._Context.PaymentScheduleTransaction.Where(a => a.BusinessID == businessID && a.LoanApplicationID == applicationID && a.IsActive == true).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransaction", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransaction", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransaction", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetPaymentScheduleTransaction", ex);
            }
        }
        public List<PaymentScheduleSummary> GetPaymentScheduleSummary(long businessID, long applicationId)
        {
            try
            {

                return this._Context.PaymentScheduleSummary.Where(a => a.BusinessID == businessID && a.LoanApplicationID == applicationId && a.IsActive == true).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetPaymentScheduleSummary", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetPaymentScheduleSummary", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetPaymentScheduleSummary", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetPaymentScheduleSummary", ex);
            }
        }
        public List<PaymentScheduleSummary> GetAllPaymentScheduleSummary()
        {
            try
            {

                return this._Context.PaymentScheduleSummary.Where(p=>p.IsActive).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetAllPaymentScheduleSummary", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetAllPaymentScheduleSummary", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetAllPaymentScheduleSummary", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetAllPaymentScheduleSummary", ex);
            }
        }
        public void SaveOrUpdatePaymentScheduleSummary(PaymentScheduleSummary paymentScheduleSummary, long userID)
        {
            try
            {
                if (paymentScheduleSummary != null && paymentScheduleSummary.ID > 0)
                {
                    _Context.Entry(paymentScheduleSummary).State = EntityState.Modified;
                }
                else
                {

                    _Context.PaymentScheduleSummary.Add(paymentScheduleSummary);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
        }
        public void SaveOrUpdatePaymentScheduleDocument(FundAgreementDocuments fundAgreementDocuments, long userID)
        {
            try
            {
                if (fundAgreementDocuments != null && fundAgreementDocuments.DocumentID > 0)
                {
                    _Context.Entry(fundAgreementDocuments).State = EntityState.Modified;
                }
                else
                {

                    _Context.FundAgreementDocuments.Add(fundAgreementDocuments);
                }

                this._Context.SaveChanges(userID);

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
        }
        public void DeletePaymentScheduleDocuments(List<FundAgreementDocuments> fundAgreementDocuments, long userID)
        {
            try
            {
                if (fundAgreementDocuments != null)
                {
                    _Context.AttachRange(fundAgreementDocuments);
                    this._Context.SaveChanges(userID);
                }            
                

            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SaveOrUpdatePaymentScheduleSummary", ex);
            }
        }
        
        public List<PaymentScheduleTransaction> GetPaymentScheduleTransactionByLoanID(long loanApplicationId)
        {
            try
            {

                return this._Context.PaymentScheduleTransaction.Where(a => a.LoanApplicationID == loanApplicationId && a.IsActive == true).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
        }       
        public PaymentScheduleTransaction GetPaymentScheduleTransactionById(long paymentScheduleID)
        {
            try
            {

                return this._Context.PaymentScheduleTransaction.Where(a => a.PaymentScheduleID == paymentScheduleID && a.IsActive == true).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
        }


        public FundAgreementDocuments GetPaymentAgreementDocument(long businessID, long applicationId)
        {
            try
            {

                return this._Context.FundAgreementDocuments.Where(a => a.BusinessID == businessID && a.LoanApplicationID == applicationId && a.IsActive == true).OrderByDescending(x => x.DocumentID).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetPaymentAgreementDocument", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetPaymentAgreementDocument", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetPaymentAgreementDocument", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetPaymentAgreementDocument", ex);
            }
        }
        public List<FundAgreementDocuments> GetAllPaymentAgreementDocumentByApplication(long businessID, long applicationId)
        {
            try
            {

                return this._Context.FundAgreementDocuments.Where(a => a.BusinessID == businessID && a.LoanApplicationID == applicationId && a.IsActive == true).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetAllPaymentAgreementDocumentByApplication", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetAllPaymentAgreementDocumentByApplication", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetAllPaymentAgreementDocumentByApplication", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetAllPaymentAgreementDocumentByApplication", ex);
            }
        }
        public FundAgreementDocuments GetPaymentAgreementDocumentByApplicationId(long applicationId)
        {
            try
            {

                return this._Context.FundAgreementDocuments.Where(a => a.LoanApplicationID == applicationId && a.IsActive == true).OrderByDescending(x => x.DocumentID).FirstOrDefault();
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
        public PaymentScheduleSummary GetPaymentScheduleSummaryByID(long id)
        {
            var paymentScheduleSummary = this._Context.PaymentScheduleSummary.Where(ps => ps.ID == id && ps.IsActive == true).FirstOrDefault();
            return paymentScheduleSummary;
        }

        public PaymentScheduleSummary GetPaymentScheduleSummaryByLoanID(long loanApplicationId)
        {
            var paymentScheduleSummary = new PaymentScheduleSummary();
            try
            {
                var paymentScheduleSummarys = this._Context.PaymentScheduleSummary.Where(ps => ps.LoanApplicationID == loanApplicationId && ps.IsActive == true);
                if(paymentScheduleSummarys != null && paymentScheduleSummarys.Count() > 0)
                {
                    paymentScheduleSummary = paymentScheduleSummarys.FirstOrDefault();
                    return paymentScheduleSummary;
                }
                return paymentScheduleSummary ;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundsourceRepository-> GetPaymentScheduleSummaryByLoanID", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundsourceRepository-> GetPaymentScheduleSummaryByLoanID", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundsourceRepository-> GetPaymentScheduleSummaryByLoanID", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundsourceRepository-> GetPaymentScheduleSummaryByLoanID", ex);
            }
        }

        public List<PaymentScheduleTransaction> GetPaymentScheduleTransactionByApplicationId(long applicationID)
        {
            try
            {
                return this._Context.PaymentScheduleTransaction.Where(a => a.LoanApplicationID == applicationID && a.IsActive == true).OrderBy(x=>x.TransactionDate).ToList();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> GetPaymentScheduleTransactionById", ex);
            }
        }

        public void SavePaymentSummaryTransactionNotify(PaymentSummaryTransactionNotify transactionNotify, long userID)
        {
            try
            {
                if (transactionNotify != null)
                {

                    if (transactionNotify != null && transactionNotify.NotifyID > 0)
                    {
                        _Context.Entry(transactionNotify).State = EntityState.Modified;
                    }
                    else
                    {

                        _Context.PaymentSummaryTransactionNotifys.Add(transactionNotify);
                    }

                    this._Context.SaveChanges(userID);
                }


            }
            catch (SqlException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SavePaymentSummaryTransactionNotify", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SavePaymentSummaryTransactionNotify", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SavePaymentSummaryTransactionNotify", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundingSourceRepository-> SavePaymentSummaryTransactionNotify", ex);
            }
        }

        public PaymentSummaryTransactionNotify GetPaymentSummaryTransactionNotify(TransactionNotifyRequest transactionNotifyRequest, long userID)
        {
            return this._Context.PaymentSummaryTransactionNotifys.Where(ps => ps.ToNotifyUserId == userID && ps.ApplicationID == transactionNotifyRequest.ApplicationID && ps.TransactionDate.Date== transactionNotifyRequest.TransactionDate.Date && ps.IsActive == true).FirstOrDefault();
            
        }

        public bool IsPaymentScheduleExist(long businessID)
        {
            try
            {

                var result= this._Context.PaymentScheduleTransaction.Where(a => a.BusinessID == businessID && a.IsActive == true).FirstOrDefault();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException("SqlException at FundTransactionRepositoryImpl-> IsPaymentScheduleExist", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("DbUpdateException at FundTransactionRepositoryImpl-> IsPaymentScheduleExist", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException at FundTransactionRepositoryImpl-> IsPaymentScheduleExist", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in FundTransactionRepositoryImpl-> IsPaymentScheduleExist", ex);
            }
        }
        #endregion Methods
    }
}