namespace ThoughtFocus.Domain.Notification
{
    public class NotificationPlaceholdersEntity : BaseAbstractEntity
    {
        public long NotificationPlaceholderID { get; set; }

        public long NotificationID { get; set; }

        public long PlaceholderID { get; set; }
    }
}
