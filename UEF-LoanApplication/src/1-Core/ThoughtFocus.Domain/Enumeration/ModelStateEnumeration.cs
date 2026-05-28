using System;

namespace ThoughtFocus.Domain.Enumeration
{
    [Serializable]
    public enum ModelStateEnumeration
    {
        /// <summary>
        /// For ContactAdded
        /// </summary>
        ADDED = 1,

        /// <summary>
        /// For ContactRemoved
        /// </summary>
        REMOVED = 2,

        /// <summary>
        /// For IsModified
        /// </summary>
        MODIFIED = 3,

        /// <summary>
        /// For DRAFTREMOVED
        /// </summary>
        DRAFTREMOVED = 4,

        /// <summary>
        /// For REMOVEDUPONREQUEST
        /// </summary>
        REMOVEDUPONREQUEST = 5,
    }
}
