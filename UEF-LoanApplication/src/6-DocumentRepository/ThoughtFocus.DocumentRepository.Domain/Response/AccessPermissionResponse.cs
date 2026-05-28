using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class AccessPermissionResponse : DocumentBaseResponse
    {
        public Guid GroupID { get; set; }
        public Guid UserID { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
