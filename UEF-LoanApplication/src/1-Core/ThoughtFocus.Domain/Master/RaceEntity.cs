namespace ThoughtFocus.Domain.Master
{
    public class RaceEntity : IEntity
    {
        #region Properties

        public long? DisplayOrder
        {
            get;
            set;
        }

        public long RaceID
        {
            get;
            set;
        }

        public string RaceName
        {
            get;
            set;
        }
        
        #endregion Properties
    }
}