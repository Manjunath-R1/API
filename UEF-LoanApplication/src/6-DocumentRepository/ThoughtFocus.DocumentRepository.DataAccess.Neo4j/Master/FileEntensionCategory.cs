namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class FileExtensionCategory
    {
        public long FileExtensionCategoryID { get; set; }
        public long CreatedByUserID { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public long LastModifiedByUserID { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
}
