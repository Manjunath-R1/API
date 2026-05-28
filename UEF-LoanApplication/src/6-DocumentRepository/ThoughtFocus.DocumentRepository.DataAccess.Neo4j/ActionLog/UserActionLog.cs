namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class UserActionLog
    {       
        public long ActionLogID { get; set; }

        public System.DateTime CreatedDateTime { get; set; }
        public long CreatedByUserID { get; set; }
        public long RoleID { get; set; }
        public long RepositoryUserID { get; set; }

        public virtual RepositoryUser RepositoryUser { get; set; }
    }
}
