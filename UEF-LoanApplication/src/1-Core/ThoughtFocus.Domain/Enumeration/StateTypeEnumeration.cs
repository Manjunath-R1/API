using System;

namespace ThoughtFocus.Domain.Enumeration
{

    [Serializable]
    public enum StateTypeEnumeration
    {
        /// <summary>
        /// For Preparation
        /// </summary>
        Created = 1,

        /// <summary>
        /// For MemberReport
        /// </summary>
        TeamFinalized = 2,

        /// <summary>
        /// For ChairReport
        /// </summary>
        InProgress = 3,

        /// <summary>
        /// For Report
        /// </summary>
        Closed = 4,

        /// <summary>
        /// For DeanFeedbackonReport
        /// </summary>
        Cancelled = 5

    }

}
