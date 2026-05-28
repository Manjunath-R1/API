namespace ThoughtFocus.Domain.Contact
{
    using System;

    [Serializable]
    public class ContactInvitationInfoEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public DateTime? ContactActionDateTime
        {
            get;
            set;
        }

        public long ContactID
        {
            get;
            set;
        }

        public long ContactInvitationInfoID
        {
            get;
            set;
        }

        public long ContactInvitationStatusID
        {
            get;
            set;
        }

        public DateTime ContactInvitedDateTime
        {
            get;
            set;
        }

        public string InvitationDescription
        {
            get;
            set;
        }

        public string InvitationEmailAddress
        {
            get;
            set;
        }

        public bool IsComplete
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