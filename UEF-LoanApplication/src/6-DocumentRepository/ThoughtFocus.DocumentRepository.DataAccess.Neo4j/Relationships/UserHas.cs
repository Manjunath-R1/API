using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class UserHas : Relationship,
        IRelationshipAllowingSourceNode<RepositoryUser>,
        IRelationshipAllowingTargetNode<AccessRole>
    {
        public UserHas(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "HAS"; }
        }
    }
}
