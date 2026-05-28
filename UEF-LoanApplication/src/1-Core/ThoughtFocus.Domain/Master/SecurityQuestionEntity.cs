namespace ThoughtFocus.Domain.Master
{
    using System;

    [Serializable]
    public class SecurityQuestionEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        public int DisplayOrder
        {
            get;
            set;
        }

        public long SecurityQuestionID
        {
            get;
            set;
        }

        public string SecurityQuestionName
        {
            get;
            set;
        }

        #endregion Properties
    }
}