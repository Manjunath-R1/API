namespace ThoughtFocus.Domain.Master
{
    public class GenderEntity :  IEntity
    {
        #region Properties

        public int DisplayOrder
        {
            get;
            set;
        }

        public long GenderID
        {
            get;
            set;
        }

        public string GenderName
        {
            get;
            set;
        }

        #endregion Properties
    }
}