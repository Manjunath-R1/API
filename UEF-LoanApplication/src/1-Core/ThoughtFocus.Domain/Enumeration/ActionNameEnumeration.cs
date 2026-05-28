using System;

namespace ThoughtFocus.Domain.Enumeration
{

    #region Enumerations

    [Serializable]
    public enum ActionNameEnumeration
    {
        /// <summary>
        /// For GetSiteVisitList
        /// </summary>
        GetSiteVisitList=41,
        /// <summary>
        /// For AcceptedStates
        /// </summary>
        AcceptedStates = 80,
        /// <summary>
        /// For MaximumAcceptedState
        /// </summary>
        MaximumAcceptedState = 81,

        /// <summary>
        /// For EditConflictOfInterest
        /// </summary>
        EditConflictOfInterest = 122,

        /// <summary>
        /// For EditNonAvailabilityDates
        /// </summary>
        EditNonAvailabilityDates = 123,

        /// <summary>
        /// For EditEmailPreference
        /// </summary>
        EditEmailPreference = 124,

        /// <summary>
        /// For RemoveProfileConflictOfInterest
        /// </summary>
        RemoveProfileConflictOfInterest = 131,

        /// <summary>
        /// For AddOrUpdateProfileNonAvailabilityDates
        /// </summary
        AddOrUpdateProfileNonAvailabilityDates = 132,

        /// <summary>
        /// For AddOrUpdateProfileEmailPreferences
        /// </summary>
        AddOrUpdateProfileEmailPreferences = 133
    }

    #endregion Enumerations

}
