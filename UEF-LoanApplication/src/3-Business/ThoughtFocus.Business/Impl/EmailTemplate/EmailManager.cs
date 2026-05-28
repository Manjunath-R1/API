 
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using ThoughtFocus.Common.Utilities;
using ThoughtFocus.Business.Interfaces.EmailTemplate;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.User;
using Microsoft.Extensions.Options;
using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Enumeration;

namespace ThoughtFocus.Business.Impl.EmailTemplate
{
    public class EmailManager : IEmailManager
    {
        #region Fields

        private  readonly ILogger<EmailManager> _logger;

        private IPreEmailConditionManager _preEmailConditionManager;
        private IPostEmailActionManager _postEmailActionManager;
         private readonly EmailSettings _emailSettings;

          private readonly SendEmail _sendEmail;
         

        #endregion Fields

        #region Constructor

        public EmailManager(IPreEmailConditionManager preEmailConditionManager, IPostEmailActionManager postEmailActionManager,
          ILogger<EmailManager> logger,IOptions<EmailSettings> emailSettings,SendEmail sendEmail )
        {
            this._preEmailConditionManager = preEmailConditionManager;
            this._postEmailActionManager = postEmailActionManager;
            this._logger = logger;
            this._emailSettings=emailSettings.Value;
            this. _sendEmail=sendEmail;
           
        }

        #endregion Constructor

        #region Methods

