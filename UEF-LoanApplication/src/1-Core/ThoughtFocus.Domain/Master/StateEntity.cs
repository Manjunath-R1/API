using System;

namespace ThoughtFocus.Domain.Master
{
    [Serializable]
    public class StateEntity :  IEntity
    {
        #region Properties
        public long StateID
        {
            get;
            set;
        }

        public string StateName
        {
            get;
            set;
        }
        public Nullable<long> DisplayOrder
        {
            get;
            set;
        }

        #endregion Properties
    }
}