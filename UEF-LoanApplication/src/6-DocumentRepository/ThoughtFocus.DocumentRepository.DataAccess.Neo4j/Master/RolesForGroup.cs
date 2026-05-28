namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class RolesInGroup
    {
         public long RolesInGroupID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public long GroupID { get; set; }
        public long RoleID { get; set; }

        public virtual Group Groups { get; set; }
        //public virtual Role Role { get; set; }
    }
}
