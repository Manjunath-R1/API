using System;

namespace ThoughtFocus.Domain.Master
{
    [Serializable]
    public class FundingTypeEntity :  IEntity
    {
        #region Properties

        public int FundingTypeID { get; set; }
        public string Type { get; set; }
        public long? DisplayOrder { get; set; }

        #endregion Properties
    }
}