using System;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class ValidationResultResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationError { get; set; }

    }
}
