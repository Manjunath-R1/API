using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Service.Interfaces
{
    public interface ISMSNotificationService
    {
        bool SendSMS(SMSSenderRequest sMSSenderRequest);
    }
}
