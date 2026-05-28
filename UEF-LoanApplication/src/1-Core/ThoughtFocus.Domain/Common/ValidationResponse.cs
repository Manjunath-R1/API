using System.Collections.Generic;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.Domain.Common
{
    public class ValidationResponse
    {
        public ValidationResponse()
        {
            Messages = new List<Message>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public List<Message> Messages { get; set; }
        public bool IsValid { get; set; }


    }
}
