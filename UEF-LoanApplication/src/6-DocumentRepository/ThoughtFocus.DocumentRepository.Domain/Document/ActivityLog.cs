using System;

namespace ThoughtFocus.DocumentRepository.Domain.Document
{
    public class ActivityLog
    {
        public Guid ActivityLogID { get; set; }
        public string ActivityName { get; set; }
        public DateTime? Date { get; set; }
        public Guid UserGuID { get; set; }
        public string NodeName { get; set; }
        public string NodeKeyName { get; set; }
        public string KeyValue { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string LogText { get; set; }
        public string ActivityIcon { get; set; }
    }
}
