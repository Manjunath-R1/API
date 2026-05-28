namespace ThoughtFocus.Domain.Master
{
    using System;

    [Serializable]
    public class SystemConfigurationEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public string ConfigurationID
        {
            get;
            set;
        }

        public string ConfigurationValue
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        #endregion Properties
    }
}