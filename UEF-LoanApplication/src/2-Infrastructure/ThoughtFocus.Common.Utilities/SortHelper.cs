using System.Collections.Generic;
using System.Linq;

namespace ThoughtFocus.Common.Utilities
{

    public static class SortHelper
    {
        public static List<T> SortAsc<T>(List<T> input, string property)
        {
            var type = typeof(T);
            var sortProperty = type.GetProperty(property);
            return input.OrderBy(p => sortProperty.GetValue(p, null)).ToList();
        }

        public static List<T> SortDesc<T>(List<T> input, string property)
        {
            var type = typeof(T);
            var sortProperty = type.GetProperty(property);
            return input.OrderByDescending(p => sortProperty.GetValue(p, null)).ToList();
        }

       

        
    }

}
