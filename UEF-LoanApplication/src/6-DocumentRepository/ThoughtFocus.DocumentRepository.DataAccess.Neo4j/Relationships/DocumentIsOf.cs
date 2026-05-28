using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class DocumentIsOf : Relationship,
        IRelationshipAllowingSourceNode<Document>,
        IRelationshipAllowingTargetNode<FileExtensionType>
    {
        public DocumentIsOf(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "IS_OF"; }
        }
    }
}
