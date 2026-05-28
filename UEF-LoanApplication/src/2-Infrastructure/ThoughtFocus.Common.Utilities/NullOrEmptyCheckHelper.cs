namespace ThoughtFocus.Common.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class NullOrEmptyCheckHelper
    {
        #region Methods
        public static bool CheckNullOrEmpty<T>(T value)
        {

            
                if (value == null) { return true; }


                if (typeof(T) == typeof(string))
                {
                    return string.IsNullOrEmpty(value.ToString().Trim());
                }

                if (typeof(T)  == typeof(int))
                {                     
                     return (Convert.ToInt32(value)<= 0);
                }
                if (typeof(T)  == typeof(long))
                {                     
                     return (Convert.ToInt64(value)<= 0);
                }

                if (typeof(T)  == typeof(bool))
                {
                  return (! Convert.ToBoolean(value)  );
                }

                if (typeof(T) == typeof(Guid))
                {
                     Guid yourGuid=Guid.Parse(value.ToString());
                     return (yourGuid == Guid.Empty);
                }

                if (typeof(T) == typeof(object))
                {
                   
                    if (value != null)
                    {
                        return string.IsNullOrEmpty(value.ToString().Trim());
                    }
                    return true;
                }
                return false;
        }
        public static bool CheckListNullOrEmpty<T>(List<T> list)
        {
            if (list == null) {
                return true;
            }    
            return list.Count == 0;
        }

        #endregion Methods
    }
}

