namespace ThoughtFocus.Domain.CustomView
{
    public class SendEmailParameter
    {
       public long NotificationID { get; set; }
       public string To {get; set;}
       public string cc {get; set;}
       public string subject {get; set;}
       public string salutation { get; set; }
       public string Header { get; set; }
       public string Footer { get; set; }
       
       public string MailContent { get; set; }
       public string body { get; set; }
       public string BodySubjectHTML { get; set; }
       public string Note { get; set; }
       public bool Reply { get; set; }
       public string ReplyToList{ get; set; } 
      public string additionalMessage { get; set; }
    }
}
