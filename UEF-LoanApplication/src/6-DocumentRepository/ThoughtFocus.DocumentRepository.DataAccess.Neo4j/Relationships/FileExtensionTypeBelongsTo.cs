using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class FileExtensionTypeBelongsTo: Relationship,
        IRelationshipAllowingSourceNode<FileExtensionType>,
        IRelationshipAllowingTargetNode<FileExtensionCategory>
    {
        public FileExtensionTypeBelongsTo(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "BELONGS_TO"; }
        }
    }
}
