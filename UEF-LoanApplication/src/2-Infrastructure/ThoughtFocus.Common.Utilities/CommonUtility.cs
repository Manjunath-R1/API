namespace ThoughtFocus.Common.Utilities
{
    using System;

    public class CommonUtility
    {
        #region Fields

        private const string AccreditationFormatDate = "dd-MMM-yyyy";
        private const string AccreditationFormatDateTime = "dd-MMM-yyyy hh:mm tt";

        #endregion Fields

        #region Methods

        public static string CreateUniqueID(string prefixText)
        {
            string returnValue = string.Empty;
            Random objRnd = new Random();
            returnValue = string.Format("{0}{1:yyyyMMddHHmmssffffff}{2}", prefixText, DateTime.Now,
                                                                        objRnd.Next(10000, 99999));
            return returnValue;
        }
        public static long CreateUniqueID()
        {
            long returnValue = 0;
            Random objRnd = new Random();
            returnValue =Convert.ToInt64(string.Format("{0:yyyyMMddHHmmss}{1}",  DateTime.Now,
                                                                        objRnd.Next(10000, 99999)));
            return returnValue;
        }
        /// <summary>
        /// Gets the ELIT format date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetFormattedDate(DateTime dateTime)
        {
            return (dateTime != DateTime.MinValue) ? dateTime.ToString(AccreditationFormatDate) : string.Empty;
        }

        /// <summary>
        /// Gets the ELIT format date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetFormattedDateTime(DateTime? dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
            {
                return string.Empty;
            }
            else
            {
                DateTime updatedTime = dateTime ?? DateTime.Now;
                return updatedTime.ToString(AccreditationFormatDateTime);
            }
        }

        public static string FirstCharToUpper(string inputString)  
        {  
        // Check for empty string.  
        if (string.IsNullOrEmpty(inputString))  
        {  
        return string.Empty;  
        }  
        // Return char and concat substring.  
        return char.ToUpper(inputString[0]) + inputString.Substring(1);  
        }  

        #endregion Methods
    }
}