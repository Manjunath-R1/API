using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using ThoughtFocus.Domain.Common;
namespace ThoughtFocus.Common.Utilities
{
    public class SendEmail
    {
        #region Fields

        /// <summary>
        /// ILog instance for logging.
        /// </summary>
        private static ILogger<SendEmail> Logger;

        #endregion Fields

        public SendEmail(ILogger<SendEmail> logger)
        {
            Logger = logger;
        }

        #region Methods

        public bool Send(System.Net.Mail.MailMessage mail, EmailSettings emailSettings)
        {
            Logger.LogDebug("Entered Send method");
            bool isEmailSent = false;
            try
            {
                string smtpAddress = emailSettings.SmtpServerAddress;
                int portNumber = emailSettings.SmtpServerPort;
                bool enableSSL = emailSettings.SmtpServerEnableSSL;
                string EmailUserName = emailSettings.EmailUserName;
                string EmailPassWord = emailSettings.EmailPassWord;
                string EmailFromUserName = emailSettings.EmailFromUserName;

                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailSettings.EmailFromUserName));
                email.To.Add(MailboxAddress.Parse(mail.To.ToString()));

                if(mail.CC.Count > 0)
                {
                    foreach(var cc in mail.CC)
                    {
                        email.Cc.Add(MailboxAddress.Parse(cc.ToString()));
                    }
                    
                }
                if(mail.Bcc.Count > 0)
                {
                    foreach(var bcc in mail.Bcc)
                    {
                        email.Bcc.Add(MailboxAddress.Parse(bcc.ToString()));
                    }
                    
                }
                
                email.Subject = mail.Subject;
                var builder = new BodyBuilder { HtmlBody = mail.Body };
                if (mail.Attachments != null)
                {
                    foreach (var attachment in mail.Attachments)
                    {
                        builder.Attachments.Add(attachment.Name);
                    }
                }
                email.Body = builder.ToMessageBody();

                if (emailSettings.IsSendMail)
                {
                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, cert, chain, e) => true;

                        if (enableSSL)
                            client.Connect(smtpAddress, portNumber, SecureSocketOptions.StartTls);
                        else
                            client.Connect(smtpAddress, portNumber, SecureSocketOptions.Auto);                        
                        client.Authenticate(EmailUserName, EmailPassWord);
                        client.Send(email);
                        client.Disconnect(true);
                        Logger.LogDebug("#Mail-" + mail.Subject + "- sent successfully.");
                    }

                }
                isEmailSent = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.InnerException.ToString());
                isEmailSent = false;
            }
            return isEmailSent;
        }
        public bool SendEmailExceptBorrower(System.Net.Mail.MailMessage mail, EmailSettings emailSettings)
        {
            Logger.LogDebug("Entered Send method");
            bool isEmailSent = false;
            try
            {
                string smtpAddress = emailSettings.SmtpServerAddress;
                int portNumber = emailSettings.SmtpServerPort;
                bool enableSSL = emailSettings.SmtpServerEnableSSL;
                string EmailUserName = emailSettings.EmailUserName;
                string EmailPassWord = emailSettings.EmailPassWord;
                string EmailFromUserName = emailSettings.EmailFromUserName;

                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailSettings.EmailFromUserName));
                
                if (mail.To.ToString() != string.Empty)
                {
                    email.To.Add(MailboxAddress.Parse(mail.To.ToString()));
                }
                

                if (mail.CC.Count > 0)
                {
                    foreach (var cc in mail.CC)
                    {
                        email.Cc.Add(MailboxAddress.Parse(cc.ToString()));
                    }

                }
                if (mail.Bcc.Count > 0)
                {
                    foreach (var bcc in mail.Bcc)
                    {
                        email.Bcc.Add(MailboxAddress.Parse(bcc.ToString()));
                    }

                }

                email.Subject = mail.Subject;
                var builder = new BodyBuilder { HtmlBody = mail.Body };
                if (mail.Attachments != null)
                {
                    foreach (var attachment in mail.Attachments)
                    {
                        builder.Attachments.Add(attachment.Name);
                    }
                }
                email.Body = builder.ToMessageBody();

                if (emailSettings.IsSendMail)
                {
                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, cert, chain, e) => true;

                        if (enableSSL)
                            client.Connect(smtpAddress, portNumber, SecureSocketOptions.StartTls);
                        else
                            client.Connect(smtpAddress, portNumber, SecureSocketOptions.Auto);
                        client.Authenticate(EmailUserName, EmailPassWord);
                        client.Send(email);
                        client.Disconnect(true);
                        Logger.LogDebug("#Mail-" + mail.Subject + "- sent successfully.");
                    }

                }
                isEmailSent = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.InnerException.ToString());
                isEmailSent = false;
            }
            return isEmailSent;
        }

        #endregion Methods
    }
}
