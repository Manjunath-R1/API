using System;

namespace ThoughtFocus.DocumentRepository.Domain.Document
{
    public class AccessPermissionModel
    {
        public string AssigneekeyName { get; set; }
        public string AssigneeNodeName { get; set; }
        public Guid AssigneeID { get; set; }
        public string ContentkeyName { get; set; }
        public string ContentNodeName { get; set; }
        public Guid ContentID { get; set; }
        public string RoleName { get; set; }
        public Guid GroupID { get; set; }
        public Guid UserID { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }    
    }
}
