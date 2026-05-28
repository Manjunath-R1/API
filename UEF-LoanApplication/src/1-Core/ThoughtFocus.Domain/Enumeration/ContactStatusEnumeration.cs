namespace ThoughtFocus.Domain.Enumeration
{
    public enum ContactStatusEnumeration
    {
        /// <summary>
        /// Created
        /// </summary>
        Created = 1,
        /// <summary>
        /// RequestAccepted
        /// </summary>
        New = 2,
        /// <summary>
        /// RequestPending
        /// </summary>        
        RequestPending = 3,
        /// <summary>
        /// RequestRejected
        /// </summary>
        RequestRejected = 4,
        /// <summary>
        /// TrainingInProgress
        /// </summary>
        TrainingInProgress = 5,
        /// <summary>
        /// TrainingCompleted
        /// </summary>
        TrainingCompleted = 6,
        /// <summary>
        /// SiteVisitEligibleMember
        /// </summary>
        SiteVisitEligibleMember = 7
    }
}
