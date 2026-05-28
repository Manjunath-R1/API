using ThoughtFocus.Domain.Common;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class QuestionsViewEntity : BaseResponse
    {
        #region Properties
        public long QuestionID { get; set; }
        public string QuestionText { get; set; }
        public bool IsActive { get; set; }
        public int ResponseTypeID { get; set; }

        #endregion Properties
    }
}
