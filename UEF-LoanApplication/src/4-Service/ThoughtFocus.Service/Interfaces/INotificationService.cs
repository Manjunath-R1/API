using ThoughtFocus.Domain.Params;
using ThoughtFocus.Domain.User;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.Application;

namespace ThoughtFocus.Services.Interfaces
{
    public interface INotificationService
    {
       
        bool SendContactEmail( string subject, string callBackURL, long notificationID, long ContactID,string additionalMessage, UserSessionEntity userSessionEntity);

        void SendResetPasswordEmail(string userEmail, string subject, string callBackURL, long notificationID, UserSessionEntity userSessionEntity);

        bool SendPostAccountActivationEmail(Contact contact, string generalEmail, long notificationID);

        bool SendProgramInvitationEmail(string subject, long notificationID, long programID, string callBackURL, long ContactID, UserSessionEntity userSessionEntity, string generalEmail);

        bool SendForgetPasswordEmail(string subject, string callBackURL, long notificationID, long ContactID,UserSessionEntity userSessionEntity);
        
        bool SendResetBusinessContactEmail(string subject, string callBackURL, long notificationID, long ContactID, UserSessionEntity userSessionEntity);
        bool SendSPAEmail(string subject, string callBackURL, long notificationID, long programInvitationID, long ContactID, long loanApplicationID, string role, UserSessionEntity userSessionEntity);
        bool SendPSEmail(string subject, string email, string callBackURL, long notificationID, long programInvitationID, long ContactID, long loanApplicationID, string role, UserSessionEntity userSessionEntity);
        bool SendSMS(LoanApplication loanApplications, long activityID, UserSessionEntity userSessionEntity);


    }
}
