using System;

namespace ThoughtFocus.Domain.Enumeration
{
    [Serializable]
    public enum MemberWithdrawalRequestStatusEnumeration
    {
        /// <summary>
        /// For Requested
        /// </summary>
        Requested,

        /// <summary>
        /// For Accepted
        /// </summary>
        Accepted,

        /// <summary>
        /// For Rejected
        /// </summary>
        Rejected,

        /// <summary>
        /// For Rejected
        /// </summary>
        Removed,
    }
}
