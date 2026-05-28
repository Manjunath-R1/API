namespace ThoughtFocus.DocumentRepository.Domain
{
    public class PermissionEntity
    {
        public int PermissionID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }
        public int PermissionValue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
