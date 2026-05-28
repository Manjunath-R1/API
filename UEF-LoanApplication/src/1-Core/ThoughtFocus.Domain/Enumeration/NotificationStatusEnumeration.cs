using System;

namespace ThoughtFocus.Domain.Enumeration
{
    [Serializable]
    public enum NotificationStatusEnumeration
    {
        /// Sent
        /// </summary>
        Sent = 1,
        /// <summary>
        /// No Preference
        /// </summary>
        NoPreference = 2,
        /// <summary>
        /// Error
        /// </summary>        
        Error = 3,
    }
}
