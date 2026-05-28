using System;

namespace ThoughtFocus.Domain.Enumeration
{
    #region Enumerations
    
    [Serializable]
    public enum EmailTemplateNameEnumeration
    {

         /// CONTACTINVITATION  
        /// </summary>
         CONTACTINVITATION=1,

         /// PROGRAMINVITATION  
        /// </summary>
         PROGRAMINVITATION=2,

         /// STATECHANGE  
        /// </summary>
         STATECHANGE=3,

         /// APPROVEDAPPLICATION  
        /// </summary>
         APPROVEDAPPLICATION=4,

        /// POSTACCOUNTACTIVATION  
        /// </summary>
         POSTACCOUNTACTIVATION=5,

         /// <summary>
        /// For APPLICATIONSUBMITION
        /// </summary>
        APPLICATIONSUBMITION = 6,

         /// <summary>
        /// For REQUESTMOREINFO
        /// </summary>
        REQUESTMOREINFO = 7,

        /// <summary>
        /// For REQUESTCOMPLETED
        /// </summary>
        REQUESTCOMPLETED = 8,

        /// <summary>
        /// For ACCEPTED
        /// </summary>
        ACCEPTED = 9,

        /// <summary>
        /// For APPROVED
        /// </summary>
        APPROVED = 10,

        /// <summary>
        /// For REJECTED
        /// </summary>
        REJECTED = 11,

        /// <summary>
        /// For AGREEMENTSUBMITTED
        /// </summary>
        AGREEMENTSUBMITTED = 12,

        /// <summary>
        /// For AGREEMENTREJECTED
        /// </summary>
        AGREEMENTREJECTED = 13,

        /// <summary>
        /// For CFOAPPROVED
        /// </summary>
        CFOAPPROVED = 14,

        /// <summary>
        /// For ACCOUNTDISBURSED
        /// </summary>
        ACCOUNTDISBURSED = 15,


    }

    #endregion Enumerations
}
