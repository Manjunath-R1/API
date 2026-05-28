namespace ThoughtFocus.Domain.Params
{
    using System;
    using System.Collections.Generic;
    using ThoughtFocus.Domain.Master;

    [Serializable]
    public class ProgramQuestionRequest
    {
        #region Properties

        public long ProgramID { get; set; }
        public List<ProgramWiseQuestion> ProgramQuestions { get; set; }

        #endregion Properties
    }
    public class ProgramWiseQuestion
    {
        public long ProgramQuestionID { get; set; }
        public int QuestionID { get; set; }
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }

    }
}