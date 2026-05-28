namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailPasswordHistoryEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public int AuditTrailPasswordHistoryID
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

        public String PasswordHash
        {
            get;
            set;
        }

        public String PasswordSalt
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