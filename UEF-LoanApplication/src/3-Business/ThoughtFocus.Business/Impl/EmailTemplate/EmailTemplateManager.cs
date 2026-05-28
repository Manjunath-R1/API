
using System;
using System.Collections.Generic;
using System.Linq; 
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using ThoughtFocus.Constants;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Notification;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User; 
using ThoughtFocus.Repository.Interfaces.Master; 
using ThoughtFocus.Common.Exceptions; 
using ThoughtFocus.Domain.CustomView.Notification;
using System.Configuration;
using System.Text.RegularExpressions;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Common.Workflow.Core.Model;
using Microsoft.Extensions.Logging;
using ThoughtFocus.Repository.Interfaces.Notification;
using ThoughtFocus.DataAccess.Models.Notification;
using Microsoft.Extensions.Options;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Business.Interfaces.SMS;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Repository.Impl.Application;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using ThoughtFocus.Repository.Interfaces.Contact;

namespace ThoughtFocus.Business.Impl.EmailTemplate
{
    public class EmailTemplateManager : IEmailTemplateManager
    {
        #region Fields
 
        private IPlaceHoldersManager _placeholderManager;
        private IEmailManager _sendEmailManager;
        private readonly ILogger<EmailTemplateManager> _logger;  
        private IActivityNotificationRepository _rActivityNotificationRepository;
        private readonly AppSettings _appSettings;
        private INotificationModeRepository _iNotificationModeRepository;
        private IProgramInviteeRepository _iProgramInviteeRepository;
        private ILoanApplicationRepository _loanApplicationRepository;
        private ISMSManager _iSMSManager;
        private SqlConnectionStrings _sqlConnectionString;
        private IProgramInvitationRepository _programInvitationRepository;
        private IFundingSourceRepository _fundingSourceRepository;
        private IContactRepository _contactRepository;
        private IActivityNotificationRepository _activityNotificationRepository;
        private IBusinessEntityRepository _businessEntityRepository;
        private IGenaralOptionRepository _genaralOptionRepository;
        
        #endregion Fields

        #region Constructor

        public EmailTemplateManager(
                IPlaceHoldersManager placeholderManager,
                IEmailManager sendEmailManager,
                ILogger<EmailTemplateManager> logger,
                IActivityNotificationRepository rActivityNotificationRepository,
                IOptions<AppSettings> appSettings,
                INotificationModeRepository notificationModeRepository,
                IProgramInviteeRepository programInviteeRepository,
                ILoanApplicationRepository loanApplicationRepository,
                ISMSManager sMSManager,
                SqlConnectionStrings sqlConnectionStrings,
                IProgramInvitationRepository programInvitationRepository,
                IFundingSourceRepository fundingSourceRepository,
                IContactRepository contactRepository,
                IActivityNotificationRepository activityNotificationRepository,
                IBusinessEntityRepository businessEntityRepository, IGenaralOptionRepository genaralOptionRepository
                )
        {
            this._placeholderManager = placeholderManager;
            this._sendEmailManager = sendEmailManager;
            this._logger=logger;
            this._rActivityNotificationRepository = rActivityNotificationRepository;
            this._appSettings = appSettings.Value;
            this._iNotificationModeRepository = notificationModeRepository;
            this._iProgramInviteeRepository = programInviteeRepository;
            this._loanApplicationRepository = loanApplicationRepository;
            this._iSMSManager = sMSManager;
            this._sqlConnectionString = sqlConnectionStrings;
            this._programInvitationRepository = programInvitationRepository;
            this._fundingSourceRepository = fundingSourceRepository;
            this._contactRepository = contactRepository;
            this._activityNotificationRepository = activityNotificationRepository;
            this._businessEntityRepository = businessEntityRepository;
            this._genaralOptionRepository =genaralOptionRepository;
    }


        #endregion Constructor

        #region Methods

