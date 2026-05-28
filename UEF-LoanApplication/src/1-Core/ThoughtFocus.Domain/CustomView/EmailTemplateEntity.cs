namespace ThoughtFocus.Domain.CustomView
{
    public class EmailTemplateEntity
    {
        public long NotificationID { get; set; }
        public long ActivityID { get; set; }
        public string NotificationType { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string NotificationTypeDescription { get; set; }
        public string MessageSubject { get; set; }
        public string TemplateName { get; set; }
        public string EmailTeamplateHead { get; set; }
        public string EmailTeamplateSalutation { get; set; }
        public string EmailTeamplateBody { get; set; }
        public string EmailTeamplateSignature { get; set; }
        public string EmailTeamplateFooter { get; set; }
        public int RecipientType { get; set; }
    }
}
