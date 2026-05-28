using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class GroupHas : Relationship,
        IRelationshipAllowingSourceNode<Group>,
        IRelationshipAllowingTargetNode<AccessRole>
    {
        public GroupHas(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "HAS"; }
        }
    }
}
