namespace ThoughtFocus.Domain.Audit
{
    using System;

    [Serializable]
    public class AuditTrailVisitEntity : IAuditBaseEntity, IEntity
    {
        #region Properties

        public String ActionType
        {
            get;
            set;
        }

        public int AuditTrailVisitID
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

        public string Description
        {
            get;
            set;
        }

        public String FunctionType
        {
            get;
            set;
        }

        public long LoginSessionID
        {
            get;
            set;
        }

        public String Module
        {
            get;
            set;
        }

        public String SubModule
        {
            get;
            set;
        }

        #endregion Properties
    }
}