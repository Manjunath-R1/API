namespace ThoughtFocus.Domain.CustomView
{
    public class NotificationTypeViewEntity
    {
        public long NotificationID { get; set; }
        public long EmailNotificationPreferenceID { get; set; }
        public string NotificationType { get; set; }
        public bool EmailPreferenceStatus { get; set; }
        public string NotificationTypeDescription { get; set; }
        public bool IsChecked { get; set; }
        public long UserID { get; set; }
    }
}
