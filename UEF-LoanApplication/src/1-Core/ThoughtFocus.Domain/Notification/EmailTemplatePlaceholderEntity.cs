using System;

namespace ThoughtFocus.Domain.Notification
{
    public class EmailTemplatePlaceholderEntity : BaseAbstractEntity
    {
        public long PlaceholderID { get; set; }
        //public System.DateTime CreatedDateTime { get; set; }
        //public long CreatedByUserID { get; set; }
        //public System.DateTime LastModifiedDateTime { get; set; }
        //public long LastModifiedByUserID { get; set; }
        //public bool IsActive { get; set; }
        public string DisplayName { get; set; }
        public string RecipientEmailAddress { get; set; }
        public string Placeholder { get; set; }
        public string Description { get; set; }
        public bool IsNotificationPlaceholder { get; set; }
        public bool IsRecipient { get; set; }
        public bool IsCc { get; set; }
        public Nullable<long> PlaceHolderTypeID { get; set; }
    }
}
