using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Master;
using ThoughtFocus.Domain.Notification;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.ServiceResponse
{
    public class EmailTemplateDataResponse : BaseResponse
    {
        public List<EmailTemplateEntity> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }

    public class PlaceholderResponse : BaseResponse
    {
        public List<EmailTemplatePlaceholderEntity> Placeholders { get; set; }
        public List<EmailTemplatePlaceholderEntity> EmailRecipientPlaceholders { get; set; }
        public List<EmailTemplatePlaceholderTypeEntity> PlaceholderTypes { get; set; }
        public string ImageContent { get; set; }
    }

    public class NotificationDataResponse : BaseResponse
    {
        public List<EmailTemplatePlaceholderTypeEntity> PlaceholderTypes { get; set; }
    }

}
