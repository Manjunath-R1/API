
using System;
using System.Collections.Generic;
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Notification;
using ThoughtFocus.Domain.Enumeration;
using Microsoft.Extensions.Logging;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocusRepository.Interfaces.Master;
using ThoughtFocus.Repository.Interfaces.Notification;
using System.Linq;
using ThoughtFocus.Domain.CustomView.Notification;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Common.Exceptions.BusinessException;
using System.Text.RegularExpressions;
using ThoughtFocus.Business.Helpers;
using ThoughtFocus.Domain;
using ThoughtFocus.DataAccess.Models.Admin;
using ThoughtFocus.Domain.Common;
using Microsoft.Extensions.Options;
using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Repository.Interfaces.Admin;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.Constants;
using ThoughtFocus.Repository.Interfaces.Master;

namespace ThoughtFocus.Business.Impl.EmailTemplate
{
    public class PlaceHoldersManager : IPlaceHoldersManager
    {
        #region Fields


        private INotificationRecipientEmailAddressRepository _notificationRecipientEmailAddressRepository;
        private readonly ILogger<PlaceHoldersManager> _logger;
        private IEmailNotificationHeaderFooterRepository _emailTemplateHeaderFooterRepository;
        private INotificationRepository _notificationRepository;
        private IActivityNotificationRepository _activityNotificationRepository;
        private INotificationRecipientsRepository _notificationRecipientRepository;
        private IEmailPlaceholderRepository _emailPlaceholderRepository;
        private IPreEmailConditionManager _preEmailConditionManager;
        private IProgramInvitationRepository _programInvitationRepository;

        private readonly EmailSettings _emailSettings;
        private readonly IGenaralOptionRepository _genaralOptionRepository;
        
        #endregion Fields

        #region Constructor

        public PlaceHoldersManager(
            INotificationRecipientEmailAddressRepository notificationRecipientEmailAddressRepository,
            IPreEmailConditionManager preEmailConditionManager,
            IEmailPlaceholderRepository emailPlaceholderRepository,
            INotificationRecipientsRepository notificationRecipientRepository,
            IActivityNotificationRepository activityNotificationRepository,
            IEmailNotificationHeaderFooterRepository emailTemplateHeaderFooterRepository,
            INotificationRepository notificationRepository,
            ILogger<PlaceHoldersManager> logger,
            IOptions<EmailSettings> emailSettings,
            IProgramInvitationRepository programInvitationRepository, IGenaralOptionRepository genaralOptionRepository
            )
        {


            this._notificationRecipientEmailAddressRepository = notificationRecipientEmailAddressRepository;
            this._preEmailConditionManager = preEmailConditionManager;
            this._activityNotificationRepository = activityNotificationRepository;
            this._notificationRepository = notificationRepository;
            this._emailTemplateHeaderFooterRepository = emailTemplateHeaderFooterRepository;
            this._notificationRecipientRepository = notificationRecipientRepository;
            this._emailPlaceholderRepository = emailPlaceholderRepository;
            this._logger = logger;
            this._emailSettings = emailSettings.Value;
            this._programInvitationRepository = programInvitationRepository;
            this._genaralOptionRepository = genaralOptionRepository;
        }

        #endregion Constructor

        #region Methods    

