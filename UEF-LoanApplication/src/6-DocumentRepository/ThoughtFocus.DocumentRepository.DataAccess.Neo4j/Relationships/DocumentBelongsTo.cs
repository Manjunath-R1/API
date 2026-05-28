using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class DocumentBelongsTo : Relationship,
        IRelationshipAllowingSourceNode<Document>,
        IRelationshipAllowingTargetNode<Project>
    {
        public DocumentBelongsTo(NodeReference targetNode) : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "BELONGS_TO"; }
        }
    }
}
