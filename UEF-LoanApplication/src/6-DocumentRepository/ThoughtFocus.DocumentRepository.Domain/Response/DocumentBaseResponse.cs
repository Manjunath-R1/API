using System;
using System.Collections.Generic;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class DocumentBaseResponse
    {
        public DocumentBaseResponse()
        {
            Messages = new List<Message>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public List<Message> Messages { get; set; }
        public Guid ID { get; set; }
        public string Type { get; set; }
        public bool IsValid { get; set; }
    }
    public class Message
    {
        public string Key { get; set; }
        public Dictionary<string, string> SubstitutionValues { get; set; }
        public string ValidationErrorMessage { get; set; }
    }
}
