using System;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class AccessPermission
    {
        public string AssigneeKeyName { get; set; }
        public string AssigneeNodeName { get; set; }
        public Guid AssigneeID { get; set; }
        public string ContentKeyName { get; set; }
        public string ContentNodeName { get; set; }
        public Guid ContentID { get; set; }
        public string RoleName { get; set; }
    }
}
