using System;
using System.Collections.Generic;

namespace ThoughtFocus.Domain.Response
{
    public class BaseResponse
    {
        #region Fields
        public ResponseStatus commonCreationStatus;
        public string invalidInputs;

        #endregion Fields

        #region Constructors
        public BaseResponse(ResponseStatus commonCreationStatus, string invalidInputs)
        {
            this.commonCreationStatus = commonCreationStatus;
            this.invalidInputs = invalidInputs;
        }

        #endregion Constructors

        public BaseResponse()
        {
            Messages = new List<Message>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public List<Message> Messages { get; set; }
        public long ID { get; set; }
        public Guid DocumentID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid GroupID { get; set; }
        public bool IsValid { get; set; }
        public List<string> AdditionalMessages { get; set; }
        public string Type { get; set; }
        public List<string> ValidationErrors { get; set; }
        public long DocumentTypeID { get; set; }
        public string FileName { get; set; }
        public string StorageKey { get; set; }
        public string FileSource { get; set; }
        public bool InvalidInput { get; set; }
        public bool IsAvailableLimitExceeds { get; set; }
    }
    public class Message
    {
        public string Key { get; set; }
        public Dictionary<string, string> SubstitutionValues { get; set; }
    }
}
