using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtFocus.Domain.Response
{
    public class ProgramInvitationEmailResponse : BaseResponse
    {
        public ProgramInvitationEmail ProgramInvitationEmail { get; set; }
    }
    public class ProgramInvitationEmail
    {
        public long NotificationID { get; set; }
        public string NotificationType { get; set; }
        public string MessageSubject { get; set; }
        public string Head { get; set; }
        public string Salutation { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public string DefaultTemplate { get; set; }
        public bool IsActive { get; set; }
    }
}