        public bool SendEmailNotification(List<SendEmailParameter> sendEmailParameters,UserSessionEntity userSessionEntity)
        {
            _logger.LogDebug("Entered SendEmailNotification method");
            if (sendEmailParameters.Count() <= 0 || sendEmailParameters == null)
            {
                _logger.LogError("sendEmailParameters is Null");
                return false;
            }

            bool emailSent = false;
            try
            {
                List<string> toEmailAddressList = null;
                List<string> ccEmailAddressList = null;
                List<string> replyToEmailAddressList = null;
                
                foreach (var emailParameter in sendEmailParameters)
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //Check email preference of contact in toList
                        toEmailAddressList = emailParameter.To.Split(',').ToList();
                        foreach (var toRecipient in toEmailAddressList)
                        {
                            bool isValidEmail = _preEmailConditionManager.IsEmailValid(toRecipient.Trim());

                            if (isValidEmail)
                            {
                               mailMessage.To.Add(toRecipient);
                            }
                        }

                        if (emailParameter.cc != null)
                        {
                            //Check email preference of contact in ccList
                            ccEmailAddressList = emailParameter.cc.Split(',').ToList();

                            foreach (var ccRecipient in ccEmailAddressList)
                            {
                                if (ccRecipient != "")
                                {
                                    bool isValidEmailCC = _preEmailConditionManager.IsEmailValid(ccRecipient.Trim());

                                    if (isValidEmailCC)
                                    {      
                                        if(emailParameter.NotificationID == (long)EmailTemplateNameEnumeration.AGREEMENTSUBMITTED || emailParameter.NotificationID == (long)EmailTemplateNameEnumeration.AGREEMENTREJECTED || emailParameter.NotificationID == (long)EmailTemplateNameEnumeration.CFOAPPROVED)  
                                            mailMessage.CC.Add(ccRecipient); 
                                        else                          
                                            mailMessage.Bcc.Add(ccRecipient);                                      
                                    }
                                }
                            }
                        }                       

                        //Prepare email message data
                        mailMessage.Subject = emailParameter.subject;
                        mailMessage.Body = String.Format("{0}{1}{2}{3}{4}{5}", emailParameter.Header, emailParameter.BodySubjectHTML, emailParameter.salutation, emailParameter.body, emailParameter.Note, emailParameter.Footer);

                        //Attachment
                        if(emailParameter.NotificationID == (long)EmailTemplateNameID.APPROVEDAPPLICATION)
                        {
                            //string attachmentPath = ;
                            //mailMessage.Attachments.Add(new Attachment("Terms of Use (Build Back Black).docx"));
                        }

                        if (emailParameter.Reply)
                        {
                            replyToEmailAddressList = emailParameter.ReplyToList.Split(',').ToList();
                            foreach (var replyTo in replyToEmailAddressList)
                            {
                                mailMessage.ReplyToList.Add(replyTo);
                            }
                        }
                        //Send email
                        if (mailMessage.To.Count() > 0)
                        {
                            emailSent = _sendEmail.Send(mailMessage,_emailSettings);
                        }
                        else
                        {
                            _logger.LogDebug("To recipient Is null in send email", emailParameter);
                        }


                        //log email notification
                        if (emailSent)
                        {

                            List<string> ccEmailParameterList = new List<string>();
                            List<string> ListOfCc = new List<string>();

                            if (emailParameter.cc != null)
                            {
                                //Check email preference of contact in ccList
                                ccEmailParameterList = emailParameter.cc.Split(',').ToList();

                                foreach (var ccEmailRecipient in ccEmailParameterList)
                                {
                                    if (ccEmailRecipient != "" && !ListOfCc.Contains(ccEmailRecipient))
                                    {
                                        bool isValidEmailCC = _preEmailConditionManager.IsEmailValid(ccEmailRecipient.Trim());

                                        if (isValidEmailCC)
                                        {
                                            ListOfCc.Add(ccEmailRecipient);
                                        }
                                    }
                                }
                            } 

                            emailParameter.cc = String.Join(",", ListOfCc);

                            this._postEmailActionManager.logEmailNotification(emailParameter, mailMessage,userSessionEntity);
                        }
                        else
                        {
                            _logger.LogError("Failed to send email, Notification ID is -"+emailParameter.NotificationID.ToString());
                             _logger.LogError("Mail recipient is :"+ mailMessage?.To.ToString());
                            continue;
                        }
                    }
                }
                _logger.LogDebug("Returned successfully from SendEmailNotification method");
            }
            catch (RepositoryException ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");
                throw new BusinessException("RepositoryException at -->SendEmailNotification", ex);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");
                throw ex;
            }
            return emailSent;
        }

        public bool SendEmailNotificationExceptBorrower(List<SendEmailParameter> sendEmailParameters, UserSessionEntity userSessionEntity)
        {
            _logger.LogDebug("Entered SendEmailNotification method");
            if (sendEmailParameters.Count() <= 0 || sendEmailParameters == null)
            {
                _logger.LogError("sendEmailParameters is Null");
                return false;
            }

            bool emailSent = false;
            try
            {
                List<string> toEmailAddressList = null;
                List<string> ccEmailAddressList = null;
                List<string> replyToEmailAddressList = null;

                foreach (var emailParameter in sendEmailParameters)
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        //Check email preference of contact in toList
                        if (emailParameter.To != string.Empty)
                        {
                            toEmailAddressList = emailParameter.To.Split(',').ToList();
                            foreach (var toRecipient in toEmailAddressList)
                            {
                                bool isValidEmail = _preEmailConditionManager.IsEmailValid(toRecipient.Trim());

                                if (isValidEmail)
                                {
                                    mailMessage.To.Add(toRecipient);
                                }
                            }
                        }
                        

                        if (emailParameter.cc != null)
                        {
                            //Check email preference of contact in ccList
                            ccEmailAddressList = emailParameter.cc.Split(',').ToList();

                            foreach (var ccRecipient in ccEmailAddressList)
                            {
                                if (ccRecipient != "")
                                {
                                    bool isValidEmailCC = _preEmailConditionManager.IsEmailValid(ccRecipient.Trim());

                                    if (isValidEmailCC)
                                    {
                                        if (emailParameter.NotificationID == (long)EmailTemplateNameEnumeration.AGREEMENTSUBMITTED || emailParameter.NotificationID == (long)EmailTemplateNameEnumeration.AGREEMENTREJECTED || emailParameter.NotificationID == (long)EmailTemplateNameEnumeration.CFOAPPROVED)
                                            mailMessage.CC.Add(ccRecipient);
                                        else
                                            mailMessage.Bcc.Add(ccRecipient);
                                    }
                                }
                            }
                        }

                        //Prepare email message data
                        mailMessage.Subject = emailParameter.subject;
                        mailMessage.Body = String.Format("{0}{1}{2}{3}{4}{5}", emailParameter.Header, emailParameter.BodySubjectHTML, emailParameter.salutation, emailParameter.body, emailParameter.Note, emailParameter.Footer);

                        //Attachment
                        if (emailParameter.NotificationID == (long)EmailTemplateNameID.APPROVEDAPPLICATION)
                        {
                            //string attachmentPath = ;
                            //mailMessage.Attachments.Add(new Attachment("Terms of Use (Build Back Black).docx"));
                        }

                        if (emailParameter.Reply)
                        {
                            replyToEmailAddressList = emailParameter.ReplyToList.Split(',').ToList();
                            foreach (var replyTo in replyToEmailAddressList)
                            {
                                mailMessage.ReplyToList.Add(replyTo);
                            }
                        }
                        //Send email
                        if (mailMessage.To.Count() > 0 || mailMessage.CC.Count > 0 || mailMessage.Bcc.Count > 0)
                        {
                            emailSent = _sendEmail.SendEmailExceptBorrower(mailMessage, _emailSettings);
                        }
                        else
                        {
                            _logger.LogDebug("To recipient Is null in send email", emailParameter);
                        }


                        //log email notification
                        if (emailSent)
                        {

                            List<string> ccEmailParameterList = new List<string>();
                            List<string> ListOfCc = new List<string>();

                            if (emailParameter.cc != null)
                            {
                                //Check email preference of contact in ccList
                                ccEmailParameterList = emailParameter.cc.Split(',').ToList();

                                foreach (var ccEmailRecipient in ccEmailParameterList)
                                {
                                    if (ccEmailRecipient != "" && !ListOfCc.Contains(ccEmailRecipient))
                                    {
                                        bool isValidEmailCC = _preEmailConditionManager.IsEmailValid(ccEmailRecipient.Trim());

                                        if (isValidEmailCC)
                                        {
                                            ListOfCc.Add(ccEmailRecipient);
                                        }
                                    }
                                }
                            }

                            emailParameter.cc = String.Join(",", ListOfCc);

                            this._postEmailActionManager.logEmailNotification(emailParameter, mailMessage, userSessionEntity);
                        }
                        else
                        {
                            _logger.LogError("Failed to send email, Notification ID is -" + emailParameter.NotificationID.ToString());
                            _logger.LogError("Mail recipient is :" + mailMessage?.To.ToString());
                            continue;
                        }
                    }
                }
                _logger.LogDebug("Returned successfully from SendEmailNotification method");
            }
            catch (RepositoryException ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");
                throw new BusinessException("RepositoryException at -->SendEmailNotification", ex);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Returned unsuccessfully from SendEmailNotification method");
                throw ex;
            }
            return emailSent;
        }

        #endregion Methods
    }
}
