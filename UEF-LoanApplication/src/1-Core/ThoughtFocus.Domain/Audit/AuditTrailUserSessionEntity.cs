namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailUserSessionEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public string BrowserName
        {
            get;
            set;
        }

        public string BrowserType
        {
            get;
            set;
        }

        public string BrowserVersion
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

        public string IPAddress
        {
            get;
            set;
        }

        public long LastModifiedByUserID
        {
            get;
            set;
        }

        public DateTime LastModifiedDateTime
        {
            get;
            set;
        }

        public DateTime? LoginDateTime
        {
            get;
            set;
        }

        public long LoginSessionID
        {
            get;
            set;
        }

        public DateTime? LogoutDateTime
        {
            get;
            set;
        }

        public long SessionID
        {
            get;
            set;
        }

        public string SystemPlatform
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