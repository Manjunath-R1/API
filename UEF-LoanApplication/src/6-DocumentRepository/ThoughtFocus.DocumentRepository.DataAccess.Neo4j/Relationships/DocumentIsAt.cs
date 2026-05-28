using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class DocumentIsAt : Relationship,
        IRelationshipAllowingSourceNode<Document>,
        IRelationshipAllowingTargetNode<DocumentCurrentVersion>
    {
        public DocumentIsAt(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "IS_AT"; }
        }
    }
}
