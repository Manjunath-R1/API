using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;
using ThoughtFocus.DataAccess.Models.Admin;
using System.IO;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Request;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IAdminService
    {
        #region Methods

        /// This method is used to add  the Business Entity
        /// </summary>
        /// <param name="saveBusinessEntity">Save Business Entity</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessViewEntityResponse AddBusinessEntity(BusinessEntityRequest saveBusinessEntity, UserSessionEntity userSessionEntity);


        /// This method is used to get all the Business Entity information
        /// </summary>
        /// <returns>BusinessEntityListResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessEntityListResponse GetAllBusinessEntityInformation();


        /// <summary>
        /// This method is used to save and send invitation
        /// </summary>
        /// <param name="programInvitationRequest">Program Invitation Request</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveProgramInvitation(ProgramInvitationRequest programInvitationRequest, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to get all the Program invitations information
        /// </summary>   
        /// <param name="userSessionEntity">User Session Entity</param> 
        /// <returns>Program invitations</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramInvitationResponse GetProgramInvitation(UserSessionEntity userSessionEntity);


        /// <summary>
        /// This method is used to get all the Program Invitation PerRequired Data
        /// </summary>   
        /// <returns>ProgramInvitationPreRequiredDataResponse</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramInvitationPreRequiredDataResponse GetProgramInvitationPerRequiredData(UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to get all the BusinessUsers
        /// </summary>   
        /// <returns>BusinessUserResponse</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessUserResponse GetBusinessUsers(long businessID);

        /// <summary>
        /// This method is used to get all the Program invitations information based on Business
        /// </summary>   
        /// <param name="businessID">Business ID</param> 
        /// <param name="userSessionEntity">User Session Entity</param> 
        /// <returns>Program invitations</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessProgramInvitationResponse GetProgramInvitationByBusinessID(long businessID, UserSessionEntity userSessionEntity);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        BusinessViewEntityResponse GetBusinessEntity(long businessEntityID);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="fromDate">fromDate</param> 
        /// <param name="toDate">toDate</param> 
        /// <param name="programID">programID</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">GetCumulativeReportData</exception>
        /// <exception cref="Exception">Exception</exception>
        ConsolidatedReportDataResponse GetCumulativeReportData(CumulativeReportRequest cumulativeReportRequest);

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">GetAllAffiliateContacts</exception>
        /// <exception cref="Exception">Exception</exception>
        AffiliateContactListResponse GetAllAffiliates();

        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="fromDate">fromDate</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">GetAllAffiliateContactById</exception>
        /// <exception cref="Exception">Exception</exception>
        AffiliateContactListResponse GetAffiliate(long? AffiliateID);


        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="affiliatecontactRequest">affiliatecontactRequest</param> 
        /// <param name="userSessionEntity">userSessionEntity</param> 
        /// <returns>BusinessEntity</returns>       
        /// <exception cref="BusinessException">SaveOrUpdateAffiliate</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateAffiliate(AffiliateContactRequest affiliatecontactRequest, UserSessionEntity userSessionEntity);
        /// This method is used to get the Business Entity information
        /// </summary>
        /// <param name="fromDate">fromDate</param> 
        /// <param name="toDate">toDate</param> 
        /// <param name="programID">programID</param> 
        /// <returns>ConsolidatedReportExportDataResponse</returns>       
        /// <exception cref="BusinessException">GetCumulativeReportData</exception>
        /// <exception cref="Exception">Exception</exception>
        List<ConsolidatedReportExportDataResponse> GetCumulativeReportExcelData(CumulativeReportRequest cumulativeReportRequest);

        /// This method is used to delete the Business Entity information
        /// </summary>
        /// <returns>common response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse DeleteBusinessEntity(long businessEntityID, UserSessionEntity userSessionEntity);


        /// This method is used to Get All Questions
        /// </summary>
        /// <returns>Questions response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        QuestionsResponse GetAllQuestions();

        /// This method is used to Get Question by id
        /// </summary>
        /// <param name="questionID">questionID</param> 
        /// <returns>Questions response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        QuestionsResponse GetQuestion(long? questionID);

        /// This method is used to save and update the Questions
        /// </summary>
        /// <param name="affiliatecontactRequest">affiliatecontactRequest</param> 
        /// <param name="userSessionEntity">userSessionEntity</param> 
        /// <returns>common response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateQuestions(QuestionsRequest affiliatecontactRequest, UserSessionEntity userSessionEntity);

        /// This method is used to Get All Documents
        /// </summary>
        /// <returns>DocumentsResponse response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DocumentsResponse GetAllDocumentTypes();

        /// This method is used to save and update the documents
        /// </summary>
        /// <param name="documentsRequest">documentsRequest</param> 
        /// <param name="userSessionEntity">userSessionEntity</param> 
        /// <returns>common response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateDocuments(DocumentsRequest documentsRequest, UserSessionEntity userSessionEntity);

        /// This method is used to Get Document by id
        /// </summary>
        /// <param name="documentTypeID">documentTypeID</param> 
        /// <returns>Questions response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        DocumentsResponse GetDocument(long? documentTypeID);

        /// This method is used to get the programWise agreement
        /// </summary>
        /// <param name="programID">programID</param> 
        /// <returns>Questions response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramAgreementResponse ProgramWiseAgreement(long programID);

        /// This method is used to save and update the agreement name
        /// </summary>
        /// <param name="agreementRequest">agreementRequest</param> 
        /// <param name="userSessionEntity">userSessionEntity</param> 
        /// <returns>common response</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveOrUpdateAgreementName(AgreementRequest agreementRequest, UserSessionEntity userSessionEntity);

        /// This method is used to get the CiviCRM Organization Data.
        /// </summary>
        /// <returns>CiviCRMOrganizationDataResponse</returns>       
        /// <exception cref="BusinessException">GetCiviCRMOrganizationExportData</exception>
        /// <exception cref="Exception">Exception</exception>
        List<CiviCRMOrganizationDataResponse> GetCiviCRMOrganizationExportData(UserSessionEntity userSessionEntity);

        /// This method is used to get the CiviCRM Contacts Data.
        /// </summary>
        /// <returns>CiviCRMContactsDataResponse.</returns>       
        /// <exception cref="BusinessException">GetCiviCRMOrganizationExportData</exception>
        /// <exception cref="Exception">Exception</exception>
        List<CiviCRMContactsDataResponse> GetCiviCRMContactExportData(UserSessionEntity userSessionEntity);


        /// <summary>
        /// This method is used to get all the CiviCRM Exported Log Data.
        /// </summary>   
        /// <returns>CiviCRMDataExportLogResponse</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CiviCRMDataExportLogResponse GetAllCiviCRMDataExportLog();
        BusinessProfileMasterDataResponse GetBusinessProfileMasterData(long businessID);
        BusinessEntityListByUserResponse GetAllBusinessEntityByUser(string userName);
        CommonResponse SendUpdateFundDetailEmailNotifiaction(long loanApplicationID, UserSessionEntity userSessionEntity);

        /// This method is used to get the Program list based on fund entity
        /// </summary>
        /// <returns>ProgramDetails</returns>       

        ProgramsResponse GetProgramDetailsByFundEntityID(long fundEntityID);

        /// This method is used to get the Report Detail By Report Type
        /// </summary>
        /// <returns>Report Detail</returns>      

        ReportDetailResponse FetchReportDetailByReportType(ReportDetailRequest reportDetailRequest);
        /// This method is used to get the Export Program Invitation
        /// </summary>
        /// <returns>Export Program Invitation</returns>   
        ProgramInvitationResponse GetExportProgramInvitation(ExportProgramInvitationsRequest request, UserSessionEntity userSessionEntity);
        /// <summary>
        /// This method is used to get all the Program invitations information
        /// </summary>   
        /// <param name="userSessionEntity">User Session Entity</param> 
        /// <returns>Program invitations</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ProgramInvitationResponse GetProgramInvitationByProgram(ProgramInvitationsRequest request, UserSessionEntity userSessionEntity);

        /// This method is used to get the Get Consolidated Fund Report Data
        /// </summary>
        /// <returns>Get Consolidated Fund Report Data</returns>   
        ConsolidatedFundReportDataResponse GetConsolidatedFundReportData(FundReportRequest request, UserSessionEntity userSessionEntity);
        #endregion Methods
    }
}