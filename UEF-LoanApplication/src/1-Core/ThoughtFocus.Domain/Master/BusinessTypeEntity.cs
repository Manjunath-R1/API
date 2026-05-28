using System;

namespace ThoughtFocus.Domain.Master
{
    [Serializable]
    public class BusinessTypeEntity :  IEntity
    {
        #region Properties

        public long BusinessTypeID { get; set; }
        public string Type { get; set; }
        public long? DisplayOrder { get; set; }

        #endregion Properties
    }
}