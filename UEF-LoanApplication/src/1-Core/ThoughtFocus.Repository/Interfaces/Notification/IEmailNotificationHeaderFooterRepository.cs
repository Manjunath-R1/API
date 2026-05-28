using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;

namespace ThoughtFocus.Repository.Interfaces.Notification
{
    public interface IEmailNotificationHeaderFooterRepository : IEFApplicationBaseRepository<EmailTemplateHeaderFooter>
    {
        List<EmailTemplateHeaderFooter> GetEmailTemplateHeaderFooter();

        EmailTemplateHeaderFooter GetEmailTemplateHeaderFooterByID(long EmailTemplateHeaderFooterByID);

        void SaveEmailTemplateHeaderFooter(EmailTemplateHeaderFooter emailNotificationHeaderFooter);

        EmailTemplateHeaderFooter GetEmailTemplateFooter();

        bool AddorUpdateFooter(EmailTemplateHeaderFooter emailNotificationHeaderFooter);

        void MakeEmailTemplateHeaderFooterInactive(EmailTemplateHeaderFooter emailNotificationHeaderFooter);

       // EmailTemplateHeaderFooter GetEmailTemplateLogoCurrentVersion();
    }
}
