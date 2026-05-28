using System;

namespace ThoughtFocus.Domain.Enumeration
{

    #region Enumerations

    [Serializable]
    public enum EventTypeEnumeration
    {
        /// <summary>
        /// For SitevisitScheduled
        /// </summary>
        SitevisitScheduled=1,
       
        /// <summary>
        /// For PreparationDocumentUploadDueForStaff
        /// </summary>
        PreparationDocumentUploadDueForStaff = 2,

        /// <summary>
        /// For PreparationDocumentUploadDueForSchool
        /// </summary>
        PreparationDocumentUploadDueForSchool = 3,

        /// <summary>
        /// For ChairReportUploadDue
        /// </summary>
        ChairReportUploadDue = 4,

        /// <summary>
        /// For DeanFeedbackReportOnReportUploadDue
        /// </summary>
        DeanFeedbackReportOnReportUploadDue = 5,

        /// <summary>
        /// For DeanSubmittedReportFeedback
        /// </summary>
        DeanSubmittedReportFeedback = 6,

        /// <summary>
        /// For ChairSubmittedReport
        /// </summary>
        ChairSubmittedReport=7,

        /// <summary>
        /// For SiteVisitCancel
        /// </summary>
        SiteVisitCancel = 8

    }

    #endregion Enumerations

}
