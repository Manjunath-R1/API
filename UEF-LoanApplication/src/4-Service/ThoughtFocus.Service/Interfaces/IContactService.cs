using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.Contact;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IContactService
    {
        #region Methods


        /// <summary>
        /// This method is used to check if Email Address exists or not
        /// </summary>
        /// <param name="EmailAddress">Email Address</param>    
        /// <returns>CommonResponse</returns>
        CommonResponse CheckEmailExists(string EmailAddress);


        /// <summary>
        /// This method is used to create the internal contact, user, role and send invitation
        /// </summary>
        /// <param name="ContactEntityParam">Contact entity param</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse AddContact(ContactRequest ContactEntityParam,
           UserSessionEntity userSessionEntity);


        /// <summary>
        /// This method is used to get contact details by ID
        /// </summary>
        /// <param name="contactID">Contact ID</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>ContactViewEntityResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ContactViewEntityResponse GetContactInfoById(long contactID, UserSessionEntity userSessionEntity);


        /// <summary>
        /// This method is used to create the business contact, user, role and send invitation
        /// </summary>
        /// <param name="ContactID">Contact ID</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse DeleteContact(long ContactID, UserSessionEntity userSessionEntity);


        /// <summary>
        /// This method is used to send contact invitation
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>bool</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        bool sendContactInvitation(ContactRequest contact, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to get information about activated account
        /// </summary>
        /// <param name="tokenID">Token ID</param>
        /// <param name="contactId">Contact ID</param>         
        /// <returns>AccountActivationResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        AccountActivationResponse GetActivateAccountInfo(string tokenID, string contactId);



        /// <summary>
        /// This method is used to activate account
        /// </summary>
        /// <param name="accountActivationParam">Account Activatio nParam</param>        
        /// <returns>AccountActivationResponse</returns>       
        /// <exception cref="Exception">Exception</exception>
        AccountActivationResponse ActivateAccount(AccountActivationParam accountActivationParam);

        /// <summary>
        /// This method is used to get all the business contact information
        /// </summary> 
        /// <param name="businessID">Business ID</param>   
        /// <returns>ContactListResponse</returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ContactListResponse GetBusinessContacts(long businessID);

        /// <summary>
        /// This method is used to resend invitation
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse ResendActivationLink(string userName,
           UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to fetch All Internal Contacts
        /// </summary>        
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>       
        ContactListResponse GetAllInternalContacts();


        /// <summary>
        /// This method is used to fetch  All Business Contacts
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>       

        ContactListResponse GetAllBusinessContacts(long? businessID);

        /// <summary>
        /// This method is used to add the role for existing business contact        
        /// <param name="BusinessContactRequest">Business Contact Request</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse AddRoleForContact(BusinessContactRequest ContactEntityParam,
           UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to SaveorUpdateContact
        /// </summary>
        /// <param name="ContactEntityParam">Contact entity param</param>
        /// <param name="userSessionEntity">User Session Entity</param>         
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>

        CommonResponse SaveorUpdateContact(ContactRequest ContactEntityParam,
           UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to ChangePassword
        /// </summary>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse ChangePassword(PasswordChangeRequest passwordChangeRequest);

        /// <summary>
        /// This method is used to UpdatePassword
        /// </summary>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse UpdatePassword(NewPasswordChangeRequest passwordChangeRequest);

        /// <summary>
        /// This method is used to ForgetPassword
        /// </summary>
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse ForgotPassword(ForgotPasswordRequest ForgetPasswordRequest);

        /// <summary>
        /// This method is used to GetForgetPasswordInfo
        /// </summary>
        /// <param name="TokenID">TokenID</param>
        /// <param name="ContactId">ContactId</param>         
        /// <returns>ForgetPasswordResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ForgotPasswordResponse GetForgotPasswordInfo(string TokenID, string ContactId);

        /// <summary>
        /// This method is used to DeactivateBusiness
        /// </summary>
        /// <param name="contactID">contactID</param>    
        /// <param name="userSessionEntity">userSessionEntity</param>     
        /// <returns>ForgetPasswordResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse DeactivateBusinessContact(long contactID, UserSessionEntity userSessionEntity);

        /// <summary>
        /// This method is used to ActivateBusinessContact
        /// </summary>
        /// <param name="contactID">contactID</param>    
        /// <param name="userSessionEntity">userSessionEntity</param>     
        /// <returns>ForgetPasswordResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse ActivateBusinessContact(long contactID, UserSessionEntity userSessionEntity);


        /// <summary>
        /// This method is used to ResetBusinessContact
        /// </summary>
        /// <param name="forgotPasswordRequest">forgotPasswordRequest</param>     
        /// <returns>ForgetPasswordResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse ResetBusinessContact(ForgotPasswordRequest forgotPasswordRequest);
        /// <summary>
        /// This method is used to GetResetContactPasswordInfo
        /// </summary>
        /// <param name="TokenID">TokenID</param>     
        /// <param name="ContactId">ContactId</param>  
        /// <returns>ForgetPasswordResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        ForgotPasswordResponse GetResetContactPasswordInfo(string TokenID, string ContactId);
        /// <summary>
        /// This method is used to UpdatePasswordForResetContact
        /// </summary>
        /// <param name="passwordChangeRequest">passwordChangeRequest</param>     
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse UpdatePasswordForResetContact(NewPasswordChangeRequest passwordChangeRequest);

        /// <summary>
        /// This method is used to UnblockBusinessContact
        /// </summary>
        /// <param name="passwordChangeRequest">passwordChangeRequest</param>     
        /// <param name="passwordChangeRequest">passwordChangeRequest</param>    
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse UnblockBusinessContact(ForgotPasswordRequest passwordChangeRequest);

        /// <summary>
        /// This method is used to dave the SaveNotificationMode
        /// </summary>
        /// <param name="notificationTypeRequest">notificationTypeRequest</param>     
        /// <param name="userSessionEntity">userSessionEntity</param>    
        /// <returns>CommonResponse</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        CommonResponse SaveNotificationMode(NotificationTypeRequest notificationTypeRequest, UserSessionEntity userSessionEntity);
        
        /// <summary>
        /// This method is used to GetNotificationModeByUser
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>GetNotificationModeByUser</returns>       
        /// <exception cref="BusinessException">Business Exception</exception>
        /// <exception cref="Exception">Exception</exception>
        NotificationTypeResponse GetNotificationModeByUser(long UserId);

        //ProgramInvitationContactRoleResponse GetProgramInvitationContactRoles(long contactID, long roleID);
        ContactListResponse ExportAllBusinessContacts(ExportBusinessContactRequest exportRequest);
        #endregion Methods

    }
}