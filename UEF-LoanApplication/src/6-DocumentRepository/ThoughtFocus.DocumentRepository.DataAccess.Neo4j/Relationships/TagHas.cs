using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class TagHas : Relationship,
        IRelationshipAllowingSourceNode<Tag>,
        IRelationshipAllowingTargetNode<TagValue>
    {
        public TagHas(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "HAS"; }
        }
    }
}
