namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailSessionInfoEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public DateTime CreatedDateTime
        {
            get;
            set;
        }

        public long SessionID
        {
            get; set;
        }

        #endregion Properties
    }
}