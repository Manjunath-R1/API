using Neo4jClient.Cypher;

namespace ThoughtFocus.DocumentRepository.DataAccess.Neo4j.Sortable
{
    public class SortDescending : ISortStrategy
    {
        public ICypherFluentQuery Sort(ICypherFluentQuery criteria, string sortColumn)
        {
            return criteria.OrderByDescending(sortColumn);
        }
    }
}
