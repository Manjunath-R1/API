namespace ThoughtFocus.Domain.Queue
{
    using System;
    using ThoughtFocus.Domain;

    [Serializable]
    public class AccountActivationQueueEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long AccountActivationQueueID
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

        public string QueueStatus
        {
            get;
            set;
        }

        public long UserAccountActivationInfoID
        {
            get;
            set;
        }

        #endregion Properties
    }
}