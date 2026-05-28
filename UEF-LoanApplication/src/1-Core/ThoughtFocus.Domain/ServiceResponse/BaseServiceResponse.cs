using System.Collections.Generic;

namespace ThoughtFocus.Domain.ServiceResponse
{
    public class BaseServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<Message> Messages { get; set; }
    }
    public class Message
    {
        public string Key { get; set; }
        public Dictionary<string, string> SubstitutionValues { get; set; }
        public string ValidationErrorMessage { get; set; }
    }
}
