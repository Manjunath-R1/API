using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Notification
{
    public class NotificationActivityEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long ID { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public EmailTemplateEntity EmailTemplateEntity { get; set; }

        #endregion Properties
    }
}
