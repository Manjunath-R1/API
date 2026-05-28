using System;
using System.Collections.Generic;
using ThoughtFocus.Domain.CustomView;

namespace ThoughtFocus.Domain.Response
{
    public class ValidationErrorResponse
    {
        public bool IsValidationError { get; set; }
        public List<ValidationError> ValidationError { get; set; }

    }
    public class ValidationError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
