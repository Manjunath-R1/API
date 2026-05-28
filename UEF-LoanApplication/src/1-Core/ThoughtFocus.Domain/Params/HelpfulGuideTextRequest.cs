namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class HelpfulGuideTextRequest
    {
        #region Properties

        public long ProgramID { get; set; }
        public string BusinessProfileHelpfulGuideText { get; set; }
        public string FundingApplicationHelpfulGuideText { get; set; }
        public string DocumentsHelpfulGuideText { get; set; }
        public string ReviewHelpfulGuideText { get; set; }

        #endregion Properties
    }
  
 
}