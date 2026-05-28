namespace ThoughtFocus.Domain.Notification
{
    public class NotificationTypeEntity 
    {

        #region Properties
        public long NotificationID { get; set; }
        public string NotificationTypeName { get; set; }
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
