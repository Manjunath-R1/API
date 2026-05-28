namespace ThoughtFocus.Domain.User
{
    using System;

    [Serializable]
    public class UserProfileEmailAddressEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public string EmailAddress
        {
            get; set;
        }

        public bool IsMain
        {
            get; set;
        }

        public int ProfileEmailAddressID
        {
            get; set;
        }

        #endregion Properties
    }
}