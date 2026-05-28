namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailBusinessExceptionLogEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public int AuditTrailBusinessExceptionLogID
        {
            get;
            set;
        }

        public string BusinessErrorCode
        {
            get;
            set;
        }

        public string BusinessErrorMessage
        {
            get;
            set;
        }

        public long CreatedByUserID
        {
            get;
            set;
        }

        public DateTime CreatedDateTime
        {
            get;
            set;
        }

        public long SessionID
        {
            get;
            set;
        }

        #endregion Properties
    }
}