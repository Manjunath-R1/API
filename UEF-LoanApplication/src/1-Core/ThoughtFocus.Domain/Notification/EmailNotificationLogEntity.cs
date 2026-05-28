namespace ThoughtFocus.Domain.Notification
{
    public class EmailNotificationLogEntity : BaseAbstractEntity
    {
        public long NotificationID { get; set; }
        //public System.DateTime CreatedDateTime { get; set; }
        public string NotificationType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public long EmailNotificationLogID { get; set; }
        public string NotificationTypeDescription { get; set; }
    }
}
