using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class TagBelongsTo : Relationship,
        IRelationshipAllowingSourceNode<Tag>,
        IRelationshipAllowingTargetNode<TagType>
    {
        public TagBelongsTo(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "BELONGS_TO"; }
        }
    }
}
