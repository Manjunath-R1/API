using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class DocumentRequires : Relationship,
        IRelationshipAllowingSourceNode<Document>,
        IRelationshipAllowingTargetNode<AccessRole>
    {
        public DocumentRequires(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "REQUIRES"; }
        }
    }
}
