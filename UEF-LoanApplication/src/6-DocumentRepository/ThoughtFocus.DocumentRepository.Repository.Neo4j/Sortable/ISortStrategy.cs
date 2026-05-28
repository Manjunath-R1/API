using Neo4jClient.Cypher;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j.Sortable
{
    public interface ISortStrategy
    {
        ICypherFluentQuery Sort(ICypherFluentQuery criteria, string sortColumn);
    }
}
