using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Notification
{
    public class NotificationOtherEntity : BaseAbstractEntity, IEntity
    {

        #region Properties

        public List<EmailTemplateEntity> EmailTemplateEntity { get; set; }

        #endregion Properties
    }
}
