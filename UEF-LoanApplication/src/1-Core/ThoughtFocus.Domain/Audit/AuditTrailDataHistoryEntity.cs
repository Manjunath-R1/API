namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailDataHistoryEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public int AuditTrailDataHistoryID
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

        public string DataInJSON
        {
            get;
            set;
        }

        public string IdentiferID
        {
            get;
            set;
        }

        public long LoginSessionID
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        #endregion Properties
    }
}