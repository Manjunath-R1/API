namespace ThoughtFocus.Common.Utilities
{
    using System;

    public class GuidIdUtility
    {
        #region Methods

        public static Guid GetNewGuidID()
        {
            return Guid.NewGuid();
        }

        #endregion Methods
    }
}