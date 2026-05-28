using System.Collections.Generic;
using ThoughtFocus.Domain.Notification;

namespace ThoughtFocus.Domain.Master
{
    public class EmailTemplatePlaceholderTypeEntity : BaseAbstractEntity, IEntity
    {
        public long PlaceHolderTypeID { get; set; }

        public string PlaceHolderType { get; set; }

        public virtual List<EmailTemplatePlaceholderEntity> Placeholders { get; set; }
    }
}
