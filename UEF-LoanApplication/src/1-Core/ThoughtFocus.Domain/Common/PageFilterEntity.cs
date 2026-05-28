using System.Collections.Generic;
using ThoughtFocus.Domain;

namespace ThoughtFocus.Domain.Common
{
    public class PageFilterEntity
    {
        public int PageNumber { get; set; }
        public int TakeRecordCount { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string SearchBy { get; set; }
        public string SearchByValue { get; set; }
        public List<FilterParameter> FilterParameters { get; set; }
        public bool IsColumnFilter { get; set; }
    }
}
