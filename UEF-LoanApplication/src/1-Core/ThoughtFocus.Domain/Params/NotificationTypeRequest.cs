namespace ThoughtFocus.Domain.Params
{
    using System;

    [Serializable]
    public class NotificationTypeRequest
    {
        #region Properties
        public long NotificationModeTypeID { get; set; }
        public int NotificationMode { get; set; }
        public long UserID { get; set; }

        #endregion Properties
    }
}