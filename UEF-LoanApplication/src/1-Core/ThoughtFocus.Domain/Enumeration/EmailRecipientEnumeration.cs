using System;

namespace ThoughtFocus.Domain.Enumeration
{
    #region Enumerations

    [Serializable]
    public enum EmailRecipientEnumeration
    {
        /// <summary>
        /// For Borrower
        /// </summary>
        Borrower,

        /// <summary>
        /// For Contact
        /// </summary>
        Contact,

        /// <summary>
        /// For Administrator
        /// </summary>
        Administrator,

        /// <summary>
        /// For NULTreasury
        /// </summary>
        NULTreasury,

        /// <summary>
        /// For UnderWriter
        /// </summary>
        UnderWriter,

        /// <summary>
        /// For Controller
        /// </summary>
        Controller,

        /// <summary>
        /// For LoanProcessor
        /// </summary>

        LoanProcessor
    }

    #endregion Enumerations
}
