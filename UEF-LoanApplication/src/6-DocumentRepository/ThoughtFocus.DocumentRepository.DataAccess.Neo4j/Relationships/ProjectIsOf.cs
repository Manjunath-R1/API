using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ProjectIsOf : Relationship,
        IRelationshipAllowingSourceNode<Project>,
        IRelationshipAllowingTargetNode<ProjectType>
    {
        public ProjectIsOf(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "IS_OF"; }
        }
    }
}
