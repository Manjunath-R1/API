namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class UserProfilePhoneAndFaxNumberEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public long CountryID
        {
            get; set;
        }

        public bool IsMain
        {
            get; set;
        }

        public string PhoneNumberExtension
        {
            get; set;
        }

        public string PhoneOrFaxNumber
        {
            get; set;
        }

        public ContactType PhoneOrFaxType
        {
            get; set;
        }

        public int ProfilePhoneAndFaxNumberID
        {
            get; set;
        }

        #endregion Properties
    }
}