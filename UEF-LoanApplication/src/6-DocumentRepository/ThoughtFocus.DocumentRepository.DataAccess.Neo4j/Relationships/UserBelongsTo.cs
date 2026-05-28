using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class UserBelongsTo : Relationship,
        IRelationshipAllowingSourceNode<RepositoryUser>,
        IRelationshipAllowingTargetNode<Group>
    {
        public UserBelongsTo(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "BELONGS_TO"; }
        }
    }
}
