namespace ThoughtFocus.Domain.Queue
{
    public class ContactInvitationQueueEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public int AttemptCount
        {
            get;
               set;
        }

        public long ContactInvitationInfoID
        {
            get;
               set;
        }

        public long ContactInvitationQueueID
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

        #endregion Properties
    }
}