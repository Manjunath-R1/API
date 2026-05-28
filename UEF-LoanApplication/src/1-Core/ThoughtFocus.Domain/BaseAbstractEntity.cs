namespace ThoughtFocus.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseAbstractEntity : IBaseEntity
    {
        #region Properties

        public long CreatedByUserID
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDateTime
        {
            get;
            set;
        }

        public bool IsActive
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

        #endregion Properties
    }
}