namespace ThoughtFocus.Domain.Notification
{
    public class EmailNotificationLogAddresseeEntity
    {
        public long EmailNotificationLogAddresseeID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public long EmailNotificationLogID { get; set; }
        public string EmailAdresss { get; set; }
        public bool IsCc { get; set; }
        public long? NotificationStatusID { get; set; }
    }
}
