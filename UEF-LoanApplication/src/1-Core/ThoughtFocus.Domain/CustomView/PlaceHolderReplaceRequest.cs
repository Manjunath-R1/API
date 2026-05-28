using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView.Notification;

namespace ThoughtFocus.Domain.CustomView
{
    public class PlaceHolderReplaceRequest
    {
        public long NotificationID { get; set; }
        public long ActivityID { get; set; }
        public long WorkflowNotificationTypeID { get; set; }        
        public long ContactID { get; set; }       
        public long UserID { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }        
        public string ExecutedActivityState { get; set; }
        public string CurrentActivityName { get; set; }
        public EmailPreviewEntity emailPreviewEntity { get; set; }
        public string CallBackURL { get; set; }
        public DateTime DueDate { get; set; }

        public string AdditionalMessage { get; set; }
        public long ProgramInvitationID { get; set; } 
        public long ApplicationID { get; set; } 

    }

    public class ProgramInvitationEmailPlaceHolderReplaceRequest : PlaceHolderReplaceRequest
    {
        public long BusinessID { get; set; }
        public long ProgramID { get; set; }
    }
}
