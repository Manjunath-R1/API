namespace ThoughtFocus.Domain.CustomView
{
    using System;

    [Serializable]
    public class ContactInvitationAnonymousEntity
    {
        #region Properties

        public long ContactID
        {
            get;
            set;
        }

        public string ContactResponseMessage
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public bool InvitationStatus
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string StatusMessage
        {
            get;
            set;
        }
        public long RoleID
        {
            get;
            set;
        }

        public string TokenID
        {
            get;
            set;
        }

        #endregion Properties
    }
}