namespace ThoughtFocus.Business.Interfaces.EmailTemplate
{
    using System.Collections.Generic;
    using ThoughtFocus.Domain.CustomView;
    using ThoughtFocus.Domain.ServiceResponse;


    public interface IGetListOfEmailTemplate
    {
        List<EmailTemplateEntity> GetActivityNotificationList(long ActivityID, long WorkflowNotificationType);

        EmailNotificationActivityResponse GetSiteVisitActivityList();

        EmailNotificationOtherResponse GetEmailNotificationOtherList();

        EmailNotificationActivityResponse GetEmailTemplateByActivityID(long? emailTemplateID, long? activityID);

        EmailNotificationReminderResponse GetReminderNotificationList();
    }
}
