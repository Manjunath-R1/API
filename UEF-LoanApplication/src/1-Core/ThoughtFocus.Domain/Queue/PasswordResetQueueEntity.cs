namespace ThoughtFocus.Domain.Queue
{
    using System;

    [Serializable]
    public class PasswordResetQueueEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public string ActionType
        {
            get;
            set;
        }

        public int AttemptCount
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public long PasswordResetQueueID
        {
            get;
            set;
        }

        public string QueueStatus
        {
            get;
            set;
        }

        public long UserPasswordResetInfoID
        {
            get;
            set;
        }

        #endregion Properties
    }
}