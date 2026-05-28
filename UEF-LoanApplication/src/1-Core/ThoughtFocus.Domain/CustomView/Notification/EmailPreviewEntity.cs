using System.Collections.Generic;

namespace ThoughtFocus.Domain.CustomView.Notification
{
    public class EmailPreviewEntity
    {
        public string To { get; set; }
        public string CC { get; set; }
        public object MVCMailerMessage { get; set; }
        public string Salutation { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Header { get; set; }
        public string EmailFooter { get; set; }
        public long NotificationID { get; set; } 
        public string MailContent { get; set; }
        public string BodySubjectHTML { get; set; }
        public string Note { get; set; }

     
    }
}
