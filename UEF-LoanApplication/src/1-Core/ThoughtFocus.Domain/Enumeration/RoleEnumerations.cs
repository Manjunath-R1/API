using System;

namespace ThoughtFocus.Domain.Enumeration
{
    #region Enumerations

    [Serializable]
    public enum RoleEnumerations
    {
        /// <summary>
        /// For Administrator
        /// </summary>
        Administrator,

        /// <summary>
        /// For Borrower
        /// </summary>
        Borrower,

        /// <summary>
        /// For UnderWriter
        /// </summary>
        UnderWriter,

        /// <summary>
        /// For NULTreasury
        /// </summary>
        NULTreasury,

        /// <summary>
        /// For Loan Processor
        /// </summary>
        LoanProcessor,

        /// <summary>
        /// For Contact
        /// </summary>
        Contact,

    }

    [Serializable]
    public enum RoleIDEnumerations
    {
        /// <summary>
        /// For Administrator
        /// </summary>
        Administrator = 1,

        /// <summary>
        /// For Borrower
        /// </summary>
        Borrower = 2,

        /// <summary>
        /// For UnderWriter
        /// </summary>
        UnderWriter = 3,

        /// <summary>
        /// For NULTreasury
        /// </summary>
        NULTreasury = 4,

       
        /// <summary>
        /// For Loan Processor
        /// </summary>
        LoanProcessor = 5,

        /// <summary>
        /// Controller
        /// </summary>
        Controller = 6,

        /// <summary>
        /// For SiteAdmin
        /// </summary>
        SiteAdmin = 7,
    }

    #endregion Enumerations
    
}
