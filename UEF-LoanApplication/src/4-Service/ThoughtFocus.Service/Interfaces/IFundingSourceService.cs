using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IFundingSourceService
    {
        #region Methods
        CommonResponse CreateFundingSource(FundingSourceParam fundingSourceParam, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to get all the Utilized Amount Details
        /// </summary>   
        /// <returns>FundingSourceResponse</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        FundingSourceResponse GetFundUtilization(long fundingSourceID);

        CommonResponse AddFundTransaction(FundTransactionParam fundTransactionParam, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to remove funds
        /// </summary>
        /// <param name="FundTransactionParam">FundTransactionParam</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonCreationParam</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse RemoveFundTransaction(FundTransactionParam fundTransactionParam, UserSessionEntity userSessionEntity);

        CommonResponse CreateOrUpdateFundingEntity(FundingEntityRequest fundingEntityRequest, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to fetch the fund transaction
        /// </summary>
        /// <param name="FundingSourceID">Funding Source ID</param>   
        /// <returns>FundTransactionListResponse</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        FundTransactionResponse GetFundTransaction(long FundingSourceID);
        FundingEntityListResponse GetAllFundingEntitiesInformation(PageFilterEntity pageFilter, UserSessionEntity userSessionEntity);
        FundingEntityResponse GetFundingEntity(long fundingEntityID, UserSessionEntity userSessionEntity);
        FundingSourceResponse GetFundingSource(long fundingSourceID);

        /// <summary>
        /// This method is used to save and update the program document
        /// </summary>
        /// <param name="programDocumentRequest">programDocumentRequest</param>   
        /// <param name="userSessionEntity">userSessionEntity</param> 
        /// <returns>CommonResponse</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateProgramDocument(ProgramDocumentRequest programDocumentRequest, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to fetch the program documents
        /// </summary>
        /// <param name="programID">programID</param>   
        /// <returns>CommonResponse</returns>
        /// <exception cref="SqlException">SQL Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramDocumentResponse GetProgramDocuments(long programID);

        /// <summary>
        ///  This method is used to save and update the program question
        /// </summary>
        /// <param name="programQuestionRequest">programQuestionRequest</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonCreationParam</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateProgramQuestions(ProgramQuestionRequest programQuestionRequest, UserSessionEntity userSession);

        /// <summary>
        /// This method is used to fetch the program questions
        /// </summary>
        /// <param name="programID">programID</param>
        /// <returns>CommonCreationParam</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramQuestionsResponse GetProgramQuestions(long programID);

        /// <summary>
        ///  This method is used to save and update the program helpfulguide text
        /// </summary>
        /// <param name="helpfulGuideTextRequest">helpfulGuideTextRequest</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonCreationParam</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateHelpfulGuideTemplate(HelpfulGuideTextRequest helpfulGuideTextRequest, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to fetch the programwise helpfulguide text
        /// </summary>
        /// <param name="programID">programID</param>
        /// <returns>CommonCreationParam</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        HelpfulGuideTextResponse GetHelpfulGuideTemplate(long programID);
        ProgramInvitationEmailResponse GetProgramInvitationEmail(long programID);

        CommonResponse SaveOrUpdateProgramInvitationEmail(ProgramInvitationEmailRequest programInvitationEmailRequest, UserSessionEntity userSessionEntity);
        List<ProgramResponse> GetAllProgramInvitations();

        PaymentScheduleSummaryResponse GetPaymentScheduleSummary(long businessID, long applicationId);
        CommonResponse SaveorUpdatePaymentScheduleAndDocument(FundPaymentScheduleParam fundPaymentScheduleParam, UserSessionEntity userSessionEntity, FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam);
        PaymentScheduleTransactionResponse GetPaymentScheduleTransaction(long businessID, long applicationID);
        PaymentScheduleTransResponse RemovePaymentScheduleTransaction(PaymentScheduleTransParam paymentScheduleTransParam, UserSessionEntity userSessionEntity);
        PaymentScheduleTransResponse AddPaymentScheduleTransaction(PaymentScheduleTransParam paymentScheduleTransParam, UserSessionEntity userSessionEntity);
        MasterOptionResponse GetProgramList(long businessID);

        PaymentScheduleItemResponse GetPaymentScheduleTransactionById(long paymentScheduleID);

        CommonResponse NotifyPaymentSummaryTransaction(TransactionNotifyRequest transactionNotifyRequest, UserSessionEntity userSessionEntity);

        PaymentScheduleSummaryResponse GetPaymentScheduleSummaryData(long businessID, long applicationId);

        MasterOptionResponse GetPaymentScheduleLoanNoList(long businessID);
        bool IsPaymentScheduleExist(long businessID);

        PaymentScheduleTransResponse DeleteAllPaymentScheduleTransactionByLoan(PaymentScheduleTransParam paymentScheduleTransParam, UserSessionEntity userSessionEntity);
        void SendPaymentScheduleEmailNotification(FundBulkPaymentScheduleParam fundBulkPaymentScheduleParam);
        decimal GetAvailableLimit(long programId);
        #endregion Methods

    }
}