        public bool SendOthersEmail(EmailSenderRequest emailRequest,UserSessionEntity userSessionEntity)
        {
            bool emailSent = false;
            List<SendEmailParameter> sendEmailParameters = null;
            try
            {
                PlaceHolderReplaceRequest replaceRequest = new PlaceHolderReplaceRequest();

                replaceRequest.NotificationID = emailRequest.NotificationID;
                replaceRequest.ContactID = emailRequest.ContactID;
                replaceRequest.CallBackURL = emailRequest.CallBackURL!=null ? emailRequest.CallBackURL : string.Empty;
                replaceRequest.UserID = emailRequest.UserID;
                replaceRequest.ActivityID = emailRequest.ActivityID;
                replaceRequest.EmailAddress = emailRequest.EmailAddress;
                replaceRequest.Role = emailRequest.Role;
                replaceRequest.AdditionalMessage = emailRequest.AdditionalMessage!=null ? emailRequest.AdditionalMessage : string.Empty;
                replaceRequest.ProgramInvitationID = emailRequest.ProgramInvitationID;
                replaceRequest.ApplicationID = emailRequest.ApplicationID;
                //Get placeholders data
                sendEmailParameters = _placeholderManager.GetPlacehodersValue(replaceRequest);
                emailSent = _sendEmailManager.SendEmailNotification(sendEmailParameters,userSessionEntity);

            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Exception at EmailTemplateManager-> SendOthersEmail", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return emailSent;
        }

        public bool SendProgramInvitationEmail(ProgramInvitationEmailSendRequest emailRequest, UserSessionEntity userSessionEntity)
        {
            bool emailSent = false;
            List<SendEmailParameter> sendEmailParameters = null;
            try
            {
                ProgramInvitationEmailPlaceHolderReplaceRequest replaceRequest = new ProgramInvitationEmailPlaceHolderReplaceRequest();

                replaceRequest.NotificationID = emailRequest.NotificationID;
                replaceRequest.ContactID = emailRequest.ContactID;
                replaceRequest.CallBackURL = emailRequest.CallBackURL != null ? emailRequest.CallBackURL : string.Empty;
                replaceRequest.UserID = emailRequest.UserID;
                replaceRequest.ActivityID = emailRequest.ActivityID;
                replaceRequest.EmailAddress = emailRequest.EmailAddress;
                replaceRequest.Role = emailRequest.Role;
                replaceRequest.AdditionalMessage = emailRequest.AdditionalMessage != null ? emailRequest.AdditionalMessage : string.Empty;
                replaceRequest.ProgramInvitationID = emailRequest.ProgramInvitationID;
                replaceRequest.BusinessID = emailRequest.BusinessID;
                replaceRequest.ProgramID = emailRequest.ProgramID;

                //Get placeholders data
                //sendEmailParameters = _placeholderManager.GetPlacehodersValue(replaceRequest);
                sendEmailParameters = _placeholderManager.GetPlacehodersEmailNotificationValue(replaceRequest);
                emailSent = _sendEmailManager.SendEmailNotification(sendEmailParameters, userSessionEntity);
                /*if(emailSent)
                {
                    var ContactId = userSessionEntity.ContactID;

                    ThoughtFocus.DataAccess.Models.Contact.Contact contact = this._contactRepository.GetContactsByID(ContactId);

                    //For admin role 
                    if (contact != null && contact.Users != null && contact.Users.UserRoles != null && contact.Users.UserRoles.Where(x => (x.RoleID == 1 || x.RoleID == 3 || x.RoleID == 5 || x.RoleID == 7 ) && x.IsActive == true).Count() > 0)
                    {
                        var programInvitationEmailRequest = new ProgramInvitationEmailRequest { ProgramID = emailRequest.ProgramID, MessageBody = sendEmailParameters.FirstOrDefault().body };

                        try
                        {
                            var programInvitationEmailPlaceHolder = new ProgramInvitationEmailPlaceHolder();
                            programInvitationEmailPlaceHolder = this._programInvitationRepository.GetProgramInvitationEmailPlaceHolder(programInvitationEmailRequest.ProgramID);

                            if (programInvitationEmailPlaceHolder != null)
                            {
                                programInvitationEmailPlaceHolder.ProgramID = programInvitationEmailRequest.ProgramID;
                                programInvitationEmailPlaceHolder.MessageBody = programInvitationEmailRequest.MessageBody;
                                programInvitationEmailPlaceHolder.LastModifiedByUserID = userSessionEntity.UserID;
                                programInvitationEmailPlaceHolder.LastModifiedDateTime = DateTime.Now;
                                programInvitationEmailPlaceHolder.MessageSubject = string.Empty;

                            }
                            else
                            {
                                programInvitationEmailPlaceHolder = new ProgramInvitationEmailPlaceHolder();
                                programInvitationEmailPlaceHolder.ProgramID = programInvitationEmailRequest.ProgramID;
                                programInvitationEmailPlaceHolder.MessageBody = programInvitationEmailRequest.MessageBody;
                                programInvitationEmailPlaceHolder.MessageSubject = string.Empty;
                            }
                            this._fundingSourceRepository.SaveOrUpdateProgramInvitationEmail(programInvitationEmailPlaceHolder, userSessionEntity.UserID);
                        }
                        catch (BusinessException ex)
                        {
                            //LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramInvitationEmail ", null);

                        }
                        catch (Exception ex)
                        {
                            // LoggerExtensions.LogInformation(_Logger, null, ex, "Exception in FundingSourceService-> SaveOrUpdateProgramInvitationEmail", null);

                        }
                    }
                    
                }*/
                

            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Exception at EmailTemplateManager-> SendOthersEmail", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return emailSent;
        }
        public bool SendWorkflowNotifications<T>(T param, long activityID)
        {
            ProcessInstance processInstance = param as ProcessInstance;
            if (processInstance.ProcessParameters == null)
            {
                LoggerExtensions.LogInformation(_logger, null,"processInstance.ProcessParameters object is Null-> SendWorkflowEmail",null);
                return false;
            }

            bool emailSent = false;
            List<ActivityNotification> rActivityNotifications = null;

            try
            {
                PlaceHolderReplaceRequest replaceRequest = new PlaceHolderReplaceRequest();
                List<SendEmailParameter> sendEmailParameters=null;

                string comment = processInstance.ProcessParameters.FirstOrDefault(a => a.Name == "Comment").Value.ToString();
                replaceRequest.AdditionalMessage = comment !=null ? comment : string.Empty;

                UserSessionEntity userEntity = (UserSessionEntity)processInstance.ProcessParameters.FirstOrDefault(a => a.Name == "UserSessionModel").Value;
                UserSessionEntity userSessionEntity = new UserSessionEntity();
                userSessionEntity.UserID = userEntity.UserID;
                replaceRequest.UserID = userEntity.UserID;
                replaceRequest.ActivityID = activityID;
                replaceRequest.CurrentActivityName = processInstance.CurrentActivityName;
                replaceRequest.ExecutedActivityState = processInstance.ExecutedActivityState;                
                replaceRequest.ApplicationID = processInstance.ProcessId;
                string _url= _appSettings.BaseUrl + "/form/0/"+processInstance.ProcessId;
                replaceRequest.CallBackURL ="<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " +  _url + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Application</a> <br /><br /> </td>";

                
                NotificationModesType notificationModesType = null;
                long userID = 0;
                long notificationModId = 0;
                //CommonConstants.ThresholdRequestAmount = 0;
                //var masterOptionResponse = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                //if (masterOptionResponse != null && masterOptionResponse.Count > 0)
                //{
                //    CommonConstants.ThresholdRequestAmount = long.Parse(masterOptionResponse.FirstOrDefault().OptionValue);
                //}
                //CommonConstants.IsPaymentSchedule = this._genaralOptionRepository.IsPaymentSchedule(processInstance.ProcessId);
                var loanApplications = _loanApplicationRepository.GetLoanApplicationByApplicationIDForNotifications(processInstance.ProcessId);

                //if(loanApplications.FundingApplication.RequestedFundAmount > 250000 && replaceRequest.ExecutedActivityState == "Accepted")
                //if (loanApplications.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount && replaceRequest.ExecutedActivityState == "Accepted")
                /*if (loanApplications.FundingApplication.IsPaymentSchedule==true && replaceRequest.ExecutedActivityState == "Accepted")
                {
                        if(replaceRequest.ExecutedActivityState == "Accepted")
                        {
                            ProgramInvitation programInvitation = this._programInvitationRepository.GetProgramInvitation(loanApplications.ProgramInvitationID);
                            _url = _appSettings.BaseUrl + "/programinvitation/form/" + programInvitation.BusinessID;
                            replaceRequest.CallBackURL = "<td style='font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center;' class='btn-primary'> <br /><br /> <a href= " + _url + " target='_blank' style='display: inline-block; color: #ffffff; background-color: #C11439; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #C11439;'>View Business</a> <br /><br /> </td>";
                            emailSent = SendEmailForSPA(22, replaceRequest, userEntity);
                        }
                        return emailSent;
                    }*/
                    userID = _iProgramInviteeRepository.GetUserIDByProgramInvitation(loanApplications.ProgramInvitationID);
                    notificationModesType = _iNotificationModeRepository.GetNotificationModeByUser(userID);
                    notificationModId = notificationModesType != null ? notificationModesType.NotificationModesID : 0;

                    if (notificationModId == (long)NotificationModesEnumeration.SMS
                        || notificationModId == (long)NotificationModesEnumeration.BOTH)
                    {
                        if (activityID == (long)ApplicationStatusEnumeration.Submitted
                            || activityID == (long)ApplicationStatusEnumeration.RequestedMoreInfo
                            || activityID == (long)ApplicationStatusEnumeration.Accepted
                            || activityID == (long)ApplicationStatusEnumeration.Approved
                            || activityID == (long)ApplicationStatusEnumeration.Rejected
                            || activityID == (long)ApplicationStatusEnumeration.AccountDisbursed
                            || activityID == (long)ApplicationStatusEnumeration.RequestedMoreDetails
                            || activityID == (long)ApplicationStatusEnumeration.UWReviewToRequestedMoreDetails
                            )
                        {
                            SMSSenderRequest sMSSenderRequest = new SMSSenderRequest();
                            sMSSenderRequest.ApplicationStatusID = loanApplications.ApplicationStatusID;
                            sMSSenderRequest.LoanNumber = loanApplications.LoanNumber;
                            sMSSenderRequest.ProgramName = loanApplications.ProgramInvitation?.FundingSource?.ProgramName;
                            sMSSenderRequest.PhoneNumber = loanApplications.LoanBusinessDetail.PhoneNumber;
                            sMSSenderRequest.LoanApplicationID = loanApplications.LoanApplicationID;
                            sMSSenderRequest.UserID = userSessionEntity.UserID;
                            sMSSenderRequest.BusinessUserID = userID;
                            bool isSent = _iSMSManager.SendSMSNotification(sMSSenderRequest);

                        }
                    }
                //}
              
                    if (replaceRequest.ActivityID != 0)
                        rActivityNotifications = _rActivityNotificationRepository.GetActivityNotificationsByActivityID(replaceRequest.ActivityID);

                    if (rActivityNotifications != null)
                    {
                        foreach (var notification in rActivityNotifications)
                        {
                            replaceRequest.NotificationID = !notification.NotificationID.HasValue ? 0 : notification.NotificationID.Value;
                            sendEmailParameters = null;

                            if (notificationModId == (long) NotificationModesEnumeration.SMS)
                            {

                                sendEmailParameters = _placeholderManager.GetPlacehodersValueExceptBorrower(replaceRequest, notificationModId);
                            if (sendEmailParameters != null)
                            {
                                emailSent = _sendEmailManager.SendEmailNotificationExceptBorrower(sendEmailParameters, userSessionEntity);
                            }
                        }
                            else
                            {
                                //Get placeholders data
                                sendEmailParameters = _placeholderManager.GetPlacehodersValue(replaceRequest);
                            if (sendEmailParameters != null)
                            {
                                emailSent = _sendEmailManager.SendEmailNotification(sendEmailParameters, userSessionEntity);
                            }
                        }
                               
                            
                        }
                    
                }
            }
            catch (RepositoryException ex)
            {
                throw new BusinessException("Exception at EmailTemplateManager-> SendWorkflowEmail", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return emailSent;
        }
        private bool SendEmailForSPA(long notificationID, PlaceHolderReplaceRequest replaceRequest, UserSessionEntity userSessionEntity)
        { 
            var emailSent = false;
            replaceRequest.NotificationID = notificationID;
            replaceRequest.ActivityID = 0;
            //replaceRequest.ActivityID = 50;
            //if (replaceRequest.ActivityID != 0)
            //    //rActivityNotifications = _rActivityNotificationRepository.GetActivityNotificationsByActivityID(replaceRequest.ActivityID);
                var rActivityNotifications = _activityNotificationRepository.GetRActivityPlaceholders(notificationID, replaceRequest.ActivityID);
            if (rActivityNotifications != null)
            {
                //foreach (var notification in rActivityNotifications)
                //{
                    if(rActivityNotifications != null)
                    {
                      
                        //Get placeholders data
                        var sendEmailParameters = _placeholderManager.GetPlacehodersValue(replaceRequest);
                        if (sendEmailParameters != null)
                        {
                            emailSent = _sendEmailManager.SendEmailNotification(sendEmailParameters, userSessionEntity);
                        return emailSent;
                        }
                    }
                    
                //}
                
            }
            return emailSent;
        }

        public bool SendSMS(LoanApplication loanApplications, long activityID, UserSessionEntity userSessionEntity)
        {
            bool isSmsSent = false;
            var userID = _iProgramInviteeRepository.GetUserIDByProgramInvitation(loanApplications.ProgramInvitationID);
            SMSSenderRequest sMSSenderRequest = new SMSSenderRequest();
            sMSSenderRequest.ApplicationStatusID = activityID;
            sMSSenderRequest.LoanNumber = loanApplications.LoanNumber;
            sMSSenderRequest.ProgramName = loanApplications.ProgramInvitation?.FundingSource?.ProgramName;
            sMSSenderRequest.PhoneNumber = loanApplications.LoanBusinessDetail.PhoneNumber;
            sMSSenderRequest.LoanApplicationID = loanApplications.LoanApplicationID;
            sMSSenderRequest.UserID = userSessionEntity.UserID;
            sMSSenderRequest.BusinessUserID = userID;
            bool isSent = _iSMSManager.SendSMSNotification(sMSSenderRequest);
            return isSmsSent;
        }
        #endregion Methods
    }
}
