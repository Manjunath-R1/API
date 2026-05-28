using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ProjectBelongsTo : Relationship,
        IRelationshipAllowingSourceNode<Project>,
        IRelationshipAllowingTargetNode<Project>
    {
        public ProjectBelongsTo(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "BELONGS_TO"; }
        }
    }
}
