using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.Notification;

namespace ThoughtFocus.Repository.Interfaces.SMS
{
    public interface ISMSNotificationTemplateRepository : IEFApplicationBaseRepository<SMSNotificationTemplate>
    {
        SMSNotificationTemplate GetSMSNotificationTemplates(long applicationStatusID);
        void SaveSMSNotificationLog(SMSNotificationLog sMSNotificationLog);


    }
}
