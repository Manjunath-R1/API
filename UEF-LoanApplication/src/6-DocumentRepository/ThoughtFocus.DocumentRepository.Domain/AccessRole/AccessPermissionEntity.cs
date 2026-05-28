using System;

namespace ThoughtFocus.DocumentRepository.Domain
{
    public class AccessPermissionEntity
    {
        public string AssigneekeyName { get; set; }
        public string AssigneeNodeName { get; set; }
        public Guid AssigneeID { get; set; }
        public string ContentkeyName { get; set; }
        public string ContentNodeName { get; set; }
        public Guid ContentID { get; set; }
        public string RoleName { get; set; }
    }
}