        public List<SendEmailParameter> GetPlacehodersValue(PlaceHolderReplaceRequest replaceRequest)
        {
            _logger.LogDebug("Entered GetPlacehodersValue method");
            List<EmailTemplatePlaceholdersViewModel> emailTemplatePlaceholders = null;
            List<SendEmailParameter> sendEmailParameters = null;
            ActivityNotification rActivityNotification = null;
            Notification notification = null;
            EmailTemplatePlaceholders emailTemplatePlaceholder = null;
            string footerNote = String.Empty;
            string imageContent = String.Empty;
            try
            {
                if (replaceRequest.NotificationID != 0)
                    notification = this._notificationRepository.GetNotificationByNotificationType(replaceRequest.NotificationID);

                EmailTemplateHeaderFooter emailTemplateHeaderFooter = _emailTemplateHeaderFooterRepository.GetEmailTemplateFooter();

                if (emailTemplateHeaderFooter != null)
                {
                    // footerNote = emailTemplateHeaderFooter.Footer == null ? footerNote : emailTemplateHeaderFooter.Footer;
                    // imageContent = String.Format(ConfigurationManager.AppSettings["EmailImageURL"].ToString(),
                    //  ConfigurationManager.AppSettings["AmazonBucketName"].ToString(),
                    //   emailTemplateHeaderFooter.FolderName == null ? String.Empty : emailTemplateHeaderFooter.FolderName, emailTemplateHeaderFooter.ImageKey == null ? String.Empty : emailTemplateHeaderFooter.ImageKey);

                }

                if (replaceRequest.NotificationID != 0)
                    rActivityNotification = _activityNotificationRepository.GetRActivityPlaceholders(replaceRequest.NotificationID, replaceRequest.ActivityID);

                if (rActivityNotification != null)
                {
                    List<NotificationRecipients> notificationRecipients = this._notificationRecipientRepository.GetRecipientPlaceholdersByActivityNotificationID((long)rActivityNotification.ActivityNotificationID);

                    //Cc recipients
                    string ccRecipient = string.Join(", ", notificationRecipients.Where(x => x.IsCC == true).Select(p => p.Placeholders.DisplayName));

                    //To recipient
                    List<NotificationRecipients> toList = new List<NotificationRecipients>();

                    if (replaceRequest.Role != null)
                    {

                        emailTemplatePlaceholder = this._emailPlaceholderRepository.FirstOrDefault(x => x.Placeholder == replaceRequest.Role && x.IsActive == true);

                        notificationRecipients = this._notificationRecipientRepository.GetRecipientPlaceholdersByPlaceholderID(emailTemplatePlaceholder.PlaceholderID);


                        toList = notificationRecipients.Where(x => x.IsTo == true && x.ActivityNotification.ActivityNotificationID == (long)rActivityNotification.ActivityNotificationID).ToList();
                    }
                    else
                    {
                        toList = notificationRecipients.Where(x => x.IsTo == true).ToList();
                    }

                    sendEmailParameters = new List<SendEmailParameter>();

                    EmailPreviewEntity emailPreviewEntity = replaceRequest.emailPreviewEntity;


                    foreach (var recipient in toList)
                    {
                        //Recipient role
                        replaceRequest.Role = recipient?.Placeholders?.Placeholder;

                        //Get all placeholders data
                        if (replaceRequest != null)
                        {

                            emailTemplatePlaceholders = this.GetEmailRecipientData(replaceRequest);

                        }

                        //Replace placeholders data for each recipient
                        foreach (var member in emailTemplatePlaceholders)
                        {
                            if (member.RecipientEmailAddress != null)
                            {
                                member.CurrentYear = DateTime.Now.Year.ToString();


                                member.CallBackURL = replaceRequest.CallBackURL == null ? "" : replaceRequest.CallBackURL;
                                member.AdditionalMessage = replaceRequest.AdditionalMessage == null ? "" : replaceRequest.AdditionalMessage;
                                member.EmailAddress = replaceRequest.EmailAddress == null ? "" : replaceRequest.EmailAddress;
                                
                                 
                                if (member.memberInfoList != null)
                                {
                                    string memberList = string.Join("<br>", member.memberInfoList);
                                    member.memberList = memberList;
                                }
                                string to = recipient?.Placeholders?.DisplayName;
                                string header = emailPreviewEntity == null ? notification.Head : emailPreviewEntity.Header;
                                string salutation = emailPreviewEntity == null ? notification.Salutation : emailPreviewEntity.Salutation;
                                string messageBody = emailPreviewEntity == null ? notification.Body : emailPreviewEntity.Body;
                                string signature = emailPreviewEntity == null ? notification.Footer : emailPreviewEntity.EmailFooter;

                                header = header.Replace("[[NUL_Standalone_Logo]]", _emailSettings.NUL_Logo_Standalone_Logo)
                                               .Replace("[[Urban_Empowerment_Fund_Logo]]", _emailSettings.Urban_Empowerment_Fund_Logo)
                                               .Replace("[[Pepsico_Logo]]", _emailSettings.Pepsico_Logo)
                                               .Replace("[[BRA_Logo]]", _emailSettings.BRA_Logo);
                                var replacements = member.ToDictionary();
                                var input = String.Format("{0}{1}", messageBody, signature);
                                var pattern = @"\@(\w+)";


                                Regex re = new Regex(pattern, RegexOptions.Compiled);
                                MatchCollection matches = re.Matches(input);

                                SendEmailParameter sendEmailParameter = new SendEmailParameter();
                                
                                //To
                                if (to != null)
                                    sendEmailParameter.To = re.Replace(to, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //CC
                                if (ccRecipient != null)
                                    sendEmailParameter.cc = re.Replace(ccRecipient, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");
                                if (sendEmailParameter.cc != null && sendEmailParameter.cc != "")
                                {
                                    String sendEmailCc = sendEmailParameter.cc;

                                    string[] CcEmailAddressee = sendEmailCc.Split(',');
                                    List<string> ListOfSendEmailCc = new List<string>();

                                    foreach (var Address in CcEmailAddressee)
                                    {
                                        if (!String.IsNullOrEmpty(Address.Trim()))
                                        {
                                            bool isValidEmailCc = _preEmailConditionManager.IsEmailValid(Address);
                                            if (isValidEmailCc)
                                            {
                                                if (!ListOfSendEmailCc.Contains(Address))
                                                    ListOfSendEmailCc.Add(Address);
                                            }
                                        }

                                    }
                                    sendEmailParameter.cc = String.Join(",", ListOfSendEmailCc);
                                }

                                //Subject
                                if (notification.MessageSubject != null)
                                    sendEmailParameter.subject = re.Replace(notification.MessageSubject, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");


                                //Mail message body
                                if (messageBody != null)
                                    sendEmailParameter.body = re.Replace(messageBody, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : replace.Value.ToString());



                                //Salutation
                                if (salutation != null)
                                    sendEmailParameter.salutation = re.Replace(salutation, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //signature
                                if (signature != null)
                                    sendEmailParameter.Footer = re.Replace(signature, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //Header
                                if (header != null)
                                    sendEmailParameter.Header = re.Replace(header, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                ////footer
                                //if (footer != null)
                                //    sendEmailParameter.Footer = re.Replace(footer, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value].ToString() : replace.Value.ToString());

                                sendEmailParameter.BodySubjectHTML = "<table width='100%' border='0' cellspacing='10' cellpadding='0'><tr><td class='h2' style='text-align:center;'>" + sendEmailParameter.subject + "<hr class='hrcolor' /></td></tr></table>";

                                sendEmailParameter.Note = re.Replace(footerNote, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");



                                //Notification recipient EmailAddress
                                NotificationRecipientEmailAddress notificationRecipientEmailAddress = _notificationRecipientEmailAddressRepository.GetRecipientByActivityNotificationID(rActivityNotification.ActivityNotificationID);

                                if (notificationRecipientEmailAddress != null)
                                {
                                    String RecipientEmailCc = notificationRecipientEmailAddress.EmailAddress;

                                    string[] RecipientEmail = RecipientEmailCc.Split(',');
                                    List<string> ListOfRecipientEmail = new List<string>();

                                    foreach (var RAddress in RecipientEmail)
                                    {
                                        if (!String.IsNullOrEmpty(RAddress.Trim()))
                                        {
                                            bool isValidEmailCc = _preEmailConditionManager.IsEmailValid(RAddress);
                                            if (isValidEmailCc)
                                            {
                                                sendEmailParameter.cc = sendEmailParameter.cc + "," + RAddress;
                                            }
                                        }
                                    }
                                }

                                sendEmailParameter.NotificationID = replaceRequest.NotificationID;

                                //Add to email list
                                sendEmailParameters.Add(sendEmailParameter);
                            }
                        }
                    }

                    if (notification.RecipientType == (int)EmailRecipientTypeEnumeration.Grouped)
                    {
                        SendEmailParameter EmailParameter = sendEmailParameters.FirstOrDefault();
                        if (sendEmailParameters != null && sendEmailParameters.Count > 1)
                        {
                            foreach (SendEmailParameter emailParameter in sendEmailParameters)
                            {
                                EmailParameter.To = EmailParameter.To == emailParameter.To ? EmailParameter.To : EmailParameter.To + "," + emailParameter.To;
                            }
                        }
                        sendEmailParameters = new List<SendEmailParameter>();
                        sendEmailParameters.Add(EmailParameter);
                    }


                }
                _logger.LogDebug("Returned successfully from GetPlacehodersValue method");
            }
            catch (RepositoryException ex)
            {
                _logger.LogDebug("Returned unsuccessfully from GetPlacehodersValue method");
                throw new BusinessException("Exception at PlaceHoldersManager -->GetPlacehodersValue method ", ex);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from GetPlacehodersValue method");
                throw ex;
            }
            return sendEmailParameters;
        }
        public List<SendEmailParameter> GetPlacehodersEmailNotificationValue(ProgramInvitationEmailPlaceHolderReplaceRequest replaceRequest)
        {
            _logger.LogDebug("Entered GetPlacehodersValue method");
            List<EmailTemplatePlaceholdersViewModel> emailTemplatePlaceholders = null;
            List<SendEmailParameter> sendEmailParameters = null;
            ActivityNotification rActivityNotification = null;
            Notification notification = null;
            ProgramInvitationEmailPlaceHolder programInvitationEmailPlaceHolder = null;
            EmailTemplatePlaceholders emailTemplatePlaceholder = null;
            string footerNote = String.Empty;
            string imageContent = String.Empty;
            try
            {
                if (replaceRequest.NotificationID != 0)
                {
                    notification = this._notificationRepository.GetNotificationByNotificationType(replaceRequest.NotificationID);
                    programInvitationEmailPlaceHolder = this._programInvitationRepository.GetProgramInvitationEmailPlaceHolder(replaceRequest.ProgramID); 
                }
                    

                EmailTemplateHeaderFooter emailTemplateHeaderFooter = _emailTemplateHeaderFooterRepository.GetEmailTemplateFooter();

                if (emailTemplateHeaderFooter != null)
                {
                    // footerNote = emailTemplateHeaderFooter.Footer == null ? footerNote : emailTemplateHeaderFooter.Footer;
                    // imageContent = String.Format(ConfigurationManager.AppSettings["EmailImageURL"].ToString(),
                    //  ConfigurationManager.AppSettings["AmazonBucketName"].ToString(),
                    //   emailTemplateHeaderFooter.FolderName == null ? String.Empty : emailTemplateHeaderFooter.FolderName, emailTemplateHeaderFooter.ImageKey == null ? String.Empty : emailTemplateHeaderFooter.ImageKey);

                }

                if (replaceRequest.NotificationID != 0)
                    rActivityNotification = _activityNotificationRepository.GetRActivityPlaceholders(replaceRequest.NotificationID, replaceRequest.ActivityID);

                if (rActivityNotification != null)
                {
                    List<NotificationRecipients> notificationRecipients = this._notificationRecipientRepository.GetRecipientPlaceholdersByActivityNotificationID((long)rActivityNotification.ActivityNotificationID);

                    //Cc recipients
                    string ccRecipient = string.Join(", ", notificationRecipients.Where(x => x.IsCC == true).Select(p => p.Placeholders.DisplayName));

                    //To recipient
                    List<NotificationRecipients> toList = new List<NotificationRecipients>();

                    if (replaceRequest.Role != null)
                    {

                        emailTemplatePlaceholder = this._emailPlaceholderRepository.FirstOrDefault(x => x.Placeholder == replaceRequest.Role && x.IsActive == true);

                        notificationRecipients = this._notificationRecipientRepository.GetRecipientPlaceholdersByPlaceholderID(emailTemplatePlaceholder.PlaceholderID);


                        toList = notificationRecipients.Where(x => x.IsTo == true && x.ActivityNotification.ActivityNotificationID == (long)rActivityNotification.ActivityNotificationID).ToList();
                    }
                    else
                    {
                        toList = notificationRecipients.Where(x => x.IsTo == true).ToList();
                    }

                    sendEmailParameters = new List<SendEmailParameter>();

                    EmailPreviewEntity emailPreviewEntity = replaceRequest.emailPreviewEntity;


                    foreach (var recipient in toList)
                    {
                        //Recipient role
                        replaceRequest.Role = recipient?.Placeholders?.Placeholder;

                        //Get all placeholders data
                        if (replaceRequest != null)
                        {

                            emailTemplatePlaceholders = this.GetEmailRecipientData(replaceRequest);

                        }

                        //Replace placeholders data for each recipient
                        foreach (var member in emailTemplatePlaceholders)
                        {
                            if (member.RecipientEmailAddress != null)
                            {
                                member.CurrentYear = DateTime.Now.Year.ToString();


                                member.CallBackURL = replaceRequest.CallBackURL == null ? "" : replaceRequest.CallBackURL;
                                member.AdditionalMessage = replaceRequest.AdditionalMessage == null ? "" : replaceRequest.AdditionalMessage;
                                member.EmailAddress = replaceRequest.EmailAddress == null ? "" : replaceRequest.EmailAddress;
                                if (member.memberInfoList != null)
                                {
                                    string memberList = string.Join("<br>", member.memberInfoList);
                                    member.memberList = memberList;
                                }
                                string to = recipient?.Placeholders?.DisplayName;
                                string header = emailPreviewEntity == null ? notification.Head : emailPreviewEntity.Header;
                                //string salutation = emailPreviewEntity == null ? notification.Salutation : emailPreviewEntity.Salutation;                                
                                var emailbody = programInvitationEmailPlaceHolder != null ? programInvitationEmailPlaceHolder.MessageBody : notification.Body;
                                string messageBody = emailbody.Replace("<p id='callBackURL' class='bodycopy' style='display: none'>", "<p id='callBackURL' class='bodycopy'>").Replace("<p id=\"callBackURL\" class=\"bodycopy\" style=\"display: none\">", "<p id=\"callBackURL\" class=\"bodycopy\">").Replace("text-align: center; display: none", "text-align: center;");
                                string signature = emailPreviewEntity == null ? notification.Footer : emailPreviewEntity.EmailFooter;

                                header = header.Replace("[[NUL_Standalone_Logo]]", _emailSettings.NUL_Logo_Standalone_Logo)
                                               .Replace("[[Urban_Empowerment_Fund_Logo]]", _emailSettings.Urban_Empowerment_Fund_Logo)
                                               .Replace("[[Pepsico_Logo]]", _emailSettings.Pepsico_Logo)
                                               .Replace("[[BRA_Logo]]", _emailSettings.BRA_Logo);
                                var replacements = member.ToDictionary();
                                var input = String.Format("{0}{1}", messageBody, null);
                                var pattern = @"\@(\w+)";


                                Regex re = new Regex(pattern, RegexOptions.Compiled);
                                MatchCollection matches = re.Matches(input);

                                SendEmailParameter sendEmailParameter = new SendEmailParameter();

                               //To
                                if (to != null)
                                    sendEmailParameter.To = re.Replace(to, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //CC
                                if (ccRecipient != null)
                                    sendEmailParameter.cc = re.Replace(ccRecipient, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");
                                if (sendEmailParameter.cc != null && sendEmailParameter.cc != "")
                                {
                                    String sendEmailCc = sendEmailParameter.cc;

                                    string[] CcEmailAddressee = sendEmailCc.Split(',');
                                    List<string> ListOfSendEmailCc = new List<string>();

                                    foreach (var Address in CcEmailAddressee)
                                    {
                                        if (!String.IsNullOrEmpty(Address.Trim()))
                                        {
                                            bool isValidEmailCc = _preEmailConditionManager.IsEmailValid(Address);
                                            if (isValidEmailCc)
                                            {
                                                if (!ListOfSendEmailCc.Contains(Address))
                                                    ListOfSendEmailCc.Add(Address);
                                            }
                                        }

                                    }
                                    sendEmailParameter.cc = String.Join(",", ListOfSendEmailCc);
                                }

                               //Subject
                                if (notification.MessageSubject != null)
                                    sendEmailParameter.subject = re.Replace(notification.MessageSubject, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");


                                //Mail message body
                                if (messageBody != null)
                                    sendEmailParameter.body = re.Replace(messageBody, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : replace.Value.ToString());



                                //Salutation
                                /*if (salutation != null)
                                    sendEmailParameter.salutation = re.Replace(salutation, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");*/

                                //signature
                                if (signature != null)
                                    sendEmailParameter.Footer = re.Replace(signature, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //Header
                                if (header != null)
                                    sendEmailParameter.Header = re.Replace(header, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                ////footer
                                //if (footer != null)
                                //    sendEmailParameter.Footer = re.Replace(footer, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value].ToString() : replace.Value.ToString());
                               
                                sendEmailParameter.BodySubjectHTML = "<table width='100%' border='0' cellspacing='10' cellpadding='0'><tr><td class='h2' style='text-align:center;'>" + sendEmailParameter.subject + "<hr class='hrcolor' /></td></tr></table>";

                                //sendEmailParameter.Note = re.Replace(footerNote, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");



                                //Notification recipient EmailAddress
                                NotificationRecipientEmailAddress notificationRecipientEmailAddress = _notificationRecipientEmailAddressRepository.GetRecipientByActivityNotificationID(rActivityNotification.ActivityNotificationID);

                                if (notificationRecipientEmailAddress != null)
                                {
                                    String RecipientEmailCc = notificationRecipientEmailAddress.EmailAddress;

                                    string[] RecipientEmail = RecipientEmailCc.Split(',');
                                    List<string> ListOfRecipientEmail = new List<string>();

                                    foreach (var RAddress in RecipientEmail)
                                    {
                                        if (!String.IsNullOrEmpty(RAddress.Trim()))
                                        {
                                            bool isValidEmailCc = _preEmailConditionManager.IsEmailValid(RAddress);
                                            if (isValidEmailCc)
                                            {
                                                sendEmailParameter.cc = sendEmailParameter.cc + "," + RAddress;
                                            }
                                        }
                                    }
                                }

                                sendEmailParameter.NotificationID = replaceRequest.NotificationID;

                                //Add to email list
                                sendEmailParameters.Add(sendEmailParameter);
                            }
                        }
                    }

                    if (notification.RecipientType == (int)EmailRecipientTypeEnumeration.Grouped)
                    {
                        SendEmailParameter EmailParameter = sendEmailParameters.FirstOrDefault();
                        if (sendEmailParameters != null && sendEmailParameters.Count > 1)
                        {
                            foreach (SendEmailParameter emailParameter in sendEmailParameters)
                            {
                                EmailParameter.To = EmailParameter.To == emailParameter.To ? EmailParameter.To : EmailParameter.To + "," + emailParameter.To;
                            }
                        }
                        sendEmailParameters = new List<SendEmailParameter>();
                        sendEmailParameters.Add(EmailParameter);
                    }


                }
                _logger.LogDebug("Returned successfully from GetPlacehodersValue method");
            }
            catch (RepositoryException ex)
            {
                _logger.LogDebug("Returned unsuccessfully from GetPlacehodersValue method");
                throw new BusinessException("Exception at PlaceHoldersManager -->GetPlacehodersValue method ", ex);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from GetPlacehodersValue method");
                throw ex;
            }
            return sendEmailParameters;
        }
        public List<SendEmailParameter> GetPlacehodersValueExceptBorrower(PlaceHolderReplaceRequest replaceRequest, long notificationModeID)
        {
            _logger.LogDebug("Entered GetPlacehodersValue method");
            List<EmailTemplatePlaceholdersViewModel> emailTemplatePlaceholders = null;
            List<SendEmailParameter> sendEmailParameters = null;
            ActivityNotification rActivityNotification = null;
            Notification notification = null;
            EmailTemplatePlaceholders emailTemplatePlaceholder = null;
            string footerNote = String.Empty;
            string imageContent = String.Empty;
            try
            {
                if (replaceRequest.NotificationID != 0)
                    notification = this._notificationRepository.GetNotificationByNotificationType(replaceRequest.NotificationID);

                EmailTemplateHeaderFooter emailTemplateHeaderFooter = _emailTemplateHeaderFooterRepository.GetEmailTemplateFooter();

                if (emailTemplateHeaderFooter != null)
                {
                    // footerNote = emailTemplateHeaderFooter.Footer == null ? footerNote : emailTemplateHeaderFooter.Footer;
                    // imageContent = String.Format(ConfigurationManager.AppSettings["EmailImageURL"].ToString(),
                    //  ConfigurationManager.AppSettings["AmazonBucketName"].ToString(),
                    //   emailTemplateHeaderFooter.FolderName == null ? String.Empty : emailTemplateHeaderFooter.FolderName, emailTemplateHeaderFooter.ImageKey == null ? String.Empty : emailTemplateHeaderFooter.ImageKey);

                }

                if (replaceRequest.NotificationID != 0)
                    rActivityNotification = _activityNotificationRepository.GetRActivityPlaceholders(replaceRequest.NotificationID, replaceRequest.ActivityID);

                if (rActivityNotification != null)
                {
                    List<NotificationRecipients> notificationRecipients = this._notificationRecipientRepository.GetRecipientPlaceholdersByActivityNotificationID((long)rActivityNotification.ActivityNotificationID);

                    //Cc recipients
                    string ccRecipient = string.Join(", ", notificationRecipients.Where(x => x.IsCC == true).Select(p => p.Placeholders.DisplayName));

                    //To recipient
                    List<NotificationRecipients> toList = new List<NotificationRecipients>();

                    if (replaceRequest.Role != null)
                    {

                        emailTemplatePlaceholder = this._emailPlaceholderRepository.FirstOrDefault(x => x.Placeholder == replaceRequest.Role && x.IsActive == true);

                        notificationRecipients = this._notificationRecipientRepository.GetRecipientPlaceholdersByPlaceholderID(emailTemplatePlaceholder.PlaceholderID);


                        toList = notificationRecipients.Where(x => x.IsTo == true && x.ActivityNotification.ActivityNotificationID == (long)rActivityNotification.ActivityNotificationID).ToList();
                    }
                    else
                    {
                        toList = notificationRecipients.Where(x => x.IsTo == true).ToList();
                    
                        
                    }

                    sendEmailParameters = new List<SendEmailParameter>();

                    EmailPreviewEntity emailPreviewEntity = replaceRequest.emailPreviewEntity;
                    
                    
                    foreach (var recipient in toList)
                    {
                        //Recipient role
                        replaceRequest.Role = recipient?.Placeholders?.Placeholder;

                        //Get all placeholders data
                        if (replaceRequest != null)
                        {

                            emailTemplatePlaceholders = this.GetEmailRecipientData(replaceRequest);

                        }

                        

                        //Replace placeholders data for each recipient
                        foreach (var member in emailTemplatePlaceholders)
                        {
                            if (member.RecipientEmailAddress != null)
                            {
                                member.CurrentYear = DateTime.Now.Year.ToString();


                                member.CallBackURL = replaceRequest.CallBackURL == null ? "" : replaceRequest.CallBackURL;
                                member.AdditionalMessage = replaceRequest.AdditionalMessage == null ? "" : replaceRequest.AdditionalMessage;
                                member.EmailAddress = replaceRequest.EmailAddress == null ? "" : replaceRequest.EmailAddress;
                                if (member.memberInfoList != null)
                                {
                                    string memberList = string.Join("<br>", member.memberInfoList);
                                    member.memberList = memberList;
                                }
                                string to = recipient?.Placeholders?.DisplayName;
                                string header = emailPreviewEntity == null ? notification.Head : emailPreviewEntity.Header;
                                string salutation = emailPreviewEntity == null ? notification.Salutation : emailPreviewEntity.Salutation;
                                string messageBody = emailPreviewEntity == null ? notification.Body : emailPreviewEntity.Body;
                                string signature = emailPreviewEntity == null ? notification.Footer : emailPreviewEntity.EmailFooter;

                                header = header.Replace("[[NUL_Standalone_Logo]]", _emailSettings.NUL_Logo_Standalone_Logo)
                                               .Replace("[[Urban_Empowerment_Fund_Logo]]", _emailSettings.Urban_Empowerment_Fund_Logo)
                                               .Replace("[[Pepsico_Logo]]", _emailSettings.Pepsico_Logo)
                                               .Replace("[[BRA_Logo]]", _emailSettings.BRA_Logo);
                                var replacements = member.ToDictionary();
                                var input = String.Format("{0}{1}", messageBody, signature);
                                var pattern = @"\@(\w+)";


                                Regex re = new Regex(pattern, RegexOptions.Compiled);
                                MatchCollection matches = re.Matches(input);

                                SendEmailParameter sendEmailParameter = new SendEmailParameter();

                                //To
                                if (to != null)
                                {
                                    if (notificationModeID == (long)NotificationModesEnumeration.SMS &&
                                        to== "@Borrower")
                                    {
                                        sendEmailParameter.To = "";
                                    }
                                    else
                                    {
                                        sendEmailParameter.To = re.Replace(to, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");
                                    }
                                }
                                //CC
                                if (ccRecipient != null)
                                {
                                    if (ccRecipient.Contains("@Borrower"))
                                        sendEmailParameter.cc = ccRecipient.Replace("@Borrower,", "").Trim();
                                    else
                                    {
                                        sendEmailParameter.cc = re.Replace(ccRecipient, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");
                                    }
                                }
                                    if (sendEmailParameter.cc != null && sendEmailParameter.cc != "")
                                {
                                    String sendEmailCc = sendEmailParameter.cc;

                                    string[] CcEmailAddressee = sendEmailCc.Split(',');
                                    List<string> ListOfSendEmailCc = new List<string>();

                                    foreach (var Address in CcEmailAddressee)
                                    {
                                        if (!String.IsNullOrEmpty(Address.Trim()))
                                        {
                                            bool isValidEmailCc = _preEmailConditionManager.IsEmailValid(Address);
                                            if (isValidEmailCc)
                                            {
                                                if (!ListOfSendEmailCc.Contains(Address))
                                                    ListOfSendEmailCc.Add(Address);
                                            }
                                        }

                                    }
                                    sendEmailParameter.cc = String.Join(",", ListOfSendEmailCc);
                                }

                                //Subject
                                if (notification.MessageSubject != null)
                                    sendEmailParameter.subject = re.Replace(notification.MessageSubject, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");


                                //Mail message body
                                if (messageBody != null)
                                    sendEmailParameter.body = re.Replace(messageBody, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : replace.Value.ToString());



                                //Salutation
                                if (salutation != null)
                                    sendEmailParameter.salutation = re.Replace(salutation, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //signature
                                if (signature != null)
                                    sendEmailParameter.Footer = re.Replace(signature, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                //Header
                                if (header != null)
                                    sendEmailParameter.Header = re.Replace(header, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");

                                ////footer
                                //if (footer != null)
                                //    sendEmailParameter.Footer = re.Replace(footer, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value].ToString() : replace.Value.ToString());

                                sendEmailParameter.BodySubjectHTML = "<table width='100%' border='0' cellspacing='10' cellpadding='0'><tr><td class='h2' style='text-align:center;'>" + sendEmailParameter.subject + "<hr class='hrcolor' /></td></tr></table>";

                                sendEmailParameter.Note = re.Replace(footerNote, replace => replacements.ContainsKey(replace.Value) ? replacements[replace.Value] == null ? "" : replacements[replace.Value].ToString() : "");



                                //Notification recipient EmailAddress
                                NotificationRecipientEmailAddress notificationRecipientEmailAddress = _notificationRecipientEmailAddressRepository.GetRecipientByActivityNotificationID(rActivityNotification.ActivityNotificationID);

                                if (notificationRecipientEmailAddress != null)
                                {
                                    String RecipientEmailCc = notificationRecipientEmailAddress.EmailAddress;

                                    string[] RecipientEmail = RecipientEmailCc.Split(',');
                                    List<string> ListOfRecipientEmail = new List<string>();

                                    foreach (var RAddress in RecipientEmail)
                                    {
                                        if (!String.IsNullOrEmpty(RAddress.Trim()))
                                        {
                                            bool isValidEmailCc = _preEmailConditionManager.IsEmailValid(RAddress);
                                            if (isValidEmailCc)
                                            {
                                                sendEmailParameter.cc = sendEmailParameter.cc + "," + RAddress;
                                            }
                                        }
                                    }
                                }

                                sendEmailParameter.NotificationID = replaceRequest.NotificationID;

                                //Add to email list
                                sendEmailParameters.Add(sendEmailParameter);
                            }
                        }
                    }

                    if (notification.RecipientType == (int)EmailRecipientTypeEnumeration.Grouped)
                    {
                        SendEmailParameter EmailParameter = sendEmailParameters.FirstOrDefault();
                        if (sendEmailParameters != null && sendEmailParameters.Count > 1)
                        {
                            foreach (SendEmailParameter emailParameter in sendEmailParameters)
                            {
                                EmailParameter.To = EmailParameter.To == emailParameter.To ? EmailParameter.To : EmailParameter.To + "," + emailParameter.To;
                            }
                        }
                        sendEmailParameters = new List<SendEmailParameter>();



                        sendEmailParameters.Add(EmailParameter);
                    }


                }
                _logger.LogDebug("Returned successfully from GetPlacehodersValue method");
            }
            catch (RepositoryException ex)
            {
                _logger.LogDebug("Returned unsuccessfully from GetPlacehodersValue method");
                throw new BusinessException("Exception at PlaceHoldersManager -->GetPlacehodersValue method ", ex);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from GetPlacehodersValue method");
                throw ex;
            }
            return sendEmailParameters;
        }

        private List<EmailTemplatePlaceholdersViewModel> GetEmailRecipientData(PlaceHolderReplaceRequest request)
        {
            EmailTemplatePlaceholdersViewModel emailRecipientPlaceholder = null;
            List<EmailTemplatePlaceholdersViewModel> emailTemplatePlaceholders = new List<EmailTemplatePlaceholdersViewModel>();
            LoanApplication application = null;
            var disbursementStatus = string.Empty;            
            if (request.ApplicationID > 0)
            {
                application = this._emailPlaceholderRepository.GetLoanApplicationDetails(request.ApplicationID);


                //CommonConstants.ThresholdRequestAmount = 0;
                //var masterOptionResponse = _genaralOptionRepository.GetMasterOption(CommonConstants.THRESHOLD_REQUEST_FLAG);
                //if (masterOptionResponse != null && masterOptionResponse.Count > 0)
                //{
                //    CommonConstants.ThresholdRequestAmount = long.Parse(masterOptionResponse.FirstOrDefault().OptionValue);
                //}               
                // if (application != null && application.FundingApplication.RequestedFundAmount > 250000)
                //if (application != null && application.FundingApplication.RequestedFundAmount > CommonConstants.ThresholdRequestAmount)
                if (application != null && application.FundingApplication.IsPaymentSchedule==true)
                {
                    disbursementStatus = application.PaymentScheduleStatus != null ? application.PaymentScheduleStatus.Status : string.Empty;
                }
            }

            if (request.Role == EmailRecipientEnumeration.Contact.ToString())
            {
                ThoughtFocus.DataAccess.Models.Contact.Contact contact = this._emailPlaceholderRepository.GetContactPlaceholderData(request.ContactID);
                if (contact != null && contact.ContactID > 0)
                {
                    emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                    emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", contact.FirstName, contact.LastName);
                    emailRecipientPlaceholder.RecipientFirstName = contact.FirstName == null ? string.Empty : contact.FirstName;
                    emailRecipientPlaceholder.RecipientLastName = contact.LastName == null ? string.Empty : contact.LastName;
                    emailRecipientPlaceholder.RecipientEmailAddress = contact.EmailAddress == null ? String.Empty : contact.EmailAddress;
                    emailRecipientPlaceholder.RecipientSalutation = contact.Salutation == null ? string.Empty : contact.Salutation.SalutationName;
                    emailRecipientPlaceholder.Contact = emailRecipientPlaceholder.RecipientEmailAddress;
                    
                    if (request.NotificationID == (long)EmailTemplateNameID.PROGRAMINVITATION)
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                        BusinessEntity BusinessEntity = this._emailPlaceholderRepository.GetBusinessEntityPlaceholderData(programInvitation.BusinessID);
                        emailRecipientPlaceholder.BusinessName = BusinessEntity.BusinessName;
                        emailRecipientPlaceholder.TimeFrame = "5 business days";
                    }
                    if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                    {
                        emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                        emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;
                    }
                }

                if (emailRecipientPlaceholder != null)
                    emailTemplatePlaceholders.Add(emailRecipientPlaceholder);
            }

            if (request.Role == EmailRecipientEnumeration.Administrator.ToString())
            {
                
                List<ThoughtFocus.DataAccess.Models.Contact.Contact> contacts = this._emailPlaceholderRepository.GetContactDetailsByRole(request.Role);
                var contactList = GetContactByPrograms(contacts, application);
                if (contactList != null && contactList.Count > 0)
                {
                    contacts = contacts.FindAll(cc => contactList.Contains(cc.ContactID));
                }
                
                foreach (var contact in contacts)
                {
                    if (contact != null && contact.ContactID > 0)
                    {
                        emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                        emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", contact.FirstName, contact.LastName);
                        emailRecipientPlaceholder.RecipientFirstName = contact.FirstName == null ? string.Empty : contact.FirstName;
                        emailRecipientPlaceholder.RecipientLastName = contact.LastName == null ? string.Empty : contact.LastName;
                        emailRecipientPlaceholder.RecipientEmailAddress = contact.EmailAddress == null ? String.Empty : contact.EmailAddress;
                        emailRecipientPlaceholder.RecipientSalutation = contact.Salutation == null ? string.Empty : contact.Salutation.SalutationName;
                        emailRecipientPlaceholder.Administrator = emailRecipientPlaceholder.RecipientEmailAddress;
                        emailRecipientPlaceholder.AdditionalMessage = request.AdditionalMessage;
                        emailRecipientPlaceholder.Disbursement = disbursementStatus;

                        if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                        {
                            emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                            emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;

                        }
                    }

                    if (emailRecipientPlaceholder != null)
                        emailTemplatePlaceholders.Add(emailRecipientPlaceholder);

                }

            }

            if (request.Role == EmailRecipientEnumeration.Borrower.ToString())
            {
                List<ThoughtFocus.DataAccess.Models.Contact.BusinessUser> BusinessUsers = this._emailPlaceholderRepository.GetBorrowerDetails(request.ApplicationID);

                foreach (var BusinessUser in BusinessUsers)
                {
                    if (BusinessUser != null && BusinessUser.BusinessUserID > 0)
                    {
                        emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                        emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", BusinessUser.Contact.FirstName, BusinessUser.Contact.LastName);
                        emailRecipientPlaceholder.RecipientFirstName = BusinessUser.Contact.FirstName == null ? string.Empty : BusinessUser.Contact.FirstName;
                        emailRecipientPlaceholder.RecipientLastName = BusinessUser.Contact.LastName == null ? string.Empty : BusinessUser.Contact.LastName;
                        emailRecipientPlaceholder.RecipientEmailAddress = BusinessUser.Contact.EmailAddress == null ? String.Empty : BusinessUser.Contact.EmailAddress;
                        emailRecipientPlaceholder.RecipientSalutation = BusinessUser.Contact.Salutation == null ? string.Empty : BusinessUser.Contact.Salutation.SalutationName;
                        emailRecipientPlaceholder.Borrower = emailRecipientPlaceholder.RecipientEmailAddress;
                        emailRecipientPlaceholder.AdditionalMessage = request.AdditionalMessage;
                        emailRecipientPlaceholder.Disbursement = disbursementStatus;

                        if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                        {
                            emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                            emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;
                        }

                    }

                    if (emailRecipientPlaceholder != null)
                        emailTemplatePlaceholders.Add(emailRecipientPlaceholder);
                }

            }

            if (request.Role == EmailRecipientEnumeration.NULTreasury.ToString())
            {
                List<ThoughtFocus.DataAccess.Models.User.User> Users = this._emailPlaceholderRepository.GetNULTreasuryDetails();

                var userList = GetUsersBYContactPrograms(Users, application);

                if(userList != null && userList.Count > 0)
                {
                    Users = Users.FindAll(u => userList.Contains(u.UserID));
                }
                foreach (var user in Users)
                {
                    if (user != null && user.ContactID > 0)
                    {
                        emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                        emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", user.Contact.FirstName, user.Contact.LastName);
                        emailRecipientPlaceholder.RecipientFirstName = user.Contact.FirstName == null ? string.Empty : user.Contact.FirstName;
                        emailRecipientPlaceholder.RecipientLastName = user.Contact.LastName == null ? string.Empty : user.Contact.LastName;
                        emailRecipientPlaceholder.RecipientEmailAddress = user.Contact.EmailAddress == null ? String.Empty : user.Contact.EmailAddress;
                        emailRecipientPlaceholder.RecipientSalutation = user.Contact.Salutation == null ? string.Empty : user.Contact.Salutation.SalutationName;
                        emailRecipientPlaceholder.NULTreasury = emailRecipientPlaceholder.RecipientEmailAddress;
                        emailRecipientPlaceholder.AdditionalMessage = request.AdditionalMessage;
                        if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                        {
                            emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                            emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;

                        }
                    }

                    if (emailRecipientPlaceholder != null)
                        emailTemplatePlaceholders.Add(emailRecipientPlaceholder);

                }

            }

            if (request.Role == EmailRecipientEnumeration.UnderWriter.ToString())
            {
                List<ThoughtFocus.DataAccess.Models.Contact.Contact> contacts = this._emailPlaceholderRepository.GetContactDetailsByRole(request.Role);

                if (request.NotificationID != (long)EmailTemplateNameID.FUNDDETAILS)
                {
                    var contactList = GetContactByPrograms(contacts, application);

                    if (contactList != null && contactList.Count > 0)
                    {
                        contacts = contacts.FindAll(cc => contactList.Contains(cc.ContactID));
                    }
                }
                else
                {
                    ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                    var contactList = GetContactByProgramID(contacts, programInvitation.ProgramID);
                    if (contactList != null && contactList.Count > 0)
                    {
                        contacts = contacts.FindAll(cc => contactList.Contains(cc.ContactID));
                    }
                }

                foreach (var contact in contacts)
                {
                    if (contact != null && contact.ContactID > 0)
                    {
                        emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                        emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", contact.FirstName, contact.LastName);
                        emailRecipientPlaceholder.RecipientFirstName = contact.FirstName == null ? string.Empty : contact.FirstName;
                        emailRecipientPlaceholder.RecipientLastName = contact.LastName == null ? string.Empty : contact.LastName;
                        emailRecipientPlaceholder.RecipientEmailAddress = contact.EmailAddress == null ? String.Empty : contact.EmailAddress;
                        emailRecipientPlaceholder.RecipientSalutation = contact.Salutation == null ? string.Empty : contact.Salutation.SalutationName;
                        emailRecipientPlaceholder.UnderWriter = emailRecipientPlaceholder.RecipientEmailAddress;
                        emailRecipientPlaceholder.AdditionalMessage = request.AdditionalMessage;
                        emailRecipientPlaceholder.Disbursement = disbursementStatus;

                        if (request.NotificationID == (long)EmailTemplateNameID.FUNDDETAILS)
                        {
                            ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                            BusinessEntity BusinessEntity = this._emailPlaceholderRepository.GetBusinessEntityPlaceholderData(programInvitation.BusinessID);
                            emailRecipientPlaceholder.BusinessName = BusinessEntity.BusinessName;
                        }

                        if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                        {
                            emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                            emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;

                        }
                    }

                    if (emailRecipientPlaceholder != null)
                        emailTemplatePlaceholders.Add(emailRecipientPlaceholder);

                }

            }

            if (request.Role == EmailRecipientEnumeration.LoanProcessor.ToString())
            {
                List<ThoughtFocus.DataAccess.Models.Contact.Contact> contacts = this._emailPlaceholderRepository.GetContactDetailsByRole(request.Role);

                var contactList = GetContactByPrograms(contacts, application);

                if (contactList != null && contactList.Count > 0)
                {
                    contacts = contacts.FindAll(cc => contactList.Contains(cc.ContactID));
                }

                foreach (var contact in contacts)
                {
                    if (contact != null && contact.ContactID > 0)
                    {
                        emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                        emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", contact.FirstName, contact.LastName);
                        emailRecipientPlaceholder.RecipientFirstName = contact.FirstName == null ? string.Empty : contact.FirstName;
                        emailRecipientPlaceholder.RecipientLastName = contact.LastName == null ? string.Empty : contact.LastName;
                        emailRecipientPlaceholder.RecipientEmailAddress = contact.EmailAddress == null ? String.Empty : contact.EmailAddress;
                        emailRecipientPlaceholder.RecipientSalutation = contact.Salutation == null ? string.Empty : contact.Salutation.SalutationName;
                        emailRecipientPlaceholder.LoanProcessor = emailRecipientPlaceholder.RecipientEmailAddress;
                        emailRecipientPlaceholder.AdditionalMessage = request.AdditionalMessage;
                        if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                        {
                            emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                            emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;

                        }
                    }

                    if (emailRecipientPlaceholder != null)
                        emailTemplatePlaceholders.Add(emailRecipientPlaceholder);

                }

            }

            if (request.Role == EmailRecipientEnumeration.Controller.ToString())
            {
                List<ThoughtFocus.DataAccess.Models.User.User> Users = this._emailPlaceholderRepository.GetControllerDetails();
                var userList = GetUsersBYContactPrograms(Users, application);

                if (userList != null && userList.Count > 0)
                {
                    Users = Users.FindAll(u => userList.Contains(u.UserID));
                }
                foreach (var user in Users)
                {
                    if (user != null && user.ContactID > 0)
                    {
                        emailRecipientPlaceholder = new EmailTemplatePlaceholdersViewModel();
                        emailRecipientPlaceholder.RecipientFullName = String.Format("{0} {1}", user.Contact.FirstName, user.Contact.LastName);
                        emailRecipientPlaceholder.RecipientFirstName = user.Contact.FirstName == null ? string.Empty : user.Contact.FirstName;
                        emailRecipientPlaceholder.RecipientLastName = user.Contact.LastName == null ? string.Empty : user.Contact.LastName;
                        emailRecipientPlaceholder.RecipientEmailAddress = user.Contact.EmailAddress == null ? String.Empty : user.Contact.EmailAddress;
                        emailRecipientPlaceholder.RecipientSalutation = user.Contact.Salutation == null ? string.Empty : user.Contact.Salutation.SalutationName;
                        emailRecipientPlaceholder.Controller = emailRecipientPlaceholder.RecipientEmailAddress;
                        emailRecipientPlaceholder.AdditionalMessage = request.AdditionalMessage;
                        if (request.NotificationID == (long)EmailTemplateNameID.STATECHANGE)
                        {
                            emailRecipientPlaceholder.NextStatus = request.ExecutedActivityState;
                            emailRecipientPlaceholder.CurrentStatus = request.CurrentActivityName;

                        }
                    }

                    if (emailRecipientPlaceholder != null)
                        emailTemplatePlaceholders.Add(emailRecipientPlaceholder);

                }

            }

            foreach (var emailTemplatePlaceholder in emailTemplatePlaceholders)
            {
                if (application != null)
                {
                    string affiliateEmailAddress = application?.LoanBusinessDetail?.Affiliate?.UrbanLeagueAffiliateContacts.Select(x => x.EmailAddress).FirstOrDefault();
                    emailTemplatePlaceholder.LoanNumber = application.LoanNumber;
                    emailTemplatePlaceholder.Affiliate = affiliateEmailAddress;
                    emailTemplatePlaceholder.BusinessName = application.LoanBusinessDetail.BusinessName;
                    emailTemplatePlaceholder.CallBackURL = request.CallBackURL;
                    emailRecipientPlaceholder.RequestedFundAmount = application.FundingApplication.RequestedFundAmount;
                    
                    //Incase business name is empty in application table then name will be fetched by ProgramInvitationID
                    if (emailTemplatePlaceholder.BusinessName == string.Empty || emailTemplatePlaceholder.BusinessName == null)
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(application.ProgramInvitationID);
                        BusinessEntity BusinessEntity = this._emailPlaceholderRepository.GetBusinessEntityPlaceholderData(programInvitation.BusinessID);
                        emailTemplatePlaceholder.BusinessName = application.LoanBusinessDetail.BusinessName;

                    }
                }

                if (emailTemplatePlaceholder.Administrator == null || emailTemplatePlaceholder.Administrator == "")
                {
                    List<ThoughtFocus.DataAccess.Models.Contact.Contact> administratorContacts = this._emailPlaceholderRepository.GetContactDetailsByRole(EmailRecipientEnumeration.Administrator.ToString());

                    var contactList = new List<long>();
                    if(application != null)
                    {
                        contactList = GetContactByPrograms(administratorContacts, application);
                    }
                    else
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                        if(programInvitation != null)
                        {
                            contactList = GetContactByProgramID(administratorContacts, programInvitation.ProgramID);
                        }
                        
                    }
                    

                    if (contactList != null && contactList.Count > 0)
                    {
                        administratorContacts = administratorContacts.FindAll(u => contactList.Contains(u.ContactID));
                        foreach (var admin in administratorContacts)
                        {
                            emailTemplatePlaceholder.Administrator += admin.EmailAddress == null ? String.Empty : admin.EmailAddress + ",";
                        }
                    }
                    
                }

                if (emailTemplatePlaceholder.UnderWriter == null || emailTemplatePlaceholder.UnderWriter == "")
                {
                    List<ThoughtFocus.DataAccess.Models.Contact.Contact> underWriterContacts = this._emailPlaceholderRepository.GetContactDetailsByRole(EmailRecipientEnumeration.UnderWriter.ToString());
                    var contactList = new List<long>();
                    if (application != null)
                    {
                        contactList = GetContactByPrograms(underWriterContacts, application);
                    }
                    else
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                        if(programInvitation != null)
                        {
                            contactList = GetContactByProgramID(underWriterContacts, programInvitation.ProgramID);
                        }
                        
                    }
                    if (contactList != null && contactList.Count > 0)
                    {
                        underWriterContacts = underWriterContacts.FindAll(u => contactList.Contains(u.ContactID));
                        foreach (var underWriter in underWriterContacts)
                        {
                            emailTemplatePlaceholder.UnderWriter += underWriter.EmailAddress == null ? String.Empty : underWriter.EmailAddress + ",";
                        }
                    }
                    
                }

                if (emailTemplatePlaceholder.LoanProcessor == null || emailTemplatePlaceholder.LoanProcessor == "")
                {
                    List<ThoughtFocus.DataAccess.Models.Contact.Contact> LoanProcessorContacts = this._emailPlaceholderRepository.GetContactDetailsByRole(EmailRecipientEnumeration.LoanProcessor.ToString());
                    var contactList = new List<long>();
                    if (application != null)
                    {
                        contactList = GetContactByPrograms(LoanProcessorContacts, application);
                    }
                    else
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                        if(programInvitation != null)
                        {
                            contactList = GetContactByProgramID(LoanProcessorContacts, programInvitation.ProgramID);
                        }
                        
                    }
                    if (contactList != null && contactList.Count > 0)
                    {
                        LoanProcessorContacts = LoanProcessorContacts.FindAll(u => contactList.Contains(u.ContactID));
                        foreach (var loanProcessor in LoanProcessorContacts)
                        {
                            emailTemplatePlaceholder.LoanProcessor += loanProcessor.EmailAddress == null ? String.Empty : loanProcessor.EmailAddress + ",";
                        }
                    }
                    
                }

                if (emailTemplatePlaceholder.NULTreasury == null || emailTemplatePlaceholder.NULTreasury == "")
                {
                    List<ThoughtFocus.DataAccess.Models.Contact.Contact> NULTreasuryContacts = this._emailPlaceholderRepository.GetContactDetailsByRole(EmailRecipientEnumeration.NULTreasury.ToString());

                    var contactList = new List<long>();
                    if (application != null)
                    {
                        contactList = GetContactByPrograms(NULTreasuryContacts, application);
                    }
                    else
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                        if(programInvitation != null)
                        {
                            contactList = GetContactByProgramID(NULTreasuryContacts, programInvitation.ProgramID);
                        }
                        
                    }

                    if (contactList != null && contactList.Count > 0)
                    {
                        NULTreasuryContacts = NULTreasuryContacts.FindAll(u => contactList.Contains(u.ContactID));
                        foreach (var treasuryContacts in NULTreasuryContacts)
                        {
                            emailTemplatePlaceholder.NULTreasury += treasuryContacts.EmailAddress == null ? String.Empty : treasuryContacts.EmailAddress + ",";
                        }
                    }
                    
                }

                if (emailTemplatePlaceholder.Controller == null || emailTemplatePlaceholder.Controller == "")
                {
                    List<ThoughtFocus.DataAccess.Models.Contact.Contact> ControllerContacts = this._emailPlaceholderRepository.GetContactDetailsByRole(EmailRecipientEnumeration.Controller.ToString());

                    var contactList = new List<long>();
                    if (application != null)
                    {
                        contactList = GetContactByPrograms(ControllerContacts, application);
                    }
                    else
                    {
                        ProgramInvitation programInvitation = this._emailPlaceholderRepository.GetProgramPlaceholderData(request.ProgramInvitationID);
                        if(programInvitation != null)
                        {
                            contactList = GetContactByProgramID(ControllerContacts, programInvitation.ProgramID);
                        }
                    }

                    if (contactList != null && contactList.Count > 0)
                    {
                        ControllerContacts = ControllerContacts.FindAll(u => contactList.Contains(u.ContactID));
                        foreach (var controller in ControllerContacts)
                        {
                            emailTemplatePlaceholder.Controller += controller.EmailAddress == null ? String.Empty : controller.EmailAddress + ",";
                        }
                    }
                    
                }

            }


            return emailTemplatePlaceholders;

        }

        private List<long> GetContactByPrograms(List<ThoughtFocus.DataAccess.Models.Contact.Contact> contacts, LoanApplication application)
        {
            var contactList = new List<long>();

            foreach (var c in contacts)
            {
                if (c.ProgramInvitationContactRoles != null && c.ProgramInvitationContactRoles.Count > 0)
                {
                    var p = c.ProgramInvitationContactRoles.Where(cr => cr.ProgramID == application.ProgramInvitation.ProgramID && cr.IsActive == true);
                    if (p != null && p.Count() > 0)
                    {
                        contactList.Add(p.FirstOrDefault().ContactID);
                    }
                    else
                    {
                        var allPrograms = c.ProgramInvitationContactRoles.Where(cr => cr.ProgramID == 0 && cr.IsActive == true);
                        if (allPrograms != null && allPrograms.Count() > 0)
                        {
                            contactList.Add(allPrograms.FirstOrDefault().ContactID);
                        }
                    }
                }
                else
                {
                    contactList.Add(c.ContactID);
                }

            }
            return contactList;
        }
        private List<long> GetContactByProgramID(List<ThoughtFocus.DataAccess.Models.Contact.Contact> contacts, long programID)
        {
            var contactList = new List<long>();

            foreach (var c in contacts)
            {
                if (c.ProgramInvitationContactRoles != null && c.ProgramInvitationContactRoles.Count > 0)
                {
                    var p = c.ProgramInvitationContactRoles.Where(cr => cr.ProgramID == programID && cr.IsActive == true);
                    if (p != null && p.Count() > 0)
                    {
                        contactList.Add(p.FirstOrDefault().ContactID);
                    }
                    else
                    {
                        var allPrograms = c.ProgramInvitationContactRoles.Where(cr => cr.ProgramID == 0 && cr.IsActive == true);
                        if (allPrograms != null && allPrograms.Count() > 0)
                        {
                            contactList.Add(allPrograms.FirstOrDefault().ContactID);
                        }
                    }
                }
                else
                {
                    contactList.Add(c.ContactID);
                }

            }
            return contactList;
        }
        private List<long> GetUsersBYContactPrograms(List<ThoughtFocus.DataAccess.Models.User.User> Users, LoanApplication application)
        {
            var userList = new List<long>();

            foreach (var u in Users)
            {

                if (u.Contact.ProgramInvitationContactRoles != null && u.Contact.ProgramInvitationContactRoles.Count > 0)
                {
                    var p = u.Contact.ProgramInvitationContactRoles.Where(cr => cr.ProgramID == application.ProgramInvitation.ProgramID && cr.IsActive == true);
                    if (p != null && p.Count() > 0)
                    {
                        userList.Add(u.UserID);
                    }
                    else
                    {
                        var allPrograms = u.Contact.ProgramInvitationContactRoles.Where(cr => cr.ProgramID == 0 && cr.IsActive == true);
                        if (allPrograms != null && allPrograms.Count() > 0)
                        {
                            userList.Add(u.UserID);
                        }
                    }
                }
                else
                {
                    userList.Add(u.UserID);
                }

            }
            return userList;
        }
        #endregion Methods
    }
}
