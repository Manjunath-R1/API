using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class ProgramQuestionsResponse : BaseResponse
    {
        public List<ProgramQuestionsViewEntity> ProgramQuestionResponse { get; set; }

    }

    public class ProgramQuestionsViewEntity
    {
        #region Properties
        public long? ProgramQuestionID { get; set; }
        public long? QuestionID { get; set; }
        public long? ProgramID { get; set; }
        public string QuestionText { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public long DisplayOrder { get; set; }

        #endregion Properties


    }
}
