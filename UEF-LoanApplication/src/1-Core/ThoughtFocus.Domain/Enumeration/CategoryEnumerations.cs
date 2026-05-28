using System;

namespace ThoughtFocus.Domain.Enumeration
{
    [Serializable]
    public enum CategoryEnumerations
    {
        /// <summary>
        /// For Preparation
        /// </summary>
        Preparation = 1,

        /// <summary>
        /// For MemberReport
        /// </summary>
        MemberReport = 2,

        /// <summary>
        /// For ChairReport
        /// </summary>
        ChairReport = 3,

        /// <summary>
        /// For AgencyReport
        /// </summary>
        ReviewerReport = 4,

        /// <summary>
        /// For DeanFeedbackonAgencyReport
        /// </summary>
        DeanfeedbackonReviewerReport = 5,

        /// <summary>
        /// For DecisionLetter
        /// </summary>
        DecisionLetter = 6,
        /// <summary>
        /// For DeanFeedbackonDecisionLetter
        /// </summary>
        DeanFeedbackonDecisionLetter = 7,


    }

}
