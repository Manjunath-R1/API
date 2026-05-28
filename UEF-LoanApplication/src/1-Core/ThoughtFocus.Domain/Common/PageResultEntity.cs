using System.Collections.Generic;

namespace ThoughtFocus.Domain.Common
{
    public class PageResultEntity<T> where T : class
    {
        public List<T> DataList { get; set; }

        public int TotalRecordCount { get; set; }

        public int FilteredRecord { get; set; }  

    }
}
