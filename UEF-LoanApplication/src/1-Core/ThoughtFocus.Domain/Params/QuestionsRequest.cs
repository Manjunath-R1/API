namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class QuestionsRequest
    {
        #region Properties

        public long QuestionID { get; set; }
        public string QuestionText { get; set; }

        #endregion Properties
    }
}