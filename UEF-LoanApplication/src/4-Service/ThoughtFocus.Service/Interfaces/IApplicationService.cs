using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IApplicationService
    {
        #region Methods

        /// <summary>
        /// This method is used to create new loan application 
        /// </summary>
        /// <param name="LoanApplicationParam">Loan Application param</param>       
        /// <returns>CommonResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        
        CommonResponse SaveLoanApplication(LoanApplicationRequest LoanApplicationParam, UserSessionEntity userSessionEntity);

        /// This method is used to get all loanApplication
        /// </summary>
        /// <param name="GetAllLoanApplicationInformation">Loan Application Information</param>       
        /// <returns>CommonResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        ApplicationListResponse GetAllLoanApplicationInformation(PageFilterEntity pageFilter, UserSessionEntity userSessionEntity);
        ApplicationDocumentResponse GetApplicationDocuments(long applicationID);

        /// This method is used to get all prepopulated and master data required for loanApplication creation
        /// </summary>
        /// <param name="ProgramInvitationID">Program Invitation ID</param>       
        /// <returns>ApplicationCreationPreRequiredData</returns>       
        /// <exception cref="Exception">Exception</exception>
        ApplicationCreationPreRequiredData GetPrePopulatedApplicationData(long ProgramInvitationID);
        
        /// <summary>
        /// This method is used to get the available commands 
        /// </summary>
        /// <param name="applicationID">Application Id</param>
        /// <param name="userSessionEntity">User Session Entity</param>        
        /// <returns>WorkFlowCommandResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        WorkFlowCommandResponse GetWorkFlowCommands(long applicationID, UserSessionEntity userSessionEntity);
        
        /// <summary>
        /// This method is used to Save the application details with state
        /// </summary>
        /// <param name="LoanApplicationRequest">Loan Application Request</param>
        /// <param name="userSessionEntity">User Session Entity</param>        
        /// <returns>BaseResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        BaseResponse ApplicationCommandHandler(LoanApplicationRequest LoanApplicationParam, UserSessionEntity userSessionEntity);
        
        /// <summary>
        /// This method is used to save the state change in the workflow
        /// </summary>
        /// <param name="processID">Process IDt</param>
        /// <param name="LoanApplicationRequest">Loan Application Request</param>
        /// <param name="workFlowID">WorkFlow ID</param>
        /// <param name="stateName">stateName</param>
        /// <param name="userSessionEntity">User Session Entity</param>        
        /// <returns>BaseResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        BaseResponse ExecuteWorkflowCommand(long processID, LoanApplicationRequest loanApplicationParam, long workFlowID, string stateName, UserSessionEntity userSessionEntity);
        /// <summary>
        /// This method is used to get application data by ApplicationID
        /// </summary>
        /// <param name="applicationID">Application ID</param>       
        /// <returns>ApplicationViewEntityResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ApplicationViewEntityResponse GetLoanApplicationData(long applicationID, UserSessionEntity userSessionEntity);
        ApplicationListResponse GetLoanApplications(ExportExcel filterRequest, UserSessionEntity userSessionEntity);
        IQueryable<LoanApplication> GetAllLoanApplicationsForUser(long UserID);
        long GetProgramStatusId(long programInvitationID);
        List<BusinessUser> GetBussinessUsers(long programInvitationId);
        CommonResponse SaveBusinessProfileData(BusinessProfileRequest businessProfileRequest, UserSessionEntity userSessionEntity);
        PaymentScheduleSummaryResponse GetThresholdApplicationSummary(long businessID, long programID, long applicationID);

        PaymentScheduleTransactionResponse GetPaymentScheduleTransactionByApplicationId(long applicationID);


        CommonResponse RequestedAddFundAllocation(LoanApplicationRequest loanApplicationParam, UserSessionEntity userSessionEntity);
        ApplicationListResponse ExportGetLoanApplications(LoanExportRequest request, UserSessionEntity userSessionEntity);
        #endregion Methods

    }
}
