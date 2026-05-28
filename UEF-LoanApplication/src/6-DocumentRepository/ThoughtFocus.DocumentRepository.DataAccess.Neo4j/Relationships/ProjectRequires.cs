using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j
{
    public class ProjectRequires : Relationship,
        IRelationshipAllowingSourceNode<Project>,
        IRelationshipAllowingTargetNode<AccessRole>
    {
        public ProjectRequires(NodeReference targetNode)
            : base(targetNode)
        {

        }

        public override string RelationshipTypeKey
        {
            get { return "REQUIRES"; }
        }
    }
}
