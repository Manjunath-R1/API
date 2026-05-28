namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailUIExceptionLogEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public string ActionName
        {
            get;
            set;
        }

        public int AuditTrailUIExceptionLogID
        {
            get;
            set;
        }

        public string ControllerName
        {
            get;
            set;
        }

        public long? CreatedByUserID
        {
            get;
            set;
        }

        public DateTime CreatedDateTime
        {
            get;
            set;
        }

        public string ErrorCode
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public string ExceptionMessage
        {
            get;
            set;
        }

        public string ExceptionStackTrace
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