namespace ThoughtFocus.Domain.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class UserSecurityQuestionInfoEntity : BaseAbstractEntity, IEntity
    {
        #region Properties

        [Required(ErrorMessage = "Please enter a secutity answer.")]
        public string SecurityAnswer
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please select a secutity question.")]
        public long SecurityQuestionID
        {
            get;
            set;
        }

        public long UserID
        {
            get;
            set;
        }

        #endregion Properties
    }
}