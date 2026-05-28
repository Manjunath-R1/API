using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Params;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models;
using System;

namespace ThoughtFocus.Domain.Response
{
    public class MasterOptionResponse
    {
        public List<MasterOptionDetail> MasterOptionDetails { get; set; }
        public bool IsValidationError { get; set; }
        public ValidationErrorResponse ValidationError { get; set; }
        public bool IsSuccess { get; set; }       
        public string Message { get; set; }

    }

    public class MasterOptionDetail
    {
 
        public string Key { get; set; }
        public string Values { get; set; }
    }
}
