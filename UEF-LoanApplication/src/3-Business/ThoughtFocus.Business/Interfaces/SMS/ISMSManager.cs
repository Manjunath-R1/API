using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Business.Interfaces.SMS
{
    public interface ISMSManager
    {
        //string SendSMSNotification(string to, string message,out string from);
        bool SendSMSNotification(SMSSenderRequest sMSSenderRequest);
    }
}
