using System;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.Domain.Common;

namespace ThoughtFocus.Business.Interfaces.SMS
{
    public interface ISendSMS
    {
        //bool SendSMSNotifications(string to, string message, SMSSettings smsSettings);
        public bool SendSMSByGetAPI(string to, string message, SMSSettings smsSettings);


        bool SendSMSNotificationsByPostAPI(string to, string message, SMSSettings smsSettings);


    }
}
