namespace ThoughtFocus.Domain.Queue
{
    public class PostUserAccountCreationQueueEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

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

        public long PostUserAccountCreationQueueID
        {
            get;
            set;
        }

        public string QueueStatus
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        #endregion Properties
    }
}