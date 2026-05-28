using ThoughtFocus.DataAccess.Models.Application;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.CustomView.Notification;
using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Interfaces.EmailTemplate
{
    public interface IEmailTemplateManager
    {
       
        bool SendOthersEmail(EmailSenderRequest emailRequest,UserSessionEntity userSessionEntity);
        bool SendProgramInvitationEmail(ProgramInvitationEmailSendRequest emailRequest, UserSessionEntity userSessionEntity);
        bool SendWorkflowNotifications<T>(T param, long activityID);
        bool SendSMS(LoanApplication loanApplications, long activityID, UserSessionEntity userSessionEntity);
    }
}
