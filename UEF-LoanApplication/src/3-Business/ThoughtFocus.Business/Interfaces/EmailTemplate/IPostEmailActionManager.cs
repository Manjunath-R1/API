using System.Net.Mail;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.CustomView.Notification;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Interfaces.EmailTemplate
{
    public interface IPostEmailActionManager
    { 
        void logEmailNotification(SendEmailParameter sendEmailParameter, MailMessage mailMessage,UserSessionEntity userSessionEntity);
    }
}
