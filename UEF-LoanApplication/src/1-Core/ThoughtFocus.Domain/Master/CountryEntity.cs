namespace ThoughtFocus.Domain.Master
{

    public class CountryEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public string CountryCallingCode
        {
            get;
            set;
        }

        public long CountryID
        {
            get;
            set;
        }

        public string CountryName
        {
            get;
            set;
        }

        public string DisplayCountryCalling
        {
            get;
            set;
        }

        public int DisplayOrder
        {
            get;
            set;
        }

        #endregion Properties
    }
}