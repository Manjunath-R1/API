namespace ThoughtFocus.Repository.Interfaces.FundingSource
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.DataAccess.Models.Admin;
    using ThoughtFocus.DataAccess.Models.FundingSource;
    using ThoughtFocus.DataAccess.Models.Master;
    using ThoughtFocus.Domain.Params;

    public interface IFundingSourceRepository : IEFApplicationBaseRepository<FundingSource>
    {
        #region Methods
        /// <summary>
        /// This method is used to get FundingSource by fundingSourceID
        /// </summary>
        /// <param name="fundingSourceID">funding Source ID</param>    
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        FundingSource GetFundingSourceByID(Int64 fundingSourceID);

        /// <summary>
        /// This method is used to save or update Funding Source
        /// </summary>
        /// <param name="FundingSource">Funding Source</param> 
        /// <param name="userID">User ID</param>   
        /// <returns>Void</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateFundingSource(FundingSource fundingSource, long userID);

        /// <summary>
        /// This method is used to fetch the fund transaction
        /// </summary>
        /// <param name="FundingSourceID">Funding Source ID</param>   
        /// <returns>List<FundTransaction></returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<FundTransaction> GetFundTransaction(long FundingSourceID);

        /// <summary>
        /// This method is used to fetch the fund source
        /// </summary>
        /// <param name="ProgramName">Program Name</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        FundingSource GetFundingSourceByProgramName(string ProgramName, long fundingEntityID);

        /// <summary>
        /// This method is used to save and update  the program documents
        /// </summary>
        /// <param name="programDocument">programDocument</param>   
        /// <param name="userID">userID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateProgramDocument(ProgramDocument programDocument, long userID);

        /// <summary>
        /// This method is used to fetch the program documents by programmid
        /// </summary>
        /// <param name="programID">programID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<ProgramDocument> GetProgramDocumentsByProgramID(long programID);

        /// <summary>
        /// This method is used to fetch the program questions by programmid
        /// </summary>
        /// <param name="programID">programID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<ProgramQuestion> GetProgramQuestionsByProgramID(long programID);

        /// <summary>
        /// This method is used to save and update  the program questions
        /// </summary>
        /// <param name="programQuestion">programQuestion</param>   
        /// <param name="userID">userID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateProgramQuestions(ProgramQuestion programQuestion, long userID);

        /// <summary>
        /// This method is used to fetch the program documents
        /// </summary>
        /// <param name="programID">programID</param>   
        /// <param name="documentTypeID">documentTypeID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramDocument GetProgramDocumentsByProgram(long programID, int documentTypeID);

        /// <summary>
        /// This method is used to fetch the program helpfulguide text
        /// </summary>
        /// <param name="programID">programID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        List<ProgramHelpfulGuide> GetProgramHelpfulGuideByProgramId(long programID);

        /// <summary>
        /// This method is used to fetch the program helpfulguide text
        /// </summary>
        /// <param name="tamplateID">tamplateID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramHelpfulGuide GetHelpfulGuideById(long tamplateID);

        /// <summary>
        /// This method is used to save and update  the helpfulguide text
        /// </summary>
        /// <param name="programHelpfulGuide">programHelpfulGuide</param>   
        /// <param name="userID">userID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        void SaveOrUpdateHelpfulGuideText(ProgramHelpfulGuide programHelpfulGuide, long userID);

        /// <summary>
        /// This method is used to fetch the program questions
        /// </summary>
        /// <param name="programID">programID</param>   
        /// <param name="questionID">questionID</param>   
        /// <returns>FundingSource</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="DbUpdateException">Database update exception</exception>
        /// <exception cref="ObjectDisposedException">Object Disposed Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramQuestion GetProgramQuestions(long programID, long questionID);

        void SaveOrUpdateProgramInvitationEmail(ProgramInvitationEmailPlaceHolder programInvitationEmailPlaceHolder, long userID);

        void SaveOrUpdatePaymentScheduleTransaction(PaymentScheduleTransaction paymentSchedule, long userID);
        void RemovePaymentScheduleTransaction(PaymentScheduleTransaction paymentSchedule, long userID);
        void DeleteAllPaymentScheduleTransactionByLoan(List<PaymentScheduleTransaction> paymentSchedule, long userID);
        List<PaymentScheduleTransaction> GetPaymentScheduleTransaction(long businessID, long applicationID);
        List<PaymentScheduleSummary> GetPaymentScheduleSummary(long businessID, long applicationId);
        void SaveOrUpdatePaymentScheduleSummary(PaymentScheduleSummary paymentScheduleSummary, long userID);
        void SaveOrUpdatePaymentScheduleDocument(FundAgreementDocuments fundAgreementDocuments, long userID);

        PaymentScheduleTransaction GetPaymentScheduleTransactionById(long paymentScheduleID);
        FundAgreementDocuments GetPaymentAgreementDocument(long businessID, long applicationId);
        FundAgreementDocuments GetPaymentAgreementDocumentByApplicationId(long applicationId);

        PaymentScheduleSummary GetPaymentScheduleSummaryByID(long id);
        List<PaymentScheduleTransaction> GetPaymentScheduleTransactionByLoanID(long loanApplicationId);
        PaymentScheduleSummary GetPaymentScheduleSummaryByLoanID(long loanApplicationId);
        List<PaymentScheduleTransaction> GetPaymentScheduleTransactionByApplicationId(long applicationID);

        void SavePaymentSummaryTransactionNotify(PaymentSummaryTransactionNotify transactionNotify, long userID);
        PaymentSummaryTransactionNotify GetPaymentSummaryTransactionNotify(TransactionNotifyRequest transactionNotifyRequest, long userID);
        bool IsPaymentScheduleExist(long businessID);
        List<FundAgreementDocuments> GetAllPaymentAgreementDocumentByApplication(long businessID, long applicationId);
        void DeletePaymentScheduleDocuments(List<FundAgreementDocuments> fundAgreementDocuments, long userID);
        List<string> GetFundingSourceByProgramIds(List<long> programs);
        List<PaymentScheduleSummary> GetAllPaymentScheduleSummary();
        #endregion Methods
    }
}
