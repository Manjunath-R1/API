using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Interfaces.EmailTemplate
{
    public interface IEmailManager
    {
        bool SendEmailNotification(List<SendEmailParameter> sendEmailParameters,UserSessionEntity userSessionEntity);
        bool SendEmailNotificationExceptBorrower(List<SendEmailParameter> sendEmailParameters, UserSessionEntity userSessionEntity);

        
    }
}
