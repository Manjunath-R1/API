namespace ThoughtFocus.Domain.Params
{

    public class CommunicationTemplateParam
    {
        #region Properties

        public long TemplateID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }

        public long CreatedByUserID { get; set; }

        public System.DateTime LastModifiedDateTime { get; set; }

        public long LastModifiedByUserID { get; set; }

        public bool IsActive { get; set; }

        public string Subject { get; set; }

        public string TemplateName { get; set; }

        public string Header { get; set; }

        public string Salutation { get; set; }

        public string Body { get; set; }

        public string Footer { get; set; }

        #endregion Properties
    }
}
