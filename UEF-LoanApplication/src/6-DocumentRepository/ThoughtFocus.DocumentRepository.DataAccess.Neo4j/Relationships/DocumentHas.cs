using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class DocumentHas : Relationship,
        IRelationshipAllowingSourceNode<Document>,
        IRelationshipAllowingTargetNode<Tag>
    {
        public DocumentHas(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "HAS"; }
        }
    }
}
