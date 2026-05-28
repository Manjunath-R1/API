using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class HelpfulGuideTextResponse : BaseResponse
    {
        public List<HelpfulGuideTextViewEntity> HelpfulGuideTextViewResponse { get; set; }

    }

    public class HelpfulGuideTextViewEntity
    {
        #region Properties
        public long? HelpfulGuideTemplateID { get; set; }
        public long? ProgramID { get; set; }
        public string Body { get; set; }
        public long TypeID { get; set; }
        public string TemplateName { get; set; }

        #endregion Properties


    }
}
