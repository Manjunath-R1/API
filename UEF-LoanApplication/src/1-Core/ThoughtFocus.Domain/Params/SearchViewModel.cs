using System.Collections.Generic;

namespace ThoughtFocus.Domain
{
    public class SearchViewModel
    {
        public string SearchByAttribute { get; set; }
        public string SearchByValue { get; set; }
        public string ProgramApprovalStatus { get; set; }
    }
    public class PagingRequest
    {
        public PagingFilterModel Params { get; set; }
    }
    public class FilterParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class PagingFilterModel
    {
        public int Length { get; set; }
        public int Start { get; set; }
        public int Draw { get; set; }
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
        public SearchModel Search { get; set; }
        public OrderModel[] Order { get; set; }
        public List<FilterParameter> FilterParameters { get; set; }
        public bool IsColumnFilter { get; set; }
    }
    public class SearchModel
    {
        public string SearchByAttribute { get; set; }
        public string value { get; set; }       
        public bool InActive { get; set; }
    }

    public class OrderModel
    {
        public int Column { get; set; }
        public string name { get; set; }
        public string Dir { get; set; }
    }
    public class ExportExcel
    {
        public List<FilterParameter> FilterParameters { get; set; }
    }
    public class LoanExportRequest
    {
        public List<FilterParameter> FilterParameters { get; set; }
        public string SearchBy { get; set; }
        public string SearchValue { get; set; }
    }
}
