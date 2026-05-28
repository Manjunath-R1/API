using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ThoughtFocus.Domain.Enumeration
{
    [Serializable]
    public enum ApplicationStatusEnumeration
    {
        /// <summary>
        /// Initialized
        /// </summary>
        Initialized = 1,

        /// <summary>
        /// For Created
        /// </summary>
        Created = 2,

        /// <summary>
        /// For Drafted
        /// </summary>
        Drafted = 3,

        /// <summary>
        /// Submitted
        /// </summary>
        Submitted = 4,

        /// <summary>
        /// RequestedMoreInfo
        /// </summary>
        RequestedMoreInfo = 5,

        /// <summary>
        /// RequestCompleted
        /// </summary> 
        RequestCompleted = 6,

        /// <summary>
        /// Accepted
        /// </summary> 
        Accepted = 7,

        /// <summary>
        /// Approved
        /// </summary> 
        Approved = 8,

        /// <summary>
        /// Rejected
        /// </summary> 
        Rejected = 9,

        /// <summary>
        /// AgreementUploaded
        /// </summary> 
        AgreementUploaded = 10,

        /// <summary>
        /// AgreementAccepted
        /// </summary> 
        AgreementAccepted = 11,

        /// <summary>
        /// CFOApproved
        /// </summary> 
        CFOApproved = 12,

        /// <summary>
        /// AccountDisbursed
        /// </summary> 
        AccountDisbursed = 13,

        /// <summary>
        /// AgreementSubmitted
        /// </summary> 
        AgreementSubmitted = 14,

        /// <summary>
        /// AgreementRejected
        /// </summary> 
        AgreementRejected = 15,

        /// <summary>
        /// RequestedMoreDetails
        /// </summary> 
        RequestedMoreDetails = 16,

        /// <summary>
        /// RequestMoreDeatailsCompleted
        /// </summary> 
        RequestMoreDeatailsCompleted = 17,


        /// <summary>
        /// UWApproved
        /// </summary> 
        UWApproved = 19,

        /// <summary>
        /// RequestMoreDeatailsCompletedByBorrower
        /// </summary> 
        RequestMoreDeatailsCompletedByBorrower = 20,

        /// <summary>
        /// UWReviewToRequestedMoreDetails
        /// </summary> 
        UWReviewToRequestedMoreDetails = 21,

        /// <summary>
        /// UWReviewRequestedMoreDetailsCompletedByBorrower
        /// </summary> 
        UWReviewRequestedMoreDetailsCompletedByBorrower = 23,

        /// <summary>
        /// AgreementSubmittedByReInitiate
        /// </summary> 
        AgreementSubmittedByReInitiate = 24,
        /// <summary>
        /// RequestedMoreInfoToSave
        /// </summary> 
        RequestedMoreInfoToSave = 25,
        /// <summary>
        /// UWReviewRequestedMoreDetailsToSave
        /// </summary> 
        UWReviewRequestedMoreDetailsToSave = 26,

        RequestedMoreInformationToSave = 27,
        //SPA

        SPASubmitted = 31,

        /// <summary>
        /// RequestedMoreInfo
        /// </summary>
        SPARequestedMoreInfo = 32,

        /// <summary>
        /// RequestCompleted
        /// </summary> 
        SPARequestCompleted = 33,

        /// <summary>
        /// Accepted
        /// </summary> 
        SPAAccepted = 34,

        /// <summary>
        /// Approved
        /// </summary> 
        SPAApproved = 35,

        /// <summary>
        /// Rejected
        /// </summary> 
        SPARejected = 36,

        /// <summary>
        /// AgreementUploaded
        /// </summary> 
        SPAAgreementUploaded = 37,

        /// <summary>
        /// AgreementAccepted
        /// </summary> 
        SPAAgreementAccepted = 38,

        /// <summary>
        /// CFOApproved
        /// </summary> 
        SPACFOApproved = 39,
        /// <summary>
        /// AgreementSubmitted
        /// </summary> 
        SPAAgreementSubmitted = 41,

        /// <summary>
        /// AgreementRejected
        /// </summary> 
        SPAAgreementRejected = 42,

        /// <summary>
        /// RequestedMoreDetails
        /// </summary> 
        SPARequestedMoreDetails = 43,

        /// <summary>
        /// RequestMoreDeatailsCompleted
        /// </summary> 
        SPARequestMoreDeatailsCompleted = 44,


        /// <summary>
        /// UWApproved
        /// </summary> 
        SPAUWApproved = 46,

        /// <summary>
        /// RequestMoreDeatailsCompletedByBorrower
        /// </summary> 
        SPARequestMoreDeatailsCompletedByBorrower = 47,

        /// <summary>
        /// UWReviewToRequestedMoreDetails
        /// </summary> 
        SPAUWReviewToRequestedMoreDetails = 48,

        /// <summary>
        /// UWReviewRequestedMoreDetailsCompletedByBorrower
        /// </summary> 
        SPAUWReviewRequestedMoreDetailsCompletedByBorrower = 50,

        /// <summary>
        /// AgreementSubmittedByReInitiate
        /// </summary> 
        SPAAgreementSubmittedByReInitiate = 51,
        /// <summary>
        /// RequestedMoreInfoToSave
        /// </summary> 
        SPARequestedMoreInfoToSave = 52,
        /// <summary>
        /// UWReviewRequestedMoreDetailsToSave
        /// </summary> 
        SPAUWReviewRequestedMoreDetailsToSave = 53,

        SPARequestedMoreInformationToSave = 54,

        SPAAccountDisbursed = 40,
    }
}
