using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class AccessRoleContains : Relationship,
        IRelationshipAllowingSourceNode<AccessRole>,
        IRelationshipAllowingTargetNode<Permission>
    {
        public AccessRoleContains(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "CONTAINS"; }
        }
    }
}
