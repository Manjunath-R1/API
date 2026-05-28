namespace ThoughtFocus.Domain
{
    using System;

    [Serializable]
    public class UserAddressEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public string AddressLine1
        {
            get; set;
        }

        public string AddressLine2
        {
            get; set;
        }

        public string AddressLine3
        {
            get; set;
        }

        public string City
        {
            get; set;
        }

        public long CountryID
        {
            get; set;
        }

        public bool IsMain
        {
            get; set;
        }

        public long StateID
        {
            get; set;
        }

        public Int64 UserAddressID
        {
            get; set;
        }

        public string ZipCode
        {
            get; set;
        }

        #endregion Properties
    }
}